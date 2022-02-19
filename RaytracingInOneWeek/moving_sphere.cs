using System;
using Math_lib;
using System.Collections.Generic;
using System.Text;

namespace RaytracingInOneWeek
{
    class moving_sphere : hittable
    {
        public moving_sphere()
        {

        }
        public moving_sphere(Point3D cen0, Point3D cen1, double _time0, double _time1, double r, material m)
        {
            center0 = cen0;
            center1 = cen1;
            time0 = _time0;
            time1 = _time1;
            radius = r;
            mat_ptr = m;
        }
        public Point3D center0;
        public Point3D center1;
        public double time0;
        public double time1;
        public double radius;
        public material mat_ptr;

        public override zwischenSpeicher Hit(Ray r, double t_min, double t_max, hit_record rec)
        {
            zwischenSpeicher zw = new zwischenSpeicher();

            Vector3D oc = r.o - center(r.tMax);
            var a = r.d.GetLengthSqrt();
            var half_b = Vector3D.Dot(oc, r.d);
            var c = oc.GetLengthSqrt() - radius * radius;

            var discriminant = half_b * half_b - a * c;
            if(discriminant < 0)
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
            Normal3D outward_normal = (Normal3D)(Vector3D)((rec.p - center(r.tMax)) / radius);
            rec.set_face_normal(r, outward_normal);
            rec.mat_ptr = mat_ptr;

            zw.rec = rec;
            zw.IsTrue = true;

            return zw;
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            Bounds3D box0 = new Bounds3D(center(time0) - new Vector3D(radius, radius, radius),
                                         center(time0) + new Vector3D(radius, radius, radius));
            Bounds3D box1 = new Bounds3D(center(time1) - new Vector3D(radius, radius, radius),
                                         center(time1) + new Vector3D(radius, radius, radius));
            bound = Bounds3D.Union(box0, box1);
            return true;
        }
        public virtual Point3D center(double time)
        {
            return center0 + ((time - time0) / (time1 - time0)) * (center1 - center0);
        }
    }
}
