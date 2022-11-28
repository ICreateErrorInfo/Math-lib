using NUnit.Framework;

namespace Moarx.Math.Tests; 

[TestFixture]
public class Point3DTests {

    [TestCaseSource(typeof(BaseTestData3D), "AddititionData")]
    public void AddTests(double[] expected, double[] firstVector, double[] secondVector) {
        Point3D<double> vector1 =  new Point3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Point3D<double> vector2 =  new Point3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Point3D<double> expectedVector = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 + vector2));
    }

    [Test]
    public void TestCtor() {

        var p = new Point3D<double>();
        Assert.That(p.X, Is.Zero);
        Assert.That(p.Y, Is.Zero);
        Assert.That(p.Z, Is.EqualTo(0));

        var p2 = new Point3D<int>();
        Assert.That(p2.X, Is.Zero);
        Assert.That(p2.Y, Is.Zero);
        Assert.That(p2.Z, Is.Zero);
    }

    [Test]
    public void TestCtorArgs() {

        var p = new Point3D<double>(1, 2, 3);

        Assert.That(p.X, Is.EqualTo(1));
        Assert.That(p.Y, Is.EqualTo(2));
        Assert.That(p.Z, Is.EqualTo(3));
    }

}