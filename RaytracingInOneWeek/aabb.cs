using System;
using Math_lib;
using System.Collections.Generic;
using System.Text;

namespace RaytracingInOneWeek
{
    public class aabb
    {
        public aabb()
        {

        }
        public aabb(Point3D a, Point3D b)
        {
            minimum = a;
            maximum = b;
        }

        public Point3D minimum;
        public Point3D maximum;

        public zwischenSpeicherAABB hit(Ray r, double t_min, double t_max)
        {
            zwischenSpeicherAABB zw = new zwischenSpeicherAABB();
            for (int a = 0; a < 3; a++)
            {
                double min = 0;
                double max = 0;
                double origin = 0;
                double direc = 0;

                if(a == 0)
                {
                    min = minimum.X;
                    max = maximum.X;
                    origin = r.o.X;
                    direc = r.d.X;
                }
                if (a == 1)
                {
                    min = minimum.Y;
                    max = maximum.Y;
                    origin = r.o.Y;
                    direc = r.d.X;
                }
                if (a == 2)
                {
                    min = minimum.Z;
                    max = maximum.Z;
                    origin = r.o.Z;
                    direc = r.d.X;
                }

                var invD = 1 / direc;
                var t0 = (min - origin) * invD;
                var t1 = (max - origin) * invD;

                if (invD < 0)
                {
                    double zwi = t0;
                    t0 = t1;
                    t1 = zwi;
                }

                t_min = t0 > t_min ? t0 : t_min;
                t_max = t1 < t_max ? t1 : t_max;

                if(t_max <= t_min)
                {
                    zw.t_max = t_max;
                    zw.t_min = t_min;
                    zw.isTrue = false;
                    return zw;
                }
            }
            zw.t_max = t_max;
            zw.t_min = t_min;
            zw.isTrue = true;
            return zw;
        }
        public static aabb surrounding_box(aabb box0, aabb box1)
        {
            Point3D small = new Point3D(Math.Min(box0.minimum.X, box1.minimum.X),
                                        Math.Min(box0.minimum.Y, box1.minimum.Y),
                                        Math.Min(box0.minimum.Z, box1.minimum.Z));

            Point3D big   = new Point3D(Math.Max(box0.maximum.X, box1.maximum.X),
                                        Math.Max(box0.maximum.Y, box1.maximum.Y),
                                        Math.Max(box0.maximum.Z, box1.maximum.Z));

            return new aabb(small, big);
        }
    }
    public struct zwischenSpeicherAABB
    {
        public aabb outputBox;
        public double t_min;
        public double t_max;
        public bool isTrue;
    }
}
