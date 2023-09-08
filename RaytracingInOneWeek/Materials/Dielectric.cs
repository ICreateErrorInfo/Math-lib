﻿using System;
using Moarx.Math;
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

            Vector3D<double> unitDirection = (rayIn.Direction).Normalize();
            double cosTheta = Math.Min(((unitDirection * -1) * interaction.Normal.ToVector()), 1);
            double sinTheta = Math.Sqrt(1 - cosTheta * cosTheta);

            bool cannotRefract = refractionRatio * sinTheta > 1;
            Vector3D<double> direction;

            if (cannotRefract)
            {
                direction = Vector3D<double>.Reflect(unitDirection, interaction.Normal.ToVector());
            }
            else
            {
                direction = Vector3D<double>.Refract(unitDirection, interaction.Normal.ToVector(), refractionRatio);
            }

            var scattered = new Ray(interaction.P, direction, double.PositiveInfinity, rayIn.Time);

            interaction.Attenuation = attenuation;
            interaction.ScatteredRay = scattered;
            interaction.HasScattered = true;

            return interaction;
        }
    }
}
