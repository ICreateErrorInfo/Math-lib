using Math_lib;

namespace Raytracing.Materials
{
    public class Lambertian : Material
    {
        private readonly Texture _albedo;

        public Lambertian(Vector3D a)
        {
            _albedo = new SolidColor(a);
        }
        public Lambertian(Texture a)
        {
            _albedo = a;
        }

        public override bool Scatter(Ray rIn, ref SurfaceInteraction isect, out Vector3D attenuation, out Ray scattered)
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
