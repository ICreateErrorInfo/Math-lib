using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;

namespace Raytracing;
public class Film {
    public RGB[,] Pixel;

    public Film(int width, int height) {
        Pixel = new RGB[width, height];
    }

    public void AddSample(RGB L, Point2D<int> currentpixel) {        

        if (Pixel[currentpixel.X, currentpixel.Y] is null) {
            Pixel[currentpixel.X, currentpixel.Y] = L;
        } else {
            Pixel[currentpixel.X, currentpixel.Y] = Pixel[currentpixel.X, currentpixel.Y] + L;
        }

    }
}
