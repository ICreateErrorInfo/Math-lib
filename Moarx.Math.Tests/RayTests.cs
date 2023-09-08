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

}