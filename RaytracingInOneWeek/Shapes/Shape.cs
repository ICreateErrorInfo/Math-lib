using Math_lib;

namespace Raytracing.Shapes
{
    public abstract class Shape
    {
        public abstract bool Intersect(Ray r, double tMin, out SurfaceInteraction isect);
        public abstract Bounds3D GetObjectBound();
    }
}
