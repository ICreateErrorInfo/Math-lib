using Math_lib;
using Raytracing.Mathmatic;
using Raytracing.Spectrum;
using System.Windows.Media.Animation;

namespace Raytracing.Materials {
    public class Metal : Material
    {
        private readonly ISpectrum _albedo;
        private readonly double _fuzz;

        public Metal(SpectrumFactory factory, ISpectrum color, double fuzz) : base(factory)
        {
            _albedo = color;
            _fuzz = fuzz < 1 ? fuzz : 1;
        }

        public override SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction)
        {
            Vector3D reflected = Vector3D.Reflect(Vector3D.Normalize(rayIn.D), (Vector3D)interaction.Normal);

            interaction.ScatteredRay   = new Ray(interaction.P, reflected + _fuzz * Vector3D.RandomInUnitSphere(),double.PositiveInfinity, rayIn.Time);
            interaction.Attenuation    = _albedo;
            interaction.HasScattered   = (Vector3D.Dot(interaction.ScatteredRay.D, (Vector3D)interaction.Normal) > 0.0);

            return interaction;
        }
    }
}
