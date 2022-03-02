using Math_lib;

namespace Raytracing
{
    public class Shape
    {
        public virtual bool TryHit(Ray r, double tMin, double tMax, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();
            return false;
        }
        public virtual bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            return false;
        }
    }
}
