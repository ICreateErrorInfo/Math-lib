using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;
using Raytracing.Camera;
using Raytracing.Primitives;

namespace Raytracing.Integrators;
public abstract class RayIntegrator: ImageTileIntegrator {

    public RayIntegrator(ICamera camera, Primitive aggregate, RGBColorSpace colorspace) : base(camera, aggregate, colorspace) {
        
    }

    public override void EvaluatePixelSample(Point2D<int> pixel, int sampleIndex) {

        SampledWavelengths lambda = SampledWavelengths.SampleUnifrom(MathmaticMethods.GetRandomDouble(0, 1));

        CameraSample sample = new CameraSample() { 
                                                    pointOnFilm = new(pixel.X + MathmaticMethods.GetRandomDouble(0, 1), pixel.Y + MathmaticMethods.GetRandomDouble(0, 1)),
                                                    pointOnLense = new(MathmaticMethods.GetRandomDouble(0, 1), MathmaticMethods.GetRandomDouble(0, 1))
                                                 };

        CameraRayInformation cameraRay = _camera.GenerateRay(sample);

        SampledSpectrum L = new SampledSpectrum(0);

        L = cameraRay.arrivedRadiance * Li(cameraRay.generatedRay, lambda);

        _camera.Film.AddSample(L.ToRGB(lambda, _colorspace), pixel);
    }

    public abstract SampledSpectrum Li(Ray ray, SampledWavelengths lambda);
}
