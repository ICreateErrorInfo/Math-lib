using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;
using NUnit.Framework;
using Raytracing.Camera;
using Raytracing.Integrators;
using Raytracing.Materials;
using Raytracing.Primitives;
using Raytracing.Shapes;
using System;
using System.Threading;

namespace Raytracing.Tests {
    [TestFixture]
    internal class RandomWalkIntegratorTests {

        // Renders the "Simple Light" scene (see MainWindow.SimpleLight) at the given maxDepth
        // and returns the average pixel brightness.
        static double RenderSimpleLightAverageBrightness(int maxDepth, int width = 60, int height = 30, int spp = 200) {
            Raytracer r = new Raytracer(null, null, null);
            r.Init();

            var cs = RGBColorSpace.sRGB;

            PrimitiveList objekts = new PrimitiveList();
            var pertext = new NoiseTexture(4, cs);
            objekts.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0, -1000, 0), 1000), new Lambertian(pertext)));
            objekts.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0, 2.1, 0), 2), new Lambertian(pertext)));

            var difflight = new DiffuseLight(new RGBIlluminantSpectrum(cs, new(10, 10, 10)), cs);
            objekts.Add(new GeometricPrimitive(new XYRect(3, 5, 1, 3, -2), difflight));

            double aspectRatio = width / (double)height;
            Bounds2D<double> screen = new Bounds2D<double>(new Point2D<double>(-1, -1 / aspectRatio), new Point2D<double>(1, 1 / aspectRatio));

            Point3D<double> origin = new(26, 3, 6);
            Point3D<double> lookAt = new(0, 2, 0);
            CameraTransform cameraToWorld = new CameraTransform(Transform.LookAt(origin, lookAt, new(0, -1, 0)).Inverse());
            ICamera camera = new PerspectiveCamera(cameraToWorld, 0, 1, width, height, screen, 0, 0, 20, lookAt);

            Scene scene = new Scene(objs: objekts, spp: spp, maxD: maxDepth, camera,
                                    background: new RGBAlbedoSpectrum(cs, new(0, 0, 0)),
                                    imageWidth: width, imageHeight: height);

            var integrator = new RandomWalkIntegrator(scene.Camera, scene.Accel, cs, maxDepth);
            var progress = new Progress<Raytracer.ProgressData>(_ => { });
            integrator.Render(scene, progress, CancellationToken.None);

            var pixels = scene.Camera.Film.Pixel;
            double sum = 0;
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    sum += pixels[i, j].R + pixels[i, j].G + pixels[i, j].B;

            return sum / (width * height * 3 * spp);
        }

        [Test]
        public void MaxDepthLimitsIndirectBounces() {
            // Regression test for the depth++ post-increment bug: the recursive call in
            // LiRandomWalk always received the pre-increment depth, so MaxDepth was never
            // enforced. In this scene (no ambient/background light), a maxDepth=1 render
            // should only capture direct light hits and be noticeably darker than a
            // maxDepth=50 render, which also gathers indirect bounce lighting.
            double depth1 = RenderSimpleLightAverageBrightness(maxDepth: 1);
            double depth50 = RenderSimpleLightAverageBrightness(maxDepth: 50);

            // Indirect contribution in this scene is modest (~10% at spp=200), so the threshold
            // only needs enough margin over sampling noise to catch a regression back to "no
            // difference at all", which is what the depth++ bug produced.
            Assert.That(depth50, Is.GreaterThan(depth1 * 1.05),
                $"Expected maxDepth=50 (avg={depth50}) to be meaningfully brighter than maxDepth=1 (avg={depth1}) " +
                "due to indirect bounces; near-equal brightness means MaxDepth is not being enforced.");
        }
    }
}
