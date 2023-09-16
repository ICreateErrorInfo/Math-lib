using Moarx.Math;
using Raytracing.Color;
using Raytracing.Mathmatic;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public class Metal : Material
    {
        private readonly ISpectrum _albedo;
        private readonly double _fuzz;

        public Metal(ISpectrum color, double fuzz, RGBColorSpace colorSpace) : base(colorSpace)
        {
            _albedo = color;
            _fuzz = fuzz < 1 ? fuzz : 1;
        }

        public override SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction)
        {
            Vector3D<double> reflected = Vector3D<double>.Reflect((rayIn.Direction).Normalize(), interaction.Normal.ToVector());

            interaction.ScatteredRay   = new Ray(interaction.P, reflected + _fuzz * Vector3D<double>.RandomInUnitSphere(),double.PositiveInfinity, rayIn.Time);
            interaction.Attenuation    = _albedo;
            interaction.HasScattered   = ((interaction.ScatteredRay.Direction * interaction.Normal.ToVector()) > 0.0);

            return interaction;
        }
    }
}
