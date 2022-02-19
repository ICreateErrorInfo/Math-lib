using System;
using Math_lib;
using System.Collections.Generic;
using System.Text;

namespace RaytracingInOneWeek
{
    class box : hittable
    {
        public box()
        {

        }
        public box(Point3D p0, Point3D p1, material ptr)
        {
            box_min = p0;
            box_max = p1;

            sides.Add(new xy_rect(p0.X, p1.X, p0.Y, p1.Y, p1.Z, ptr));
            sides.Add(new xy_rect(p0.X, p1.X, p0.Y, p1.Y, p0.Z, ptr));
                                 
            sides.Add(new xz_rect(p0.X, p1.X, p0.Z, p1.Z, p1.Y, ptr));
            sides.Add(new xz_rect(p0.X, p1.X, p0.Z, p1.Z, p0.Y, ptr));
                                   
            sides.Add(new yz_rect(p0.Y, p1.Y, p0.Z, p1.Z, p1.X, ptr));
            sides.Add(new yz_rect(p0.Y, p1.Y, p0.Z, p1.Z, p0.X, ptr));

        }
        public Point3D box_min;
        public Point3D box_max;
        public hittable_list sides = new hittable_list();

        public override zwischenSpeicher Hit(Ray r, double t_min, double t_max, hit_record rec)
        {
            return sides.Hit(r, t_min, t_max, rec);
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(box_min, box_max);
            return true;
        }
    }
}
