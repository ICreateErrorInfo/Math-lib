using Math_lib;
using Raytracing.Mathmatic;

namespace Raytracing.Shapes {
    public abstract class Shape
    {
        public Transform WorldToObject;
        public Transform ObjectToWorld;

        public abstract bool Intersect(Ray r, out double tMax, out SurfaceInteraction interaction);
        public abstract Bounds3D GetObjectBound();
    }
}
