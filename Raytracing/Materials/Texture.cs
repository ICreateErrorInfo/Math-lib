using System;
using Moarx.Math;
using Raytracing.Color;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public abstract class Texture
    {
        public RGBColorSpace _ColorSpace;

        public Texture(RGBColorSpace colorSpace) {
            _ColorSpace = colorSpace;
        }

        public abstract ISpectrum Value(double u, double v,Point3D<double> p);
    }
    class SolidColor : Texture
    {
        private readonly ISpectrum _colorValue;

        public SolidColor(ISpectrum c, RGBColorSpace colorspace) : base(colorspace)
        {
            _colorValue = c;
        }
        public SolidColor(double red, double green, double blue, RGBColorSpace colorspace) : base(colorspace)
        {
            _colorValue = new RGBAlbedoSpectrum(_ColorSpace, new(red, green, blue));
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

        public CheckerTexture(Texture even, Texture odd, RGBColorSpace colorspace) : base(colorspace)
        {
            _even = even;
            _odd = odd;
        }
        public CheckerTexture(ISpectrum color1, ISpectrum color2, RGBColorSpace colorspace) : base(colorspace)
        {
            _even = new SolidColor(color1, colorspace);
            _odd = new SolidColor(color2, colorspace);
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

        public NoiseTexture(double scale, RGBColorSpace colorspace) : base(colorspace)
        {
            _scale = scale;
        }

        public override ISpectrum Value(double u, double v,Point3D<double> p)
        {
            Vector3D<double> rgb = new Vector3D<double>(1,1,1) * 0.5 * (1 + Math.Sin(_scale * p.Z + 10*_noise.Turb(_scale * p.ToVector())));
            return new RGBAlbedoSpectrum(_ColorSpace, new(rgb.X, rgb.Y, rgb.Z));
        }
    }
}
