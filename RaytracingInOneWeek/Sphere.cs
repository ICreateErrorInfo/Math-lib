using System;
using Math_lib;

namespace RaytracingInOneWeek
{
    class sphere : hittable
    {
        public sphere() { }
        public sphere(Point3D cen, double r, material m)
        {
            center = cen;
            radius = r;
            mat_ptr = m;
        }
        Point3D center;
        double radius;
        material mat_ptr;

        public override zwischenSpeicher Hit(Ray r, double t_min, double t_max, hit_record rec)
        {
            zwischenSpeicher zw = new zwischenSpeicher();
            Vector3D oc = r.o - center;
            var a = r.d.GetLengthSqrt();
            var half_b = Vector3D.Dot(oc, r.d);
            var c = oc.GetLengthSqrt() - radius * radius;

            var discriminant = half_b * half_b - a * c;
            if (discriminant < 0)
            {
                zw.IsTrue = false;
                return zw;
            }
            var sqrtd = Math.Sqrt(discriminant);

            var root = (-half_b - sqrtd) / a;
            if (root < t_min || t_max < root)
            {
                root = (-half_b + sqrtd) / a;
                if (root < t_min || t_max < root)
                {
                    zw.IsTrue = false;
                    return zw;
                }
            }

            rec.t = root;
            rec.p = r.At(rec.t);
            Normal3D outward_normal = (Normal3D)((rec.p - center) / radius);
            rec.set_face_normal(r, outward_normal);
            (rec.u, rec.v) = get_sphere_uv(outward_normal, rec.u, rec.v);
            rec.mat_ptr = mat_ptr;

            zw.rec = rec;
            zw.IsTrue = true;

            return zw;
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(center - new Vector3D(radius, radius, radius),
                                 center + new Vector3D(radius, radius, radius));
            return true;
        }
        private (double u, double v) get_sphere_uv(Normal3D p, double u, double v)
        {
            var theta = Math.Acos(-p.Y);
            var phi = Math.Atan2(-p.Z, p.X) + Math.PI;

            u = phi / (2 * Math.PI);
            v = theta / Math.PI;

            return (u, v);
        }

        private double u;
        private double v;
    }
}
