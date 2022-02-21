using Math_lib;

namespace Raytracing
{
    class lambertian : material
    {
        public Texture albedo;

        public lambertian(Vector3D a)
        {
            albedo = new solid_color(a);
        }
        public lambertian(Texture a)
        {
            albedo = a;
        }

        public override bool scatter(Ray r_in, ref SurfaceInteraction isect, ref Vector3D attenuation, ref Ray scattered)
        {
            var scatter_direction = (Vector3D)isect.normal + Vector3D.RandomInUnitSphere();

            if (scatter_direction.NearZero())
            {
                scatter_direction = (Vector3D)isect.normal;
            }

            scattered = new Ray(isect.p, scatter_direction, r_in.tMax);
            attenuation = albedo.value(isect.u, isect.v, isect.p);
                
            return true;
        }
    }
}
