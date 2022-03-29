using Math_lib;
using NUnit.Framework;
using Raytracing.Materials;
using Raytracing.Shapes;

namespace Raytracing.Tests.Shapes
{
    [TestFixture]
    public class ConeTests
    {
        [Test]
        public void TestCtor1()
        {
            var s = new Cone(new(0),1,1,360, new Metal(new(0, 0, 0), 1));
            Assert.That(s, Is.Not.Null);
        }

        [Test]
        public void IntersectionTest()
        {
            var s = new Cone(new(0), 1, 1, 360, new Metal(new(0, 0, 0), 1));

            Ray r = new Ray(new(0,0.2,0.5), new(0,1,0));

            SurfaceInteraction isect = new SurfaceInteraction();

            Assert.That(s.Intersect(r, 0.1, out isect), Is.True);
        }
        [Test]
        public void IntersectionTest1()
        {
            var s = new Cone(new(0), 1, 1, 360, new Metal(new(0, 0, 0), 1));

            Ray r = new Ray(new(0, 1, 0.5), new(0, 1, 0));

            SurfaceInteraction isect = new SurfaceInteraction();

            Assert.That(s.Intersect(r, 0.1, out isect), Is.False);
        }

        [Test]
        public void TestObjectBound()
        {
            var s = new Cone(new(0), 1, 1, 360, new Metal(new(0, 0, 0), 1));


            Assert.That(s.GetObjectBound().pMin, Is.EqualTo(new Point3D(-1,-1,0)));
            Assert.That(s.GetObjectBound().pMax, Is.EqualTo(new Point3D( 1, 1,1)));
        }
        [Test]
        public void TestObjectBound2()
        {
            var s = new Cone(new(1), 1, 1, 360, new Metal(new(0, 0, 0), 1));


            Assert.That(s.GetObjectBound().pMin, Is.EqualTo(new Point3D(-1, -1, 0)));
            Assert.That(s.GetObjectBound().pMax, Is.EqualTo(new Point3D(1, 1, 1)));
        }
    }
}
