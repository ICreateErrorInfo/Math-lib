using Math_lib;
using Raytracing.Spectrum;
using System.Windows.Media.Animation;

namespace Raytracing.Materials {
    public class Metal : Material
    {
        private readonly ISpectrum _albedo;
        private readonly double _fuzz;

        public Metal(SpectrumFactory factory, ISpectrum a, double f) : base(factory)
        {
            _albedo = a;
            _fuzz = f < 1 ? f : 1;
        }

        public override SurfaceInteraction Scatter(Ray rIn, SurfaceInteraction isect)
        {
            Vector3D reflected = Vector3D.Reflect(Vector3D.Normalize(rIn.D), (Vector3D)isect.Normal);

            isect.ScatteredRay   = new Ray(isect.P, reflected + _fuzz * Vector3D.RandomInUnitSphere(),double.PositiveInfinity, rIn.Time);
            isect.Attenuation    = _albedo;
            isect.HasScattered   = (Vector3D.Dot(isect.ScatteredRay.D, (Vector3D)isect.Normal) > 0.0);

            return isect;
        }
    }
}
