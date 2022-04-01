using Math_lib;
using Raytracing.Materials;
using Raytracing.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    public class GeometricPrimitive : Primitive
    {
        private Shape _shape;
        private Material _material;

        public GeometricPrimitive(Shapes.Shape shape, Material material)
        {
            _shape = shape;
            _material = material;
        }

        public override bool Intersect(Ray r, out SurfaceInteraction intersection)
        {
            double tHit = 0;
            if (!_shape.Intersect(r, out tHit, out intersection))
            {
                return false;
            }
            r.TMax = tHit;
            intersection.Primitive = this;
            return true;
        }
        public override Material GetMaterial()
        {
            return _material;
        }
        public override Bounds3D GetWorldBound()
        {
            return _shape.ObjectToWorld.m * _shape.GetObjectBound();
        }
    }
}
