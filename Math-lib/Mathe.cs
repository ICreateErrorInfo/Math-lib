using System;
using System.Collections.Generic;
using System.Linq;

namespace Math_lib
{
    public static class Mathe
    {
        public static double infinity = double.PositiveInfinity;
        [ThreadStatic]
        private static Random _random;

        public static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new System.Random();
                }
                return _random;
            }
        }

        public static double ToRad(double deg) => deg * Math.PI / 180;
        public static double Lerp(double t, double v1, double v2)
        {
            return (1 - t) * v1 + t * v2;
        }
        public static void Swap(ref double d, ref double d1)
        {
            (d1, d) = (d, d1);
        }
        public static bool SolveQuadratic(double a, double b, double c, out double t0, out double t1)
        {
            t0 = 0;
            t1 = 0;

            var discriminant = (b * b) - (4 * a * c);

            if (discriminant < 0)
            {
                return false;
            }

            var sqrtd = Math.Sqrt(discriminant);

            t0 = (-b - sqrtd) / (2 * a);
            t1 = (-b + sqrtd) / (2 * a);

            return true;
        }
        public static bool SolveQuadratic(double a, double halfB, double c, out double t0, double tMin, double tMax)
        {
            t0 = 0;

            var discriminant = halfB * halfB - a * c;

            if (discriminant < 0)
            {
                return false;
            }
            var sqrtd = Math.Sqrt(discriminant);

            t0 = (-halfB - sqrtd) / a;
            if (t0 < tMin || tMax < t0)
            {
                t0 = (-halfB + sqrtd) / a;
                if (t0 < tMin || tMax < t0)
                {
                    return false;
                }
            }            
            //makes sure there is no self intersection can be fixed with rounding error

            return true;
        }
        public static bool SolveQuadratic(double a, double b, double c, out double t0, out double t1, double tMin, double tMax)
        {
            t0 = 0;
            t1 = 0;

            var discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                return false;
            }
            var sqrtd = Math.Sqrt(discriminant);

            t0 = (-b - sqrtd) / (2 * a);
            if (t0 < tMin || tMax < t0)
            {
                t0 = (-b + sqrtd) / (2 * a);
                if (t0 < tMin || tMax < t0)
                {
                    return false;
                }
            }
            //makes sure there is no self intersection can be fixed with rounding error

            return true;
        }
        public static List<T> Partition<T>(List<T> primitiveInfos, int start, int end, Func<T, bool> predicate, out int mid)
        {
            List<T> elementsToSort = new List<T>();
            for (int i = start; i < end; i++)
            {
                elementsToSort.Add(primitiveInfos[i]);
            }

            IEnumerable<IGrouping<bool, T>> sortedElements = elementsToSort.GroupBy(predicate);

            List<T> newPrimitiveInfos = new List<T>();
            List<T> primitevesTrue = new List<T>();
            List<T> primitevesfalse = new List<T>();
            mid = start;
            foreach (var group in sortedElements)
            {
                foreach (var info in group)
                {
                    if (group.Key == true)
                    {
                        mid++;
                        primitevesTrue.Add(info);
                    }
                    if (group.Key == false)
                    {
                        primitevesfalse.Add(info);
                    }
                }
            }
            for (int i = 0; i < start; i++)
            {
                newPrimitiveInfos.Add(primitiveInfos[i]);
            }
            foreach (var info in primitevesTrue)
            {
                newPrimitiveInfos.Add(info);
            }
            foreach (var info in primitevesfalse)
            {
                newPrimitiveInfos.Add(info);
            }
            for (int i = primitiveInfos.Count - 1; i > end; i--)
            {
                newPrimitiveInfos.Add(primitiveInfos[i]);
            }

            return newPrimitiveInfos;
        }

        public static double Random1Tom1()
        {
            return GetRandomDouble(-1, 1);
        }
        public static double GetRandomDouble(double minimum, double maximum)
        {
            return Random.NextDouble() * (maximum - minimum) + minimum;
        }
        public static int GetRandomInt(int minimum, int maximum)
        {
            return Random.Next(minimum, maximum);
        }
    }
}
