using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class CubicBezierCurve2DTests {

    [Test]
    public void TestCtor() {
        var c = new CubicBezierCurve2D<double>(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 1),
            new Point2D<double>(3, 0));

        Assert.That(c.StartPoint, Is.EqualTo(new Point2D<double>(0, 0)));
        Assert.That(c.Point1, Is.EqualTo(new Point2D<double>(1, 1)));
        Assert.That(c.Point2, Is.EqualTo(new Point2D<double>(2, 1)));
        Assert.That(c.EndPoint, Is.EqualTo(new Point2D<double>(3, 0)));
    }
    [Test]
    public void TestCreate() {
        var c = CubicBezierCurve2D.Create(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 1),
            new Point2D<double>(3, 0));

        Assert.That(c, Is.EqualTo(new CubicBezierCurve2D<double>(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 1),
            new Point2D<double>(3, 0))));
    }
    [Test]
    public void TestIndexer() {
        var c = new CubicBezierCurve2D<double>(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 1),
            new Point2D<double>(3, 0));

        Assert.That(c[0], Is.EqualTo(c.StartPoint));
        Assert.That(c[1], Is.EqualTo(c.Point1));
        Assert.That(c[2], Is.EqualTo(c.Point2));
        Assert.That(c[3], Is.EqualTo(c.EndPoint));
    }
    [Test]
    public void TestIndexerThrowsOnOutOfRange() {
        var c = new CubicBezierCurve2D<double>(
            new Point2D<double>(0, 0),
            new Point2D<double>(1, 1),
            new Point2D<double>(2, 1),
            new Point2D<double>(3, 0));

        Assert.Throws<IndexOutOfRangeException>(() => _ = c[4]);
        Assert.Throws<IndexOutOfRangeException>(() => _ = c[-1]);
    }
}
