namespace Moarx.Math;
public class MathmaticMethods {
    [ThreadStatic]
    private static Random _random;

    public static Random Random {
        get {
            if (_random == null) {
                _random = new System.Random();
            }
            return _random;
        }
    }
    public static double Random1Tom1() {
        return GetRandomDouble(-1, 1);
    }
    public static double GetRandomDouble(double minimum, double maximum) {
        return Random.NextDouble() * (maximum - minimum) + minimum;
    }
    public static int GetRandomInt(int minimum, int maximum) {
        return Random.Next(minimum, maximum);
    }

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
    public static int FindInterval(int size, Predicate<int> pred) {
        int first = 1;
        int ssize = size - 2;

        while(ssize > 0) {
            int half = ssize >> 1, middle = first + half;
            bool predResult = pred(middle);
            first = predResult ? middle + 1 : first;
            ssize = predResult ? ssize - (half + 1) : half;
        }

        return System.Math.Clamp(first - 1, 0, size - 2);
    } 
    public static List<T> Partition<T>(List<T> primitiveInfos, int start, int end, Func<T, bool> predicate, out int mid) {
        List<T> elementsToSort = new List<T>();
        for (int i = start; i < end; i++) {
            elementsToSort.Add(primitiveInfos[i]);
        }

        IEnumerable<IGrouping<bool, T>> sortedElements = elementsToSort.GroupBy(predicate);

        List<T> newPrimitiveInfos = new List<T>();
        List<T> primitevesTrue = new List<T>();
        List<T> primitevesfalse = new List<T>();
        mid = start;
        foreach (var group in sortedElements) {
            foreach (var info in group) {
                if (group.Key == true) {
                    mid++;
                    primitevesTrue.Add(info);
                }
                if (group.Key == false) {
                    primitevesfalse.Add(info);
                }
            }
        }
        for (int i = 0; i < start; i++) {
            newPrimitiveInfos.Add(primitiveInfos[i]);
        }
        foreach (var info in primitevesTrue) {
            newPrimitiveInfos.Add(info);
        }
        foreach (var info in primitevesfalse) {
            newPrimitiveInfos.Add(info);
        }
        for (int i = primitiveInfos.Count - 1; i > end; i--) {
            newPrimitiveInfos.Add(primitiveInfos[i]);
        }

        return newPrimitiveInfos;
    }
    public static bool SolveQuadratic(double a, double b, double c, out double t0, out double t1) {
        t0 = 0;
        t1 = 0;

        var discriminant = (b * b) - (4 * a * c);

        if (discriminant < 0) {
            return false;
        }

        var sqrtd = System.Math.Sqrt(discriminant);

        t0 = (-b - sqrtd) / (2 * a);
        t1 = (-b + sqrtd) / (2 * a);

        return true;
    }
    public static bool SolveQuadratic(double a, double halfB, double c, out double t0, double tMin, double tMax) {
        t0 = 0;

        var discriminant = halfB * halfB - a * c;

        if (discriminant < 0) {
            return false;
        }
        var sqrtd = System.Math.Sqrt(discriminant);

        t0 = (-halfB - sqrtd) / a;
        if (t0 < tMin || tMax < t0) {
            t0 = (-halfB + sqrtd) / a;
            if (t0 < tMin || tMax < t0) {
                return false;
            }
        }
        //makes sure there is no self intersection can be fixed with rounding error

        return true;
    }
    public static bool SolveQuadratic(double a, double b, double c, out double t0, out double t1, double tMin, double tMax) {
        t0 = 0;
        t1 = 0;

        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0) {
            return false;
        }
        var sqrtd = System.Math.Sqrt(discriminant);

        t0 = (-b - sqrtd) / (2 * a);
        if (t0 < tMin || tMax < t0) {
            t0 = (-b + sqrtd) / (2 * a);
            if (t0 < tMin || tMax < t0) {
                return false;
            }
        }
        //makes sure there is no self intersection can be fixed with rounding error

        return true;
    }
}
