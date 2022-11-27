using System;
using Math_lib;
using Math_lib.Spectrum;

namespace Raytracing.Materials
{
    public abstract class Texture
    {
        public abstract SampledSpectrum Value(double u, double v, Point3D p);
    }
    class SolidColor : Texture
    {
        private readonly SampledSpectrum _colorValue;

        public SolidColor()
        {

        }
        public SolidColor(SampledSpectrum c)
        {
            _colorValue = c;
        }
        public SolidColor(double red, double green, double blue)
        {
            _colorValue = SampledSpectrum.FromRGB(new double[] { red, green, blue }, SampledSpectrum.SpectrumType.Reflectance);
        }

        public override SampledSpectrum Value(double u, double v, Point3D p)
        {
            return _colorValue;
        }
    }
    public class CheckerTexture : Texture
    {
        private readonly Texture _odd;
        private readonly Texture _even;

        public CheckerTexture()
        {

        }
        public CheckerTexture(Texture even, Texture odd)
        {
            _even = even;
            _odd = odd;
        }
        public CheckerTexture(SampledSpectrum c1, SampledSpectrum c2)
        {
            _even = new SolidColor(c1);
            _odd = new SolidColor(c2);
        }

        public override SampledSpectrum Value(double u, double v, Point3D p)
        {
            var sines = Math.Sin(10 * p.X) * Math.Sin(10 * p.Y) * Math.Sin(10 * p.Z);
            if(sines < 0)
            {
                return _odd.Value(u, v, p);
            }
            else
            {
                return _even.Value(u, v, p);
            }
        }
    }
    class NoiseTexture : Texture
    {
        readonly public Perlin _noise = new Perlin();
        private readonly double _scale;

        public NoiseTexture()
        {

        }
        public NoiseTexture(double scale)
        {
            _scale = scale;
        }

        public override SampledSpectrum Value(double u, double v, Point3D p)
        {
            Vector3D rgb = new Vector3D(1,1,1) * 0.5 * (1 + Math.Sin(_scale * p.Z + 10*_noise.Turb(_scale * p.ToVector())));
            return SampledSpectrum.FromRGB(new double[] {rgb.X, rgb.Y, rgb.Z}, SampledSpectrum.SpectrumType.Reflectance);
        }
    }
}
