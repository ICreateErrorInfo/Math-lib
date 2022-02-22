using Math_lib;

namespace Raytracing
{
    public class Metal : Material
    {
        private readonly Vector3D _albedo;
        private readonly double _fuzz;

        public Metal(Vector3D a, double f)
        {
            _albedo = a;
            _fuzz = f < 1 ? f : 1;
        }

        public override bool Scatter(Ray rIn, ref SurfaceInteraction isect, ref Vector3D attenuation, ref Ray scattered)
        {
            Vector3D reflected = Vector3D.Reflect(Vector3D.Normalize(rIn.D), (Vector3D)isect.Normal);

            scattered   = new Ray(isect.P, reflected + _fuzz * Vector3D.RandomInUnitSphere(), rIn.TMax);
            attenuation = _albedo;

            return (Vector3D.Dot(scattered.D, (Vector3D)isect.Normal) > 0.0);
        }
    }
}
