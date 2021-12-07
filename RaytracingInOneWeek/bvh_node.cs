using System;
using System.Collections.Generic;
using System.Text;
using Math_lib;
using System.Diagnostics.CodeAnalysis;

namespace RaytracingInOneWeek
{
    class bvh_node : hittable
    {
        public bvh_node()
        {

        }
        public bvh_node(hittable_list list, double time0, double time1)
        {
            new bvh_node(list.objects, 0, list.objects.Count, time0, time1);
        }
        public bvh_node(List<hittable> src_objects, int start, int end, double time0, double time1)
        {
            var axis = Mathe.random(0, 2, 1);

            IComparer<hittable> comparator;
            if (axis == 0)
            {
                comparator = new XComparator();
            }
            else if (axis == 1)
            {
                comparator = new YComparator();
            }
            else
            {
                comparator = new ZComparator();
            }

            var objekts = src_objects;       

            int object_span = end - start;

            if(object_span == 1)
            {
                left = right = objekts[start];
            }else if (object_span == 2)
            {
                if(comparator.Compare(objekts[(int)start], objekts[(int)start + 1]) <= 0)
                {
                    left = objekts[start];
                    right = objekts[start + 1];
                }
                else
                {
                    left = objekts[start + 1];
                    right = objekts[start];
                }
            }
            else
            {
                objekts.Sort(start, end, comparator);

                var mid = start + object_span / 2;
                left = new bvh_node(objekts, start, mid, time0, time1);
                right = new bvh_node(objekts, mid, end, time0, time1);
            }

            aabb box_left = new aabb();
            aabb box_right = new aabb();

            if(!left.bounding_box(time0, time1, box_left).isTrue || !right.bounding_box(time0, time1, box_right).isTrue)
            {
                //geht nicht

            }

            box = aabb.surrounding_box(box_left, box_right);
        }
        public hittable left;
        public hittable right;
        aabb box;

        public override zwischenSpeicher Hit(Ray r, double t_min, double t_max, hit_record rec)
        {
            zwischenSpeicherAABB zw = box.hit(r, t_min, t_max);
            t_min = zw.t_min;
            t_max = zw.t_max;
            zwischenSpeicher zw1 = left.Hit(r, t_min, t_max, rec);

            if (!zw.isTrue)
            {
                zw1.IsTrue = false;
                return zw1;
            }

            bool hit_left = zw1.IsTrue;
            bool hit_right = right.Hit(r, t_min, hit_left ? zw1.rec.t : t_max, zw1.rec).IsTrue;

            zw1.IsTrue = hit_left || hit_right;

            return zw1;
        }
        public override zwischenSpeicherAABB bounding_box(double time0, double time1, aabb output_box)
        {
            zwischenSpeicherAABB zw = new zwischenSpeicherAABB();
            output_box = box;
            zw.outputBox = output_box;
            zw.isTrue = true;
            return zw;
        }
        public static int box_compare(hittable a, hittable b, int axis)
        {
            aabb box_a = new aabb();
            aabb box_b = new aabb();

            zwischenSpeicherAABB zwa = new zwischenSpeicherAABB();
            zwa = a.bounding_box(0, 0, box_a);
            zwischenSpeicherAABB zwb = new zwischenSpeicherAABB();
            zwb = a.bounding_box(0, 0, box_b);

            if (!zwa.isTrue || !zwb.isTrue)
            {
                //geht nicht
            }

            if (axis == 0)
            {
                if(box_a.minimum.X < box_b.minimum.X)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            if (axis == 1)
            {
                if(box_a.minimum.Y < box_b.minimum.Y)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            if (axis == 2)
            {
                if(box_a.minimum.Z < box_b.minimum.Z)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

            return 0;
        }
    }
    public class XComparator : IComparer<hittable>
    {
        public int Compare([AllowNull] hittable x, [AllowNull] hittable y)
        {
            return bvh_node.box_compare(x, y, 0);
        }
    }

    public class YComparator : IComparer<hittable>
    {
        public int Compare([AllowNull] hittable x, [AllowNull] hittable y)
        {
            return bvh_node.box_compare(x, y, 1);
        }
    }

    public class ZComparator : IComparer<hittable>
    {
        public int Compare([AllowNull] hittable x, [AllowNull] hittable y)
        {
            return bvh_node.box_compare(x, y, 2);
        }
    }
}
