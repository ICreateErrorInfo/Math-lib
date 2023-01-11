using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
internal class Rectangle2DTests {
    [Test]
    public void TestCtor() {
        Rectangle2D rect = new Rectangle2D(new(1, 1), new(4, 3));

        Assert.That(rect.PointBottomLeft, Is.EqualTo(new Point2D<double>(1, 1)));
        Assert.That(rect.PointBottomRight, Is.EqualTo(new Point2D<double>(4, 1)));
        Assert.That(rect.PointUpperRight, Is.EqualTo(new Point2D<double>(4, 3)));
        Assert.That(rect.PointUpperLeft, Is.EqualTo(new Point2D<double>(1, 3)));
    }
}

