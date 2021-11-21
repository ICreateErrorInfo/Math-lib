using System;

using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class Matrix4x4Test
    {
        [Test]
        public void TestCtor1()
        {
            var m = new Matrix4x4();

            Assert.That(m[0, 0], Is.EqualTo(1));
            Assert.That(m[1, 0], Is.EqualTo(0));
            Assert.That(m[0, 1], Is.EqualTo(0));
            Assert.That(m[1, 1], Is.EqualTo(1));
        }
        [Test]
        public void TestCtor2()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 0;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix4x4(d);

            Assert.That(m[0, 0], Is.EqualTo(0));
            Assert.That(m[1, 0], Is.EqualTo(2));
            Assert.That(m[0, 1], Is.EqualTo(0));
            Assert.That(m[1, 2], Is.EqualTo(4));
        }
        [Test]
        public void TestCtor3()
        {
            double t00 = 0;
            double t01 = 1;
            double t02 = 0;
            double t03 = 2;
            double t10 = 0;
            double t11 = 3;
            double t12 = 0;
            double t13 = 4;
            double t20 = 0;
            double t21 = 5;
            double t22 = 0;
            double t23 = 6;
            double t30 = 0;
            double t31 = 7;
            double t32 = 0;
            double t33 = 8;

            var m = new Matrix4x4(t00, t01,  t02,  t03,  t10,
                                  t11, t12,  t13,  t20,  t21,
                                  t22, t23,  t30,  t31,  t32,
                                  t33);

            Assert.That(m[0, 0], Is.EqualTo(0));
            Assert.That(m[0, 1], Is.EqualTo(1));
            Assert.That(m[0, 3], Is.EqualTo(2));
            Assert.That(m[1, 1], Is.EqualTo(3));
            Assert.That(m[1, 2], Is.EqualTo(0));
            Assert.That(m[3, 3], Is.EqualTo(8));
        }


        [Test]
        public void TestMulMP()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[3, 3] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix4x4(d);

            Point3D p = new Point3D(1,2,3);

            var erg = m * p;

            Assert.That(erg.X, Is.EqualTo(1));
            Assert.That(erg.Y, Is.EqualTo(16));
            Assert.That(erg.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestMulMV()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[3, 3] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix4x4(d);

            Vector3D p = new Vector3D(1, 2, 3);

            var erg = m * p;

            Assert.That(erg.X, Is.EqualTo(1));
            Assert.That(erg.Y, Is.EqualTo(16));
            Assert.That(erg.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestMulMM()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[3, 3] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix4x4(d);

            double[,] d1 = new double[4, 4];
            d1[0, 0] = 1;
            d1[1, 1] = 1;
            d1[2, 2] = 1;
            d1[3, 3] = 1;
            d1[0, 2] = 4;
            d1[3, 1] = 3;
            var m1 = new Matrix4x4(d1);

            var erg = m * m1;

            Assert.That(erg[0, 0], Is.EqualTo(1));
            Assert.That(erg[0, 1], Is.EqualTo(0));
            Assert.That(erg[0, 2], Is.EqualTo(4));
            Assert.That(erg[0, 3], Is.EqualTo(0));

            Assert.That(erg[1, 0], Is.EqualTo(2));
            Assert.That(erg[1, 1], Is.EqualTo(1));
            Assert.That(erg[1, 2], Is.EqualTo(12));
            Assert.That(erg[1, 3], Is.EqualTo(0));

            Assert.That(erg[2, 0], Is.EqualTo(0));
            Assert.That(erg[2, 1], Is.EqualTo(0));
            Assert.That(erg[2, 2], Is.EqualTo(1));
            Assert.That(erg[2, 3], Is.EqualTo(0));

            Assert.That(erg[3, 0], Is.EqualTo(0));
            Assert.That(erg[3, 1], Is.EqualTo(3));
            Assert.That(erg[3, 2], Is.EqualTo(0));
            Assert.That(erg[3, 3], Is.EqualTo(1));
        }
        [Test]
        public void TestEquals()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[3, 3] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix4x4(d);
            var m1 = new Matrix4x4(d);

            double[,] d1 = new double[4, 4];
            d1[0, 0] = 1;
            d1[1, 1] = 1;
            d1[2, 2] = 1;
            d1[3, 3] = 1;
            d1[0, 2] = 4;
            d1[3, 1] = 3;
            var m2 = new Matrix4x4(d1);

            Assert.That(m == m1, Is.EqualTo(true));
            Assert.That(m == m2, Is.EqualTo(false));
        }
        [Test]
        public void TestNotEquals()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[3, 3] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix4x4(d);
            var m1 = new Matrix4x4(d);

            double[,] d1 = new double[4, 4];
            d1[0, 0] = 1;
            d1[1, 1] = 1;
            d1[2, 2] = 1;
            d1[3, 3] = 1;
            d1[0, 2] = 4;
            d1[3, 1] = 3;
            var m2 = new Matrix4x4(d1);

            Assert.That(m != m1, Is.EqualTo(false));
            Assert.That(m != m2, Is.EqualTo(true));
        }
    }
}
