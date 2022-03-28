using System;
using Math_lib;

namespace Raytracing.Materials
{
    class Dielectric : Material
    {
        private readonly double _ir;

        public Dielectric(double indexOfRefraction)
        {
            _ir = indexOfRefraction;
        }

        public override bool Scatter(Ray rIn, ref SurfaceInteraction isect, out Vector3D attenuation, out Ray scattered)
        {
            attenuation = new Vector3D(1, 1, 1);
            double refractionRatio = isect.FrontFace ? (1 / _ir) : _ir;

            Vector3D unitDirection = Vector3D.Normalize(rIn.D);
            double cosTheta = Math.Min(Vector3D.Dot(unitDirection * -1, (Vector3D)isect.Normal), 1);
            double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            bool cannotRefract = refractionRatio * sinTheta > 1;
            Vector3D direction;

            if (cannotRefract)
            {
                direction = Vector3D.Reflect(unitDirection, (Vector3D)isect.Normal);
            }
            else
            {
                direction = Vector3D.Refract(unitDirection, (Vector3D)isect.Normal, refractionRatio);
            }

            scattered = new Ray(isect.P, direction, rIn.TMax, rIn.Time);

            isect.Attenuation = attenuation;
            isect.Scattered = scattered;

            return true;
        }
    }
}
