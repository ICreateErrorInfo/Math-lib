using System;
using System.Collections.Generic;
using System.Text;
using Math_lib;
using System.Diagnostics.CodeAnalysis;

namespace Raytracing
{
    class BvhNode : Shape
    {
        private Shape _left;
        private Shape _right;
        private Bounds3D _box;

        public BvhNode()
        {

        }
        public BvhNode(HittableList list, double time0, double time1)
        {
            BvhNode node = new BvhNode(list.Objects, 0, list.Objects.Count, time0, time1);
            _left = node._left;
            _right = node._right;
            _box = node._box;
        }
        public BvhNode(List<Shape> objects, int start, int end, double time0, double time1)
        {
            var axis = Mathe.GetRandomInt(0, 2);

            IComparer<Shape> comparator;
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

            int objectSpan = end - start;

            if(objectSpan == 1)
            {
                _left = _right = objects[start];
            }
            else if (objectSpan == 2)
            {
                if(comparator.Compare((Shape)objects[(int)start], (Shape)objects[(int)start + 1]) <= 0)
                {
                    _left = objects[start];
                    _right = objects[start + 1];
                }
                else
                {
                    _left = objects[start + 1];
                    _right = objects[start];
                }
            }
            else
            {
                objects.Sort(start, objectSpan, comparator);

                var mid = start + objectSpan / 2;
                _left = new BvhNode(objects, start, mid, time0, time1);
                _right = new BvhNode(objects, mid, end, time0, time1);
            }

            Bounds3D boxLeft = new Bounds3D();
            Bounds3D boxRight = new Bounds3D();

            if(!_left.BoundingBox(time0, time1, ref boxLeft) || !_right.BoundingBox(time0, time1, ref boxRight))
            {
                //geht nicht
            }

            _box = Bounds3D.Union(boxLeft, boxRight);
        }

        public override bool Intersect(Ray r, double tMin, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();
            double a = 0, b = 0;
            bool foundBoxInsec = _box.IntersectP(r, out a, out b);

            r.TMax = b;

            if (!foundBoxInsec)
            {
                return false;
            }

            var isectLeft = new SurfaceInteraction();
            bool hitLeft = _left.Intersect(r, tMin, out isectLeft);
            var isectRight = new SurfaceInteraction();
            bool hitRight = _right.Intersect(r, tMin, out isectRight);

            if(hitRight)
            {
                isect = isectRight;
            }
            else
            {
                isect = isectLeft;
            }

            return hitLeft || hitRight;
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = _box;
            return true;
        }
        public static int box_compare(Shape a, Shape b, int axis)
        {
            Bounds3D boxA = new Bounds3D();
            Bounds3D boxB = new Bounds3D();

            if (!a.BoundingBox(0, 0, ref boxA) || !a.BoundingBox(0, 0, ref boxB))
            {
                //geht nicht
            }

            if (axis == 0)
            {
                if(boxA.pMin.X < boxB.pMin.X)
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
                if(boxA.pMin.Y < boxB.pMin.Y)
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
                if(boxA.pMin.Z < boxB.pMin.Z)
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

    public class XComparator : IComparer<Shape>
    {
        public int Compare([AllowNull] Shape x, [AllowNull] Shape y)
        {
            return BvhNode.box_compare(x, y, 0);
        }
    }
    public class YComparator : IComparer<Shape>
    {
        public int Compare([AllowNull] Shape x, [AllowNull] Shape y)
        {
            return BvhNode.box_compare(x, y, 1);
        }
    }
    public class ZComparator : IComparer<Shape>
    {
        public int Compare([AllowNull] Shape x, [AllowNull] Shape y)
        {
            return BvhNode.box_compare(x, y, 2);
        }
    }
}
