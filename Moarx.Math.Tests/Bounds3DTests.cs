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
}
