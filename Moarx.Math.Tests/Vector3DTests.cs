using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class Vector3DTests {

    [Test]
    public void TestLength() {

        var v = new Vector3D<double>(3, 4, 5);

        Assert.That(v.GetLengthSquared(), Is.EqualTo(3 * 3 + 4 * 4 + 5 * 5));
        Assert.That(v.GetLength(),        Is.EqualTo(System.Math.Sqrt(50)));
    }

}