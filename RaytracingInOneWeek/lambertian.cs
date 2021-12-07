using Math_lib;

namespace RaytracingInOneWeek
{
    class lambertian : material
    {
        public lambertian(Vector3D a)
        {
            albedo = new solid_color(a);
        }
        public lambertian(Texture a)
        {
            albedo = a;
        }
        public Texture albedo;

        public override zwischenSpeicher scatter(Ray r_in, hit_record rec, Vector3D attenuation, Ray scattered)
        {
            zwischenSpeicher zw = new zwischenSpeicher();
            var scatter_direction = (Vector3D)rec.normal + Vector3D.RandomInUnitSphere();

            //catch degenerate scatter direction
            if (scatter_direction.NearZero())
            {
                scatter_direction = (Vector3D)rec.normal;
            }

            scattered = new Ray(rec.p, scatter_direction, r_in.tMax);

            zw.scattered = scattered;
            zw.attenuation = albedo.value(rec.u, rec.v, rec.p);
            zw.IsTrue = true;
                
            return zw;
        }
    }
}
