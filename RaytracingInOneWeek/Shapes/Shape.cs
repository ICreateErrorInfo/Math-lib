using Math_lib;

namespace Raytracing.Shapes
{
    public abstract class Shape
    {
        public Transform WorldToObject;
        public Transform ObjectToWorld;

        public abstract bool Intersect(Ray r, out double tMax, out SurfaceInteraction isect);
        public abstract Bounds3D GetObjectBound();
    }
}
