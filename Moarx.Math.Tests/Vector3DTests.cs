using NUnit.Framework;
using System.Numerics;

namespace Moarx.Math.Tests;

[TestFixture]
public class Vector3DTests {
    [Test]
    public void TestCtor() {

        var p = new Vector3D<double>();
        Assert.That(p.X, Is.Zero);
        Assert.That(p.Y, Is.Zero);
        Assert.That(p.Z, Is.EqualTo(0));
    }
    [Test]
    public void TestCtorArgs() {

        var p = new Vector3D<double>(1, 2, 3);

        Assert.That(p.X, Is.EqualTo(1));
        Assert.That(p.Y, Is.EqualTo(2));
        Assert.That(p.Z, Is.EqualTo(3));

        var v1 = new Vector3D<double>(1);

        Assert.That(v1.X, Is.EqualTo(1));
        Assert.That(v1.Y, Is.EqualTo(1));
        Assert.That(v1.Z, Is.EqualTo(1));
    }
    [Test]
    public void TestCtorNaN() {

        Assert.Throws<ArgumentOutOfRangeException>(() => new Vector3D<double>(double.NaN, 2, 3), "X is NaN");
        Assert.Throws<ArgumentOutOfRangeException>(() => new Vector3D<double>(1, double.NaN, 3), "Y is NaN");
        Assert.Throws<ArgumentOutOfRangeException>(() => new Vector3D<double>(1, 2, double.NaN), "Z is NaN");
        Assert.Throws<ArgumentOutOfRangeException>(() => new Vector3D<double>(double.NaN, double.NaN, double.NaN), "Data is NaN");

        var p = Vector3D<double>.Empty;

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p with { X = double.NaN }, "X is NaN");
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p with { Y = double.NaN }, "Y is NaN");
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p with { Z = double.NaN }, "Z is NaN");

    }
    [Test]
    public void TestOperatorException() {
        var p = new Vector3D<double>(1, -4, 3);

        Assert.Throws<DivideByZeroException>(() => _ = p / 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p * double.NaN);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = double.NaN * p);
    }
    [Test]
    public void TestGetLengthSquared() {

        var v = new Vector3D<double>(3, 4, 0);

        Assert.That(v.GetLengthSquared(), Is.EqualTo(25));

        var v1 = new Vector3D<double>(3, 4, 1);

        Assert.That(v1.GetLengthSquared(), Is.EqualTo(26));
    }
    [Test]
    public void TestCrossProduct() {

        var v = new Vector3D<double>(3, 4, 1);
        var v1 = new Vector3D<double>(1, -4, 2);

        Assert.That(Vector3D<double>.CrossProduct(v, v1), Is.EqualTo(new Vector3D<double>(12, -5, -16)));
    }


    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.AdditionData))]
    public void AdditionTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 =  new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 + vector2));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.SubtractionData))]
    public void SubtractionTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 =  new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 - vector2));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.DivisionData))]
    public void DivisionTests(double[] expected, double[] vector, double[] scalar) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 / scalar[0]));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.NegationData))]
    public void NegationTests(double[] expected, double[] vector) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(-vector1));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.MultiplicationData))]
    public void MultiplicationTests(double[] expected, double[] vector, double[] scalar) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 * scalar[0]));
        Assert.That(expectedVector, Is.EqualTo(scalar[0] * vector1));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.AccessOperatorData))]
    public void AccessOperatorTests(double[] expected, double[] vector, double[] access) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Assert.That(expected[0], Is.EqualTo(vector1[(int)access[0]]));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.DotProductData))]
    public void DotProductTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 = new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Assert.That(expected[0], Is.EqualTo(vector1 * vector2));
    }


    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.CastData))]
    public void PointCastTests(double[] expected, double[] vector) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Point3D<double> expectedVector = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1.ToPoint()));
    }
    [Test]
    public void TestIsNormalized() {
        var v = new Vector3D<double>(0, 1, 0);
        var v1 = new Vector3D<double>(3, 4, 0);

        Assert.That(v.IsNormalized(), Is.True);
        Assert.That(v1.IsNormalized(), Is.False);
    }
    [Test]
    public void TestPermute() {
        var p = new Vector3D<double>(10, 20, 30);

        Assert.That(Vector3D<double>.Permute(p, 2, 0, 1), Is.EqualTo(new Vector3D<double>(30, 10, 20)));
        Assert.That(Vector3D<double>.Permute(p, 0, 1, 2), Is.EqualTo(p));
    }
    [Test]
    public void TestMaxDimension() {
        Assert.That(Vector3D<double>.MaxDimension(new Vector3D<double>(5, 1, 3)), Is.EqualTo(0));
        Assert.That(Vector3D<double>.MaxDimension(new Vector3D<double>(1, 5, 3)), Is.EqualTo(1));
        Assert.That(Vector3D<double>.MaxDimension(new Vector3D<double>(1, 3, 5)), Is.EqualTo(2));
    }
    [Test]
    public void TestAbs() {
        var v = new Vector3D<double>(-1, 2, -3);

        Assert.That(Vector3D<double>.Abs(v), Is.EqualTo(new Vector3D<double>(1, 2, 3)));
    }
    [Test]
    public void TestRefractAtNormalIncidenceIsUnchanged() {
        // Straight-on incidence never bends, regardless of eta.
        var v = new Vector3D<double>(0, -1, 0);
        var n = new Vector3D<double>(0, 1, 0);

        var t = Vector3D<double>.Refract(v, n, 0.75);

        Assert.That(t.X, Is.EqualTo(0).Within(1e-10));
        Assert.That(t.Y, Is.EqualTo(-1).Within(1e-10));
        Assert.That(t.Z, Is.EqualTo(0).Within(1e-10));
    }
    [Test]
    public void TestRefractBendsTowardNormalForDenserMedium() {
        // 3-4-5 incidence angle (sini=0.8, cosi=0.6) against the surface normal.
        var v = new Vector3D<double>(0.8, -0.6, 0);
        var n = new Vector3D<double>(0, 1, 0);
        double eta = 0.5;

        var t = Vector3D<double>.Refract(v, n, eta);

        double cosi = 0.6;
        double cost2 = 1 - (eta * eta * (1 - (cosi * cosi)));
        double expectedX = eta * v.X;
        double expectedY = (eta * v.Y) + ((eta * cosi) - System.Math.Sqrt(cost2));

        Assert.That(t.X, Is.EqualTo(expectedX).Within(1e-10));
        Assert.That(t.Y, Is.EqualTo(expectedY).Within(1e-10));
        Assert.That(t.Z, Is.EqualTo(0).Within(1e-10));
        // The refracted direction must stay unit length, same as the incident direction.
        Assert.That(t.GetLengthSquared(), Is.EqualTo(1).Within(1e-10));
    }
    [Test]
    public void TestRefractTotalInternalReflectionReturnsZero() {
        // eta > 1 with a steep incidence angle pushes sin2Theta_t above 1 -> no real refraction.
        var v = new Vector3D<double>(0.8, -0.6, 0);
        var n = new Vector3D<double>(0, 1, 0);

        var t = Vector3D<double>.Refract(v, n, 2.0);

        Assert.That(t, Is.EqualTo(new Vector3D<double>(0, 0, 0)));
    }
    [Test]
    public void TestReflect() {
        var v = new Vector3D<double>(1, -1, 0);
        var n = new Vector3D<double>(0, 1, 0);

        Assert.That(Vector3D<double>.Reflect(v, n), Is.EqualTo(new Vector3D<double>(1, 1, 0)));
    }
    [Test]
    public void TestAngleBetweenParallelVectorsIsZero() {
        var v = new Vector3D<double>(1, 0, 0);

        Assert.That(Vector3D<double>.AngleBetween(v, v), Is.EqualTo(0).Within(1e-10));
    }
    [Test]
    public void TestAngleBetweenPerpendicularVectorsIsHalfPi() {
        var v1 = new Vector3D<double>(1, 0, 0);
        var v2 = new Vector3D<double>(0, 1, 0);

        Assert.That(Vector3D<double>.AngleBetween(v1, v2), Is.EqualTo(System.Math.PI / 2).Within(1e-10));
    }
    [Test]
    public void TestAngleBetweenOppositeVectorsIsPi() {
        var v1 = new Vector3D<double>(1, 0, 0);
        var v2 = new Vector3D<double>(-1, 0, 0);

        Assert.That(Vector3D<double>.AngleBetween(v1, v2), Is.EqualTo(System.Math.PI).Within(1e-10));
    }
    [Test]
    public void TestNearZero() {
        Assert.That(new Vector3D<double>(0, 0, 0).NearZero(), Is.True);
        Assert.That(new Vector3D<double>(1e-9, 1e-9, 1e-9).NearZero(), Is.True);
        Assert.That(new Vector3D<double>(0.1, 0, 0).NearZero(), Is.False);
    }
    [Test]
    public void TestRandomIsWithinRange() {
        for (int i = 0; i < 100; i++) {
            var v = Vector3D<double>.Random(-2, 3);

            Assert.That(v.X, Is.InRange(-2, 3));
            Assert.That(v.Y, Is.InRange(-2, 3));
            Assert.That(v.Z, Is.InRange(-2, 3));
        }
    }
    [Test]
    public void TestRandomInUnitSphereStaysWithinUnitSphere() {
        for (int i = 0; i < 100; i++) {
            var v = Vector3D<double>.RandomInUnitSphere();

            Assert.That(v.GetLengthSquared(), Is.LessThan(1));
        }
    }
    [Test]
    public void TestIndexerThrowsOnOutOfRange() {
        var v = new Vector3D<double>(1, 2, 3);

        Assert.Throws<IndexOutOfRangeException>(() => _ = v[3]);
        Assert.Throws<IndexOutOfRangeException>(() => _ = v[-1]);
    }
    [Test]
    public void TestFloatInstantiation() {
        var v1 = new Vector3D<float>(3f, 4f, 1f);
        var v2 = new Vector3D<float>(1f, -4f, 2f);

        Assert.That(Vector3D<float>.CrossProduct(v1, v2), Is.EqualTo(new Vector3D<float>(12f, -5f, -16f)));
        Assert.That(v1.GetLengthSquared(), Is.EqualTo(26f));
        Assert.That(Vector3D<float>.Abs(new Vector3D<float>(-1f, 2f, -3f)), Is.EqualTo(new Vector3D<float>(1f, 2f, 3f)));

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Vector3D<float>(float.NaN, 2f, 3f));
    }
}