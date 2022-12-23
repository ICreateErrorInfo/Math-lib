using NUnit.Framework;

namespace Moarx.Math.Tests; 
[TestFixture]
public class Bounds3DTests {
    [Test]
    public void TestSyntax() {
        var b = new Bounds3D<double> { PMin = new Point3D<double>(1, 1, 1), PMax = new Point3D<double>(2, 2, 2) };

        Assert.That(b.PMin, Is.EqualTo(new Point3D<double>(1, 1, 1)));
        Assert.That(b.PMax, Is.EqualTo(new Point3D<double>(2, 2, 2)));
    }
    [Test]
    public void TestEmptyBound3D() {
        var b = new Bounds3D<double>();
        Assert.That(b.PMin, Is.EqualTo(new Point3D<double>(double.MaxValue)));
        Assert.That(b.PMax, Is.EqualTo(new Point3D<double>(double.MinValue)));
    }
    [Test]
    public void TestCtor1() {
        Point3D<double> p = new Point3D<double>(1);
        var b = new Bounds3D<double>(p);

        Assert.That(b.PMin, Is.EqualTo(p));
        Assert.That(b.PMax, Is.EqualTo(p));
    }
    [Test]
    public void TestCtor2() {
        Point3D<double> p1 = new Point3D<double>(1);
        Point3D<double> p2 = new Point3D<double>(7);

        var b = new Bounds3D<double>(p1, p2);

        Assert.That(b.PMin, Is.EqualTo(p1));
        Assert.That(b.PMax, Is.EqualTo(p2));
    }

    [Test]
    public void TestCorner() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b = new Bounds3D<double>(pMin, pMax);

        Assert.That(b.Corner(0), Is.EqualTo(new Point3D<double>(-2.5, -1, -1.5)));
        Assert.That(b.Corner(1), Is.EqualTo(new Point3D<double>(2.5, -1, -1.5)));
        Assert.That(b.Corner(2), Is.EqualTo(new Point3D<double>(-2.5, 1, -1.5)));
        Assert.That(b.Corner(3), Is.EqualTo(new Point3D<double>(2.5, 1, -1.5)));
        Assert.That(b.Corner(4), Is.EqualTo(new Point3D<double>(-2.5, -1, 1)));
        Assert.That(b.Corner(5), Is.EqualTo(new Point3D<double>(2.5, -1, 1)));
        Assert.That(b.Corner(6), Is.EqualTo(new Point3D<double>(-2.5, 1, 1)));
        Assert.That(b.Corner(7), Is.EqualTo(new Point3D<double>(2.5, 1, 1)));
    }
    [Test]
    public void TestUnion() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Point3D<double> pMax1 = new Point3D<double>(1, 1, 2);

        Bounds3D<double> b3 = new Bounds3D<double>(new(-2.5, -1, -1.5), new(2.5, 1, 2));

        Assert.That(Bounds3D<double>.Union(b1, pMax1).PMin, Is.EqualTo(b3.PMin));
        Assert.That(Bounds3D<double>.Union(b1, pMax1).PMax, Is.EqualTo(b3.PMax));
    }
    [Test]
    public void TestUnion2() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Point3D<double> pMin1 = new Point3D<double>(-1,-1,0);
        Point3D<double> pMax1 = new Point3D<double>(1,1,2);

        Bounds3D<double> b2 = new Bounds3D<double>(pMin1, pMax1);

        Bounds3D<double> b3 = new Bounds3D<double>(new(-2.5, -1, -1.5), new(2.5, 1, 2));

        Assert.That(Bounds3D<double>.Union(b1, b2).PMin, Is.EqualTo(b3.PMin));
        Assert.That(Bounds3D<double>.Union(b1, b2).PMax, Is.EqualTo(b3.PMax));
    }
    [Test]
    public void TestOverlap() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Point3D<double> pMin1 = new Point3D<double>(-1, -1, 0);
        Point3D<double> pMax1 = new Point3D<double>(1, 1, 2);

        Bounds3D<double> b2 = new Bounds3D<double>(pMin1, pMax1);

        Assert.That(Bounds3D<double>.Overlaps(b1, b2), Is.True);
    }
    [Test]
    public void TestInside() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Point3D<double> p = new(1.5, 0.4, 0.3);
        Point3D<double> p1 = new(15, 0.4, 0.3);

        Assert.That(Bounds3D<double>.Inside(p, b1), Is.True);
        Assert.That(Bounds3D<double>.Inside(p1, b1), Is.False);
    }
    [Test]
    public void TestExpand() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Bounds3D<double> b2 = new Bounds3D<double>((pMin - new Point3D<double>(1,1,1)).ToPoint(), pMax + new Vector3D<double>(1));

        Assert.That(Bounds3D<double>.Expand(b1, 1).PMin, Is.EqualTo(b2.PMin));
        Assert.That(Bounds3D<double>.Expand(b1, 1).PMax, Is.EqualTo(b2.PMax));
    }
    [Test]
    public void TestDiagonal() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Vector3D<double> v = pMax - pMin;

        Assert.That(b1.Diagonal(), Is.EqualTo(v));
    }
    [Test]
    public void TestVolume() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        double v = b1.Diagonal().X * b1.Diagonal().Y * b1.Diagonal().Z;

        Assert.That(b1.Volume(), Is.EqualTo(v));
    }
    [Test]
    public void TestSurfaceArea() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        double v = 2 * (b1.Diagonal().X * b1.Diagonal().Y + b1.Diagonal().X * b1.Diagonal().Z + b1.Diagonal().Y * b1.Diagonal().Z);

        Assert.That(b1.SurfaceArea(), Is.EqualTo(v));
    }
    [Test]
    public void TestMaximumExtend() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Assert.That(b1.MaximumExtent(), Is.EqualTo(0));
        Assert.That(b1.MaximumExtent(), !Is.EqualTo(1));
    }
    //[Test]
    //public void TestLerp() {
    //    Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
    //    Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

    //    Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

    //    Point3D<double> p = new Point3D<double>(1,1,1);

    //    Point3D<double> ex = new(Mathe.Lerp(1, pMin.X, pMax.X), Mathe.Lerp(1, pMin.Y, pMax.Y), Mathe.Lerp(1, pMin.Z, pMax.Z));

    //    Assert.That(b1.Lerp(p), Is.EqualTo(ex));
    //}
    [Test]
    public void TestOffset() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Point3D<double> p = new(1,1,1);

        Vector3D<double> o = p - pMin;

        double newX = o.X, newY = o.Y, newZ = o.Z;

        if (pMax.X > pMin.X)
            newX /= pMax.X - pMin.X;
        if (pMax.Y > pMin.Y)
            newY /= pMax.Y - pMin.Y;
        if (pMax.Z > pMin.Z)
            newZ /= pMax.Z - pMin.Z;

        o = new Vector3D<double>(newX, newY, newZ);

        Assert.That(b1.Offset(p), Is.EqualTo(o));
    }
    [Test]
    public void TestGet() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Assert.That(b1[0], Is.EqualTo(pMin));
        Assert.That(b1[1], Is.EqualTo(pMax));
    }
}
