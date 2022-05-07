﻿using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Accelerators
{
    public class BVHAccelerator : Aggregate
    {
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

        public enum SplitMethod { SAH, HLBVH, Middle, EqualCounts }
        private int _maxPrimitivesInNode;
        SplitMethod _splitMethod;
        List<Primitive> _primitives;
        int nBuckets = 12;

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

            }
            else
            {
                BVHBuildNode node = RecursiveBuild(primitiveInfos, 0, primitives.Count, ref totalNodes, ref orderedPrims);
            }
            primitives = orderedPrims;
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
                                var midGrouped = primitiveInfos.GroupBy(primitiveInfos =>
                                {
                                    return primitiveInfos.Centroid[dim] < pmid;
                                });

                                List<BVHPrimitiveInfo> newPrimitiveInfos = new List<BVHPrimitiveInfo>();
                                mid = 0;
                                int counter = 0;
                                foreach (var group in midGrouped)
                                {
                                    foreach (var info in group)
                                    {
                                        if (counter >= start && counter <= end)
                                        {
                                            newPrimitiveInfos.Add(info);
                                        }
                                        else
                                        {
                                            newPrimitiveInfos.Add(primitiveInfos[counter]);
                                        }
                                        if (group.Key == true)
                                        {
                                            mid++;
                                        }
                                        counter++;
                                    }
                                }
                                primitiveInfos = newPrimitiveInfos;
                                mid = mid + start;

                                if (mid != start && mid != end)
                                {
                                    break;
                                }

                                goto case SplitMethod.EqualCounts;
                            }
                        case SplitMethod.EqualCounts:
                            {
                                mid = (start + end) / 2;

                                var primitiveMid = primitiveInfos[mid];
                                var midGrouped = primitiveInfos.GroupBy(primitiveInfo =>
                                {
                                    return primitiveInfo.Centroid[dim] < primitiveMid.Centroid[dim];
                                });

                                List<BVHPrimitiveInfo> newPrimitiveInfos = new List<BVHPrimitiveInfo>();
                                foreach (var group in midGrouped)
                                {
                                    foreach (var info in group)
                                    {
                                        newPrimitiveInfos.Add(info);
                                    }
                                }

                                break;
                            }
                        case SplitMethod.SAH:
                            {
                                BucketInfo[] buckets = new BucketInfo[nBuckets];

                                for(int i = 0; i < buckets.Length; i++)
                                {
                                    buckets[i].Bounds = new Bounds3D(new(0));
                                }

                                for (int i = start; i < end; ++i)
                                {
                                    int b = (int)(nBuckets * centroidBounds.Offset(primitiveInfos[i].Centroid)[dim]);
                                    if (b == nBuckets) b = nBuckets - 1;
                                    buckets[b].Count++;
                                    buckets[b].Bounds = Bounds3D.Union(buckets[b].Bounds, primitiveInfos[i].Bounds);
                                }

                                double[] cost = new double[nBuckets - 1];
                                for (int i = 0; i < nBuckets - 1; ++i)
                                {
                                    Bounds3D b0 = new Bounds3D();
                                    Bounds3D b1 = new Bounds3D();

                                    int count0 = 0, count1 = 0;
                                    for (int j = 0; j <= i; ++j)
                                    {
                                        b0 = Bounds3D.Union(b0, buckets[j].Bounds);
                                        count0 += buckets[j].Count;
                                    }

                                    for (int j = i + 1; j < nBuckets; ++j)
                                    {
                                        b1 = Bounds3D.Union(b1, buckets[j].Bounds);
                                        count1 += buckets[j].Count;
                                    }

                                    cost[i] = .125f + (count0 * b0.SurfaceArea() +
                                                       count1 * b1.SurfaceArea()) / bounds.SurfaceArea();
                                }

                                double minCost = cost[0];
                                int minCostSplitBucket = 0;
                                for (int i = 1; i < nBuckets - 1; ++i)
                                {
                                    if (cost[i] < minCost)
                                    {
                                        minCost = cost[i];
                                        minCostSplitBucket = i;
                                    }
                                }

                                double leafCost = nPrimitives;
                                if(nPrimitives > _maxPrimitivesInNode || minCost < leafCost)
                                {
                                    var midGrouped = primitiveInfos.GroupBy(primitiveInfos =>
                                    {
                                        int b = (int)(nBuckets * centroidBounds.Offset(primitiveInfos.Centroid)[dim]);
                                        if (b == nBuckets) b = nBuckets - 1;
                                        return b <= minCostSplitBucket;
                                    });

                                    List<BVHPrimitiveInfo> newPrimitiveInfos = new List<BVHPrimitiveInfo>();
                                    mid = 0;
                                    int counter = 0;
                                    foreach (var group in midGrouped)
                                    {
                                        foreach (var info in group)
                                        {
                                            if (counter >= start && counter <= end)
                                            {
                                                newPrimitiveInfos.Add(info);
                                            }
                                            else
                                            {
                                                newPrimitiveInfos.Add(primitiveInfos[counter]);
                                            }
                                            if (group.Key == true)
                                            {
                                                mid++;
                                            }
                                            counter++;
                                        }
                                    }
                                    primitiveInfos = newPrimitiveInfos;
                                    mid = mid + start;
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

                                break;
                            }
                    }
                    node.InitInterior(dim, RecursiveBuild(primitiveInfos, start, mid, ref totalNodes, ref orderedPrimitives),
                                           RecursiveBuild(primitiveInfos, mid  , end, ref totalNodes, ref orderedPrimitives));
                }
            }
            return node;
        }

        public override Bounds3D GetWorldBound()
        {
            throw new NotImplementedException();
        }

        public override bool Intersect(Ray r, out SurfaceInteraction intersection)
        {
            throw new NotImplementedException();
        }
    }
}
