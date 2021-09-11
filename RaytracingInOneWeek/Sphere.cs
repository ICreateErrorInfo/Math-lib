using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaytracingInOneWeek
{
    class Sphere : Shape
    {
        //Properties
        public Point3D center;
        public double radius;

        //Ctors
        public Sphere()
        {

        }
        public Sphere(Point3D cen, double r)
        {
            center = cen;
            radius = r;
        }

        public override bool hit(Ray r, double tMin, ref IntersectionData ID)
        {
            Vector3D oc = r.o - center;
            double a = r.d.GetLengthSqrt();
            double halfB = Vector3D.Dot(oc, r.d);
            double c = oc.GetLengthSqrt() - radius * radius;

            double discriminant = halfB * halfB - a * c;
            if (discriminant < 0) return false;

            double sqrtd = Math.Sqrt(discriminant);
            double root = (-halfB - sqrtd) / a;
            if (root < tMin || r.tMax < root)
            {
                root = (-halfB + sqrtd) / a;
                if (root < tMin || r.tMax < root)
                    return false;
            }

            ID.t = root;
            ID.p = r.At(root);
            ID.normal = (Normal3D)(ID.p - center) / radius;
            ID.setFaceNormal(r, ID.normal);
            return true;
        }
    }
}
