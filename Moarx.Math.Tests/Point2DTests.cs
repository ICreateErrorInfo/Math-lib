﻿using NUnit.Framework;

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
    }

    [Test]
    public void TestCtorNaN() {

        Assert.Throws<ArgumentOutOfRangeException>(() => new Point2D<double>(double.NaN, 2),          "Guckguck");
        Assert.Throws<ArgumentOutOfRangeException>(() => new Point2D<double>(1,          double.NaN), "Guckguck");
        Assert.Throws<ArgumentOutOfRangeException>(() => new Point2D<double>(double.NaN, double.NaN), "Guckguck");
        
        var p = Point2D<double>.Empty;

        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p with { X = double.NaN }, "Guckguck");
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = p with { Y = double.NaN }, "Guckguck");

    }

    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.AdditionData))]
    public void AdditionTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);
        Point2D<double> point2 =  new Point2D<double>(secondPoint[0], secondPoint[1]);
        Vector2D<double> vector1 =  new Vector2D<double>(secondPoint[0], secondPoint[1]);

        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(point1 + point2));
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
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.PointMultiplicationData))]
    public void PointMultiplicationTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);
        Point2D<double> point2 =  new Point2D<double>(secondPoint[0], secondPoint[1]);

        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(point1 * point2));
    }


    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.MinimumData))]
    public void MinimumTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);
        Point2D<double> point2 =  new Point2D<double>(secondPoint[0], secondPoint[1]);

        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(Point2D<double>.Minimum(point1, point2)));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.MaximumData))]
    public void MaximumTests(double[] expected, double[] firstPoint, double[] secondPoint) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);
        Point2D<double> point2 =  new Point2D<double>(secondPoint[0], secondPoint[1]);

        Point2D<double> expectedPoint = new Point2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(Point2D<double>.Maximum(point1, point2)));
    }
    [TestCaseSource(typeof(BaseTestData2D), nameof(BaseTestData2D.CastData))]
    public void VectorCastTests(double[] expected, double[] firstPoint) {
        Point2D<double> point1 =  new Point2D<double>(firstPoint[0], firstPoint[1]);

        Vector2D<double> expectedPoint = new Vector2D<double>(expected[0], expected[1]);

        Assert.That(expectedPoint, Is.EqualTo(point1.ToVector()));
    }
}

