using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;
using Raytracing.Camera;
using Raytracing.Primitives;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Raytracing.Raytracer;

namespace Raytracing.Integrators;
public abstract class ImageTileIntegrator: IIntegrator {

    private protected ICamera _camera;
    private protected ISpectrum background;

    public ImageTileIntegrator(ICamera camera, Primitive aggregate, RGBColorSpace colorspace) : base(aggregate, colorspace) {
        _camera = camera;
    }

    public override void Render(Scene scene, IProgress<ProgressData> progress) {
        Bounds2D<int> pixelBounds = new(new((int)_camera.ResolutionWidth, (int)_camera.ResolutionHeight), new(0,0)); //TODO
        int spp = scene.SamplesPerPixel;
        background = scene.Background;

        int totalCount = spp;
        var current = 0;
        progress.Report(new ProgressData(totalCount, current));

        int waveStart = 0, waveEnd = 1, nextWaveSize = 1;

        while(waveStart < spp) {
            Parallel.For(0, pixelBounds.PMax.X, i => {
                //for (int i = 0; i < pixelBounds.PMax.X; i++) {
                for (int j = 0; j < pixelBounds.PMax.Y; j++) {
                    Point2D<int> currentPixel = new Point2D<int>(i, j);

                    for (int sampleIndex = waveStart; sampleIndex < waveEnd; ++sampleIndex) {

                        EvaluatePixelSample(currentPixel, sampleIndex);
                    }
                }

            }
            );

            progress.Report(new(totalCount, waveEnd - waveStart));

            waveStart = waveEnd;
            waveEnd = System.Math.Min(spp, waveEnd + nextWaveSize);
            nextWaveSize = System.Math.Min(2 * nextWaveSize, 64);
        }
    }

    public abstract void EvaluatePixelSample(Point2D<int> pixel, int sampleIndex);
}
