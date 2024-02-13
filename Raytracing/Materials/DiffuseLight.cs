using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;
using Raytracing.Mathmatic;

namespace Raytracing.Materials {
    class DiffuseLight : Material
    {
        private readonly Texture _emit;

        public DiffuseLight(Texture a) : base(a._ColorSpace)
        {
            _emit = a;
        }
        public DiffuseLight(ISpectrum color, RGBColorSpace colorspace) : base(colorspace)
        {
            _emit = new SolidColor(color, colorspace);
        }

        public override SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction)
        {
            interaction.ScatteredRay = new Ray();
            interaction.HasScattered = false;

            return interaction;
        }

        public override ISpectrum Emitted(double u, double v,Point3D<double> p)
        {
            return _emit.Value(u, v, p);
        }
    }
}
