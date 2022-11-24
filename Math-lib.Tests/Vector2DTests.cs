using System;

using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class Vector2DTests
    {
        [Test]
        public void TestSyntax()
        {
            var v = new Vector2D { X = 3, Y = 2 };

            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(2)); 
        }
        [Test]
        public void TestEmptyVector()
        {
            var v = new Vector2D();

            Assert.That(v.X, Is.EqualTo(0));
            Assert.That(v.Y, Is.EqualTo(0));
        }
        [Test]
        public void TestCtor1()
        {
            var v = new Vector2D(3);

            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(3));
        }
        [Test]
        public void TestCtor2()
        {
            var v = new Vector2D(1, 2);

            Assert.That(v.X, Is.EqualTo(1));
            Assert.That(v.Y, Is.EqualTo(2));
        }


        [Test]
        public void TestIsNaN()
        {
            Vector2D v = new Vector2D { X = double.NaN};

            Assert.That(Vector2D.IsNaN(v), Is.True);
        }
        [Test]
        public void TestGetLength()
        {
            var v4 = new Vector2D(3, 4);
            Assert.That(v4.GetLength(), Is.EqualTo(Math.Sqrt(25)));

            var v = new Vector2D(0, 1);
            Assert.That(v.GetLength(), Is.EqualTo(1));
            var v1 = new Vector2D(2, 0);
            Assert.That(v1.GetLength(), Is.EqualTo(2));
            var v2 = new Vector2D(3, 0);
            Assert.That(v2.GetLength(), Is.EqualTo(3));

            var v3 = new Vector2D(1, 1);
            Assert.That(v3.GetLength(), Is.EqualTo(Math.Sqrt(2)));
        }
        [Test]
        public void TestGetLengthSqrt()
        {
            var v = new Vector2D(3, 4);
            Assert.That(v.GetLengthSqrt(), Is.EqualTo(25));
        }
        [Test]
        public void TestNormalize()
        {
            var v = new Vector2D(3, 4);
            Assert.That(Math.Round(Vector2D.Normalize(v).GetLength()), Is.EqualTo(1));
        }
        [Test]
        public void TestDot()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(1, 5);

            Assert.That(Vector2D.Dot(v, v1), Is.EqualTo(23));
        }
        public void TestAbs()
        {
            var v = new Vector2D(3, 4);
            var erg = new Vector2D(Math.Abs(3), Math.Abs(4));

            Assert.That(Vector2D.Abs(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestCeiling()
        {
            var v = new Vector2D(3, 4);
            var erg = new Vector2D(Math.Ceiling(3.0), Math.Ceiling(4.0));

            Assert.That(Vector2D.Ceiling(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestFloor()
        {
            var v = new Vector2D(3, 4);
            var erg = new Vector2D(Math.Floor(3.0), Math.Floor(4.0));

            Assert.That(Vector2D.Floor(v), Is.EqualTo(erg));
        }
        [Test]
        public void TestMax()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(4, 5);
            var erg = new Vector2D(Math.Max(v.X, v1.X), Math.Max(v.Y, v1.Y));

            Assert.That(Vector2D.Max(v, v1), Is.EqualTo(erg));
        }
        [Test]
        public void TestMin()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(4, 5);
            var erg = new Vector2D(Math.Min(v.X, v1.X), Math.Min(v.Y, v1.Y));

            Assert.That(Vector2D.Min(v, v1), Is.EqualTo(erg));
        }
        [Test]
        public void TestClamp()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(3, 2);
            var v2 = new Vector2D(7, 4);
            var erg = new Vector2D(Math.Clamp(v.X, v1.X, v2.X), Math.Clamp(v.Y, v1.Y, v2.Y));

            Assert.That(Vector2D.Clamp(v, v1, v2), Is.EqualTo(erg));
        }
        [Test]
        public void TestMaxDimension()
        {
            var v = new Vector2D(3, 4);

            Assert.That(Vector2D.MaxDimension(v), Is.EqualTo(1));
        }
        [Test]
        public void TestPermute()
        {
            var v = new Vector2D(3, 4);
            var erg = new Vector2D(4, 3);

            Assert.That(Vector2D.Permute(v, 1, 0), Is.EqualTo(erg));
        }
        [Test]
        public void TestSqrt()
        {
            var v = new Vector2D(4, 49);
            var erg = new Vector2D(2, 7);

            Assert.That(Vector2D.SquareRoot(v), Is.EqualTo(erg));
        }


        //+
        [Test]
        public void TestOpPlusVV()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(5, 6);

            var erg = new Vector2D(8, 10);

            Assert.That(v + v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpPlus()
        {
            var v = new Vector2D(3, 4);

            var erg = new Vector2D(3, 4);

            Assert.That(+v, Is.EqualTo(erg));
        }

        //-
        [Test]
        public void TestOpMinusVV()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(5, 6);

            var erg = new Vector2D(-2, -2);

            Assert.That(v - v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMinus()
        {
            var v = new Vector2D(3, 4);

            var erg = new Vector2D(-3, -4);

            Assert.That(-v, Is.EqualTo(erg));
        }

        //*
        [Test]
        public void TestOpMulVV()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(5, 6);

            var erg = new Vector2D(15, 24);

            Assert.That(v * v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulVD()
        {
            var v = new Vector2D(3, 4);
            var v1 = 5;

            var erg = new Vector2D(15, 20);

            Assert.That(v * v1, Is.EqualTo(erg));
        }
        [Test]
        public void TestOpMulDV()
        {
            var v = new Vector2D(3, 4);
            var v1 = 5;

            var erg = new Vector2D(15, 20);

            Assert.That(v1 * v, Is.EqualTo(erg));
        }

        // /
        [Test]
        public void TestOpDivVD()
        {
            var v = new Vector2D(3, 4);
            var v1 = 5;

            double inv = (double)1 / v1;

            var erg = new Vector2D(3.0 * inv, 4.0 * inv);

            Assert.That(v / v1, Is.EqualTo(erg));
        }

        // ==
        [Test]
        public void TestOpEqualVV()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(5, 6);

            Assert.That(v == v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpEqualVD()
        {
            var v = new Vector2D(3, 4);
            var v1 = 6;

            Assert.That(v == v1, Is.EqualTo(false));
        }
        [Test]
        public void TestOpEqualDV()
        {
            var v = new Vector2D(5, 5);
            var v1 = 5;

            Assert.That(v1 == v, Is.EqualTo(true));
        }

        // !=
        [Test]
        public void TestOpNotEqualVV()
        {
            var v = new Vector2D(3, 4);
            var v1 = new Vector2D(5, 6);

            Assert.That(v != v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpNotEqualVD()
        {
            var v = new Vector2D(3, 4);
            var v1 = 6;

            Assert.That(v != v1, Is.EqualTo(true));
        }
        [Test]
        public void TestOpNotEqualDV()
        {
            var v = new Vector2D(5, 5);
            var v1 = 5;

            Assert.That(v1 != v, Is.EqualTo(false));
        }


        //[]
        [Test]
        public void TestGet()
        {
            var v = new Vector2D(3, 4);

            Assert.That(v[0], Is.EqualTo(3));
            Assert.That(v[1], Is.EqualTo(4));
        }
    }
}
