using System;
using Math_lib;
using Raytracing.Mathmatic;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    class Dielectric : Material
    {
        private readonly double _ir;

        public Dielectric(SpectrumFactory factory, double indexOfRefraction) : base(factory)
        {
            _ir = indexOfRefraction;
        }

        public override SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction)
        {
            var attenuation = Factory.CreateFromRGB(new double[] { 1, 1, 1 }, SpectrumMaterialType.Reflectance);
            double refractionRatio = interaction.FrontFace ? (1 / _ir) : _ir;

            Vector3D unitDirection = Vector3D.Normalize(rayIn.D);
            double cosTheta = Math.Min(Vector3D.Dot(unitDirection * -1, (Vector3D)interaction.Normal), 1);
            double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            bool cannotRefract = refractionRatio * sinTheta > 1;
            Vector3D direction;

            if (cannotRefract)
            {
                direction = Vector3D.Reflect(unitDirection, (Vector3D)interaction.Normal);
            }
            else
            {
                direction = Vector3D.Refract(unitDirection, (Vector3D)interaction.Normal, refractionRatio);
            }

            var scattered = new Ray(interaction.P, direction, double.PositiveInfinity, rayIn.Time);

            interaction.Attenuation = attenuation;
            interaction.ScatteredRay = scattered;
            interaction.HasScattered = true;

            return interaction;
        }
    }
}
