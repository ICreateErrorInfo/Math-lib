using Math_lib;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    class DiffuseLight : Material
    {
        private readonly Texture _emit;

        public DiffuseLight(Texture a) : base(a.Factory)
        {
            _emit = a;
        }
        public DiffuseLight(SpectrumFactory factory, ISpectrum c) : base(factory)
        {
            _emit = new SolidColor(factory, c);
        }

        public override bool Scatter(Ray rIn, ref SurfaceInteraction isect, out ISpectrum attenuation, out Ray scattered)
        {
            attenuation = Factory.CreateSpectrum();
            scattered = new Ray();
            return false;
        }

        public override ISpectrum Emitted(double u, double v, Point3D p)
        {
            return _emit.Value(u, v, p);
        }
    }
}
