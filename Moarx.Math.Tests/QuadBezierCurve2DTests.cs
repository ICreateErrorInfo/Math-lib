using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class QuadBezierCurve2DTests {

    [Test]
    public void TestCtor() {
        var c = new QuadBezierCurve2D<double>(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 0));

        Assert.That(c.StartPoint, Is.EqualTo(new Point2D<double>(0, 0)));
        Assert.That(c.Point1, Is.EqualTo(new Point2D<double>(1, 1)));
        Assert.That(c.EndPoint, Is.EqualTo(new Point2D<double>(2, 0)));
    }
    [Test]
    public void TestCreate() {
        var c = QuadBezierCurve2D.Create(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 0));

        Assert.That(c, Is.EqualTo(new QuadBezierCurve2D<double>(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 0))));
    }
    [Test]
    public void TestIndexer() {
        var c = new QuadBezierCurve2D<double>(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 0));

        Assert.That(c[0], Is.EqualTo(c.StartPoint));
        Assert.That(c[1], Is.EqualTo(c.Point1));
        Assert.That(c[2], Is.EqualTo(c.EndPoint));
    }
    [Test]
    public void TestIndexerThrowsOnOutOfRange() {
        var c = new QuadBezierCurve2D<double>(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 0));

        Assert.Throws<IndexOutOfRangeException>(() => _ = c[3]);
        Assert.Throws<IndexOutOfRangeException>(() => _ = c[-1]);
    }
    [Test]
    public void TestFloatInstantiation() {
        var c = new QuadBezierCurve2D<float>(
            new Point2D<float>(0f, 0f),
            new Point2D<float>(1f, 1f),
            new Point2D<float>(2f, 0f));

        Assert.That(c[0], Is.EqualTo(c.StartPoint));
        Assert.That(c[2], Is.EqualTo(c.EndPoint));
    }
}
