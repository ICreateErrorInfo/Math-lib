using System;
using Math_lib;

namespace Raytracing
{
    class dielectric : material
    {
        public dielectric(double index_of_refraction)
        {
            ir = index_of_refraction;
        }
        public double ir;

        public override bool scatter(Ray r_in, ref SurfaceInteraction isect, ref Vector3D attenuation, ref Ray scattered)
        {
            attenuation = new Vector3D(1, 1, 1);
            double refraction_ratio = isect.front_face ? (1 / ir) : ir;

            Vector3D unit_direction = Vector3D.Normalize(r_in.d);
            double cos_theta = Math.Min(Vector3D.Dot(unit_direction * -1, (Vector3D)isect.normal), 1);
            double sin_theta = Math.Sqrt(1 - cos_theta * cos_theta);

            bool cannot_refract = refraction_ratio * sin_theta > 1;
            Vector3D direction;

            if (cannot_refract)
            {
                direction = Vector3D.Reflect(unit_direction, (Vector3D)isect.normal);
            }
            else
            {
                direction = Vector3D.Refract(unit_direction, (Vector3D)isect.normal, refraction_ratio);
            }

            scattered = new Ray(isect.p, direction, r_in.tMax);

            isect.attenuation = attenuation;
            isect.scattered = scattered;

            return true;
        }
    }
}
