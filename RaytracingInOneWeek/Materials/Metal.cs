using Math_lib;
using Math_lib.Spectrum;

namespace Raytracing.Materials
{
    public class Metal : Material
    {
        private readonly SampledSpectrum _albedo;
        private readonly double _fuzz;

        public Metal(SampledSpectrum a, double f)
        {
            _albedo = a;
            _fuzz = f < 1 ? f : 1;
        }

        public override bool Scatter(Ray rIn, ref SurfaceInteraction isect, out SampledSpectrum attenuation, out Ray scattered)
        {
            Vector3D reflected = Vector3D.Reflect(Vector3D.Normalize(rIn.D), (Vector3D)isect.Normal);

            scattered   = new Ray(isect.P, reflected + _fuzz * Vector3D.RandomInUnitSphere(),double.PositiveInfinity, rIn.Time);
            attenuation = _albedo;

            return (Vector3D.Dot(scattered.D, (Vector3D)isect.Normal) > 0.0);
        }
    }
}
