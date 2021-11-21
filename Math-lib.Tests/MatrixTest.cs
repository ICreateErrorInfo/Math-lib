using System;

using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class MatrixTest
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
    }
}
