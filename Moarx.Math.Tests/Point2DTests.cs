using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class Point2DTests {

    [Test]
    public void TestCtor() {

        var p = new Point2D<double>();
        Assert.That(p.X, Is.Zero);
        Assert.That(p.Y, Is.Zero);
    }
    [Test]
    public void TestCtorArgs() {

        var p = new Point2D<double>(1, 2);

        Assert.That(p.X, Is.EqualTo(1));
        Assert.That(p.Y, Is.EqualTo(2));

        var p1 = new Point2D<double>(0);

        Assert.That(p1.X, Is.EqualTo(0));
        Assert.That(p1.Y, Is.EqualTo(0));
    }
    [Test]
    public void TestCtorNaN() {

        Assert.Throws<ArgumentOutOfRangeException>(() => new Point2D<double>(double.NaN, 2),          "X is NaN");
        Assert.Throws<ArgumentOutOfRangeException>(() => new Point2D<double>(1,          double.NaN), "Y is NaN");
        Assert.Throws<ArgumentOutOfRangeException>(() => new Point2D<double>(double.NaN, double.NaN), "Data is NaN");
        
        var p = Point2D<double>.Empty;

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p with { X = double.NaN }, "X is NaN");
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p with { Y = double.NaN }, "Y is NaN");

    }
    [Test]
    public void TestOperatorException() {
        var p = new Point2D<double>(1, -4);

        Assert.Throws<DivideByZeroException>(() => _ = p / 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p * double.NaN);
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = double.NaN * p);
    }


    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.AdditionData))]
    public void AdditionTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);
        Vector2D<double> vector1 =  new Vector2D<double>(secondPoint[0], secondPoint[1]);

        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(point1 + vector1));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.SubtractionData))]
    public void SubtractionTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);
        Point2D<double> point2 =  new Point2D<double>(secondPoint[0], secondPoint[1]);
        Vector2D<double> vector1 =  new Vector2D<double>(secondPoint[0], secondPoint[1]);

        Vector2D<double> expectedVector = new Vector2D<double>(expected[0], expected[1]);
        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedVector, Is.EqualTo(point1 - point2));
        Assert.That(expectedPoint, Is.EqualTo(point1 - vector1));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.DivisionData))]
    public void DivisionTests(double[] expected, double[] firstPoint, double[] scalar) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);

        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(point1 / scalar[0]));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.NegationData))]
    public void NegationTests(double[] expected, double[] point) {
        Point2D<double> point1 =  new Point2D<double>(point[0], point[1]);

        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(-point1));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.MultiplicationData))]
    public void MultiplicationTests(double[] expected, double[] point, double[] scalar) {
        Point2D<double> point1 =  new Point2D<double>(point[0], point[1]);

        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(point1 * scalar[0]));
        Assert.That(expectedPoint, Is.EqualTo(scalar[0] * point1));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.AccessOperatorData))]
    public void AccessOperatorTests(double[] expected, double[] vector, double[] access) {
        Point2D<double> point1 =  new Point2D<double>(vector[0], vector[1]);

        Assert.That(expected[0], Is.EqualTo(point1[(int)access[0]]));
    }


    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.CastData))]
    public void VectorCastTests(double[] expected, double[] firstPoint) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);

        Vector2D<double> expectedPoint = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(point1.ToVector()));
    }
    [Test]
    public void TestMin() {
        var p1 = new Point2D<double>(1, 5);
        var p2 = new Point2D<double>(3, 2);

        Assert.That(Point2D<double>.Min(p1, p2), Is.EqualTo(new Point2D<double>(1, 2)));
    }
    [Test]
    public void TestMax() {
        var p1 = new Point2D<double>(1, 5);
        var p2 = new Point2D<double>(3, 2);

        Assert.That(Point2D<double>.Max(p1, p2), Is.EqualTo(new Point2D<double>(3, 5)));
    }
    [Test]
    public void TestExplicitCastToInt() {
        var p = new Point2D<double>(1.7, 2.2);

        var casted = (Point2D<int>)p;

        Assert.That(casted.X, Is.EqualTo(2));
        Assert.That(casted.Y, Is.EqualTo(2));
    }
    [Test]
    public void TestExplicitCastToDouble() {
        var p = new Point2D<int>(1, 2);

        var casted = (Point2D<double>)p;

        Assert.That(casted.X, Is.EqualTo(1));
        Assert.That(casted.Y, Is.EqualTo(2));
    }
    [Test]
    public void TestIndexerThrowsOnOutOfRange() {
        var p = new Point2D<double>(1, 2);

        Assert.Throws<IndexOutOfRangeException>(() => _ = p[2]);
        Assert.Throws<IndexOutOfRangeException>(() => _ = p[-1]);
    }
    [Test]
    public void TestFloatInstantiation() {
        var p = new Point2D<float>(1.5f, -2.5f);
        var v = new Vector2D<float>(1f, 1f);

        Assert.That(p + v, Is.EqualTo(new Point2D<float>(2.5f, -1.5f)));
        Assert.That(p * 2f, Is.EqualTo(new Point2D<float>(3f, -5f)));
        Assert.That(Point2D<float>.Min(p, new Point2D<float>(0f, 0f)), Is.EqualTo(new Point2D<float>(0f, -2.5f)));

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Point2D<float>(float.NaN, 2f));
    }
}

