using System;

using NUnit.Framework;

namespace Math_lib.Tests {

    [TestFixture]
    public class Vector3DTests {

        [Test]
        public void TestSyntax() {
            var v = new Vector3D {X = 3, Y = 2, Z = 1};

            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(2));
            Assert.That(v.Z, Is.EqualTo(1));
        }
        [Test]
        public void TestEmptyVector() {

            var v = new Vector3D();
            Assert.That(v.X, Is.EqualTo(0));
            Assert.That(v.Y, Is.EqualTo(0));
            Assert.That(v.Z, Is.EqualTo(0));
        }
        [Test]
        public void TestCtor1() {
            var v = new Vector3D(3);
            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(3));
            Assert.That(v.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestCtor2() {
            var v = new Vector3D(1, 2, 3);
            Assert.That(v.X, Is.EqualTo(1));
            Assert.That(v.Y, Is.EqualTo(2));
            Assert.That(v.Z, Is.EqualTo(3));
        }


        [Test]
        public void TestGetLength() {
            var v4 = new Vector3D(3, 4, 5);
            Assert.That(v4.GetLength(), Is.EqualTo(Math.Sqrt(50)));

            var v = new Vector3D(0, 0, 1);
            Assert.That(v.GetLength(), Is.EqualTo(1));
            var v1 = new Vector3D(0, 2, 0);
            Assert.That(v1.GetLength(), Is.EqualTo(2));
            var v2 = new Vector3D(3, 0, 0);
            Assert.That(v2.GetLength(), Is.EqualTo(3));

            var v3 = new Vector3D(1, 1, 0);
            Assert.That(v3.GetLength(), Is.EqualTo(Math.Sqrt(2)));
        }
        [Test]
        public void TestGetLengthSqrt() {
            var v = new Vector3D(3, 4, 5);
            Assert.That(v.GetLengthSqrt(), Is.EqualTo(50));
        }
        [Test]
        public void TestNormalize()
        {
            var v = new Vector3D(3, 4, 5);
            Assert.That(Math.Round(Vector3D.Normalize(v).GetLength()), Is.EqualTo(1));
        }
        [Test]
        public void TestDot()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(1, 5, 3);

            Assert.That(Vector3D.Dot(v, v1), Is.EqualTo(38));          
        }
        [Test]
        public void TestCross()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(1, 5, 3);

            var erg = new Vector3D(-13, -4, 11);

            Assert.That(Vector3D.Cross(v, v1), Is.EqualTo(erg));
        }
        [Test]
        public void TestAbs()
        {
            var v = new Vector3D(3, 4, 5);
            var erg = new Vector3D(Math.Abs(3), Math.Abs(4), Math.Abs(5));

            Assert.That(Vector3D.Abs(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestCeiling()
        {
            var v = new Vector3D(3, 4, 5);
            var erg = new Vector3D(Math.Ceiling(3.0), Math.Ceiling(4.0), Math.Ceiling(5.0));

            Assert.That(Vector3D.Ceiling(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestFloor()
        {
            var v = new Vector3D(3, 4, 5);
            var erg = new Vector3D(Math.Floor(3.0), Math.Floor(4.0), Math.Floor(5.0));

            Assert.That(Vector3D.Floor(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestMax()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(4, 5, 3);
            var erg = new Vector3D(Math.Max(v.X, v1.X), Math.Max(v.Y, v1.Y), Math.Max(v.Z, v1.Z));

            Assert.That(Vector3D.Max(v, v1), Is.EqualTo(erg));
        }
        [Test]
        public void TestMin()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(4, 5, 3);
            var erg = new Vector3D(Math.Min(v.X, v1.X), Math.Min(v.Y, v1.Y), Math.Min(v.Z, v1.Z));

            Assert.That(Vector3D.Min(v, v1), Is.EqualTo(erg));
        }
        [Test]
        public void TestClamp()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(3, 2, 3);
            var v2 = new Vector3D(7, 4, 5);
            var erg = new Vector3D(Math.Clamp(v.X, v1.X, v2.X), Math.Clamp(v.Y, v1.Y, v2.Y), Math.Clamp(v.Z, v1.Z, v2.Z));

            Assert.That(Vector3D.Clamp(v, v1, v2), Is.EqualTo(erg));
        }
        [Test]
        public void TestMaxDimension()
        {
            var v = new Vector3D(3, 4, 5);

            Assert.That(Vector3D.MaxDimension(v), Is.EqualTo(2));
        }
        [Test]
        public void TestPermute()
        {
            var v = new Vector3D(3, 4, 5);
            var erg = new Vector3D(5, 4, 3);

            Assert.That(Vector3D.Permute(v, 2, 1, 0), Is.EqualTo(erg));
        }
        [Test]
        public void TestSqrt()
        {
            var v = new Vector3D(4, 49, 16);
            var erg = new Vector3D(2, 7, 4);

            Assert.That(Vector3D.SquareRoot(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestRandomInUnitSphere()
        {
            var v = Vector3D.RandomInUnitSphere();

            Assert.That(v.GetLengthSqrt() < 1, Is.EqualTo(true));
        }


        //+
        [Test]
        public void TestOpPlusVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            var erg = new Vector3D(8, 10, 12);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlusVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 5;

            var erg = new Vector3D(8, 9, 10);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlusDV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 5;

            var erg = new Vector3D(8, 9, 10);

            Assert.That(v1 + v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlusVP()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            var erg = new Vector3D(8, 10, 12);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlus()
        {
            var v = new Vector3D(3, 4, 5);

            var erg = new Vector3D(3, 4, 5);

            Assert.That(+v, Is.EqualTo(erg));
        }

        //-
        [Test]
        public void TestOpMinusVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            var erg = new Vector3D(-2, -2, -2);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinusVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 5;

            var erg = new Vector3D(-2, -1, 0);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinusDV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 5;

            var erg = new Vector3D(2, 1, 0);

            Assert.That(v1 - v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinusVP()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            var erg = new Vector3D(-2, -2, -2);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinus()
        {
            var v = new Vector3D(3, 4, 5);

            var erg = new Vector3D(-3, -4, -5);

            Assert.That(-v, Is.EqualTo(erg));
        }

        //*
        [Test]
        public void TestOpMulVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            var erg = new Vector3D(15, 24, 35);

            Assert.That(v * v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 5;

            var erg = new Vector3D(15, 20, 25);

            Assert.That(v * v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulDV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 5;

            var erg = new Vector3D(15, 20, 25);

            Assert.That(v1 * v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulVP()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            var erg = new Vector3D(15, 24, 35);

            Assert.That(v * v1, Is.EqualTo(erg));
        }

        // /
        [Test]
        public void TestOpDivVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            var erg = new Vector3D(3.0 / 5, 4.0 / 6, 5.0 / 7);

            Assert.That(v / v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpDivVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 5;

            double inv = (double)1 / v1;

            var erg = new Vector3D(3.0 * inv, 4.0  * inv, 5.0 * inv);

            Assert.That(v / v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpDivDV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 5;

            var erg = new Vector3D(5.0 / 3, 5.0 / 4, 5.0 / 5);

            Assert.That(v1 / v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpDivVP()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            var erg = new Vector3D(3.0 / 5, 4.0 / 6, 5.0 / 7);

            Assert.That(v / v1, Is.EqualTo(erg));
        }

        // >
        [Test]
        public void TestOpGreaterThanVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            Assert.That(v > v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterThanVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v > v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterThanDV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v1 > v, Is.EqualTo(true));
        }

        // <
        [Test]
        public void TestOpLessThanVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            Assert.That(v < v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessThanVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v < v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessThanDV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v1 < v, Is.EqualTo(false));
        }

        // ==
        [Test]
        public void TestOpEqualVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            Assert.That(v == v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpEqualVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v == v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpEqualDV()
        {
            var v = new Vector3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 == v, Is.EqualTo(true));
        }

        // !=
        [Test]
        public void TestOpNotEqualVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            Assert.That(v != v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpNotEqualVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v != v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpNotEqualDV()
        {
            var v = new Vector3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 != v, Is.EqualTo(false));
        }

        // <=
        [Test]
        public void TestOpLessEqualVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(2, 4, 5);

            Assert.That(v <= v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpLessEqualVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v <= v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessEqualDV()
        {
            var v = new Vector3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 <= v, Is.EqualTo(true));
        }

        // >=
        [Test]
        public void TestOpGreaterEqualVV()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Vector3D(2, 3, 4);

            Assert.That(v >= v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpGreaterEqualVD()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v >= v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterEqualDV()
        {
            var v = new Vector3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 >= v, Is.EqualTo(true));
        }

        //[]
        [Test]
        public void TestGet()
        {
            var v = new Vector3D(3, 4, 5);

            Assert.That(v[0], Is.EqualTo(3));
            Assert.That(v[1], Is.EqualTo(4));
            Assert.That(v[2], Is.EqualTo(5));
        }
    }
}