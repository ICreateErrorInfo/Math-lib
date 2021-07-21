using NUnit.Framework;

namespace Math_lib.Tests {

    [TestFixture]
    public class VectorTests {

        [Test]
        public void TestEmptyVector() {

            var v = new Vector();
            Assert.That(v.X, Is.EqualTo(0));
            Assert.That(v.Y, Is.EqualTo(0));
            Assert.That(v.Z, Is.EqualTo(0));
        }
    }
}
