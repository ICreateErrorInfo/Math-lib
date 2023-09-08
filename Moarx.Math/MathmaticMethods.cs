namespace Moarx.Math;
public class MathmaticMethods {
    public static double SafeASin(double x) {
        return System.Math.Asin(System.Math.Clamp(x, -1, 1));
    }
    public static double SafeACos(double x) {
        return System.Math.Acos(System.Math.Clamp(x, -1, 1));
    }
    public static double SafeSqrt(double x) {
        return System.Math.Sqrt(System.Math.Max(0, x));
    }

    public static double DifferenceOfProducts(double a, double b, double c, double d) {
        var cd  = c * d;
        var differenceOfProducts = FMA(a, b, -cd);
        var error = FMA(-c, d, cd);
        return differenceOfProducts + error;
    }
    public static double FMA(double a, double b, double c) {
        return System.Math.FusedMultiplyAdd(a, b, c);
    }
    public static double InnerProduct(double a, double b, params double[] terms) {
        double ab = a * b;
        double tp = InnerProduct(terms);
        return ab + tp;
    }
    static double InnerProduct(params double[] terms) {
        double sum = 0;

        for (int i = 0; i < terms.Length - 1; i++) {
            sum += terms[i] * terms[++i];
        }

        return sum;
    }
    public static void Swap(ref double d, ref double d1) {
        (d1, d) = (d, d1);
    }
    public static double Lerp(double x, double a, double b) {
        return (1 - x) * a + x * b;
    }
    public static double ConvertToRadians(double deg) {
        return (System.Math.PI / 180) * deg;
    }
    public static double ConvertToDegrees(double rad) {
        return (180 / System.Math.PI) * rad;
    }
}
