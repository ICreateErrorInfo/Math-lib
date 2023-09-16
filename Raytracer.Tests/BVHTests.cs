using Moarx.Math;
using NUnit.Framework;
using Raytracing.Accelerators;
using Raytracing.Color;
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
            Raytracer r = new Raytracer(null, null, null);
            r.Init();

            var cs = RGBColorSpace.sRGB;
            List<Primitive> primitives = new List<Primitive>();
            
            var checker = new CheckerTexture(new RGBAlbedoSpectrum(cs, new(.2, .3, .1 )), new RGBAlbedoSpectrum(cs, new(.9, .9, .9 )), cs);

            var material2 = new Metal(new RGBAlbedoSpectrum(cs, new(0.90, 0.76, 0.46)), 0, cs);
            var material3 = new Metal(new RGBAlbedoSpectrum(cs, new(0.65, 0.77, 0.97)), 0, cs);
            var material4 = new Metal(new RGBAlbedoSpectrum(cs, new(0.90, 0.90, 0.90)), 0, cs);

            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0.0, -10004, -20), 10000), new Lambertian(checker)));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(5, 0, -15), 2), material2));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(5, 0, -25), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(2, 0, -18), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(-5.5, 0, -15), 3), material4));

            BVHAccelerator BVH = new BVHAccelerator(primitives, 4, BVHSplitMethod.SAH);
        }
        [Test]
        public void TestRecusive()
        {
            Raytracer r = new Raytracer(null, null, null);
            r.Init();

            var cs = RGBColorSpace.sRGB;
            List<Primitive> primitives = new List<Primitive>();

            var checker = new CheckerTexture(new RGBAlbedoSpectrum(cs, new(.2, .3, .1 )), new RGBAlbedoSpectrum(cs, new(.9, .9, .9 )),cs);

            var material2 = new Metal(new RGBAlbedoSpectrum(cs, new(0.90, 0.76, 0.46)), 0, cs);
            var material3 = new Metal(new RGBAlbedoSpectrum(cs, new(0.65, 0.77, 0.97)), 0, cs);
            var material4 = new Metal(new RGBAlbedoSpectrum(cs, new(0.90, 0.90, 0.90)), 0, cs);

            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0.0, -10004, -20), 10000), new Lambertian(checker)));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(5, 0, -15), 2), material2));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(5, 0, -25), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(2, 0, -18), 3), material3));
            primitives.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(-5.5, 0, -15), 3), material4));

            BVHAccelerator BVH = new BVHAccelerator(primitives, 4, BVHSplitMethod.Middle);
        }
    }
}
