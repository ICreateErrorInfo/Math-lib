using Math_lib;
using NUnit.Framework;
using Raytracing.Shapes;
using System;

namespace Raytracing.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void IntersectionTest()
        {
            var s = new Sphere(new(0, 0, 0), 2, new Metal(new(0, 0, 0), 1));

            Ray r = new Ray(new(-8,0,0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;

            Assert.That(s.Intersect(r, 0, out insec), Is.True);
        }
        [Test]
        public void IntersectionTest2()
        {
            var s = new Sphere(new(0, 0, 0), 2, new Metal(new(0, 0, 0), 1));

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            s.Intersect(r, 0, out insec);

            Assert.That(Point3D.Round((Point3D)(Vector3D)insec.Normal, 6), Is.EqualTo(Point3D.Round((Point3D)Vector3D.Normalize(new Vector3D(-0.5, 1.9364916731037, 0)), 6)));
        }
        [Test]
        public void IntersectionTest3()
        {
            Material m = new Metal(new(0, 0, 0), 1);
            var s = new Sphere(new(0, 0, 0), 2, m);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            s.Intersect(r, 0, out insec);

            Assert.That(Math.Round(insec.T, 6), Is.EqualTo(1));
        }
        [Test]
        public void IntersectionTest4()
        {
            Material m = new Metal(new(0, 0, 0), 1);
            var s = new Sphere(new(0, 0, 0), 2, m);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            s.Intersect(r, 0, out insec);

            Assert.That(insec.Material, Is.EqualTo(m));
        }
        [Test]
        public void IntersectionTest6()
        {
            Material m = new Metal(new(0, 0, 0), 1);
            var s = new Sphere(new(0, 0, 0), 2, m);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            s.Intersect(r, 0, out insec);

            Assert.That(Point3D.Round(insec.P, 6), Is.EqualTo(Point3D.Round(new Point3D(-0.5, 1.9364916731, 0), 6)));
        }
        [Test]
        public void IntersectionTest7()
        {
            Material m = new Metal(new(0, 0, 0), 1);
            var s = new Sphere(new(0, 0, 0), 2, m);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            s.Intersect(r, 0, out insec);

            Assert.That(r.O, Is.EqualTo(new Point3D(-8, 0, 0)));
            Assert.That(r.D, Is.EqualTo(new Vector3D(7.5, 1.9364916731037, 0)));
        }

        [Test]
        public void IntersectionTestTranslated()
        {
            var s = new Sphere(new(0, 1, 0), 2, new Metal(new(0, 0, 0), 1));

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;

            bool intersectionIsFound = s.Intersect(r, 0, out insec);

            Assert.That(intersectionIsFound, Is.True);
        }

        [Test]
        public void IntersectionTest8()
        {
            Vector3D u = new(4,-4, 0);
            Vector3D n = new(0, 1, 0);

            Assert.That(Vector3D.Reflect(u, n), Is.EqualTo(new Vector3D(4, 4, 0)));

            Vector3D u1 = new(4, 0, 0);

            Assert.That(Vector3D.Reflect(u1, n), Is.EqualTo(new Vector3D(4, 0, 0)));
        }
    }
}