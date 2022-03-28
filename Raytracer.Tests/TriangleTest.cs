using Math_lib;
using NUnit.Framework;
using Raytracing.Materials;
using Raytracing.Shapes;
using System.Collections.Generic;

namespace Raytracing.Tests
{
    [TestFixture]
    public class TriangleTest
    {
        [Test]
        public void IntersectionTest()
        {
            var material = new Metal(new Vector3D(.65, .7, .46), 0);
            int nTri = 1;
            List<int> indices = new List<int>() { 0, 1, 2 };
            int nVert = 3;
            List<Point3D> Points = new List<Point3D>() { new(1, 0, 0), new(0, 1, 0), new(0, 0, 1) };

            Transform objToWorld = Transform.Translate(new Vector3D(0, 1, 0));
            Transform worldToObj = Transform.Translate(new Vector3D(0, -1, 0));

            TriangleMesh mesh = new TriangleMesh(objToWorld, nTri, indices, nVert, Points, material);

            Point3D p0 = worldToObj.m * mesh.Point[0];
            Point3D p1 = worldToObj.m * mesh.Point[1];
            Point3D p2 = worldToObj.m * mesh.Point[2];

            Assert.That(p0, Is.EqualTo(new Point3D(1,0,0)));
            Assert.That(p1, Is.EqualTo(new Point3D(0,1,0)));
            Assert.That(p2, Is.EqualTo(new Point3D(0,0,1)));
        }
        [Test]
        public void IntersectionTest1()
        {
            var material = new Metal(new Vector3D(.65, .7, .46), 0);

            int nTri = 1;
            List<int> indices = new List<int>() { 0, 1, 2 };
            int nVert = 3;
            List<Point3D> Points = new List<Point3D>() { new(-2, 0, 0), new(2, 0, 0), new(0, 2, 0) };

            Transform objToWorld = Transform.Translate(new Vector3D(0));
            Transform worldToObj = Transform.Translate(new Vector3D(0));

            TriangleMesh mesh = new TriangleMesh(objToWorld, nTri, indices, nVert, Points, material);

            Triangle tri = new Triangle(objToWorld, worldToObj, mesh, 0);

            Ray ray = new Ray(new(0,1,3), new(0,0,-1));

            SurfaceInteraction surfaceInteraction = new SurfaceInteraction();
            bool hit = tri.Intersect(ray, 0, out surfaceInteraction);

            Assert.That(hit, Is.True);
        }
        [Test]
        public void IntersectionTest2()
        {
            var material = new Metal(new Vector3D(.65, .7, .46), 0);

            int nTri = 1;
            List<int> indices = new List<int>() { 0, 1, 2 };
            int nVert = 3;
            List<Point3D> Points = new List<Point3D>() { new(-2, 0, 0), new(2, 0, 0), new(0, 2, 0) };

            Transform objToWorld = Transform.Translate(new Vector3D(0));
            Transform worldToObj = Transform.Translate(new Vector3D(0));

            TriangleMesh mesh = new TriangleMesh(objToWorld, nTri, indices, nVert, Points, material);

            Triangle tri = new Triangle(objToWorld, worldToObj, mesh, 0);

            Ray ray = new Ray(new(0, 0, -0.5), new(-0.69, 0.84, 0.5));

            SurfaceInteraction surfaceInteraction = new SurfaceInteraction();
            bool hit = tri.Intersect(ray, 0, out surfaceInteraction);

            Assert.That(hit, Is.True);
        }
    }
}
