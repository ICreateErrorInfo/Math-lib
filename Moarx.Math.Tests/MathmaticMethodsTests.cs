using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class MathmaticMethodsTests {

    [Test]
    public void TestSafeASin() {
        Assert.That(MathmaticMethods.SafeASin(0), Is.EqualTo(0).Within(1e-10));
        Assert.That(MathmaticMethods.SafeASin(1), Is.EqualTo(System.Math.PI / 2).Within(1e-10));
        Assert.That(MathmaticMethods.SafeASin(1.5), Is.EqualTo(System.Math.PI / 2).Within(1e-10));
        Assert.That(MathmaticMethods.SafeASin(-1.5), Is.EqualTo(-System.Math.PI / 2).Within(1e-10));
    }
    [Test]
    public void TestSafeACos() {
        Assert.That(MathmaticMethods.SafeACos(1), Is.EqualTo(0).Within(1e-10));
        Assert.That(MathmaticMethods.SafeACos(-1), Is.EqualTo(System.Math.PI).Within(1e-10));
        Assert.That(MathmaticMethods.SafeACos(1.5), Is.EqualTo(0).Within(1e-10));
        Assert.That(MathmaticMethods.SafeACos(-1.5), Is.EqualTo(System.Math.PI).Within(1e-10));
    }
    [Test]
    public void TestSafeSqrt() {
        Assert.That(MathmaticMethods.SafeSqrt(4), Is.EqualTo(2));
        Assert.That(MathmaticMethods.SafeSqrt(0), Is.EqualTo(0));
        Assert.That(MathmaticMethods.SafeSqrt(-4), Is.EqualTo(0));
    }
    [Test]
    public void TestDifferenceOfProducts() {
        // a*b - c*d
        Assert.That(MathmaticMethods.DifferenceOfProducts(3, 4, 1, 2), Is.EqualTo((3 * 4) - (1 * 2)).Within(1e-10));
        Assert.That(MathmaticMethods.DifferenceOfProducts(0, 0, 0, 0), Is.EqualTo(0));
    }
    [Test]
    public void TestFMA() {
        Assert.That(MathmaticMethods.FMA(2, 3, 4), Is.EqualTo((2 * 3) + 4));
        Assert.That(MathmaticMethods.FMA(0, 5, 7), Is.EqualTo(7));
    }
    [Test]
    public void TestInnerProduct() {
        // a*b + (terms[0]*terms[1] + terms[2]*terms[3] + ...)
        double result = MathmaticMethods.InnerProduct(1, 2, 3, 4, 5, 6);
        double expected = (1 * 2) + (3 * 4) + (5 * 6);
        Assert.That(result, Is.EqualTo(expected));
    }
    [Test]
    public void TestSwap() {
        double a = 1, b = 2;
        MathmaticMethods.Swap(ref a, ref b);
        Assert.That(a, Is.EqualTo(2));
        Assert.That(b, Is.EqualTo(1));
    }
    [Test]
    public void TestLerp() {
        Assert.That(MathmaticMethods.Lerp(0, 10, 20), Is.EqualTo(10));
        Assert.That(MathmaticMethods.Lerp(1, 10, 20), Is.EqualTo(20));
        Assert.That(MathmaticMethods.Lerp(0.5, 10, 20), Is.EqualTo(15));
    }
    [Test]
    public void TestConvertToRadians() {
        Assert.That(MathmaticMethods.ConvertToRadians(180), Is.EqualTo(System.Math.PI).Within(1e-10));
        Assert.That(MathmaticMethods.ConvertToRadians(0), Is.EqualTo(0));
    }
    [Test]
    public void TestConvertToDegrees() {
        Assert.That(MathmaticMethods.ConvertToDegrees(System.Math.PI), Is.EqualTo(180).Within(1e-10));
        Assert.That(MathmaticMethods.ConvertToDegrees(0), Is.EqualTo(0));
    }
    [Test]
    public void TestFindInterval() {
        // pred(i) true while i < 3, size 6 => sorted boundary between index 2 and 3
        bool Pred(int i) => i < 3;

        int result = MathmaticMethods.FindInterval(6, Pred);

        Assert.That(result, Is.EqualTo(2));
    }
    [Test]
    public void TestFindIntervalClampsToLowerBound() {
        bool Pred(int i) => false;

        int result = MathmaticMethods.FindInterval(6, Pred);

        Assert.That(result, Is.EqualTo(0));
    }
    [Test]
    public void TestFindIntervalClampsToUpperBound() {
        bool Pred(int i) => true;

        int result = MathmaticMethods.FindInterval(6, Pred);

        Assert.That(result, Is.EqualTo(4));
    }
    [Test]
    public void TestSolveQuadratic() {
        // x^2 - 3x + 2 = 0 -> roots 1 and 2
        bool solved = MathmaticMethods.SolveQuadratic(1, -3, 2, out double t0, out double t1);

        Assert.That(solved, Is.True);
        Assert.That(t0, Is.EqualTo(1).Within(1e-10));
        Assert.That(t1, Is.EqualTo(2).Within(1e-10));
    }
    [Test]
    public void TestSolveQuadraticNoRealRoots() {
        // x^2 + 1 = 0 -> no real roots
        bool solved = MathmaticMethods.SolveQuadratic(1, 0, 1, out double t0, out double t1);

        Assert.That(solved, Is.False);
        Assert.That(t0, Is.EqualTo(0));
        Assert.That(t1, Is.EqualTo(0));
    }
    [Test]
    public void TestSolveQuadraticWithRangeInRange() {
        // x^2 - 3x + 2 = 0 -> roots 1 and 2, tMin/tMax include the smaller root
        bool solved = MathmaticMethods.SolveQuadratic(1, -3, 2, out double t0, out double t1, 0, 5);

        Assert.That(solved, Is.True);
        Assert.That(t0, Is.EqualTo(1).Within(1e-10));
    }
    [Test]
    public void TestSolveQuadraticWithRangeFallsBackToSecondRoot() {
        // roots 1 and 2, tMin/tMax excludes the smaller root but includes the larger
        bool solved = MathmaticMethods.SolveQuadratic(1, -3, 2, out double t0, out double t1, 1.5, 5);

        Assert.That(solved, Is.True);
        Assert.That(t0, Is.EqualTo(2).Within(1e-10));
    }
    [Test]
    public void TestSolveQuadraticWithRangeOutOfRange() {
        // roots 1 and 2, tMin/tMax excludes both
        bool solved = MathmaticMethods.SolveQuadratic(1, -3, 2, out double t0, out double t1, 3, 5);

        Assert.That(solved, Is.False);
    }
    [Test]
    public void TestSolveQuadraticHalfB() {
        // x^2 - 3x + 2 = 0, halfB = -3/2 = -1.5, roots 1 and 2
        bool solved = MathmaticMethods.SolveQuadratic(1, -1.5, 2, out double t0, 0, 5);

        Assert.That(solved, Is.True);
        Assert.That(t0, Is.EqualTo(1).Within(1e-10));
    }
    [Test]
    public void TestSolveQuadraticHalfBOutOfRange() {
        bool solved = MathmaticMethods.SolveQuadratic(1, -1.5, 2, out double t0, 3, 5);

        Assert.That(solved, Is.False);
    }
    [Test]
    public void TestSolveQuadraticHalfBNoRealRoots() {
        // x^2 + 1 = 0 -> halfB = 0, discriminant = 0 - 1*1 = -1 < 0
        bool solved = MathmaticMethods.SolveQuadratic(1, 0, 1, out double t0, 0, 5);

        Assert.That(solved, Is.False);
        Assert.That(t0, Is.EqualTo(0));
    }
    [Test]
    public void TestSolveQuadraticHalfBFallsBackToSecondRoot() {
        // x^2 - 3x + 2 = 0, halfB = -1.5, roots 1 and 2; tMin/tMax excludes 1 but includes 2
        bool solved = MathmaticMethods.SolveQuadratic(1, -1.5, 2, out double t0, 1.5, 5);

        Assert.That(solved, Is.True);
        Assert.That(t0, Is.EqualTo(2).Within(1e-10));
    }
    [Test]
    public void TestPartitionSplitsByPredicate() {
        var list = new List<int> { 5, 1, 4, 2, 3 };

        var result = MathmaticMethods.Partition(list, 0, list.Count, x => x < 3, out int mid);

        Assert.That(result.Take(mid), Has.All.LessThan(3));
        Assert.That(result.Skip(mid).Take(list.Count - mid), Has.All.GreaterThanOrEqualTo(3));
    }
    [Test]
    public void TestPartitionPreservesPrefixAndTailForSubRange() {
        // prefix: indices 0,1 untouched; partitioned range: indices 2..5 (half-open); tail: indices 6,7,8 untouched
        var list = new List<int> { 100, 101, 9, 4, 7, 2, 200, 201, 202 };

        var result = MathmaticMethods.Partition(list, 2, 6, x => x % 2 == 0, out int mid);

        Assert.That(result, Has.Count.EqualTo(list.Count));
        Assert.That(result.Take(2), Is.EqualTo(new[] { 100, 101 }));
        Assert.That(result.Skip(2).Take(mid - 2), Has.All.Matches<int>(x => x % 2 == 0));
        Assert.That(result.Skip(mid).Take(6 - mid), Has.All.Matches<int>(x => x % 2 != 0));
        Assert.That(result.Skip(6), Is.EqualTo(new[] { 200, 201, 202 }));
    }
    [Test]
    public void TestParallelFor2DCoversEntireExtentExactlyOnce() {
        var extent = new Bounds2D<int>(new Point2D<int>(0, 0), new Point2D<int>(37, 23));
        var visited = new List<(int X, int Y)>();
        var gate = new object();

        MathmaticMethods.ParallelFor2D(extent, tile => {
            lock (gate) {
                for (int x = tile.PMin.X; x < tile.PMax.X; x++) {
                    for (int y = tile.PMin.Y; y < tile.PMax.Y; y++) {
                        visited.Add((x, y));
                    }
                }
            }
        });

        Assert.That(visited, Has.Count.EqualTo(37 * 23));
        Assert.That(visited.Distinct().Count(), Is.EqualTo(37 * 23));
    }
    [Test]
    public void TestParallelFor2DWithNonZeroOriginCoversEntireExtent() {
        // regression: tile-count was computed from extent.PMax directly instead of extent.Diagonal(),
        // so extents not starting at (0,0) were only partially (or, for PMax at/near the origin, never) processed
        var extent = new Bounds2D<int>(new Point2D<int>(-5, -5), new Point2D<int>(5, 5));
        var visited = new List<(int X, int Y)>();
        var gate = new object();

        MathmaticMethods.ParallelFor2D(extent, tile => {
            lock (gate) {
                for (int x = tile.PMin.X; x < tile.PMax.X; x++) {
                    for (int y = tile.PMin.Y; y < tile.PMax.Y; y++) {
                        visited.Add((x, y));
                    }
                }
            }
        });

        Assert.That(visited, Has.Count.EqualTo(100));
        Assert.That(visited.Distinct().Count(), Is.EqualTo(100));
    }
    [Test]
    public void TestParallelFor2DWithEntirelyNegativeExtentIsNotSkipped() {
        // regression: with the old PMax-based tile count, an extent fully left of/above the origin
        // (PMax == (0,0)) computed sumTiles == 0, so func was never invoked at all
        var extent = new Bounds2D<int>(new Point2D<int>(-5, -5), new Point2D<int>(0, 0));
        var visited = new List<(int X, int Y)>();
        var gate = new object();

        MathmaticMethods.ParallelFor2D(extent, tile => {
            lock (gate) {
                for (int x = tile.PMin.X; x < tile.PMax.X; x++) {
                    for (int y = tile.PMin.Y; y < tile.PMax.Y; y++) {
                        visited.Add((x, y));
                    }
                }
            }
        });

        Assert.That(visited, Has.Count.EqualTo(25));
        Assert.That(visited.Distinct().Count(), Is.EqualTo(25));
    }
    [Test]
    public void TestParallelFor2DSinglePixelExtent() {
        var extent = new Bounds2D<int>(new Point2D<int>(0, 0), new Point2D<int>(1, 1));
        var callCount = 0;
        var gate = new object();

        MathmaticMethods.ParallelFor2D(extent, tile => {
            lock (gate) {
                callCount++;
            }
        });

        Assert.That(callCount, Is.EqualTo(1));
    }
    [Test]
    public void TestRandom1Tom1WithinRange() {
        for (int i = 0; i < 100; i++) {
            double value = MathmaticMethods.Random1Tom1();
            Assert.That(value, Is.InRange(-1, 1));
        }
    }
    [Test]
    public void TestGetRandomDoubleWithinRange() {
        for (int i = 0; i < 100; i++) {
            double value = MathmaticMethods.GetRandomDouble(2, 5);
            Assert.That(value, Is.InRange(2, 5));
        }
    }
    [Test]
    public void TestGetRandomIntWithinRange() {
        for (int i = 0; i < 100; i++) {
            int value = MathmaticMethods.GetRandomInt(2, 5);
            Assert.That(value, Is.InRange(2, 4));
        }
    }
    [Test]
    public void TestSampleUniformDiskConcentricCenter() {
        var result = MathmaticMethods.SampleUniformDiskConcentric(new Point2D<double>(0.5, 0.5));

        Assert.That(result, Is.EqualTo(new Point2D<double>(0, 0)));
    }
    [Test]
    public void TestSampleUniformDiskConcentricStaysWithinUnitDisk() {
        var result = MathmaticMethods.SampleUniformDiskConcentric(new Point2D<double>(0.9, 0.2));

        Assert.That((result.X * result.X) + (result.Y * result.Y), Is.LessThanOrEqualTo(1.0001));
    }
    [Test]
    public void TestSampleUniformDiskConcentricElseBranch() {
        // |uOffset.Y| > |uOffset.X| -> takes the "else" branch (r = uOffset.Y)
        var result = MathmaticMethods.SampleUniformDiskConcentric(new Point2D<double>(0.2, 0.9));

        double radius = System.Math.Sqrt((result.X * result.X) + (result.Y * result.Y));
        Assert.That(radius, Is.EqualTo(0.8).Within(1e-10));
    }
}
