using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class BoundsExtensionsTests {

    [Test]
    public void TestDistanceForPointInsideIsZero() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        Assert.That(b.Distance(new Point3D<double>(0, 0, 0)), Is.EqualTo(0));
    }
    [Test]
    public void TestDistanceForPointOutsideOnOneAxis() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        // Outside along X only; Y and Z are inside the bounds and must not contribute.
        Assert.That(b.Distance(new Point3D<double>(3, 0, 0)), Is.EqualTo(2));
    }
    [Test]
    public void TestDistanceForPointOutsideOnAllAxes() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        Assert.That(b.Distance(new Point3D<double>(3, 3, 3)), Is.EqualTo(System.Math.Sqrt(12)).Within(1e-10));
    }
    [Test]
    public void TestDistanceWithDifferentPointType() {
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        Assert.That(b.Distance(new Point3D<int>(3, 0, 0)), Is.EqualTo(2));
    }
}
