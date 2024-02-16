namespace Moarx.Math;
public class MathmaticMethods {
    [ThreadStatic]
    private static Random _random;

    const double PiOver2 = 1.57079632679489661923;
    const double PiOver4 = 0.78539816339744830961;

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

    public static Point2D<double> SampleUniformDiskConcentric(Point2D<double> u) {
        Point2D<double> uOffset = 2 * u - new Vector2D<double>(1, 1);
        if (uOffset.X == 0 && uOffset.Y == 0)
            return new Point2D<double>(0, 0);

        double theta, r;
        if (System.Math.Abs(uOffset.X) > System.Math.Abs(uOffset.Y)) {
            r = uOffset.X;
            theta = PiOver4 * (uOffset.Y / uOffset.X);
        } else {
            r = uOffset.Y;
            theta = PiOver2 - PiOver4 * (uOffset.X / uOffset.Y);
        }
        return r * new Point2D<double>(System.Math.Cos(theta), System.Math.Sin(theta));
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

    public static void ParallelFor2D(Bounds2D<int> extent, Action<Bounds2D<int>> func) {

        int tileSize = System.Math.Clamp((int)(System.Math.Sqrt(extent.Diagonal().X * extent.Diagonal().Y /
                                       (8 * Environment.ProcessorCount))),
                                        1, 32);

        Point2D<int> nextStart = extent.PMin;

        int sumTiles = (int)(System.Math.Ceiling((double)extent.PMax.X / tileSize) * System.Math.Ceiling((double)extent.PMax.Y / tileSize));

        Bounds2D<int>[] tiles = new Bounds2D<int>[sumTiles];

        for(int i = 0; i < sumTiles; i++) {
            Point2D<int> end = nextStart + new Vector2D<int>(tileSize, tileSize);
            Bounds2D<int> b = Bounds2D<int>.Intersect(new Bounds2D<int>(nextStart, end), extent);

            nextStart = new(nextStart.X + tileSize, nextStart.Y);
            if (nextStart.X >= extent.PMax.X) {
                nextStart = new(extent.PMin.X, nextStart.Y + tileSize);
            }

            tiles[i] = b;
        }

        Parallel.For(0, sumTiles, (i) => {
            func(tiles[i]);
        });
    }
}
