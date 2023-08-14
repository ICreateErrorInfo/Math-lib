using Math_lib;
using Raytracing.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raytracing.Accelerators {
    public partial class BVHAccelerator : Aggregate
    {
        private const int BucketCount = 12;
        private readonly int _maxPrimitivesInNode;
        private readonly BVHSplitMethod _splitMethod;
        private readonly List<Primitive> _primitives;
        private readonly LinearBVHNode[] _nodes;

        public BVHAccelerator(List<Primitive> primitives, int maxPrimitivesInNode, BVHSplitMethod splitMethod)
        {
            _maxPrimitivesInNode = Math.Min(maxPrimitivesInNode, 255);
            _splitMethod = splitMethod;
            _primitives = primitives;

            if (_primitives.Count == 0)
            {
                return;
            }

            List<PrimitiveInfo> primitiveInfos = new List<PrimitiveInfo>();
            for (int i = 0; i < _primitives.Count; i++)
            {
                primitiveInfos.Add(new PrimitiveInfo(i, _primitives[i].GetWorldBound()));
            }

            int totalNodes = 0;
            List<Primitive> orderedPrims = new List<Primitive>();
            BuildNode root;
            if (splitMethod == BVHSplitMethod.HLBVH)
            {
                throw new NotImplementedException();
            }
            else
            {
                root = RecursiveBuild(primitiveInfos, 0, primitives.Count, ref totalNodes, orderedPrims);
            }
            _primitives = orderedPrims;
            _nodes = FlattenBVHTree(root, totalNodes);
        }

        private BuildNode RecursiveBuild(List<PrimitiveInfo> primitiveInfos, int start, int end, ref int totalNodes, List<Primitive> orderedPrimitives)
        {
            BuildNode node = new BuildNode();
            totalNodes++;

            Bounds3D bounds = new Bounds3D();
            for (int i = start; i < end; i++)
            {
                bounds = Bounds3D.Union(bounds, primitiveInfos[i].Bounds);
            }

            int nPrimitives = end - start;
            if (nPrimitives == 1)
            {
                int firstPrimitiveOffset = orderedPrimitives.Count;
                for (int i = start; i < end; i++)
                {
                    int primitiveNumber = primitiveInfos[i].PrimitiveNumber;
                    orderedPrimitives.Add(_primitives[primitiveNumber]);
                }
                node.InitLeaf(firstPrimitiveOffset, nPrimitives, bounds);
                return node;
            }
            else
            {
                Bounds3D centroidBounds = new Bounds3D();
                for (int i = start; i < end; i++)
                {
                    centroidBounds = Bounds3D.Union(centroidBounds, primitiveInfos[i].Centroid);
                }
                int dim = centroidBounds.MaximumExtent();

                int mid = (start + end) / 2;
                if (centroidBounds.pMax[dim] == centroidBounds.pMin[dim])
                {
                    int firstPrimitiveOffset = orderedPrimitives.Count;
                    for (int i = start; i < end; i++)
                    {
                        int primitiveNumber = primitiveInfos[i].PrimitiveNumber;
                        orderedPrimitives.Add(_primitives[primitiveNumber]);
                    }
                    node.InitLeaf(firstPrimitiveOffset, nPrimitives, bounds);
                    return node;
                }
                else
                {
                    switch (_splitMethod)
                    {
                        case BVHSplitMethod.Middle:
                            {
                                double pmid = (centroidBounds.pMin[dim] + centroidBounds.pMax[dim]) / 2;

                                primitiveInfos = Mathe.Partition(primitiveInfos, start, end, pi => pi.Centroid[dim] < pmid, out mid);

                                if (mid != start && mid != end)
                                {
                                    break;
                                }
                                goto case BVHSplitMethod.EqualCounts;
                            }
                        case BVHSplitMethod.EqualCounts:
                            {
                                mid = (start + end) / 2;

                                Sort sort = new Sort(dim);
                                primitiveInfos.Sort(start, end - start, sort);

                                break;
                            }
                        case BVHSplitMethod.SAH:
                            {
                                if (nPrimitives <= 2)
                                {
                                    mid = (start + end) / 2;

                                    Sort sort = new Sort(dim);
                                    primitiveInfos.Sort(start, end - start, sort);
                                }
                                else
                                {
                                    BucketInfo[] buckets = new BucketInfo[BucketCount];
                                    for (int i = 0; i < buckets.Length; i++)
                                    {
                                        buckets[i].Bounds = new Bounds3D(new(0));
                                    }

                                    for (int i = start; i < end; ++i)
                                    {
                                        int b = (int)(BucketCount * centroidBounds.Offset(primitiveInfos[i].Centroid)[dim]);
                                        if (b == BucketCount) b = BucketCount - 1;
                                        buckets[b].Count++;
                                        buckets[b].Bounds = Bounds3D.Union(buckets[b].Bounds, primitiveInfos[i].Bounds);
                                    }

                                    double[] cost = new double[BucketCount - 1];
                                    for (int i = 0; i < BucketCount - 1; ++i)
                                    {
                                        Bounds3D b0 = new Bounds3D();
                                        Bounds3D b1 = new Bounds3D();

                                        int count0 = 0, count1 = 0;
                                        for (int j = 0; j <= i; ++j)
                                        {
                                            b0 = Bounds3D.Union(b0, buckets[j].Bounds);
                                            count0 += buckets[j].Count;
                                        }

                                        for (int j = i + 1; j < BucketCount; ++j)
                                        {
                                            b1 = Bounds3D.Union(b1, buckets[j].Bounds);
                                            count1 += buckets[j].Count;
                                        }

                                        cost[i] = 0.1 + (count0 * b0.SurfaceArea() +
                                                       count1 * b1.SurfaceArea()) / bounds.SurfaceArea();
                                    }

                                    double minCost = cost[0];
                                    int minCostSplitBucket = 0;
                                    for (int i = 1; i < BucketCount - 1; ++i)
                                    {
                                        if (cost[i] < minCost)
                                        {
                                            minCost = cost[i];
                                            minCostSplitBucket = i;
                                        }
                                    }

                                    double leafCost = nPrimitives;
                                    if (nPrimitives > _maxPrimitivesInNode || minCost < leafCost)
                                    {
                                        primitiveInfos = Mathe.Partition(primitiveInfos, start, end, pi =>
                                        {
                                            int b = (int)(BucketCount * centroidBounds.Offset(pi.Centroid)[dim]);
                                            if (b == BucketCount) b = BucketCount - 1;
                                            return b <= minCostSplitBucket;
                                        }, out mid);
                                    }
                                    else
                                    {
                                        int firstPrimOffset = orderedPrimitives.Count();
                                        for (int i = start; i < end; ++i)
                                        {
                                            int primNum = primitiveInfos[i].PrimitiveNumber;
                                            orderedPrimitives.Add(_primitives[primNum]);
                                        }
                                        node.InitLeaf(firstPrimOffset, nPrimitives, bounds);
                                        return node;
                                    }
                                }
                                break;
                            }
                    }
                    node.InitInterior(dim, RecursiveBuild(primitiveInfos, start, mid, ref totalNodes, orderedPrimitives),
                                           RecursiveBuild(primitiveInfos, mid, end, ref totalNodes, orderedPrimitives));
                }
            }
            return node;
        }
        private static LinearBVHNode[] FlattenBVHTree( BuildNode root, int totalNodes)
        {
            var nodes = new LinearBVHNode[totalNodes];
            var offset = 0;
            FlattenBVHTree(nodes, root, ref offset);
            return nodes;
        }
        private static int FlattenBVHTree(LinearBVHNode[] nodes, BuildNode root, ref int offset)
        {
            LinearBVHNode linearNode = nodes[offset];
            linearNode.Bounds = root.Bounds;
            int myOffset = offset++;
            if (root.NPrimitives > 0)
            {
                linearNode.PrimitivesOffset = root.FirstPrimOffset;
                linearNode.NPrimitives = (Int16)root.NPrimitives;
            }
            else
            {
                linearNode.Axis = (Int16)root.SplitAxis;
                linearNode.NPrimitives = 0;
                FlattenBVHTree(nodes, root.Children[0], ref offset);
                linearNode.SecondChildOffset = FlattenBVHTree(nodes, root.Children[1], ref offset);
            }
            nodes[myOffset] = linearNode;
            return myOffset;
        }


        //for HLBVH not needed now
        /*
        private BVHBuildNode EmitLBVH(BVHBuildNode buildNodes,
                                      List<BVHPrimitiveInfo> primitiveInfo,
                                      MortonPrimitive[] mortonPrims, int nPrimitives, int totalNodes,
                                      List<Primitive> orderedPrims,
                                      int orderedPrimsOffset, int bitIndex)
        {
            if (bitIndex == -1 || nPrimitives < _maxPrimitivesInNode)
            {
                totalNodes++;
                BVHBuildNode node = buildNodes;
                Bounds3D bounds = new Bounds3D();
                int firstPrimOffset = orderedPrimsOffset + nPrimitives;
                for (int i = 0; i < nPrimitives; i++)
                {
                    int primitiveIndex = mortonPrims[i].primitiveIndex;
                    orderedPrims[firstPrimOffset + i] = _primitives[primitiveIndex];
                    bounds = Bounds3D.Union(bounds, primitiveInfo[primitiveIndex].Bounds);
                }
                node.InitLeaf(firstPrimOffset, nPrimitives, bounds);
                return node;
            }
            else
            {
                int mask = 1 << bitIndex;
                if ((mortonPrims[0].mortonCode & mask) ==
                    (mortonPrims[nPrimitives - 1].mortonCode & mask))
                    return EmitLBVH(buildNodes, primitiveInfo, mortonPrims, nPrimitives,
                                    totalNodes, orderedPrims, orderedPrimsOffset,
                                    bitIndex - 1);

                int searchStart = 0, searchEnd = nPrimitives - 1;
                while (searchStart + 1 != searchEnd)
                {
                    int mid = (searchStart + searchEnd) / 2;
                    if ((mortonPrims[searchStart].mortonCode & mask) ==
                        (mortonPrims[mid].mortonCode & mask))
                        searchStart = mid;
                    else
                        searchEnd = mid;
                }
                int splitOffset = searchEnd;

                totalNodes++;
                BVHBuildNode node = buildNodes;
                BVHBuildNode[] lbvh = {
                    EmitLBVH(buildNodes, primitiveInfo, mortonPrims, splitOffset,
                             totalNodes, orderedPrims, orderedPrimsOffset, bitIndex - 1),
                    EmitLBVH(buildNodes, primitiveInfo, mortonPrims,
                             nPrimitives - splitOffset, totalNodes, orderedPrims,
                             orderedPrimsOffset, bitIndex - 1)
                };

                int axis = bitIndex % 3;
                node.InitInterior(axis, lbvh[0], lbvh[1]);
                return node;
            }

        }
        private Int32 LeftShift3(Int32 x)
        {
            if (x == (1 << 10)) --x;
            x = (x | (x << 16)) & 0b00000011000000000000000011111111;
            x = (x | (x << 8)) & 0b00000011000000001111000000001111;
            x = (x | (x << 4)) & 0b00000011000011000011000011000011;
            x = (x | (x << 2)) & 0b00001001001001001001001001001001;
            return x;
        }
        private Int32 EncodeMorton3(Vector3D v)
        {
            return (LeftShift3((Int32)v.Z) << 2 |
                   (LeftShift3((Int32)v.Y) << 1) |
                    LeftShift3((Int32)v.X));
        }
        private static void RadixSort(ref MortonPrimitive[] v)
        {
            MortonPrimitive[] tempList = new MortonPrimitive[v.Length];
            int bitsPerPass = 6;
            int nBits = 30;
            int nPasses = nBits / bitsPerPass;
            for (int pass = 0; pass < nPasses; ++pass)
            {
                int lowBit = pass * bitsPerPass;
                MortonPrimitive[] VIn = (pass >= 1 & true) ? tempList : v;
                MortonPrimitive[] VOut = (pass >= 1 & true) ? v : tempList;

                //Count in each Bucket
                int nBuckets = 1 << bitsPerPass;
                int[] bucketCount = new int[nBuckets];
                int bitMask = (1 << bitsPerPass) - 1;
                foreach (MortonPrimitive mp in VIn)
                {
                    int bucket = (mp.mortonCode >> lowBit) & bitMask;
                    ++bucketCount[bucket];
                }

                int[] outIndex = new int[nBuckets];
                outIndex[0] = 0;
                for (int i = 1; i < nBuckets; ++i)
                {
                    outIndex[i] = outIndex[i - 1] + bucketCount[i - 1];
                }

                foreach (MortonPrimitive mp in VIn)
                {
                    int bucket = (mp.mortonCode >> lowBit) & bitMask;
                    VOut[outIndex[bucket]++] = mp;
                }

                if (nPasses % 2 == 0)
                {
                    (v, tempList) = (tempList, v);
                }
            }
        }
        */

        public override Bounds3D GetWorldBound()
        {
            return _nodes.Count() == 1 ? _nodes[0].Bounds : new Bounds3D();
        }

        public override SurfaceInteraction Intersect(Ray ray, SurfaceInteraction intersection)
        {
            intersection = new SurfaceInteraction();

            bool hit = false;
            Vector3D invDir = new Vector3D(1 / ray.D.X, 1 / ray.D.Y, 1 / ray.D.Z);
            bool[] dirIsNeg = new bool[] { invDir.X < 0, invDir.Y < 0, invDir.Z < 0 };

            int toVisitOffset = 0, currentNodeIndex = 0;
            int[] nodesToVisit = new int[64];
            while (true)
            {
                LinearBVHNode node = _nodes[currentNodeIndex];
                if (node.Bounds.IntersectP(ray, invDir, dirIsNeg))
                {
                    if (node.NPrimitives > 0)
                    {
                        for (int i = 0; i < node.NPrimitives; i++)
                        {
                            SurfaceInteraction oldSurfaceInteraction = intersection;
                            intersection = _primitives[node.PrimitivesOffset + i].Intersect(ray, intersection);
                            if (intersection.HasIntersection)
                            {
                                hit = true;
                            }
                            if (intersection.Primitive == null)
                            {
                                intersection = oldSurfaceInteraction;
                            }
                        }
                        if (toVisitOffset == 0) break;
                        currentNodeIndex = nodesToVisit[--toVisitOffset];
                    }
                    else
                    {
                        if (dirIsNeg[node.Axis])
                        {
                            nodesToVisit[toVisitOffset++] = currentNodeIndex + 1;
                            currentNodeIndex = node.SecondChildOffset;
                        }
                        else
                        {
                            nodesToVisit[toVisitOffset++] = node.SecondChildOffset;
                            currentNodeIndex = currentNodeIndex + 1;
                        }
                    }
                }
                else
                {
                    if (toVisitOffset == 0) break;
                    currentNodeIndex = nodesToVisit[--toVisitOffset];
                }
            }

            intersection.HasIntersection = hit;
            return intersection;
        }
    }
}
