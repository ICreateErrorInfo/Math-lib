using Math_lib;

namespace Raytracing
{
    public class Metal : material
    {
        public Metal(Vector3D a, double f)
        {
            albedo = a;
            fuzz = f < 1 ? f : 1;
        }
        public Vector3D albedo;
        public double fuzz;

        public override bool scatter(Ray r_in, ref SurfaceInteraction isect, ref Vector3D attenuation, ref Ray scattered)
        {
            Vector3D reflected = Vector3D.Reflect(Vector3D.Normalize(r_in.d), (Vector3D)isect.normal);

            scattered   = new Ray(isect.p, reflected + fuzz * Vector3D.RandomInUnitSphere(), r_in.tMax);
            attenuation = albedo;

            return (Vector3D.Dot(scattered.d, (Vector3D)isect.normal) > 0.0);
        }
    }
}
