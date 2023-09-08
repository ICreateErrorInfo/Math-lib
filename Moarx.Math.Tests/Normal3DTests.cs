using NUnit.Framework;
using System;

namespace Moarx.Math.Tests;

[TestFixture]
public class Normal3DTests {

    [Test]
    public void TestCtor0() {

        Assert.Throws<ArgumentException>(() => _ = new Normal3D<double>(), "");

        Assert.Throws<ArgumentException>(() => _ = new Normal3D<double>(new()), "");

        Assert.Throws<ArgumentException>(() => _ = new Normal3D<double>(0, 0, 0), "");
    }
    [Test]
    public void TestCtor1() {

        var p1 = new Normal3D<double>(new(0, 1, 0));
        Assert.That(p1.X, Is.EqualTo(0));
        Assert.That(p1.Y, Is.EqualTo(1));
        Assert.That(p1.Z, Is.EqualTo(0));
        Assert.That(p1.ToVector().IsNormalized(), Is.EqualTo(true));

        var p2 = new Normal3D<double>(1, 0, 0);
        Assert.That(p2.X, Is.EqualTo(1));
        Assert.That(p2.Y, Is.EqualTo(0));
        Assert.That(p2.Z, Is.EqualTo(0));
        Assert.That(p2.ToVector().IsNormalized(), Is.EqualTo(true));
    }
    [Test]
    public void TestIndexer() {

        var p1 = new Normal3D<double>(new(0, 1, 0));
        Assert.That(p1[0], Is.EqualTo(0));
        Assert.That(p1[1], Is.EqualTo(1));
        Assert.That(p1[2], Is.EqualTo(0));
    }
    [Test]
    public void TestLength() {

        var p1 = new Normal3D<double>(new(12, -1, 3));
        Assert.That(p1.GetLengthSquared(), Is.EqualTo(1));
        Assert.That(p1.GetLength(), Is.EqualTo(1));
    }
    [Test]
    public void TestAdd() {

        var p1 = new Normal3D<double>(new(0, 1, 0));
        var p2 = new Normal3D<double>(new(1, 0, 0));

        var n = new Normal3D<double>(1,1,0);       

        Assert.That(p1 + p2, Is.EqualTo(n));
    }
    [Test]
    public void TestMinus() {

        var p1 = new Normal3D<double>(new(0, 1, 0));
        var p2 = new Normal3D<double>(new(1, 0, 0));

        var n = new Normal3D<double>(-1,1,0);

        Assert.That(p1 - p2, Is.EqualTo(n));
    }
    [Test]
    public void TestNegate() {

        var p1 = new Normal3D<double>(new(0, 1, 0));

        var n = new Normal3D<double>(0,-1,0);

        Assert.That(-p1, Is.EqualTo(n));

        Assert.That((-p1).ToVector().IsNormalized(), Is.True);
    }
    [Test]
    public void TestMultiplication() {

        var p1 = new Normal3D<double>(new(0, 1, 0));
        var p2 = new Normal3D<double>(new(1, 0, 0));

        var n = new Vector3D<double>(0,1,0) * new Vector3D<double>(1, 0, 0);

        Assert.That(p1 * p2, Is.EqualTo(n));
    }

}

