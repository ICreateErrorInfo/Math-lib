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

            Bounds3D box_left = new Bounds3D();
            Bounds3D box_right = new Bounds3D();

            if(!left.bounding_box(time0, time1, ref box_left) || !right.bounding_box(time0, time1, ref box_right))
            {
                //geht nicht
            }

            box = Bounds3D.Union(box_left, box_right);
        }
        public hittable left;
        public hittable right;
        Bounds3D box;

        public override zwischenSpeicher Hit(Ray r, double t_min, double t_max, hit_record rec)
        {
            bool foundHit = box.IntersectP(r, ref t_min, ref t_max);
            zwischenSpeicher zw1 = left.Hit(r, t_min, t_max, rec);

            if (!foundHit)
            {
                zw1.IsTrue = false;
                return zw1;
            }

            bool hit_left = zw1.IsTrue;
            bool hit_right = right.Hit(r, t_min, hit_left ? zw1.rec.t : t_max, zw1.rec).IsTrue;

            zw1.IsTrue = hit_left || hit_right;

            return zw1;
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            bound = box;
            return true;
        }
        public static int box_compare(hittable a, hittable b, int axis)
        {
            Bounds3D box_a = new Bounds3D();
            Bounds3D box_b = new Bounds3D();

            if (!a.bounding_box(0, 0, ref box_a) || !a.bounding_box(0, 0, ref box_b))
            {
                //geht nicht
            }

            if (axis == 0)
            {
                if(box_a.pMin.X < box_b.pMin.X)
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
                if(box_a.pMin.Y < box_b.pMin.Y)
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
                if(box_a.pMin.Z < box_b.pMin.Z)
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
