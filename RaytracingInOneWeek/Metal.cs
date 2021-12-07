using Math_lib;

namespace RaytracingInOneWeek
{
    class Metal : material
    {
        public Metal(Vector3D a, double f)
        {
            albedo = a;
            fuzz = f < 1 ? f : 1;
        }
        public Vector3D albedo;
        public double fuzz;

        public override zwischenSpeicher scatter(Ray r_in, hit_record rec, Vector3D attenuation, Ray scattered)
        {
            zwischenSpeicher zw = new zwischenSpeicher();
            Vector3D reflected = Vector3D.Reflect(Vector3D.Normalize(r_in.d), (Vector3D)rec.normal);
            scattered = new Ray(rec.p, reflected + fuzz * Vector3D.RandomInUnitSphere(), r_in.tMax);
            attenuation = albedo;

            zw.scattered = scattered;
            zw.attenuation = attenuation;
            zw.IsTrue = (Vector3D.Dot(scattered.d, (Vector3D)rec.normal) > 0.0);

            return zw;
        }
    }
}
