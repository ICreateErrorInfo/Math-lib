using Math_lib;
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

        public enum SplitMethod { SAH, HLBVH, Middle, EqualCounts }
        private int _maxPrimitivesInNode;
        SplitMethod _splitMethod;
        List<Primitive> _primitives;

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
