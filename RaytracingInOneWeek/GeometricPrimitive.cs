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

        public override SurfaceInteraction Intersect(Ray r, SurfaceInteraction interaction)
        {
            double tHit = 0;
            if (!_shape.Intersect(r, out tHit, out interaction))
            {
                interaction.HasIntersection = false; //TODO theoretisch unnötig
                return interaction;
            }
            r.TMax = tHit;
            interaction.Primitive = this;
            interaction.HasIntersection = true;
            return interaction;
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
