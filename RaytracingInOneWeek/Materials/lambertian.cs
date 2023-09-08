﻿using Moarx.Math;
using Raytracing.Mathmatic;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public class Lambertian : Material
    {
        private readonly Texture _albedo;

        public Lambertian(SpectrumFactory factory, ISpectrum color) : base(factory)
        {
            _albedo = new SolidColor(factory, color);
        }
        public Lambertian(Texture a) : base(a.Factory)
        {
            _albedo = a;
        }

        public override SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction)
        {
            var scatterDirection = interaction.Normal.ToVector() + Vector3D<double>.RandomInUnitSphere();

            if (scatterDirection.NearZero())
            {
                scatterDirection = interaction.Normal.ToVector();
            }

            interaction.ScatteredRay = new Ray(interaction.P, scatterDirection, double.PositiveInfinity, rayIn.Time);
            interaction.Attenuation = _albedo.Value(interaction.UCoordinate, interaction.VCoordinate, interaction.P);
            interaction.HasScattered = true;    

            return interaction;
        }
    }
}
