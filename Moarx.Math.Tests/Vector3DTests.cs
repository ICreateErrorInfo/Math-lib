using NUnit.Framework;

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

}