﻿using NUnit.Framework;

namespace Moarx.Math.Tests; 

[TestFixture]
public class PointTests {

    [Test]
    public void TestCtor() {

        var p = new Point3D<double>();

        Assert.That(p.X, Is.Zero);
        Assert.That(p.Y, Is.Zero);
        Assert.That(p.Z, Is.Zero);
    }

    [Test]
    public void TestCtorArgs() {

        var p = new Point3D<double>(1, 2, 3);

        Assert.That(p.X, Is.EqualTo(1));
        Assert.That(p.Y, Is.EqualTo(2));
        Assert.That(p.Z, Is.EqualTo(3));
    }

}