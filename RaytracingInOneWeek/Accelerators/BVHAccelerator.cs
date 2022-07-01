using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Accelerators
{
    public class BVHAccelerator : Aggregate
    {
        class Sort : IComparer<BVHPrimitiveInfo>
        {
            int _dim = 0;

            public Sort(int dim)
            {
                _dim = dim;
            }

            public int Compare(BVHPrimitiveInfo x, BVHPrimitiveInfo y)
            {
                bool value = x.Centroid[_dim] < y.Centroid[_dim];
                if(value == true)
                {
                    return -1;
                }
                if (value == false)
                {
                    return 1;
                }
                throw new NullReferenceException("value can't be null");
            }
        }

        struct BVHPrimitiveInfo
        {
            public int PrimitiveNumber;
            public Bounds3D Bounds;
            public Point3D Centroid;

            public BVHPrimitiveInfo(int primitiveNumber, Bounds3D bounds)
            {
                PrimitiveNumber = primitiveNumber;
                Bounds = bounds;
                Centroid = 0.5 * bounds.pMin + 0.5 * bounds.pMax;
            }
        }
        struct BVHBuildNode
        {
            public Bounds3D Bounds;
            public BVHBuildNode[] Children;
            public int SplitAxis, FirstPrimOffset, NPrimitives;

            public void InitLeaf(int first, int n, Bounds3D b)
            {
                FirstPrimOffset = first;
                NPrimitives = n;
                Bounds = b;
            }
            public void InitInterior(int axis, BVHBuildNode c0, BVHBuildNode c1)
            {
                Children = new BVHBuildNode[2];
                Children[0] = c0;
                Children[1] = c1;
                Bounds = Bounds3D.Union(c0.Bounds, c1.Bounds);
                SplitAxis = axis;
                NPrimitives = 0;
            }
        }
        struct BucketInfo
        {
            public int Count;
            public Bounds3D Bounds;
        }
        struct MortonPrimitive
        {
            public int PrimitiveIndex;
            public Int32 MortonCode;
        }
        struct LBVHTreelet
        {
            public int StartIndex, NPrimitives;
            public BVHBuildNode BuildNodes;
            public LBVHTreelet(int startIndex, int nPrimitives, BVHBuildNode buildNodes)
            {
                StartIndex = startIndex;
                NPrimitives = nPrimitives;
                BuildNodes = buildNodes;
            }
        }
        struct LinearBVHNode
        {
            public Bounds3D Bounds;
            public int PrimitivesOffset;
            public int SecondChildOffset;
            public Int16 NPrimitives;
            public Int16 Axis;
        }

        public enum SplitMethod { SAH, HLBVH, Middle, EqualCounts }
        private int _maxPrimitivesInNode;
        private SplitMethod _splitMethod;
        private List<Primitive> _primitives;
        private const int _nBuckets = 12;
        private LinearBVHNode[] _nodes;

        public BVHAccelerator(List<Primitive> primitives, int maxPrimitivesInNode, SplitMethod splitMethod)
        {
            _maxPrimitivesInNode = Math.Min(maxPrimitivesInNode, 255);
            _splitMethod = splitMethod;
            _primitives = primitives;

            if (_primitives.Count == 0)
            {
                return;
            }

            List<BVHPrimitiveInfo> primitiveInfos = new List<BVHPrimitiveInfo>();
            for (int i = 0; i < _primitives.Count; i++)
            {
                primitiveInfos.Add(new BVHPrimitiveInfo(i, _primitives[i].GetWorldBound()));
            }

            int totalNodes = 0;
            List<Primitive> orderedPrims = new List<Primitive>();
            BVHBuildNode root;
            if (splitMethod == SplitMethod.HLBVH)
            {
                throw new NotImplementedException();
            }
            else
            {
                root = RecursiveBuild(primitiveInfos, 0, primitives.Count, ref totalNodes, ref orderedPrims);
            }
            _primitives = orderedPrims;
            _nodes = new LinearBVHNode[totalNodes];
            int offset = 0;
            FlattenBVHTree(root, ref offset);
        }

        private BVHBuildNode RecursiveBuild(List<BVHPrimitiveInfo> primitiveInfos, int start, int end, ref int totalNodes, ref List<Primitive> orderedPrimitives)
        {
            BVHBuildNode node = new BVHBuildNode();
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
                        case SplitMethod.Middle:
                            {
                                double pmid = (centroidBounds.pMin[dim] + centroidBounds.pMax[dim]) / 2;

                                primitiveInfos = Mathe.Partition(primitiveInfos, start, end, pi => pi.Centroid[dim] < pmid, out mid);

                                if (mid != start && mid != end)
                                {
                                    break;
                                }
                                goto case SplitMethod.EqualCounts;
                            }
                        case SplitMethod.EqualCounts:
                            {
                                mid = (start + end) / 2;

                                Sort sort = new Sort(dim);
                                primitiveInfos.Sort(start, end - start, sort);

                                break;
                            }
                        case SplitMethod.SAH:
                            {
                                if (nPrimitives <= 2)
                                {
                                    mid = (start + end) / 2;

                                    Sort sort = new Sort(dim);
                                    primitiveInfos.Sort(start, end - start, sort);
                                }
                                else
                                {
                                    BucketInfo[] buckets = new BucketInfo[_nBuckets];
                                    for (int i = 0; i < buckets.Length; i++)
                                    {
                                        buckets[i].Bounds = new Bounds3D(new(0));
                                    }

                                    for (int i = start; i < end; ++i)
                                    {
                                        int b = (int)(_nBuckets * centroidBounds.Offset(primitiveInfos[i].Centroid)[dim]);
                                        if (b == _nBuckets) b = _nBuckets - 1;
                                        buckets[b].Count++;
                                        buckets[b].Bounds = Bounds3D.Union(buckets[b].Bounds, primitiveInfos[i].Bounds);
                                    }

                                    double[] cost = new double[_nBuckets - 1];
                                    for (int i = 0; i < _nBuckets - 1; ++i)
                                    {
                                        Bounds3D b0 = new Bounds3D();
                                        Bounds3D b1 = new Bounds3D();

                                        int count0 = 0, count1 = 0;
                                        for (int j = 0; j <= i; ++j)
                                        {
                                            b0 = Bounds3D.Union(b0, buckets[j].Bounds);
                                            count0 += buckets[j].Count;
                                        }

                                        for (int j = i + 1; j < _nBuckets; ++j)
                                        {
                                            b1 = Bounds3D.Union(b1, buckets[j].Bounds);
                                            count1 += buckets[j].Count;
                                        }

                                        cost[i] = 0.1 + (count0 * b0.SurfaceArea() +
                                                       count1 * b1.SurfaceArea()) / bounds.SurfaceArea();
                                    }

                                    double minCost = cost[0];
                                    int minCostSplitBucket = 0;
                                    for (int i = 1; i < _nBuckets - 1; ++i)
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
                                            int b = (int)(_nBuckets * centroidBounds.Offset(pi.Centroid)[dim]);
                                            if (b == _nBuckets) b = _nBuckets - 1;
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
                    node.InitInterior(dim, RecursiveBuild(primitiveInfos, start, mid, ref totalNodes, ref orderedPrimitives),
                                           RecursiveBuild(primitiveInfos, mid, end, ref totalNodes, ref orderedPrimitives));
                }
            }
            return node;
        }
        private int FlattenBVHTree(BVHBuildNode node, ref int offset)
        {
            LinearBVHNode linearNode = _nodes[offset];
            linearNode.Bounds = node.Bounds;
            int myOffset = offset++;
            if (node.NPrimitives > 0)
            {
                linearNode.PrimitivesOffset = node.FirstPrimOffset;
                linearNode.NPrimitives = (Int16)node.NPrimitives;
            }
            else
            {
                linearNode.Axis = (Int16)node.SplitAxis;
                linearNode.NPrimitives = 0;
                FlattenBVHTree(node.Children[0], ref offset);
                linearNode.SecondChildOffset = FlattenBVHTree(node.Children[1], ref offset);
            }
            _nodes[myOffset] = linearNode;
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

        public override bool Intersect(Ray ray, out SurfaceInteraction intersection)
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
                            if (_primitives[node.PrimitivesOffset + i].Intersect(ray, out intersection))
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

            return hit;
        }
    }
}
