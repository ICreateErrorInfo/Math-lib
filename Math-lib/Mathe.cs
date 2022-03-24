using System;

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
