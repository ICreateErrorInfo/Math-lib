using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class Vector2DTests {

    [Test]
    public void TestCtor() {

        var p = new Vector2D<double>();
        Assert.That(p.X, Is.Zero);
        Assert.That(p.Y, Is.Zero);
    }
    [Test]
    public void TestCtorArgs() {

        var p = new Vector2D<double>(1, 2);

        Assert.That(p.X, Is.EqualTo(1));
        Assert.That(p.Y, Is.EqualTo(2));
    }

    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.AdditionData))]
    public void AdditionTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector2D<double> vector1 =  new Vector2D<double>(firstVector[0], firstVector[1]);
        Vector2D<double> vector2 =  new Vector2D<double>(secondVector[0], secondVector[1]);

        Vector2D<double> expectedVector = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(vector1 + vector2));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.SubtractionData))]
    public void SubtractionTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector2D<double> vector1 =  new Vector2D<double>(firstVector[0], firstVector[1]);
        Vector2D<double> vector2 =  new Vector2D<double>(secondVector[0], secondVector[1]);

        Vector2D<double> expectedVector = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(vector1 - vector2));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.DivisionData))]
    public void DivisionTests(double[] expected, double[] vector, double[] scalar) {
        Vector2D<double> vector1 =  new Vector2D<double>(vector[0], vector[1]);

        Vector2D<double> expectedVector = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(vector1 / scalar[0]));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.NegationData))]
    public void NegationTests(double[] expected, double[] vector) {
        Vector2D<double> vector1 =  new Vector2D<double>(vector[0], vector[1]);

        Vector2D<double> expectedVector = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(-vector1));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.MultiplicationData))]
    public void MultiplicationTests(double[] expected, double[] vector, double[] scalar) {
        Vector2D<double> vector1 =  new Vector2D<double>(vector[0], vector[1]);

        Vector2D<double> expectedVector = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(vector1 * scalar[0]));
        Assert.That(expectedVector, Is.EqualTo(scalar[0] * vector1));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.AccessOperatorData))]
    public void AccessOperatorTests(double[] expected, double[] vector, double[] access) {
        Vector2D<double> vector1 =  new Vector2D<double>(vector[0], vector[1]);

        Assert.That(expected[0], Is.EqualTo(vector1[(int)access[0]]));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.DotProductData))]
    public void DotProductTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector2D<double> vector1 =  new Vector2D<double>(firstVector[0], firstVector[1]);
        Vector2D<double> vector2 = new Vector2D<double>(secondVector[0], secondVector[1]);

        Assert.That(expected[0], Is.EqualTo(vector1 * vector2));
    }


    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.MinimumData))]
    public void MinimumTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector2D<double> vector1 =  new Vector2D<double>(firstVector[0], firstVector[1]);
        Vector2D<double> vector2 =  new Vector2D<double>(secondVector[0], secondVector[1]);

        Vector2D<double> expectedVector = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(Vector2D<double>.Minimum(vector1, vector2)));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.MaximumData))]
    public void MaximumTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector2D<double> vector1 =  new Vector2D<double>(firstVector[0], firstVector[1]);
        Vector2D<double> vector2 =  new Vector2D<double>(secondVector[0], secondVector[1]);

        Vector2D<double> expectedVector = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(Vector2D<double>.Maximum(vector1, vector2)));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.CastData))]
    public void PointCastTests(double[] expected, double[] vector) {
        Vector2D<double> vector1 =  new Vector2D<double>(vector[0], vector[1]);

        Point2D<double> expectedVector = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(vector1.ToPoint()));
    }
}

