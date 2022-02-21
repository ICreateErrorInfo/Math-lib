using Math_lib;

namespace Raytracing
{
    public class material
    {
        public virtual Vector3D emitted(double u, double v, Point3D p)
        {
            return new Vector3D(0,0,0);
        }
        public virtual bool scatter(Ray r_in, ref SurfaceInteraction rec, ref Vector3D attenuation, ref Ray scattered)
        {
            return false;
        }
    }
}
