﻿using NUnit.Framework;
using System.Numerics;

namespace Moarx.Math.Tests;

[TestFixture]
public class Vector3DTests {

    [TestCaseSource(typeof(BaseTestData3D), "AddititionData")]
    public void AddTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 =  new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 + vector2));
    }

}