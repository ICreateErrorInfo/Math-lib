using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
internal class Rectangle2DTests {

    [Test]
    public void TestCtor() {
        var rect = new Rectangle2D<int>(new(1, 1), new(4, 3));

        Assert.That(rect.TopRight, Is.EqualTo(new Point2D<int>(4, 1)));
        Assert.That(rect.TopLeft,  Is.EqualTo(new Point2D<int>(1, 1)));

        Assert.That(rect.BottomLeft,  Is.EqualTo(new Point2D<int>(1, 3)));
        Assert.That(rect.BottomRight, Is.EqualTo(new Point2D<int>(4, 3)));
    }
    [Test]
    public void TestCreateFromXYWidthHeight() {
        var rect = Rectangle2D.Create(1, 1, 3, 2);

        Assert.That(rect, Is.EqualTo(new Rectangle2D<int>(1, 1, 3, 2)));
    }
    [Test]
    public void TestCreateFromCorners() {
        var rect = Rectangle2D.Create(new Point2D<int>(1, 1), new Point2D<int>(4, 3));

        Assert.That(rect, Is.EqualTo(new Rectangle2D<int>(new Point2D<int>(1, 1), new Point2D<int>(4, 3))));
    }
    [Test]
    public void TestCreateFromFourCorners() {
        var rect = Rectangle2D.Create(new Point2D<int>(1, 1), new Point2D<int>(1, 3), new Point2D<int>(4, 3), new Point2D<int>(4, 1));

        Assert.That(rect, Is.EqualTo(new Rectangle2D<int>(new Point2D<int>(1, 1), new Point2D<int>(1, 3), new Point2D<int>(4, 3), new Point2D<int>(4, 1))));
    }
    [Test]
    public void TestXYWidthHeightAccessors() {
        var rect = new Rectangle2D<int>(1, 2, 3, 4);

        Assert.That(rect.X, Is.EqualTo(1));
        Assert.That(rect.Left, Is.EqualTo(1));
        Assert.That(rect.Y, Is.EqualTo(2));
        Assert.That(rect.Top, Is.EqualTo(2));
        Assert.That(rect.Right, Is.EqualTo(4));
        Assert.That(rect.Bottom, Is.EqualTo(6));
        Assert.That(rect.Width, Is.EqualTo(3));
        Assert.That(rect.Height, Is.EqualTo(4));
    }
    [Test]
    public void TestEmpty() {
        Assert.That(Rectangle2D<int>.Empty, Is.EqualTo(new Rectangle2D<int>(0, 0, 0, 0)));
    }
    [Test]
    public void TestIsEmpty() {
        Assert.That(Rectangle2D<int>.Empty.IsEmpty, Is.True);
        Assert.That(new Rectangle2D<int>(1, 2, 3, 4).IsEmpty, Is.False);
    }
    [Test]
    public void TestIntersect() {
        var a = new Rectangle2D<int>(0, 0, 10, 10);
        var b = new Rectangle2D<int>(5, 5, 10, 10);

        Assert.That(Rectangle2D<int>.Intersect(a, b), Is.EqualTo(new Rectangle2D<int>(5, 5, 5, 5)));
    }
    [Test]
    public void TestIntersectOfNonOverlappingRectanglesIsEmpty() {
        var a = new Rectangle2D<int>(0, 0, 5, 5);
        var b = new Rectangle2D<int>(10, 10, 5, 5);

        Assert.That(Rectangle2D<int>.Intersect(a, b), Is.EqualTo(Rectangle2D<int>.Empty));
    }
    [Test]
    public void TestUnion() {
        var a = new Rectangle2D<int>(0, 0, 5, 5);
        var b = new Rectangle2D<int>(3, 3, 5, 5);

        Assert.That(Rectangle2D<int>.Union(a, b), Is.EqualTo(new Rectangle2D<int>(0, 0, 8, 8)));
    }
    [Test]
    public void TestIntersectsWith() {
        var a = new Rectangle2D<int>(0, 0, 10, 10);
        var b = new Rectangle2D<int>(5, 5, 10, 10);
        var c = new Rectangle2D<int>(20, 20, 5, 5);

        Assert.That(a.IntersectsWith(b), Is.True);
        Assert.That(a.IntersectsWith(c), Is.False);
    }
    [Test]
    public void TestContainsPoint() {
        var rect = new Rectangle2D<int>(0, 0, 10, 10);

        Assert.That(rect.Contains(5, 5), Is.True);
        Assert.That(rect.Contains(0, 0), Is.True, "left/top edge is inclusive");
        Assert.That(rect.Contains(10, 5), Is.False, "right edge is exclusive");
        Assert.That(rect.Contains(5, 10), Is.False, "bottom edge is exclusive");
        Assert.That(rect.Contains(new Point2D<int>(5, 5)), Is.True);
    }
    [Test]
    public void TestContainsRectangleFullyInside() {
        var outer = new Rectangle2D<int>(0, 0, 10, 10);
        var inner = new Rectangle2D<int>(2, 2, 2, 2);

        Assert.That(outer.Contains(inner), Is.True);
    }
    [Test]
    public void TestContainsRectangleExtendingBeyondBottomIsNotContained() {
        // Regression test: Contains(Rectangle2D<T>) used to compare rect.Bottom against Right
        // instead of Bottom, so a rectangle extending past the bottom edge but still within
        // the (unrelated) right edge was incorrectly reported as contained.
        var outer = new Rectangle2D<int>(0, 0, 10, 3);
        var extendsPastBottom = new Rectangle2D<int>(2, 2, 2, 2);

        Assert.That(outer.Contains(extendsPastBottom), Is.False);
    }
    [Test]
    public void TestInflateBySingleValue() {
        var rect = new Rectangle2D<int>(2, 2, 4, 4);

        var inflated = rect.Inflate(1);

        Assert.That(inflated, Is.EqualTo(new Rectangle2D<int>(1, 1, 6, 6)));
    }
    [Test]
    public void TestInflateByWidthAndHeight() {
        var rect = new Rectangle2D<int>(2, 2, 4, 4);

        var inflated = rect.Inflate(1, 2);

        Assert.That(inflated, Is.EqualTo(new Rectangle2D<int>(1, 0, 6, 8)));
    }
    [Test]
    public void TestOffsetByXY() {
        var rect = new Rectangle2D<int>(1, 1, 4, 3);

        var offset = rect.Offset(2, 3);

        Assert.That(offset, Is.EqualTo(new Rectangle2D<int>(3, 4, 4, 3)));
    }
    [Test]
    public void TestOffsetByPoint() {
        // Regression test: Offset(Point2D<T>) used to be declared void and discard the
        // computed result, making it a silent no-op on this immutable struct.
        var rect = new Rectangle2D<int>(1, 1, 4, 3);

        var offset = rect.Offset(new Point2D<int>(2, 3));

        Assert.That(offset, Is.EqualTo(new Rectangle2D<int>(3, 4, 4, 3)));
    }
    [Test]
    public void TestToString() {
        var rect = new Rectangle2D<int>(1, 2, 3, 4);

        Assert.That(rect.ToString(), Is.EqualTo("{X=1,Y=2,Width=3,Height=4}"));
    }
}