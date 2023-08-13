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

        public override SurfaceInteraction Scatter(Ray rIn, SurfaceInteraction isect)
        {
            var scatterDirection = (Vector3D)isect.Normal + Vector3D.RandomInUnitSphere();

            if (scatterDirection.NearZero())
            {
                scatterDirection = (Vector3D)isect.Normal;
            }

            isect.ScatteredRay = new Ray(isect.P, scatterDirection, double.PositiveInfinity, rIn.Time);
            isect.Attenuation = _albedo.Value(isect.UCoordinate, isect.VCoordinate, isect.P);
            isect.HasScattered = true;    

            return isect;
        }
    }
}
