using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
internal class RayTests {

    [Test]
    public void TestSyntax() {
        _ = new Ray();
    }

    [Test]
    public void TestEmptyRay() {
        var r = new Ray();
        Assert.That(r.Origin,    Is.EqualTo(new Point3D<double>(0, 0, 0)));
        Assert.That(r.Direction, Is.EqualTo(new Vector3D<double>(0, 0, 0)));
    }

    [Test]
    public void TestCtor1() {
        var r = new Ray(new Point3D<double>(1, 1, 1), new Vector3D<double>(2, 2, 2));

        Assert.That(r.Origin,    Is.EqualTo(new Point3D<double>(1, 1, 1)));
        Assert.That(r.Direction, Is.EqualTo(new Vector3D<double>(2, 2, 2)));
    }

    [Test]
    public void TestAt() {
        var r = new Ray(new Point3D<double>(1, 1, 1), new Vector3D<double>(2, 2, 2));

        Assert.That(r.At(1), Is.EqualTo(new Point3D<double>(3, 3, 3)));
    }
    [Test]
    public void TestDefaultCtorSetsTMaxAndTime() {
        var r = new Ray();

        Assert.That(r.TMax, Is.EqualTo(double.PositiveInfinity));
        Assert.That(r.Time, Is.EqualTo(0));
    }
    [Test]
    public void TestParameterizedCtorDefaultsTMaxAndTime() {
        var r = new Ray(new Point3D<double>(1, 1, 1), new Vector3D<double>(2, 2, 2));

        Assert.That(r.TMax, Is.EqualTo(double.PositiveInfinity));
        Assert.That(r.Time, Is.EqualTo(0));
    }
    [Test]
    public void TestParameterizedCtorWithExplicitTMaxAndTime() {
        var r = new Ray(new Point3D<double>(1, 1, 1), new Vector3D<double>(2, 2, 2), 10, 3.5);

        Assert.That(r.TMax, Is.EqualTo(10));
        Assert.That(r.Time, Is.EqualTo(3.5));
    }
    [Test]
    public void TestToString() {
        var r = new Ray(new Point3D<double>(1, 2, 3), new Vector3D<double>(4, 5, 6));

        Assert.That(r.ToString(), Is.EqualTo($"[o={r.Origin}, d={r.Direction}]"));
    }
}