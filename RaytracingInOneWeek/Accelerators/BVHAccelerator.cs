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
            int _primitiveNumber;
            Bounds3D _bounds;
            Point3D _centroid;

            public BVHPrimitiveInfo(int primitiveNumber, Bounds3D bounds)
            {
                _primitiveNumber = primitiveNumber;
                _bounds = bounds;
                _centroid = 0.5 * bounds.pMin + 0.5 * bounds.pMax;
            }
        }
        struct BVHBuildNode
        {
            Bounds3D _bounds;
            BVHBuildNode?[] _children;
            int _splitAxis, _firstPrimOffset, _nPrimitives;

            void InitLeaf(int first, int n, Bounds3D b)
            {
                _firstPrimOffset = first;
                _nPrimitives = n;
                _bounds = b;
                _children[0] = _children[1] = null;
            }
            void InitInterior(int axis, BVHBuildNode c0, BVHBuildNode c1)
            {
                _children[0] = c0;
                _children[1] = c1;
                _bounds = Bounds3D.Union(c0._bounds, c1._bounds);
                _splitAxis = axis;
                _nPrimitives = 0;
            }
        }

        public enum SplitMethod { SAH, HLBVH, Middle, EqualCounts}
        private int _maxPrimitivesInNode;
        SplitMethod _splitMethod;
        List<Primitive> _primitives;

        public BVHAccelerator(List<Primitive> primitives, int maxPrimitivesInNode, SplitMethod splitMethod)
        {
            _maxPrimitivesInNode = Math.Min(maxPrimitivesInNode, 255);
            _splitMethod = splitMethod;
            _primitives = primitives;
            
            if(_primitives.Count == 0)
            {
                return;
            }

            List<BVHPrimitiveInfo> primitiveInfos = new List<BVHPrimitiveInfo>();
            for(int i = 0; i < _primitives.Count; i++)
            {
                primitiveInfos.Add(new BVHPrimitiveInfo(i, _primitives[i].GetWorldBound()));
            }

            int totalNodes = 0;
            List<Primitive> orderedPrims = new List<Primitive>();
            BVHBuildNode root;
            if(splitMethod == SplitMethod.HLBVH)
            {

            }
            else
            {

            }
            primitives = orderedPrims;
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
