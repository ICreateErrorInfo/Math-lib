using System;
using Math_lib;

namespace Raytracing
{
    public class sphere : hittable
    {
        readonly Point3D center;
        readonly double radius;
        readonly material mat_ptr;
        public double Radius => radius;
        public Point3D Center => center;
        public material MatPtr => mat_ptr;

        public sphere() { }
        public sphere(Point3D cen, double r, material m)
        {
            center = cen;
            radius = r;
            mat_ptr = m;
        }

        public override bool TryHit(Ray r, double t_min, double t_max, ref SurfaceInteraction isect)
        {
            Vector3D oc = r.o - center;
            var a = r.d.GetLengthSqrt();
            var half_b = Vector3D.Dot(oc, r.d);
            var c = oc.GetLengthSqrt() - radius * radius;
            var discriminant = half_b * half_b - a * c;

            if (discriminant < 0)
            {
                return false;
            }
            var sqrtd = Math.Sqrt(discriminant);

            var root = (-half_b - sqrtd) / a;
            if (root < t_min || t_max < root)
            {
                root = (-half_b + sqrtd) / a;
                if (root < t_min || t_max < root)
                {
                    return false;
                }
            }

            isect.t = root;
            isect.p = r.At(root);
            Normal3D outward_normal = (Normal3D)((isect.p - center) / radius);
            isect.set_face_normal(r, outward_normal);
            (isect.u, isect.v) = get_sphere_uv(outward_normal);
            isect.mat_ptr = mat_ptr;

            return true;
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(center - new Vector3D(radius, radius, radius),
                                 center + new Vector3D(radius, radius, radius));
            return true;
        }
        private (double u, double v) get_sphere_uv(Normal3D p)
        {
            var theta = Math.Acos(-p.Y);
            var phi = Math.Atan2(-p.Z, p.X) + Math.PI;

            var u = phi / (2 * Math.PI);
            var v = theta / Math.PI;

            return (u, v);
        }
    }
}
