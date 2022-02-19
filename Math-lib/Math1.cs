namespace Math_lib
{
    public class Math1
    {
        public static double Lerp(double t, double v1, double v2)
        {
            return (1 - t) * v1 + t * v2;
        }
        public static double ToRad(double deg)
        {
            return deg * System.Math.PI / 180;
        }
        public static void Swap(ref double d, ref double d1)
        {
            (d1, d) = (d, d1);
        }
    }
}
