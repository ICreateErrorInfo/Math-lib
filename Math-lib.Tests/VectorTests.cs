using System;

using NUnit.Framework;

namespace Math_lib.Tests {

    [TestFixture]
    public class VectorTests {

        [Test]
        public void TestSyntax() {
            var v = new Vector {X = 3, Y = 2, Z = 1};

            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(2));
            Assert.That(v.Z, Is.EqualTo(1));
        }

        [Test]
        public void TestEmptyVector() {

            var v = new Vector();
            Assert.That(v.X, Is.EqualTo(0));
            Assert.That(v.Y, Is.EqualTo(0));
            Assert.That(v.Z, Is.EqualTo(0));
        }

        [Test]
        public void TestCtor1() {
            var v = new Vector(3);
            Assert.That(v.X, Is.EqualTo(3));
            Assert.That(v.Y, Is.EqualTo(3));
            Assert.That(v.Z, Is.EqualTo(3));
        }

        [Test]
        public void TestCtor2() {
            var v = new Vector(1, 2, 3);
            Assert.That(v.X, Is.EqualTo(1));
            Assert.That(v.Y, Is.EqualTo(2));
            Assert.That(v.Z, Is.EqualTo(3));
        }

        [Test]
        public void TestGetLength() {
            var v = new Vector(3, 4, 5);
            Assert.That(v.GetLength(), Is.EqualTo(Math.Sqrt(50)));
        }

        [Test]
        public void TestGetLengthSqrt() {
            var v = new Vector(3, 4, 5);
            Assert.That(v.GetLengthSqrt(), Is.EqualTo(50));
        }

    }

}