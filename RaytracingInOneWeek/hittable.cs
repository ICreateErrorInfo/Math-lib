using Math_lib;

namespace RaytracingInOneWeek
{
    public struct hit_record
    {
        public Point3D p;
        public Normal3D normal;
        public material mat_ptr;
        public double t;
        public double u;
        public double v;
        public bool front_face;

        public void set_face_normal(Ray r, Normal3D outwardNormal)
        {
            front_face = Vector3D.Dot(r.d, (Vector3D)outwardNormal) < 0;
            normal = front_face ? outwardNormal : -outwardNormal;
        }
    }

    public class hittable
    {
        public virtual zwischenSpeicher Hit(Ray r, double t_min, double t_max, hit_record rec)
        {
            zwischenSpeicher zw = new zwischenSpeicher();
            return zw; 
        }
        public virtual bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            return false;
        }
    }

    public struct zwischenSpeicher
    {
        public bool IsTrue;
        public hit_record rec;
        public Ray scattered;
        public Vector3D attenuation;
    }
}
