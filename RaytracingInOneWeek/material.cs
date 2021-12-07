using Math_lib;

namespace RaytracingInOneWeek
{
    public class material
    {
        public virtual Vector3D emitted(double u, double v, Point3D p)
        {
            return new Vector3D(0,0,0);
        }
        public virtual zwischenSpeicher scatter(Ray r_in, hit_record rec, Vector3D attenuation, Ray scattered)
        {
            zwischenSpeicher zw = new zwischenSpeicher();
            return zw;
        }
    }
}
