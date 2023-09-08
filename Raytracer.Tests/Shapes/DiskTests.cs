using Moarx.Math;
using NUnit.Framework;
using Raytracing.Materials;
using Raytracing.Mathmatic;
using Raytracing.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Tests.Shapes {
    [TestFixture]
    public class DiskTests
    {
        [Test]
        public void TestCtor1()
        {
            var s = new Disk(new(0), 1, 1,0, 360);
            Assert.That(s, Is.Not.Null);
        }

        [Test]
        public void IntersectionTest()
        {
            var s = new Disk(new(0), 1, 1, 0, 360);

            Ray r = new Ray(new(0, 0, 0.5), new(0, 0, 1));

            SurfaceInteraction isect = new SurfaceInteraction();
            double tMax;

            Assert.That(s.Intersect(r, out tMax, out isect), Is.True);
        }
        [Test]
        public void IntersectionTest1()
        {
            var s = new Disk(new(0), 1, 1, 0, 360);

            Ray r = new Ray(new(0, 1, 0.5), new(0, 1, 0));

            SurfaceInteraction isect = new SurfaceInteraction();
            double tMax;

            Assert.That(s.Intersect(r, out tMax, out isect), Is.False);
        }

        [Test]
        public void TestObjectBound()
        {
            var s = new Disk(new(0), 1, 1, 0, 360);

            Assert.That(s.GetObjectBound().PMin, Is.EqualTo(new Point3D<double>(-1, -1, 1)));
            Assert.That(s.GetObjectBound().PMax, Is.EqualTo(new Point3D<double>(1, 1, 1)));
        }
        [Test]
        public void TestObjectBound2()
        {
            var s = new Disk(new(0), 1, 1, 0, 360);

            Assert.That(s.GetObjectBound().PMin, Is.EqualTo(new Point3D<double>(-1, -1, 1)));
            Assert.That(s.GetObjectBound().PMax, Is.EqualTo(new Point3D<double>(1, 1, 1)));
        }
    }
}
