using Math_lib;

namespace Raytracing
{
    public class hittable
    {
        public virtual bool TryHit(Ray r, double t_min, double t_max, ref SurfaceInteraction insec)
        {
            return false;
        }
        public virtual bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            return false;
        }
    }
}
