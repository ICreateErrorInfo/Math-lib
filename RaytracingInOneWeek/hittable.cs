using Math_lib;

namespace Raytracing
{
    public class Hittable
    {
        public virtual bool TryHit(Ray r, double tMin, double tMax, ref SurfaceInteraction insec)
        {
            return false;
        }
        public virtual bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            return false;
        }
    }
}
