﻿using NUnit.Framework;
using System.Numerics;

namespace Moarx.Math.Tests;

[TestFixture]
public class Vector3DTests {
    [Test]
    public void TestCtor() {

        var p = new Vector3D<double>();
        Assert.That(p.X, Is.Zero);
        Assert.That(p.Y, Is.Zero);
        Assert.That(p.Z, Is.EqualTo(0));
    }

    [Test]
    public void TestCtorArgs() {

        var p = new Vector3D<double>(1, 2, 3);

        Assert.That(p.X, Is.EqualTo(1));
        Assert.That(p.Y, Is.EqualTo(2));
        Assert.That(p.Z, Is.EqualTo(3));
    }

    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.AdditionData))]
    public void AdditionTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 =  new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 + vector2));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.SubtractionData))]
    public void SubtractionTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 =  new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 - vector2));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.DivisionData))]
    public void DivisionTests(double[] expected, double[] vector, double[] scalar) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 / scalar[0]));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.NegationData))]
    public void NegationTests(double[] expected, double[] vector) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(-vector1));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.MultiplicationData))]
    public void MultiplicationTests(double[] expected, double[] vector, double[] scalar) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1 * scalar[0]));
        Assert.That(expectedVector, Is.EqualTo(scalar[0] * vector1));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.AccessOperatorData))]
    public void AccessOperatorTests(double[] expected, double[] vector, double[] access) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Assert.That(expected[0], Is.EqualTo(vector1[(int)access[0]]));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.DotProductData))]
    public void DotProductTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 = new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Assert.That(expected[0], Is.EqualTo(vector1 * vector2));
    }


    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.MinimumData))]
    public void MinimumTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 =  new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(Vector3D<double>.Minimum(vector1, vector2)));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.MaximumData))]
    public void MaximumTests(double[] expected, double[] firstVector, double[] secondVector) {
        Vector3D<double> vector1 =  new Vector3D<double>(firstVector[0], firstVector[1], firstVector[2]);
        Vector3D<double> vector2 =  new Vector3D<double>(secondVector[0], secondVector[1], secondVector[2]);

        Vector3D<double> expectedVector = new Vector3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(Vector3D<double>.Maximum(vector1, vector2)));
    }
    [TestCaseSource(typeof(BaseTestData3D), nameof(BaseTestData3D.CastData))]
    public void PointCastTests(double[] expected, double[] vector) {
        Vector3D<double> vector1 =  new Vector3D<double>(vector[0], vector[1], vector[2]);

        Point3D<double> expectedVector = new Point3D<double>(expected[0], expected[1], expected[2]);

        Assert.That(expectedVector, Is.EqualTo(vector1.ToPoint()));
    }
}