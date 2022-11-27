using Math_lib;
using Math_lib.Spectrum;

namespace Raytracing.Materials
{
    class DiffuseLight : Material
    {
        private readonly Texture _emit;

        public DiffuseLight(Texture a)
        {
            _emit = a;
        }
        public DiffuseLight(SampledSpectrum c)
        {
            _emit = new SolidColor(c);
        }

        public override bool Scatter(Ray rIn, ref SurfaceInteraction isect, out SampledSpectrum attenuation, out Ray scattered)
        {
            attenuation = new SampledSpectrum();
            scattered = new Ray();
            return false;
        }

        public override SampledSpectrum Emitted(double u, double v, Point3D p)
        {
            return _emit.Value(u, v, p);
        }
    }
}
