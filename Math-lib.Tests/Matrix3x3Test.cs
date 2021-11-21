using System;

using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class Matrix3x3Test
    {
        [Test]
        public void TestCtor1()
        {
            var m = new Matrix3x3();

            Assert.That(m[0, 0], Is.EqualTo(1));
            Assert.That(m[1, 0], Is.EqualTo(0));
            Assert.That(m[0, 1], Is.EqualTo(0));
            Assert.That(m[1, 1], Is.EqualTo(1));
        }
        [Test]
        public void TestCtor2()
        {
            double[,] d = new double[3, 3];
            d[0, 0] = 0;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix3x3(d);

            Assert.That(m[0, 0], Is.EqualTo(0));
            Assert.That(m[1, 0], Is.EqualTo(2));
            Assert.That(m[0, 1], Is.EqualTo(0));
            Assert.That(m[1, 2], Is.EqualTo(4));
        }


        [Test]
        public void TestMulMP()
        {
            double[,] d = new double[3, 3];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix3x3(d);

            Point3D p = new Point3D(1, 2, 3);

            var erg = m * p;

            Assert.That(erg.X, Is.EqualTo(1));
            Assert.That(erg.Y, Is.EqualTo(16));
            Assert.That(erg.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestMulMV()
        {
            double[,] d = new double[3, 3];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix3x3(d);

            Vector3D p = new Vector3D(1, 2, 3);

            var erg = m * p;

            Assert.That(erg.X, Is.EqualTo(1));
            Assert.That(erg.Y, Is.EqualTo(16));
            Assert.That(erg.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestMulMM()
        {
            double[,] d = new double[3, 3];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix3x3(d);

            double[,] d1 = new double[3, 3];
            d1[0, 0] = 1;
            d1[1, 1] = 1;
            d1[2, 2] = 1;
            d1[0, 2] = 4;
            var m1 = new Matrix3x3(d1);

            var erg = m * m1;

            Assert.That(erg[0, 0], Is.EqualTo(1));
            Assert.That(erg[0, 1], Is.EqualTo(0));
            Assert.That(erg[0, 2], Is.EqualTo(4));

            Assert.That(erg[1, 0], Is.EqualTo(2));
            Assert.That(erg[1, 1], Is.EqualTo(1));
            Assert.That(erg[1, 2], Is.EqualTo(12));

            Assert.That(erg[2, 0], Is.EqualTo(0));
            Assert.That(erg[2, 1], Is.EqualTo(0));
            Assert.That(erg[2, 2], Is.EqualTo(1));
        }
    }
}
