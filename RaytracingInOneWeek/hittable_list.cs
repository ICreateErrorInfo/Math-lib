﻿using System.Collections.Generic;
using System.Linq;
using Math_lib;

namespace RaytracingInOneWeek
{
    public class hittable_list : hittable
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
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            if (!objects.Any())
            {
                return false;
            }

            Bounds3D currentBoundingBox = new Bounds3D();
            bool isFirstBox = true;

            foreach(var _object in objects)
            {
                if (!_object.bounding_box(time0, time1, ref currentBoundingBox))
                {
                    bound = isFirstBox ? currentBoundingBox : Bounds3D.Union(bound, currentBoundingBox);
                    isFirstBox = false;
                }
            }

            return true;
        }
    }
}
