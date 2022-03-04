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
