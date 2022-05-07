using Math_lib;
using NUnit.Framework;
using Raytracing.Accelerators;
using Raytracing.Materials;
using Raytracing.Shapes;
using System.Collections.Generic;

namespace Raytracing.Tests
{
    [TestFixture]
    internal class BVHTests
    {
        [Test]
        public void TestCtor1()
        {
            List<Primitive> primitives = new List<Primitive>();

            var checker = new CheckerTexture(new Vector3D(.2, .3, .1), new Vector3D(.9, .9, .9));

            var material = new Metal(new Vector3D(.7, .7, .7), 0.7);
            var material1 = new Metal(new Vector3D(1, 0.32, 0.36), 0);
            var material2 = new Metal(new Vector3D(0.90, 0.76, 0.46), 0);
            var material3 = new Metal(new Vector3D(0.65, 0.77, 0.97), 0);
            var material4 = new Metal(new Vector3D(0.90, 0.90, 0.90), 0);

            var center2 = new Point3D(0, 0, -20) + new Vector3D(0, 0.5, 0);

            //primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(0.0, -10004, -20), 10000), new Lambertian(checker)));
            //primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(5, 0, -15), 2), material2));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(5, 0, -25), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(2, 0, -18), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(-5.5, 0, -15), 3), material4));


            BVHAccelerator BVH = new BVHAccelerator(primitives, 0, BVHAccelerator.SplitMethod.SAH);
        }
    }
}
