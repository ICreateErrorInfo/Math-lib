using System.Collections.Generic;
using System.Linq;
using Math_lib;

namespace RaytracingInOneWeek
{
    class hittable_list : hittable
    {
        public hittable_list()
        {

        }
        public hittable_list(hittable obj)
        {
            objects.Add(obj);
        }
        public void Add(hittable obj)
        {
            objects.Add(obj);
        }

        public List<hittable> objects = new List<hittable>();

        public override zwischenSpeicher Hit(Ray r, double t_min, double t_max, hit_record rec)
        {
            zwischenSpeicher zw = new zwischenSpeicher();
            hit_record temp_rec = new hit_record();
            bool hit_anything = false;
            var closest_so_far = t_max;

            foreach (var Object in objects)
            {
                zwischenSpeicher zw1 = Object.Hit(r, t_min, closest_so_far, temp_rec);
                if (zw1.IsTrue)
                {
                    temp_rec = zw1.rec;
                    hit_anything = true;
                    closest_so_far = temp_rec.t;
                    rec = temp_rec;
                }
            }

            zw.rec = rec;
            zw.IsTrue = hit_anything;

            return zw;
        }
        public override zwischenSpeicherAABB bounding_box(double time0, double time1, aabb output_box)
        {
            zwischenSpeicherAABB zw = new zwischenSpeicherAABB();
            if (!objects.Any())
            {
                zw.isTrue = false;
                return zw;
            }

            aabb temp_Box = new aabb();
            bool first_box = true;

            foreach(var _object in objects)
            {
                zw = _object.bounding_box(time0, time1, temp_Box);
                if (!zw.isTrue)
                {
                    output_box = first_box ? zw.outputBox : aabb.surrounding_box(output_box, zw.outputBox);
                    first_box = false;
                }
            }

            zw.outputBox = output_box;
            zw.isTrue = true;
            return zw;
        }
    }
}
