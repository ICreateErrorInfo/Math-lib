using Math_lib;
using NUnit.Framework;
using Raytracing.Accelerators;
using Raytracing.Materials;
using Raytracing.Primitives;
using Raytracing.Shapes;
using Raytracing.Spectrum;
using System.Collections.Generic;

namespace Raytracing.Tests {
    [TestFixture]
    internal class BVHTests
    {
        [Test]
        public void TestCtor1()
        {
            SpectrumFactory factory = new SampledSpectrumFactory();

            Raytracer r = new Raytracer(factory, null, null, null);
            r.Init();
            List<Primitive> primitives = new List<Primitive>();

            var checker = new CheckerTexture(factory, factory.CreateFromRGB(new double[] {.2, .3, .1 }, SpectrumMaterialType.Reflectance), factory.CreateFromRGB(new double[] {.9, .9, .9 }, SpectrumMaterialType.Reflectance));

            var material2 = new Metal(factory, factory.CreateFromRGB(new double[] {0.90, 0.76, 0.46}, SpectrumMaterialType.Reflectance), 0);
            var material3 = new Metal(factory, factory.CreateFromRGB(new double[] {0.65, 0.77, 0.97}, SpectrumMaterialType.Reflectance), 0);
            var material4 = new Metal(factory, factory.CreateFromRGB(new double[] {0.90, 0.90, 0.90}, SpectrumMaterialType.Reflectance), 0);

            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(0.0, -10004, -20), 10000), new Lambertian(checker)));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(5, 0, -15), 2), material2));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(5, 0, -25), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(2, 0, -18), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(-5.5, 0, -15), 3), material4));

            BVHAccelerator BVH = new BVHAccelerator(primitives, 4, BVHSplitMethod.SAH);
        }
        [Test]
        public void TestRecusive()
        {
            SpectrumFactory factory = new SampledSpectrumFactory();

            Raytracer r = new Raytracer(factory, null, null, null);
            r.Init();
            List<Primitive> primitives = new List<Primitive>();

            var checker = new CheckerTexture(factory, factory.CreateFromRGB(new double[] {.2, .3, .1 }, SpectrumMaterialType.Reflectance), factory.CreateFromRGB(new double[] {.9, .9, .9 }, SpectrumMaterialType.Reflectance));

            var material2 = new Metal(factory, factory.CreateFromRGB(new double[] {0.90, 0.76, 0.46}, SpectrumMaterialType.Reflectance), 0);
            var material3 = new Metal(factory, factory.CreateFromRGB(new double[] {0.65, 0.77, 0.97}, SpectrumMaterialType.Reflectance), 0);
            var material4 = new Metal(factory, factory.CreateFromRGB(new double[] {0.90, 0.90, 0.90}, SpectrumMaterialType.Reflectance), 0);

            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(0.0, -10004, -20), 10000), new Lambertian(checker)));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(5, 0, -15), 2), material2));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(5, 0, -25), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(2, 0, -18), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D(-5.5, 0, -15), 3), material4));

            BVHAccelerator BVH = new BVHAccelerator(primitives, 4, BVHSplitMethod.Middle);
        }
    }
}
