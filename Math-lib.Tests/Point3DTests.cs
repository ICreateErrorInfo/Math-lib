using System;

using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class Point3DTests
    {
        [Test]
        public void TestSyntax()
        {
            var v = new Point3D { X = 3, Y = 2, Z = 1 };

            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(2));
            Assert.That(v.Z, Is.EqualTo(1));
        }
        [Test]
        public void TestEmptyVector()
        {

            var v = new Point3D();
            Assert.That(v.X, Is.EqualTo(0));
            Assert.That(v.Y, Is.EqualTo(0));
            Assert.That(v.Z, Is.EqualTo(0));
        }
        [Test]
        public void TestCtor1()
        {
            var v = new Point3D(3);
            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(3));
            Assert.That(v.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestCtor2()
        {
            var v = new Point3D(1, 2, 3);
            Assert.That(v.X, Is.EqualTo(1));
            Assert.That(v.Y, Is.EqualTo(2));
            Assert.That(v.Z, Is.EqualTo(3));
        }


        [Test]
        public void TestIsNaN()
        {
            Point3D p = new Point3D { X = double.NaN };

            Assert.That(Point3D.IsNaN(p), Is.True);
        }
        [Test]
        public void TestGetLength()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(0, 0, 1);

            Assert.That(Point3D.Distance(v, v1), Is.EqualTo((v - v1).GetLength()));
        }
        [Test]
        public void TestGetLengthSqrt()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(0, 0, 1);

            Assert.That(Point3D.DistanceSqrt(v, v1), Is.EqualTo((v - v1).GetLengthSqrt()));
        }
        [Test]
        public void TestLerp()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            Assert.That(Point3D.Lerp(1, v, v1), Is.EqualTo(v1));
        }
        [Test]
        public void TestAbs()
        {
            var v = new Point3D(3, 4, 5);
            var erg = new Point3D(Math.Abs(3), Math.Abs(4), Math.Abs(5));

            Assert.That(Point3D.Abs(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestCeiling()
        {
            var v = new Point3D(3, 4, 5);
            var erg = new Point3D(Math.Ceiling(3.0), Math.Ceiling(4.0), Math.Ceiling(5.0));

            Assert.That(Point3D.Ceiling(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestFloor()
        {
            var v = new Point3D(3, 4, 5);
            var erg = new Point3D(Math.Floor(3.0), Math.Floor(4.0), Math.Floor(5.0));

            Assert.That(Point3D.Floor(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestMax()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(4, 5, 3);
            var erg = new Point3D(Math.Max(v.X, v1.X), Math.Max(v.Y, v1.Y), Math.Max(v.Z, v1.Z));

            Assert.That(Point3D.Max(v, v1), Is.EqualTo(erg));
        }
        [Test]
        public void TestMin()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(4, 5, 3);
            var erg = new Point3D(Math.Min(v.X, v1.X), Math.Min(v.Y, v1.Y), Math.Min(v.Z, v1.Z));

            Assert.That(Point3D.Min(v, v1), Is.EqualTo(erg));
        }
        [Test]
        public void TestPermute()
        {
            var v = new Point3D(3, 4, 5);
            var erg = new Point3D(5, 4, 3);

            Assert.That(Point3D.Permute(v, 2, 1, 0), Is.EqualTo(erg));
        }


        //+
        [Test]
        public void TestOpPlusPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            var erg = new Point3D(8, 10, 12);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlusPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 5;

            var erg = new Point3D(8, 9, 10);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlusDP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 5;

            var erg = new Point3D(8, 9, 10);

            Assert.That(v1 + v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlusPV()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            var erg = new Point3D(8, 10, 12);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlus()
        {
            var v = new Point3D(3, 4, 5);

            var erg = new Point3D(3, 4, 5);

            Assert.That(+v, Is.EqualTo(erg));
        }

        //-
        [Test]
        public void TestOpMinusPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            var erg = new Vector3D(-2, -2, -2);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinusPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 5;

            var erg = new Point3D(-2, -1, 0);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinusDP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 5;

            var erg = new Point3D(2, 1, 0);

            Assert.That(v1 - v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinusPV()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            var erg = new Point3D(-2, -2, -2);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinus()
        {
            var v = new Point3D(3, 4, 5);

            var erg = new Point3D(-3, -4, -5);

            Assert.That(-v, Is.EqualTo(erg));
        }

        //*
        [Test]
        public void TestOpMulPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            var erg = new Point3D(15, 24, 35);

            Assert.That(v * v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 5;

            var erg = new Point3D(15, 20, 25);

            Assert.That(v * v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulDP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 5;

            var erg = new Point3D(15, 20, 25);

            Assert.That(v1 * v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulPV()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            var erg = new Point3D(15, 24, 35);

            Assert.That(v * v1, Is.EqualTo(erg));
        }

        // /
        [Test]
        public void TestOpDivPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            var erg = new Point3D(3.0 / 5, 4.0 / 6, 5.0 / 7);

            Assert.That(v / v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpDivPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 5;

            double inv = (double)1 / v1;

            var erg = new Point3D(3.0 * inv, 4.0 * inv, 5.0 * inv);

            Assert.That(v / v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpDivDP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 5;

            var erg = new Point3D(5.0 / 3, 5.0 / 4, 5.0 / 5);

            Assert.That(v1 / v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpDivPV()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Vector3D(5, 6, 7);

            var erg = new Point3D(3.0 / 5, 4.0 / 6, 5.0 / 7);

            Assert.That(v / v1, Is.EqualTo(erg));
        }

        // >
        [Test]
        public void TestOpGreaterThanPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            Assert.That(v > v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterThanPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v > v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterThanDP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v1 > v, Is.EqualTo(true));
        }

        // <
        [Test]
        public void TestOpLessThanPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            Assert.That(v < v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessThanPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v < v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessThanDP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v1 < v, Is.EqualTo(false));
        }

        // ==
        [Test]
        public void TestOpEqualPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            Assert.That(v == v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpEqualPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v == v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpEqualDP()
        {
            var v = new Point3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 == v, Is.EqualTo(true));
        }

        // !=
        [Test]
        public void TestOpNotEqualPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(5, 6, 7);

            Assert.That(v != v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpNotEqualPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v != v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpNotEqualDP()
        {
            var v = new Point3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 != v, Is.EqualTo(false));
        }

        // <=
        [Test]
        public void TestOpLessEqualPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(2, 4, 5);

            Assert.That(v <= v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpLessEqualPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v <= v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessEqualDP()
        {
            var v = new Point3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 <= v, Is.EqualTo(true));
        }

        // >=
        [Test]
        public void TestOpGreaterEqualPP()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = new Point3D(2, 3, 4);

            Assert.That(v >= v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpGreaterEqualPD()
        {
            var v = new Point3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v >= v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterEqualDP()
        {
            var v = new Point3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 >= v, Is.EqualTo(true));
        }

        //[]
        [Test]
        public void TestGet()
        {
            var v = new Point3D(3, 4, 5);

            Assert.That(v[0], Is.EqualTo(3));
            Assert.That(v[1], Is.EqualTo(4));
            Assert.That(v[2], Is.EqualTo(5));
        }
    }
}
