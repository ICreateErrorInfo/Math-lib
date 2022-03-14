using Math_lib;

namespace Raytracing
{
    public abstract class Shape
    {
        public abstract bool Intersect(Ray r, double tMin, double tMax, out SurfaceInteraction isect);
        public abstract bool BoundingBox(double time0, double time1, ref Bounds3D bound);
    }
}
