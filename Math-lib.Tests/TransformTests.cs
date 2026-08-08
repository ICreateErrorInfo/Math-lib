using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class TransformTests
    {
        [Test]
        public void TestDefaultCtorIsIdentity()
        {
            var t = new Transform();

            Assert.That(t.IsIdentity(), Is.True);
            Assert.That(t.m[0, 0], Is.EqualTo(1));
            Assert.That(t.m[1, 1], Is.EqualTo(1));
            Assert.That(t.m[2, 2], Is.EqualTo(1));
            Assert.That(t.m[3, 3], Is.EqualTo(1));

            Assert.That(t.mInv[0, 0], Is.EqualTo(1));
            Assert.That(t.mInv[1, 1], Is.EqualTo(1));
            Assert.That(t.mInv[2, 2], Is.EqualTo(1));
            Assert.That(t.mInv[3, 3], Is.EqualTo(1));
        }
    }
}
