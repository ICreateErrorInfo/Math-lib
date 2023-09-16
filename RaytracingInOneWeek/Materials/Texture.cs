using System;
using Moarx.Math;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public abstract class Texture
    {
        public abstract ISpectrum Value(double u, double v,Point3D<double> p);
    }
    class SolidColor : Texture
    {
        private readonly ISpectrum _colorValue;

        public SolidColor(ISpectrum c)
        {
            _colorValue = c;
        }
        public SolidColor(double red, double green, double blue)
        {
            _colorValue = new RGBAlbedoSpectrum(Raytracer.ColorSpace, new(red, green, blue));
        }

        public override ISpectrum Value(double u, double v,Point3D<double> p)
        {
            return _colorValue;
        }
    }
    public class CheckerTexture : Texture
    {
        private readonly Texture _odd;
        private readonly Texture _even;

        public CheckerTexture(Texture even, Texture odd)
        {
            _even = even;
            _odd = odd;
        }
        public CheckerTexture(ISpectrum color1, ISpectrum color2)
        {
            _even = new SolidColor(color1);
            _odd = new SolidColor(color2);
        }

        public override ISpectrum Value(double u, double v,Point3D<double> p)
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

        public NoiseTexture(double scale)
        {
            _scale = scale;
        }

        public override ISpectrum Value(double u, double v,Point3D<double> p)
        {
            Vector3D<double> rgb = new Vector3D<double>(1,1,1) * 0.5 * (1 + Math.Sin(_scale * p.Z + 10*_noise.Turb(_scale * p.ToVector())));
            return new RGBAlbedoSpectrum(Raytracer.ColorSpace, new(rgb.X, rgb.Y, rgb.Z));
        }
    }
}
