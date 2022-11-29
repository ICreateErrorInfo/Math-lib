using NUnit.Framework;

namespace Moarx.Math.Tests; 

[TestFixture]
public class Point3DTests {

    [Test]
    public void TestCtor() {

        var p = new Point3D<double>();
        Assert.That(p.X, Is.Zero);
        Assert.That(p.Y, Is.Zero);
        Assert.That(p.Z, Is.EqualTo(0));
    }

    [Test]
    public void TestCtorArgs() {

        var p = new Point3D<double>(1, 2, 3);

        Assert.That(p.X, Is.EqualTo(1));
        Assert.That(p.Y, Is.EqualTo(2));
        Assert.That(p.Z, Is.EqualTo(3));
    }


    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.AdditionData))]
    public void AdditionTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point3D<double> point1 =  new Point3D<double>(firstPoint[0], firstPoint[1], firstPoint[2]);
        Point3D<double> point2 =  new Point3D<double>(secondPoint[0], secondPoint[1], secondPoint[2]);
        Vector3D<double> vector1 =  new Vector3D<double>(secondPoint[0], secondPoint[1], secondPoint[2]);

        Point3D<double> expectedPoint = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedPoint, Is.EqualTo(point1 + point2));
        Assert.That(expectedPoint, Is.EqualTo(point1 + vector1));
    }

    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.SubtractionData))]
    public void SubtractionTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point3D<double> point1 =  new Point3D<double>(firstPoint[0], firstPoint[1], firstPoint[2]);
        Point3D<double> point2 =  new Point3D<double>(secondPoint[0], secondPoint[1], secondPoint[2]);
        Vector3D<double> vector1 =  new Vector3D<double>(secondPoint[0], secondPoint[1], secondPoint[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);
        Point3D<double> expectedPoint = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(point1 - point2));
        Assert.That(expectedPoint, Is.EqualTo(point1 - vector1));
    }

    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.DivisionData))]
    public void DivisionTests(double[] expected, double[] firstPoint, double[] scalar) {
        Point3D<double> point1 =  new Point3D<double>(firstPoint[0], firstPoint[1], firstPoint[2]);

        Point3D<double> expectedPoint = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedPoint, Is.EqualTo(point1 / scalar[0]));
    }

    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.NegationData))]
    public void NegationTests(double[] expected, double[] point) {
        Point3D<double> point1 =  new Point3D<double>(point[0], point[1], point[2]);

        Point3D<double> expectedPoint = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedPoint, Is.EqualTo(-point1));
    }

    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.MultiplicationData))]
    public void MultiplicationTests(double[] expected, double[] vector, double[] scalar) {
        Point3D<double> vector1 =  new Point3D<double>(vector[0], vector[1], vector[2]);

        Point3D<double> expectedVector = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 * scalar[0]));
        Assert.That(expectedVector, Is.EqualTo(scalar[0] * vector1));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.AccessOperatorData))]
    public void AccessOperatorTests(double[] expected, double[] vector, double[] access) {
        Point3D<double> vector1 =  new Point3D<double>(vector[0], vector[1], vector[2]);

        Assert.That(expected[0], Is.EqualTo(vector1[(int)access[0]]));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.PointMultiplicationData))]
    public void PointMultiplicationTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point3D<double> point1 =  new Point3D<double>(firstPoint[0], firstPoint[1], firstPoint[2]);
        Point3D<double> point2 =  new Point3D<double>(secondPoint[0], secondPoint[1], secondPoint[2]);

        Point3D<double> expectedPoint = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedPoint, Is.EqualTo(point1 * point2));
    }


    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.MinimumData))]
    public void MinimumTests(double[] expected, double[] firstVector, double[] secondVector) {
        Point3D<double> vector1 =  new Point3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Point3D<double> vector2 =  new Point3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Point3D<double> expectedVector = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(Point3D<double>.Min(vector1, vector2)));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.MaximumData))]
    public void MaximumTests(double[] expected, double[] firstVector, double[] secondVector) {
        Point3D<double> vector1 =  new Point3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Point3D<double> vector2 =  new Point3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Point3D<double> expectedVector = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(Point3D<double>.Max(vector1, vector2)));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.CastData))]
    public void VectorCastTests(double[] expected, double[] firstVector) {
        Point3D<double> vector1 =  new Point3D<double>(firstVector[0], firstVector[1], firstVector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1.ToVector()));
    }
}