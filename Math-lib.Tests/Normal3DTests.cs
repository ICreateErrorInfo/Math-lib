using NUnit.Framework;
using System;

namespace Math_lib.Tests
{
    [TestFixture]
    public class Normal3DTests
    {
        [Test]
        public void TestSyntax()
        {
            var v = new Normal3D { X = 3, Y = 2, Z = 1 };

            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(2));
            Assert.That(v.Z, Is.EqualTo(1));
        }
        [Test]
        public void TestEmptyVector()
        {

            var v = new Normal3D();
            Assert.That(v.X, Is.EqualTo(0));
            Assert.That(v.Y, Is.EqualTo(0));
            Assert.That(v.Z, Is.EqualTo(0));
        }
        [Test]
        public void TestCtor1()
        {
            var v = new Normal3D(3);
            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(3));
            Assert.That(v.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestCtor2()
        {
            var v = new Normal3D(1, 2, 3);
            Assert.That(v.X, Is.EqualTo(1));
            Assert.That(v.Y, Is.EqualTo(2));
            Assert.That(v.Z, Is.EqualTo(3));
        }


        [Test]
        public void TestIsNaN()
        {
            Normal3D n = new Normal3D { X = double.NaN };

            Assert.That(Normal3D.IsNaN(n), Is.True);
        }
        [Test]
        public void TestGetLength()
        {
            var v4 = new Normal3D(3, 4, 5);
            Assert.That(v4.GetLength(), Is.EqualTo(Math.Sqrt(50)));

            var v = new Normal3D(0, 0, 1);
            Assert.That(v.GetLength(), Is.EqualTo(1));
            var v1 = new Normal3D(0, 2, 0);
            Assert.That(v1.GetLength(), Is.EqualTo(2));
            var v2 = new Normal3D(3, 0, 0);
            Assert.That(v2.GetLength(), Is.EqualTo(3));

            var v3 = new Normal3D(1, 1, 0);
            Assert.That(v3.GetLength(), Is.EqualTo(Math.Sqrt(2)));
        }
        [Test]
        public void TestGetLengthSqrt()
        {
            var v = new Normal3D(3, 4, 5);
            Assert.That(v.GetLengthSqrt(), Is.EqualTo(50));
        }
        [Test]
        public void TestNormalize()
        {
            var v = new Normal3D(3, 4, 5);
            Assert.That(Math.Round(Normal3D.Normalize(v).GetLength()), Is.EqualTo(1));
        }
        [Test]
        public void TestDotNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(1, 5, 3);

            Assert.That(Normal3D.Dot(v, v1), Is.EqualTo(38));
        }
        [Test]
        public void TestDotNV()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Vector3D(1, 5, 3);

            Assert.That(Normal3D.Dot(v, v1), Is.EqualTo(38));
        }
        [Test]
        public void TestDotVN()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Normal3D(1, 5, 3);

            Assert.That(Normal3D.Dot(v, v1), Is.EqualTo(38));
        }
        [Test]
        public void TestAbsDotNN()
        {
            var v = new Normal3D(3, 4, -5);
            var v1 = new Normal3D(1, -5, -3);

            Assert.That(Normal3D.AbsDot(v, v1), Is.EqualTo(2));
        }
        [Test]
        public void TestAbsDotNV()
        {
            var v = new Normal3D(3, 4, -5);
            var v1 = new Vector3D(1, -5, -3);

            Assert.That(Normal3D.AbsDot(v, v1), Is.EqualTo(2));
        }
        [Test]
        public void TestAbsDotVN()
        {
            var v = new Vector3D(3, 4, -5);
            var v1 = new Normal3D(1, -5, -3);

            Assert.That(Normal3D.AbsDot(v, v1), Is.EqualTo(2));
        }
        [Test]
        public void TestCrossVN()
        {
            var v = new Vector3D(3, 4, 5);
            var v1 = new Normal3D(1, 5, 3);

            var erg = new Vector3D(-13, -4, 11);

            Assert.That(Normal3D.Cross(v, v1), Is.EqualTo(erg));
        }
        [Test]
        public void TestCrossNV()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Vector3D(1, 5, 3);

            var erg = new Vector3D(-13, -4, 11);

            Assert.That(Normal3D.Cross(v, v1), Is.EqualTo(erg));
        }


        //+
        [Test]
        public void TestOpPlusNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(5, 6, 7);

            var erg = new Normal3D(8, 10, 12);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlusND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 5;

            var erg = new Normal3D(8, 9, 10);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlusDN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 5;

            var erg = new Normal3D(8, 9, 10);

            Assert.That(v1 + v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlus()
        {
            var v = new Normal3D(3, 4, 5);

            var erg = new Normal3D(3, 4, 5);

            Assert.That(+v, Is.EqualTo(erg));
        }

        //-
        [Test]
        public void TestOpMinusNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(5, 6, 7);

            var erg = new Normal3D(-2, -2, -2);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinusND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 5;

            var erg = new Normal3D(-2, -1, 0);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinusDN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 5;

            var erg = new Normal3D(2, 1, 0);

            Assert.That(v1 - v, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinus()
        {
            var v = new Normal3D(3, 4, 5);

            var erg = new Normal3D(-3, -4, -5);

            Assert.That(-v, Is.EqualTo(erg));
        }

        //*
        [Test]
        public void TestOpMulNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(5, 6, 7);

            var erg = new Normal3D(15, 24, 35);

            Assert.That(v * v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 5;

            var erg = new Normal3D(15, 20, 25);

            Assert.That(v * v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulDN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 5;

            var erg = new Normal3D(15, 20, 25);

            Assert.That(v1 * v, Is.EqualTo(erg));
        }

        // /
        [Test]
        public void TestOpDivNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(5, 6, 7);

            var erg = new Normal3D(3.0 / 5, 4.0 / 6, 5.0 / 7);

            Assert.That(v / v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpDivND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 5;

            double inv = (double)1 / v1;

            var erg = new Normal3D(3.0 * inv, 4.0 * inv, 5.0 * inv);

            Assert.That(v / v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpDivDN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 5;

            var erg = new Normal3D(5.0 / 3, 5.0 / 4, 5.0 / 5);

            Assert.That(v1 / v, Is.EqualTo(erg));
        }

        // >
        [Test]
        public void TestOpGreaterThanNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(5, 6, 7);

            Assert.That(v > v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterThanND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v > v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterThanDN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v1 > v, Is.EqualTo(true));
        }

        // <
        [Test]
        public void TestOpLessThanNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(5, 6, 7);

            Assert.That(v < v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessThanND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v < v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessThanDN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v1 < v, Is.EqualTo(false));
        }

        // ==
        [Test]
        public void TestOpEqualNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(5, 6, 7);

            Assert.That(v == v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpEqualND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v == v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpEqualDN()
        {
            var v = new Normal3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 == v, Is.EqualTo(true));
        }

        // !=
        [Test]
        public void TestOpNotEqualNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(5, 6, 7);

            Assert.That(v != v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpNotEqualND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v != v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpNotEqualDN()
        {
            var v = new Normal3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 != v, Is.EqualTo(false));
        }

        // <=
        [Test]
        public void TestOpLessEqualNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(2, 4, 5);

            Assert.That(v <= v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpLessEqualND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v <= v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpLessEqualDN()
        {
            var v = new Normal3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 <= v, Is.EqualTo(true));
        }

        // >=
        [Test]
        public void TestOpGreaterEqualNN()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = new Normal3D(2, 3, 4);

            Assert.That(v >= v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpGreaterEqualND()
        {
            var v = new Normal3D(3, 4, 5);
            var v1 = 6;

            Assert.That(v >= v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpGreaterEqualDN()
        {
            var v = new Normal3D(5, 5, 5);
            var v1 = 5;

            Assert.That(v1 >= v, Is.EqualTo(true));
        }
    }
}
