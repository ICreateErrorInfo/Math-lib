using System;

using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class MatrixTests
    {
        [Test]
        public void TestCtor1()
        {
            var m = new Matrix(2,2);

            Assert.That(m[0,0], Is.EqualTo(1));
            Assert.That(m[1,0], Is.EqualTo(0));
            Assert.That(m[0,1], Is.EqualTo(0));
            Assert.That(m[1,1], Is.EqualTo(1));
        }
        [Test]
        public void TestCtor2()
        {
            double[,] d = new double[2,3];
            d[0, 0] = 0;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix(d);

            Assert.That(m[0, 0], Is.EqualTo(0));
            Assert.That(m[1, 0], Is.EqualTo(2));
            Assert.That(m[0, 1], Is.EqualTo(0));
            Assert.That(m[1, 2], Is.EqualTo(4));
        }
        [Test]
        public void TestCtor3()
        {
            double[] d = new double[3];
            d[0] = 0;
            d[1] = 2;
            d[2] = 4;
            var m = new Matrix(d);

            Assert.That(m[0, 0], Is.EqualTo(0));
            Assert.That(m[0, 1], Is.EqualTo(2));
            Assert.That(m[0, 2], Is.EqualTo(4));
        }


        [Test]
        public void TestIdentity()
        {
            var m = new Matrix(new double[,] { { 1, 2, 3 },
                                               { 4, 5, 6 },
                                               { 7, 8, 9 } });
            m.Identity();

            Assert.That(m[0, 0], Is.EqualTo(1));
            Assert.That(m[0, 1], Is.EqualTo(0));
            Assert.That(m[0, 2], Is.EqualTo(0));
            Assert.That(m[2, 2], Is.EqualTo(1));
            Assert.That(m[1, 1], Is.EqualTo(1));
        }


        [Test]
        public void TestMulMP3x3()
        {
            double[,] d = new double[3, 3];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix(d);

            Point3D p = new Point3D(1, 2, 3);

            var erg = m * p;

            Assert.That(erg.X, Is.EqualTo(1));
            Assert.That(erg.Y, Is.EqualTo(16));
            Assert.That(erg.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestMulMP4x4()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[3, 3] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix(d);

            Point3D p = new Point3D(1, 2, 3);

            var erg = m * p;

            Assert.That(erg.X, Is.EqualTo(1));
            Assert.That(erg.Y, Is.EqualTo(16));
            Assert.That(erg.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestMulMV3x3()
        {
            double[,] d = new double[3, 3];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix(d);

            Vector3D p = new Vector3D(1, 2, 3);

            var erg = m * p;

            Assert.That(erg.X, Is.EqualTo(1));
            Assert.That(erg.Y, Is.EqualTo(16));
            Assert.That(erg.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestMulMV4x4()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[3, 3] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix(d);

            Vector3D p = new Vector3D(1, 2, 3);

            var erg = m * p;

            Assert.That(erg.X, Is.EqualTo(1));
            Assert.That(erg.Y, Is.EqualTo(16));
            Assert.That(erg.Z, Is.EqualTo(3));
        }
        [Test]
        public void TestMulMM4x4()
        {
            double[,] d = new double[4, 4];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[3, 3] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix(d);

            double[,] d1 = new double[4, 4];
            d1[0, 0] = 1;
            d1[1, 1] = 1;
            d1[2, 2] = 1;
            d1[3, 3] = 1;
            d1[0, 2] = 4;
            d1[3, 1] = 3;
            var m1 = new Matrix(d1);

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
        public void TestMulMM3x3()
        {
            double[,] d = new double[3, 3];
            d[0, 0] = 1;
            d[1, 1] = 1;
            d[2, 2] = 1;
            d[1, 0] = 2;
            d[1, 2] = 4;
            var m = new Matrix(d);

            double[,] d1 = new double[3, 3];
            d1[0, 0] = 1;
            d1[1, 1] = 1;
            d1[2, 2] = 1;
            d1[0, 2] = 4;
            var m1 = new Matrix(d1);

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
        [Test]
        public void TestMulMM()
        {
            Matrix m = new Matrix(new double[,] { { 3, 5, -1 },
                                                  { 4, -8, 2 } });

            Matrix m1 = new Matrix(new double[,] { { 0,  3, 1 },
                                                   { 6,  5, 0 },
                                                   { 2, -7, 3 } });

            var erg = m * m1;

            Assert.That(erg[0, 0], Is.EqualTo(28));
            Assert.That(erg[0, 1], Is.EqualTo(41));
            Assert.That(erg[0, 2], Is.EqualTo(0));

            Assert.That(erg[1, 0], Is.EqualTo(-44));
            Assert.That(erg[1, 1], Is.EqualTo(-42));
            Assert.That(erg[1, 2], Is.EqualTo(10));
        }
        [Test]
        public void TestInverse() {
            Matrix m = Transform.Perspective(20, 1e-2f, 1000).m;
            Matrix mInv = Matrix.Inverse4x4(m);

            Matrix m2 = new Matrix(4,4);
            m2.Identity();

            for (int i = 0; i < 4; i++) {
                for(int j = 0; j < 4; j++) {
                    Assert.That(Math.Round((m * mInv)[i,j], 6), Is.EqualTo(m2[i, j]));
                }
            }
        }
    }
}
