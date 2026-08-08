using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class Ellipse2DTests {

    [Test]
    public void TestCtorWithSeparateStretch() {
        var e = new Ellipse2D<double>(new Point2D<double>(1, 2), 3, 4);

        Assert.That(e.MidPoint, Is.EqualTo(new Point2D<double>(1, 2)));
        Assert.That(e.HorizontalStretch, Is.EqualTo(3));
        Assert.That(e.VerticalStretch, Is.EqualTo(4));
    }
    [Test]
    public void TestCtorWithRadius() {
        var e = new Ellipse2D<double>(new Point2D<double>(1, 2), 5);

        Assert.That(e.MidPoint, Is.EqualTo(new Point2D<double>(1, 2)));
        Assert.That(e.HorizontalStretch, Is.EqualTo(5));
        Assert.That(e.VerticalStretch, Is.EqualTo(5));
    }
    [Test]
    public void TestCreate() {
        var e = Ellipse2D.Create(new Point2D<double>(1, 2), 3, 4);

        Assert.That(e, Is.EqualTo(new Ellipse2D<double>(new Point2D<double>(1, 2), 3, 4)));
    }
    [Test]
    public void TestCircle() {
        var e = Ellipse2D.Circle(new Point2D<double>(1, 2), 5);

        Assert.That(e, Is.EqualTo(new Ellipse2D<double>(new Point2D<double>(1, 2), 5)));
    }
    [Test]
    public void TestFloatInstantiation() {
        var e = new Ellipse2D<float>(new Point2D<float>(1f, 2f), 3f, 4f);

        Assert.That(e.MidPoint, Is.EqualTo(new Point2D<float>(1f, 2f)));
        Assert.That(e.HorizontalStretch, Is.EqualTo(3f));
        Assert.That(e.VerticalStretch, Is.EqualTo(4f));
    }
}
