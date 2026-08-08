using NUnit.Framework;

namespace Moarx.Math.Tests;
[TestFixture]
public class Bounds2DTests {
    [Test]
    public void TestSyntax() {
        var b = new Bounds2D<double> { PMin = new Point2D<double>(1, 1), PMax = new Point2D<double>(2, 2) };

        Assert.That(b.PMin, Is.EqualTo(new Point2D<double>(1, 1)));
        Assert.That(b.PMax, Is.EqualTo(new Point2D<double>(2, 2)));
    }
    [Test]
    public void TestEmptyBounds2D() {
        var b = new Bounds2D<double>();
        Assert.That(b.PMin, Is.EqualTo(new Point2D<double>(double.MaxValue)));
        Assert.That(b.PMax, Is.EqualTo(new Point2D<double>(double.MinValue)));
    }
    [Test]
    public void TestCtor1() {
        Point2D<double> p = new(1, 1);
        var b = new Bounds2D<double>(p);

        Assert.That(b.PMin, Is.EqualTo(p));
        Assert.That(b.PMax, Is.EqualTo(p));
    }
    [Test]
    public void TestCtor2() {
        Point2D<double> p1 = new(1, 1);
        Point2D<double> p2 = new(7, 7);

        var b = new Bounds2D<double>(p1, p2);

        Assert.That(b.PMin, Is.EqualTo(p1));
        Assert.That(b.PMax, Is.EqualTo(p2));
    }
    [Test]
    public void TestCtor2UnordersPoints() {
        Point2D<double> p1 = new(7, -3);
        Point2D<double> p2 = new(-2, 5);

        var b = new Bounds2D<double>(p1, p2);

        Assert.That(b.PMin, Is.EqualTo(new Point2D<double>(-2, -3)));
        Assert.That(b.PMax, Is.EqualTo(new Point2D<double>(7, 5)));
    }
    [Test]
    public void TestCtorThrowsOnNaN() {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Bounds2D<double> { PMin = new Point2D<double>(double.NaN, 0) });
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Bounds2D<double> { PMax = new Point2D<double>(0, double.NaN) });
    }
    [Test]
    public void TestToRectangle() {
        Point2D<double> pMin = new(-2.5, -1);
        Point2D<double> pMax = new(2.5, 1);

        Bounds2D<double> b = new(pMin, pMax);

        Assert.That(b.ToRectangle(), Is.EqualTo(new Rectangle2D<double>(pMin, pMax)));
    }
    [Test]
    public void TestDiagonal() {
        Point2D<double> pMin = new(-2.5, -1);
        Point2D<double> pMax = new(2.5, 1);

        Bounds2D<double> b = new(pMin, pMax);

        Assert.That(b.Diagonal(), Is.EqualTo(pMax - pMin));
    }
    [Test]
    public void TestIntersect() {
        Bounds2D<double> b1 = new(new(-2, -3), new(3, 1));
        Bounds2D<double> b2 = new(new(0, -4), new(5, -1));

        Bounds2D<double> bExp = new(new(0, -3), new(3, -1));

        Assert.That(Bounds2D<double>.Intersect(b1, b2), Is.EqualTo(bExp));
    }
    [Test]
    public void TestGet() {
        Point2D<double> pMin = new(-2.5, -1);
        Point2D<double> pMax = new(2.5, 1);

        Bounds2D<double> b = new(pMin, pMax);

        Assert.That(b[0], Is.EqualTo(pMin));
        Assert.That(b[1], Is.EqualTo(pMax));
    }
    [Test]
    public void TestGetThrowsOnOutOfRange() {
        Bounds2D<double> b = new(new(0, 0), new(1, 1));

        Assert.Throws<IndexOutOfRangeException>(() => _ = b[2]);
        Assert.Throws<IndexOutOfRangeException>(() => _ = b[-1]);
    }
}
