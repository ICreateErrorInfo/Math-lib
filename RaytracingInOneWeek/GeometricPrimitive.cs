using Math_lib;
using Raytracing.Materials;
using Raytracing.Shapes;

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

        public override SurfaceInteraction Intersect(Ray r, SurfaceInteraction intersection)
        {
            double tHit = 0;
            if (!_shape.Intersect(r, out tHit, out intersection))
            {
                intersection.HasIntersection = false; //TODO theoretisch unnötig
                return intersection;
            }
            r.TMax = tHit;
            intersection.Primitive = this;
            intersection.HasIntersection = true;
            return intersection;
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
