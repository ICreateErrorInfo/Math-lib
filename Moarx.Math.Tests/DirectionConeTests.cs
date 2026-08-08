using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class DirectionConeTests {

    [Test]
    public void TestDefaultCtorIsEmpty() {
        var cone = new DirectionCone();

        Assert.That(cone.CosTheta, Is.EqualTo(double.PositiveInfinity));
        Assert.That(cone.IsEmpty(), Is.True);
    }
    [Test]
    public void TestCtorWithVectorAndCosTheta() {
        var cone = new DirectionCone(new Vector3D<double>(0, 0, 5), 0.5);

        Assert.That(cone.W, Is.EqualTo(new Vector3D<double>(0, 0, 1)));
        Assert.That(cone.CosTheta, Is.EqualTo(0.5));
        Assert.That(cone.IsEmpty(), Is.False);
    }
    [Test]
    public void TestCtorWithVectorOnlyDefaultsToUnitCone() {
        var cone = new DirectionCone(new Vector3D<double>(0, 3, 0));

        Assert.That(cone.W, Is.EqualTo(new Vector3D<double>(0, 1, 0)));
        Assert.That(cone.CosTheta, Is.EqualTo(1));
    }
    [Test]
    public void TestEntireSphere() {
        var cone = DirectionCone.EntireSphere();

        Assert.That(cone.W, Is.EqualTo(new Vector3D<double>(0, 0, 1)));
        Assert.That(cone.CosTheta, Is.EqualTo(-1));
        Assert.That(cone.IsEmpty(), Is.False);
    }
    [Test]
    public void TestInside() {
        var cone = new DirectionCone(new Vector3D<double>(0, 0, 1), System.Math.Cos(MathmaticMethods.ConvertToRadians(30)));

        Assert.That(cone.Inside(new Vector3D<double>(0, 0, 1)), Is.True);
        Assert.That(cone.Inside(new Vector3D<double>(1, 0, 0)), Is.False);
    }
    [Test]
    public void TestInsideOnEmptyConeIsAlwaysFalse() {
        var cone = new DirectionCone();

        Assert.That(cone.Inside(new Vector3D<double>(0, 0, 1)), Is.False);
    }
    [Test]
    public void TestBoundSubtendedDirectionsPointOutsideSphere() {
        Bounds3D<double> b = new(new Point3D<double>(-1, -1, -1), new Point3D<double>(1, 1, 1));
        Point3D<double> p = new(0, 0, 10);

        var cone = new DirectionCone().BoundSubtendedDirections(b, p);

        var (center, radius) = b.BoundingSphere();
        double expectedSin2ThetaMax = (radius * radius) / (p - center).GetLengthSquared();
        double expectedCosThetaMax = MathmaticMethods.SafeSqrt(1 - expectedSin2ThetaMax);

        Assert.That(cone.W.X, Is.EqualTo(0).Within(1e-10));
        Assert.That(cone.W.Y, Is.EqualTo(0).Within(1e-10));
        Assert.That(cone.W.Z, Is.EqualTo(-1).Within(1e-10));
        Assert.That(cone.CosTheta, Is.EqualTo(expectedCosThetaMax).Within(1e-10));
    }
    [Test]
    public void TestBoundSubtendedDirectionsPointInsideSphereReturnsEntireSphere() {
        Bounds3D<double> b = new(new Point3D<double>(-1, -1, -1), new Point3D<double>(1, 1, 1));
        Point3D<double> p = new(0, 0, 0);

        var cone = new DirectionCone().BoundSubtendedDirections(b, p);

        Assert.That(cone.CosTheta, Is.EqualTo(-1));
    }
    [Test]
    public void TestUnionWhenAIsEmptyReturnsB() {
        var a = new DirectionCone();
        var b = new DirectionCone(new Vector3D<double>(1, 0, 0), 0.5);

        Assert.That(DirectionCone.Union(a, b), Is.EqualTo(b));
    }
    [Test]
    public void TestUnionWhenBIsEmptyReturnsA() {
        var a = new DirectionCone(new Vector3D<double>(1, 0, 0), 0.5);
        var b = new DirectionCone();

        Assert.That(DirectionCone.Union(a, b), Is.EqualTo(a));
    }
    [Test]
    public void TestUnionWhenBIsContainedInAReturnsA() {
        var a = new DirectionCone(new Vector3D<double>(0, 0, 1), 0);   // 90 degree half-angle
        var b = new DirectionCone(new Vector3D<double>(0, 0, 1), 0.9); // ~25.8 degree half-angle, same axis

        Assert.That(DirectionCone.Union(a, b), Is.EqualTo(a));
    }
    [Test]
    public void TestUnionWhenAIsContainedInBReturnsB() {
        var a = new DirectionCone(new Vector3D<double>(0, 0, 1), 0.9);
        var b = new DirectionCone(new Vector3D<double>(0, 0, 1), 0);

        Assert.That(DirectionCone.Union(a, b), Is.EqualTo(b));
    }
    [Test]
    public void TestUnionWhenCombinedAngleExceedsPiReturnsEntireSphere() {
        var a = new DirectionCone(new Vector3D<double>(0, 0, 1), -0.9);
        var b = new DirectionCone(new Vector3D<double>(0, 0, -1), -0.9);

        var result = DirectionCone.Union(a, b);

        Assert.That(result.CosTheta, Is.EqualTo(-1));
    }
    [Test]
    public void TestUnionWhenAxesAreOppositeAndCrossProductIsZeroReturnsEntireSphere() {
        var a = new DirectionCone(new Vector3D<double>(0, 0, 1), 0.99);
        var b = new DirectionCone(new Vector3D<double>(0, 0, -1), 0.99);

        var result = DirectionCone.Union(a, b);

        Assert.That(result.CosTheta, Is.EqualTo(-1));
    }
    [Test]
    public void TestUnionWithNoShortcutThrowsNotImplemented() {
        // Neither cone contains the other, combined angle < pi, axes not (anti-)parallel:
        // hits the unfinished rotation branch in DirectionCone.Union.
        var a = new DirectionCone(new Vector3D<double>(1, 0, 0), 0);
        var b = new DirectionCone(new Vector3D<double>(0, 1, 0), 0);

        Assert.Throws<NotImplementedException>(() => DirectionCone.Union(a, b));
    }
}
