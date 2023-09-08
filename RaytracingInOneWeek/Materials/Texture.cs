using System;
using Moarx.Math;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public abstract class Texture
    {
        public SpectrumFactory Factory { get; }

        protected Texture(SpectrumFactory factory) {
            Factory = factory;
        }

        public abstract ISpectrum Value(double u, double v,Point3D<double> p);
    }
    class SolidColor : Texture
    {
        private readonly ISpectrum _colorValue;

        public SolidColor(SpectrumFactory factory) : base(factory)
        {

        }
        public SolidColor(SpectrumFactory factory, ISpectrum c) : base(factory)
        {
            _colorValue = c;
        }
        public SolidColor(SpectrumFactory factory, double red, double green, double blue) : base(factory)
        {
            _colorValue = factory.CreateFromRGB(new double[] { red, green, blue }, SpectrumMaterialType.Reflectance);
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

        public CheckerTexture(SpectrumFactory factory) : base(factory)
        {

        }
        public CheckerTexture(SpectrumFactory factory, Texture even, Texture odd) : base(factory)
        {
            _even = even;
            _odd = odd;
        }
        public CheckerTexture(SpectrumFactory factory, ISpectrum color1, ISpectrum color2) : base(factory)
        {
            _even = new SolidColor(factory, color1);
            _odd = new SolidColor(factory, color2);
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

        public NoiseTexture(SpectrumFactory factory) : base(factory)
        {

        }
        public NoiseTexture(SpectrumFactory factory, double scale) : base(factory)
        {
            _scale = scale;
        }

        public override ISpectrum Value(double u, double v,Point3D<double> p)
        {
            Vector3D<double> rgb = new Vector3D<double>(1,1,1) * 0.5 * (1 + Math.Sin(_scale * p.Z + 10*_noise.Turb(_scale * p.ToVector())));
            return Factory.CreateFromRGB(new double[] {rgb.X, rgb.Y, rgb.Z}, SpectrumMaterialType.Reflectance);
        }
    }
}
