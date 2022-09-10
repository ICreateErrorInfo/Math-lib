using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.Intrinsics;
using static Moarx.Math.Tests.Vector3DTests;

namespace Moarx.Math.Tests;

[TestFixture]
public class Vector3DTests {

    public record VectorTestData<T>(T X, T Y, T Z, T Expected);

    static IEnumerable<VectorTestData<double>> GetLengthTestData() {
        yield return new(X: 1, Y: 2, Z: 2, Expected: 3);
        yield return new(X: 2, Y: 3, Z: 6, Expected: 7);
        yield return new(X: 4, Y: 13, Z: 16, Expected: 21);
    }

    [Test]
    [TestCaseSource(nameof(GetLengthTestData))]
    public void GetLengthTest(VectorTestData<double> td) {

        var v = new Vector3D<double>(td.X, td.Y, td.Z);

        Assert.That(v.GetLengthSquared(), Is.EqualTo(System.Math.Pow(td.Expected, 2)));
        Assert.That(v.GetLength(),        Is.EqualTo(td.Expected));
    }

    public record AbsTestData(Vector3D<double> Vector, Vector3D<double> Expect);

    static IEnumerable<AbsTestData> GetAbsTestData() {
        yield return new(Vector: new(X: 1, Y: 2, Z: 3),
                         Expect: new(X: 1, Y: 2, Z: 3));

        yield return new(Vector: new(X: -1, Y: 2, Z: 3),
                         Expect: new(X: 1, Y: 2, Z: 3));

        yield return new(Vector: new(X: 1, Y: -2, Z: 3),
                         Expect: new(X: 1, Y: 2, Z: 3));

        yield return new(Vector: new(X: 1, Y: 2, Z: -3),
                         Expect: new(X: 1, Y: 2, Z: 3));

        yield return new(Vector: new(X: -1, Y: 2, Z: -3),
                         Expect: new(X: 1, Y: 2, Z: 3));

        yield return new(Vector: new(X: -1, Y: -2, Z: -3),
                         Expect: new(X: 1, Y: 2, Z: 3));
    }

    [Test]
    [TestCaseSource(nameof(GetAbsTestData))]
    public void AbsTest(AbsTestData td) {
        Assert.That(td.Vector.Abs(), Is.EqualTo(td.Expect));
    }

    public record MaxDimesionTestData(double X, double Y, double Z, int Expected);

    static IEnumerable<MaxDimesionTestData> GetMaxDimensionTestData() {

        yield return new(X: 0, Y: 0, Z: 0, Expected: 2); // TODO hm Check!

        yield return new(X: 1, Y: 2, Z: 3, Expected: 2);
        yield return new(X: 1, Y: 3, Z: 2, Expected: 1);
        yield return new(X: 3, Y: 2, Z: 1, Expected: 0);

        yield return new(X: -3, Y: -2, Z: -1, Expected: 2);
        yield return new(X: -3, Y: -1, Z: -2, Expected: 1);
        yield return new(X: -1, Y: -2, Z: -3, Expected: 0);

    }

    [Test]
    [TestCaseSource(nameof(GetMaxDimensionTestData))]
    public void MaxDimensionTest(MaxDimesionTestData td) {
        var vector = new Vector3D<double>(td.X, td.Y, td.Z);
        Assert.That(vector.MaxDimension(), Is.EqualTo(td.Expected));
    }

    public record CrossProductTestData<T>(Vector3D<T> Vector1, Vector3D<T> Vector2, Vector3D<T> Expected) where T: INumber<T>;

    static IEnumerable<CrossProductTestData<double>> GetCrossProductTestData() {

        yield return new(Vector1:  new(1, 0, 0),
                         Vector2:  new(0, 1, 0),
                         Expected: new(0, 0, 1));

        yield return new(Vector1:  new(1,  2, 0),
                         Vector2:  new(0,  1, 3),
                         Expected: new(6, -3, 1));

        yield return new(Vector1: new(1.5, 2.5, 0),
                         Vector2: new(0, 1.5, 3),
                         Expected: new(7.5, -4.5, 2.25));

        yield return new(Vector1:  new(0, 0, 0),
                         Vector2:  new(0, 0, 0),
                         Expected: new(0, 0, 0));

        yield return new(Vector1: new(0, 1, 0),
                         Vector2: new(0, -1, 0),
                         Expected: new(0, 0, 0));

    }

    [Test]
    [TestCaseSource(nameof(GetCrossProductTestData))]
    public void CrossProductTest(CrossProductTestData<double> td) {
        var vector1 = td.Vector1;
        var vector2 = td.Vector2;

        Assert.That(vector1.CrossProduct(vector2), Is.EqualTo(td.Expected));
    }

    public record PermuteTestData<T>(Vector3D<T> Vector ,int x, int y, int z, Vector3D<T> Expected) where T: INumber<T>;

    static IEnumerable<PermuteTestData<double>> GetPermuteTestData() {

        yield return new(Vector: new(1, 2, 3),
                         x: 0, y: 1, z: 2,
                         Expected: new(1, 2, 3));

        yield return new(Vector: new(1, 2, 3),
                         x: 1, y: 0, z: 2,
                         Expected: new(2, 1, 3));

        yield return new(Vector: new(1, 2, 3),
                         x: 0, y: 2, z: 1,
                         Expected: new(1, 3, 2));

    }

    [Test]
    [TestCaseSource(nameof(GetPermuteTestData))]
    public void PermuteTest(PermuteTestData<double> td) {
        var vector = td.Vector;
        Assert.That(vector.Permute(td.x, td.y, td.z), Is.EqualTo(td.Expected));
    }

    public record ClampTestData<T>(Vector3D<T> Vector, T Min, T Max, Vector3D<T> Expected) where T : INumber<T>;

    static IEnumerable<ClampTestData<double>> GetClampTestData() {
        yield return new(Vector: new(2, 1, 1),
                         Min: 0, Max: 1,
                         Expected: new(1, 1, 1));
        yield return new(Vector: new(1, 2, 1),
                         Min: 0, Max: 1,
                         Expected: new(1, 1, 1));
        yield return new(Vector: new(1, 1, 2),
                         Min: 0, Max: 1,
                         Expected: new(1, 1, 1));

        yield return new(Vector: new(3, 1, 2),
                         Min: 0, Max: 2,
                         Expected: new(2, 1, 2));
        yield return new(Vector: new(2, 3, 1),
                         Min: 0, Max: 2,
                         Expected: new(2, 2, 1));
        yield return new(Vector: new(2, 1, 3),
                         Min: 0, Max: 2,
                         Expected: new(2, 1, 2));

        yield return new(Vector: new(-2, 1, 1),
                         Min: -1, Max: 1,
                         Expected: new(-1, 1, 1));
    }

    [Test]
    [TestCaseSource(nameof(GetClampTestData))]
    public void ClampTest(ClampTestData<double> td) {
        var vector = td.Vector;
        Assert.That(vector.Clamp(td.Min, td.Max), Is.EqualTo(td.Expected));
    }
}