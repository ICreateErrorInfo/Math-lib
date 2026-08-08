using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class Triangle2DTests {

    [Test]
    public void TestCtor() {
        var t = new Triangle2D<double>(new Point2D<double>(0, 0), new Point2D<double>(1, 0), new Point2D<double>(0, 1));

        Assert.That(t.Point1, Is.EqualTo(new Point2D<double>(0, 0)));
        Assert.That(t.Point2, Is.EqualTo(new Point2D<double>(1, 0)));
        Assert.That(t.Point3, Is.EqualTo(new Point2D<double>(0, 1)));
    }
    [Test]
    public void TestCreate() {
        var t = Triangle2D.Create(new Point2D<double>(0, 0), new Point2D<double>(1, 0), new Point2D<double>(0, 1));

        Assert.That(t, Is.EqualTo(new Triangle2D<double>(new Point2D<double>(0, 0), new Point2D<double>(1, 0), new Point2D<double>(0, 1))));
    }
    [Test]
    public void TestGetBoundingBox() {
        var t = new Triangle2D<double>(new Point2D<double>(3, -1), new Point2D<double>(-2, 4), new Point2D<double>(1, 1));

        var bounds = t.GetBoundingBox();

        Assert.That(bounds.PMin, Is.EqualTo(new Point2D<double>(-2, -1)));
        Assert.That(bounds.PMax, Is.EqualTo(new Point2D<double>(3, 4)));
    }
    [Test]
    public void TestTransform() {
        var t = new Triangle2D<double>(new Point2D<double>(0, 0), new Point2D<double>(1, 0), new Point2D<double>(0, 1));

        var moved = t.Transform(new Vector2D<double>(10, -5));

        Assert.That(moved.Point1, Is.EqualTo(new Point2D<double>(10, -5)));
        Assert.That(moved.Point2, Is.EqualTo(new Point2D<double>(11, -5)));
        Assert.That(moved.Point3, Is.EqualTo(new Point2D<double>(10, -4)));
    }
}
