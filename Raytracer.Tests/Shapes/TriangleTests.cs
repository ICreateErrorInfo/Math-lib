using Moarx.Math;
using NUnit.Framework;
using Raytracing.Materials;
using Raytracing.Mathmatic;
using Raytracing.Primitives;
using Raytracing.Shapes;
using System.Collections.Generic;
using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;

namespace Raytracing.Tests.Shapes {
    [TestFixture]
    public class TriangleTests
    {
        [Test]
        public void TestCtor1()
        {
            var s = new Triangle(Transform.Translate(new(0)), Transform.Translate(new(0)), new TriangleMesh(), 1);
            Assert.That(s, Is.Not.Null);
        }

        [Test]
        public void IntersectionTest()
        {
            SampledSpectrumConstants.Init();

            var cs = RGBColorSpace.sRGB;
            var material = new Metal(new RGBAlbedoSpectrum(cs, new(.65, .7, .46 )), 0, cs);
            int nTri = 1;
            List<int> indices = new List<int>() { 0, 1, 2 };
            int nVert = 3;
            List<Point3D<double>> Points = new List<Point3D<double>>() { new(1, 0, 0), new(0, 1, 0), new(0, 0, 1) };

            Transform objToWorld = Transform.Translate(new Vector3D<double>(0, 1, 0));
            Transform worldToObj = Transform.Translate(new Vector3D<double>(0, -1, 0));

            TriangleMesh mesh = new TriangleMesh(objToWorld, nTri, indices, nVert, Points, material);

            Point3D<double> p0 = worldToObj * mesh.Point[0];
            Point3D<double> p1 = worldToObj * mesh.Point[1];
            Point3D<double> p2 = worldToObj * mesh.Point[2];

            Assert.That(p0, Is.EqualTo(new Point3D<double>(1,0,0)));
            Assert.That(p1, Is.EqualTo(new Point3D<double>(0,1,0)));
            Assert.That(p2, Is.EqualTo(new Point3D<double>(0,0,1)));
        }
        [Test]
        public void IntersectionTest1()
        {
            SampledSpectrumConstants.Init();
            var cs = RGBColorSpace.sRGB;

            var material = new Metal(new RGBAlbedoSpectrum(cs, new(.65, .7, .46 )), 0, cs);

            int nTri = 1;
            List<int> indices = new List<int>() { 0, 1, 2 };
            int nVert = 3;
            List<Point3D<double>> Points = new List<Point3D<double>>() { new(-2, 0, 0), new(2, 0, 0), new(0, 2, 0) };

            Transform objToWorld = Transform.Translate(new Vector3D<double>(0));
            Transform worldToObj = Transform.Translate(new Vector3D<double>(0));

            TriangleMesh mesh = new TriangleMesh(objToWorld, nTri, indices, nVert, Points, material);

            Triangle tri = new Triangle(objToWorld, worldToObj, mesh, 0);

            Ray ray = new Ray(new(0,1,3), new(0,0,-1));

            SurfaceInteraction surfaceInteraction = new SurfaceInteraction();
            double tMax;
            bool hit = tri.Intersect(ray, out tMax, out surfaceInteraction);

            Assert.That(hit, Is.True);
        }
        [Test]
        public void IntersectionTest2()
        {
            SampledSpectrumConstants.Init();
            var cs = RGBColorSpace.sRGB;

            var material = new Metal(new RGBAlbedoSpectrum(cs, new( .65, .7, .46 )), 0, cs);

            int nTri = 1;
            List<int> indices = new List<int>() { 0, 1, 2 };
            int nVert = 3;
            List<Point3D<double>> Points = new List<Point3D<double>>() { new(-2, 0, 0), new(2, 0, 0), new(0, 2, 0) };

            Transform objToWorld = Transform.Translate(new Vector3D<double>(0));
            Transform worldToObj = Transform.Translate(new Vector3D<double>(0));

            TriangleMesh mesh = new TriangleMesh(objToWorld, nTri, indices, nVert, Points, material);

            Triangle tri = new Triangle(objToWorld, worldToObj, mesh, 0);

            Ray ray = new Ray(new(0, 0, -0.5), new(-0.69, 0.84, 0.5));

            SurfaceInteraction surfaceInteraction = new SurfaceInteraction();
            double tMax;
            bool hit = tri.Intersect(ray, out tMax, out surfaceInteraction);

            Assert.That(hit, Is.True);
        }

        [Test]
        public void TestObjectBound()
        {
            var cs = RGBColorSpace.sRGB;

            TriangleMesh mesh = new TriangleMesh(Transform.Translate(new(0)), 1, new List<int> { 0,1,2 }, 3, new List<Point3D<double>> { new(-2, 3, 0), new(2, 0, 0), new(-2, -1, 1) }, new Metal(new RGBAlbedoSpectrum(cs, new(0,0,0)), 1, cs));
            var t = new Triangle(Transform.Translate(new(0)), Transform.Translate(new(0)), mesh, 0);

            Assert.That(t.GetObjectBound().PMin, Is.EqualTo(new Point3D<double>(-2, -1, 0)));
            Assert.That(t.GetObjectBound().PMax, Is.EqualTo(new Point3D<double>(2, 3, 1)));
        }
        [Test]
        public void TestObjectBound2()
        {
            var cs = RGBColorSpace.sRGB;

            TriangleMesh mesh = new TriangleMesh(Transform.Translate(new(1)), 1, new List<int> { 0, 1, 2 }, 3, new List<Point3D<double>> { new(-2, 3, 0), new(2, 0, 0), new(-2, -1, 1) }, new Metal(new RGBAlbedoSpectrum(cs, new(0,0,0)), 1, cs));
            var t = new Triangle(Transform.Translate(new(1)), Transform.Translate(new(-1)), mesh, 0);

            Assert.That(t.GetObjectBound().PMin, Is.EqualTo(new Point3D<double>(-2, -1, 0)));
            Assert.That(t.GetObjectBound().PMax, Is.EqualTo(new Point3D<double>(2, 3, 1)));
        }
    }
}
