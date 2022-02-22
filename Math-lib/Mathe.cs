using System;

namespace Math_lib
{
    public static class Mathe
    {
        public static double infinity = double.PositiveInfinity;
        private readonly static Random _random = new System.Random();

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
            return GetRandomDouble(-1,1);
        }
        public static double GetRandomDouble(double minimum, double maximum)
        {
            return _random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
