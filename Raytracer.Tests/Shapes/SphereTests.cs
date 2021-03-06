using Math_lib;
using NUnit.Framework;
using Raytracing.Materials;
using Raytracing.Shapes;
using System;

namespace Raytracing.Tests.Shapes
{
    [TestFixture]
    public class SphereTests
    {
        [Test]
        public void TestCtor1()
        {
            var s = new Sphere(new(0,2,1), 1);
            Assert.That(s, Is.Not.Null);
        }
        [Test]
        public void TestCtor2()
        {
            var s = new Sphere(new(0, 2, 1), 1, -1, 1, 360);
            Assert.That(s, Is.Not.Null);
        }

        [Test]
        public void IntersectionTest()
        {
            var s = new Sphere(new(0, 0, 0), 2);

            Ray r = new Ray(new(-8,0,0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            double tMax;

            Assert.That(s.Intersect(r, out tMax, out insec), Is.True);
        }
        [Test]
        public void IntersectionTest2()
        {
            var s = new Sphere(new(0, 0, 0), 2);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            double tMax;
            s.Intersect(r, out tMax, out insec);

            Assert.That(Point3D.Round((Point3D)(Vector3D)insec.Normal, 6), Is.EqualTo(Point3D.Round((Point3D)Vector3D.Normalize(new Vector3D(-0.5, 1.9364916731037, 0)), 6)));
        }
        [Test]
        public void IntersectionTest3()
        {
            var s = new Sphere(new(0, 0, 0), 2);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            double tMax;
            s.Intersect(r, out tMax, out insec);

            Assert.That(Math.Round(tMax, 6), Is.EqualTo(1));
        }
        [Test]
        public void IntersectionTest6()
        {
            var s = new Sphere(new(0, 0, 0), 2);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            double tMax;
            s.Intersect(r, out tMax, out insec);

            Assert.That(Point3D.Round(insec.P, 6), Is.EqualTo(Point3D.Round(new Point3D(-0.5, 1.9364916731, 0), 6)));
        }
        [Test]
        public void IntersectionTest7()
        {
            var s = new Sphere(new(0, 0, 0), 2);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            double tMax;
            s.Intersect(r, out tMax, out insec);

            Assert.That(r.O, Is.EqualTo(new Point3D(-8, 0, 0)));
            Assert.That(r.D, Is.EqualTo(new Vector3D(7.5, 1.9364916731037, 0)));
        }
        [Test]
        public void IntersectionTestTranslated()
        {
            var s = new Sphere(new(0, 1, 0), 2);

            Ray r = new Ray(new(-8, 0, 0), new(7.5, 1.9364916731037, 0));
            SurfaceInteraction insec;
            double tMax;

            bool intersectionIsFound = s.Intersect(r, out tMax, out insec);

            Assert.That(intersectionIsFound, Is.True);
        }
        [Test]
        public void TestObjectBound()
        {
            var s = new Sphere(new(0, 1, 0), 2);

            Assert.That(s.GetObjectBound().pMin, Is.EqualTo(new Point3D(-2,-2,-2)));
            Assert.That(s.GetObjectBound().pMax, Is.EqualTo(new Point3D(2,2,2)));
        }
        [Test]
        public void TestObjectBound2()
        {
            var s = new Sphere(new(0, 1, 0), 2, -1, 1, 360);

            Assert.That(s.GetObjectBound().pMin, Is.EqualTo(new Point3D(-2, -2, -1)));
            Assert.That(s.GetObjectBound().pMax, Is.EqualTo(new Point3D(2, 2, 1)));
        }

    }
}