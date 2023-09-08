using Moarx.Math;
using Raytracing.Mathmatic;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    class DiffuseLight : Material
    {
        private readonly Texture _emit;

        public DiffuseLight(Texture a) : base(a.Factory)
        {
            _emit = a;
        }
        public DiffuseLight(SpectrumFactory factory, ISpectrum color) : base(factory)
        {
            _emit = new SolidColor(factory, color);
        }

        public override SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction)
        {
            interaction.Attenuation = Factory.CreateSpectrum();
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
