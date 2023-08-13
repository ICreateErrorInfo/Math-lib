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

        public override SurfaceInteraction Scatter(Ray rIn, SurfaceInteraction isect)
        {
            isect.Attenuation = Factory.CreateSpectrum();
            isect.ScatteredRay = new Ray();
            isect.HasScattered = false;

            return isect;
        }

        public override ISpectrum Emitted(double u, double v, Point3D p)
        {
            return _emit.Value(u, v, p);
        }
    }
}
