using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;

namespace Raytracing;
public class Film {
    public RGB[,] Pixel;

    public Film(int width, int height) {
        Pixel = new RGB[width, height];
    }

    public void AddSample(RGB l, Point2D<int> currentpixel) {

        if (Pixel[currentpixel.X, currentpixel.Y] is null) {
            Pixel[currentpixel.X, currentpixel.Y] = l;
        } else {
            Pixel[currentpixel.X, currentpixel.Y] = Pixel[currentpixel.X, currentpixel.Y] + l;
        }

    }

    public RGB ToSensorRGB(SampledSpectrum l, SampledWavelengths lambda) {
        l = SampledSpectrum.SafeDiv(l, lambda.PDF());

        return new RGB((SampledSpectrumConstants.X.Sample(lambda) * l).Average(),
                       (SampledSpectrumConstants.Y.Sample(lambda) * l).Average(),
                       (SampledSpectrumConstants.Z.Sample(lambda) * l).Average());
    }
}
