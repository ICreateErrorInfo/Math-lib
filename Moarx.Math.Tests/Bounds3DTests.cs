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
    public void TestIntersect() {
        Bounds3D<double> b1 = new(new(-2, -3, -2), new(3, 1, 2));
        Bounds3D<double> b2 = new(new(0, -4, -3), new(5, -1, 0));

        Bounds3D<double> bExp = new(new(0, -3, -2), new(3, -1, 0));

        Assert.That(Bounds3D<double>.Intersect(b1, b2), Is.EqualTo(bExp));
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
        Point3D<double> p2 = new Point3D<double>(-2.5, -1, -1.5);

        Assert.That(Bounds3D<double>.Inside(p, b1), Is.True);
        Assert.That(Bounds3D<double>.Inside(p1, b1), Is.False);
        Assert.That(Bounds3D<double>.Inside(p2, b1), Is.True);
    }
    [Test]
    public void TestInsideExclusive() {
        Bounds3D<double> b1 = new(new(0, -4, -3), new(5, -1, 0));

        Point3D<double> p = new(0, -1, -2);
        Point3D<double> p1 = new(2, -2, -0.3);

        Assert.That(Bounds3D<double>.InsideExclusive(p, b1), Is.False);
        Assert.That(Bounds3D<double>.InsideExclusive(p1, b1), Is.True);
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

        Assert.That(b1.MaxDimension(), Is.EqualTo(0));
        Assert.That(b1.MaxDimension(), !Is.EqualTo(1));
    }
    [Test]
    public void TestLerp() {
        Point3D<double> pMin = new Point3D<double>(-2.5, -1, -1.5);
        Point3D<double> pMax = new Point3D<double>(2.5, 1, 1);

        Bounds3D<double> b1 = new Bounds3D<double>(pMin, pMax);

        Point3D<double> t = new Point3D<double>(0.25, 0.5, 0.75);

        Point3D<double> expected = new(
            MathmaticMethods.Lerp(t.X, pMin.X, pMax.X),
            MathmaticMethods.Lerp(t.Y, pMin.Y, pMax.Y),
            MathmaticMethods.Lerp(t.Z, pMin.Z, pMax.Z));

        Assert.That(b1.Lerp(t), Is.EqualTo(expected));
    }
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
    [Test]
    public void TestGetThrowsOnOutOfRange() {
        Bounds3D<double> b1 = new(new(0, 0, 0), new(1, 1, 1));

        Assert.Throws<IndexOutOfRangeException>(() => _ = b1[2]);
        Assert.Throws<IndexOutOfRangeException>(() => _ = b1[-1]);
    }
    [Test]
    public void TestDistanceSquaredForPointInsideIsZero() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        Assert.That(b.DistanceSquared(new Point3D<double>(0, 0, 0)), Is.EqualTo(0));
    }
    [Test]
    public void TestDistanceSquaredForPointOnBoundaryIsZero() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        Assert.That(b.DistanceSquared(new Point3D<double>(1, 1, 1)), Is.EqualTo(0));
    }
    [Test]
    public void TestDistanceSquaredForPointOutsideOnOneAxis() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        // Outside along X only; Y and Z are inside the bounds and must not contribute.
        Assert.That(b.DistanceSquared(new Point3D<double>(3, 0, 0)), Is.EqualTo(4));
    }
    [Test]
    public void TestDistanceSquaredForPointOutsideOnAllAxes() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        Assert.That(b.DistanceSquared(new Point3D<double>(3, 3, 3)), Is.EqualTo(12));
    }
    [Test]
    public void TestIntersectPHit() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));
        Ray ray = new(new Point3D<double>(-5, 0, 0), new Vector3D<double>(1, 0, 0));

        bool hit = b.IntersectP(ray, out double hitt0, out double hitt1);

        Assert.That(hit, Is.True);
        Assert.That(hitt0, Is.EqualTo(4));
        Assert.That(hitt1, Is.EqualTo(6));
    }
    [Test]
    public void TestIntersectPMiss() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));
        // Parallel to the box along X, but offset outside its Y range.
        Ray ray = new(new Point3D<double>(-5, 5, 0), new Vector3D<double>(1, 0, 0));

        bool hit = b.IntersectP(ray, out _, out _);

        Assert.That(hit, Is.False);
    }
    [Test]
    public void TestIntersectPWithPrecomputedInvDirHit() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));
        Ray ray = new(new Point3D<double>(-5, 0, 0), new Vector3D<double>(1, 0, 0));
        Vector3D<double> invDir = new(1 / ray.Direction.X, 1 / ray.Direction.Y, 1 / ray.Direction.Z);
        bool[] dirIsNeg = { ray.Direction.X < 0, ray.Direction.Y < 0, ray.Direction.Z < 0 };

        bool hit = b.IntersectP(ray, invDir, dirIsNeg);

        Assert.That(hit, Is.True);
    }
    [Test]
    public void TestIntersectPWithPrecomputedInvDirMiss() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));
        Ray ray = new(new Point3D<double>(-5, 5, 0), new Vector3D<double>(1, 0, 0));
        Vector3D<double> invDir = new(1 / ray.Direction.X, 1 / ray.Direction.Y, 1 / ray.Direction.Z);
        bool[] dirIsNeg = { ray.Direction.X < 0, ray.Direction.Y < 0, ray.Direction.Z < 0 };

        bool hit = b.IntersectP(ray, invDir, dirIsNeg);

        Assert.That(hit, Is.False);
    }
    [Test]
    public void TestBoundingSphere() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        var (center, radius) = b.BoundingSphere();

        Assert.That(center, Is.EqualTo(new Point3D<double>(0, 0, 0)));
        Assert.That(radius, Is.EqualTo(System.Math.Sqrt(3)).Within(1e-10));
    }
    [Test]
    public void TestBoundingSphereForDegenerateBoundsReturnsZeroRadius() {
        // The default ctor's sentinel PMin/PMax never contain their own center point.
        Bounds3D<double> b = new();

        var (center, radius) = b.BoundingSphere();

        Assert.That(center, Is.EqualTo(new Point3D<double>(0, 0, 0)));
        Assert.That(radius, Is.EqualTo(0));
    }
    [Test]
    public void TestPMinPMaxThrowOnNaN() {
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Bounds3D<double> { PMin = new Point3D<double>(double.NaN, 0, 0) });
        Assert.Throws<ArgumentOutOfRangeException>(() => _ = new Bounds3D<double> { PMax = new Point3D<double>(0, double.NaN, 0) });
    }
    [Test]
    public void TestInvertedBoundsVolumeIsNegative() {
        // The two-point ctor always sorts PMin/PMax, but object-initializer syntax bypasses that,
        // producing an inverted box. Volume/SurfaceArea are not guarded against this.
        Bounds3D<double> b = new() { PMin = new Point3D<double>(5, 5, 5), PMax = new Point3D<double>(1, 1, 1) };

        Assert.That(b.Volume(), Is.EqualTo(-64));
    }
}
