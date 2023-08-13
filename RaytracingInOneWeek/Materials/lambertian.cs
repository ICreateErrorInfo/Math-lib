using Math_lib;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public class Lambertian : Material
    {
        private readonly Texture _albedo;

        public Lambertian(SpectrumFactory factory, ISpectrum a) : base(factory)
        {
            _albedo = new SolidColor(factory, a);
        }
        public Lambertian(Texture a) : base(a.Factory)
        {
            _albedo = a;
        }

        public override bool Scatter(Ray rIn, ref SurfaceInteraction isect, out ISpectrum attenuation, out Ray scattered)
        {
            var scatterDirection = (Vector3D)isect.Normal + Vector3D.RandomInUnitSphere();

            if (scatterDirection.NearZero())
            {
                scatterDirection = (Vector3D)isect.Normal;
            }

            scattered = new Ray(isect.P, scatterDirection, double.PositiveInfinity, rIn.Time);
            attenuation = _albedo.Value(isect.U, isect.V, isect.P);
                
            return true;
        }
    }
}
