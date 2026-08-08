using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class Line2DTests {

    [Test]
    public void TestCtor() {
        var l = new Line2D<double>(new Point2D<double>(1, 2), new Point2D<double>(3, 4));

        Assert.That(l.StartPoint, Is.EqualTo(new Point2D<double>(1, 2)));
        Assert.That(l.EndPoint, Is.EqualTo(new Point2D<double>(3, 4)));
    }
    [Test]
    public void TestCreate() {
        var l = Line2D.Create(new Point2D<double>(1, 2), new Point2D<double>(3, 4));

        Assert.That(l, Is.EqualTo(new Line2D<double>(new Point2D<double>(1, 2), new Point2D<double>(3, 4))));
    }
    [Test]
    public void TestGetBoundingBox() {
        var l = new Line2D<double>(new Point2D<double>(3, -1), new Point2D<double>(-2, 4));

        var bounds = l.GetBoundingBox();

        Assert.That(bounds.PMin, Is.EqualTo(new Point2D<double>(-2, -1)));
        Assert.That(bounds.PMax, Is.EqualTo(new Point2D<double>(3, 4)));
    }
    [Test]
    public void TestTransform() {
        var l = new Line2D<double>(new Point2D<double>(1, 2), new Point2D<double>(3, 4));

        var moved = l.Transform(new Vector2D<double>(10, -5));

        Assert.That(moved.StartPoint, Is.EqualTo(new Point2D<double>(11, -3)));
        Assert.That(moved.EndPoint, Is.EqualTo(new Point2D<double>(13, -1)));
    }
    [Test]
    public void TestFloatInstantiation() {
        var l = new Line2D<float>(new Point2D<float>(1f, 2f), new Point2D<float>(3f, 4f));

        var moved = l.Transform(new Vector2D<float>(10f, -5f));

        Assert.That(moved.StartPoint, Is.EqualTo(new Point2D<float>(11f, -3f)));
        Assert.That(moved.EndPoint, Is.EqualTo(new Point2D<float>(13f, -1f)));
    }
}
