using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
internal class Rectangle2DTests {

    [Test]
    public void TestCtor() {
        var rect = new Rectangle2D<int>(new(1, 1), new(4, 3));

        Assert.That(rect.TopRight, Is.EqualTo(new Point2D<double>(4, 1)));
        Assert.That(rect.TopLeft,  Is.EqualTo(new Point2D<double>(1, 1)));

        Assert.That(rect.BottomLeft,  Is.EqualTo(new Point2D<double>(1, 3)));
        Assert.That(rect.BottomRight, Is.EqualTo(new Point2D<double>(4, 3)));
    }

}