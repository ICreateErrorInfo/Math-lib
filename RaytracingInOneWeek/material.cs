using Math_lib;

namespace Raytracing
{
    public class Material
    {
        public virtual Vector3D Emitted(double u, double v, Point3D p)
        {
            return new Vector3D(0,0,0);
        }
        public virtual bool Scatter(Ray rIn, ref SurfaceInteraction rec, out Vector3D attenuation, out Ray scattered)
        {
            attenuation = new Vector3D();
            scattered = new Ray();
            return false;
        }
    }
}
