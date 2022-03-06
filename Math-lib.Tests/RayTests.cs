

using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class RayTests
    {
        [Test]
        public void TestSyntax()
        {
            var r = new Ray { TMax = 100, Time = 0 };

            Assert.That(r.TMax, Is.EqualTo(100));
            Assert.That(r.Time, Is.EqualTo(0));
        }
        [Test]
        public void TestEmptyRay()
        {
            var r = new Ray();
            Assert.That(r.O, Is.EqualTo(new Point3D(0,0,0)));
            Assert.That(r.D, Is.EqualTo(new Vector3D(0,0,0)));
            Assert.That(r.TMax, Is.EqualTo(double.PositiveInfinity));
            Assert.That(r.Time, Is.EqualTo(0));
        }
        [Test]
        public void TestCtor1()
        {
            Ray r = new Ray(new Point3D(1,1,1), new Vector3D(2,2,2), 1, 10);

            Assert.That(r.O, Is.EqualTo(new Point3D(1,1,1)));
            Assert.That(r.D, Is.EqualTo(new Vector3D(2,2,2)));
            Assert.That(r.TMax, Is.EqualTo(1));
            Assert.That(r.Time, Is.EqualTo(10));
        }

        [Test]
        public void TestAt()
        {
            Ray r = new Ray(new Point3D(1, 1, 1), new Vector3D(2, 2, 2), 1, 10);

            Assert.That(r.At(1), Is.EqualTo(new Point3D(3,3,3)));
        }
    }
}
