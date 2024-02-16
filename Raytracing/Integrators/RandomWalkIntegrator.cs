using Microsoft.VisualBasic;
using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;
using Raytracing.Camera;
using Raytracing.Mathmatic;
using Raytracing.Primitives;
using System.Windows.Documents;

namespace Raytracing.Integrators;
public class RandomWalkIntegrator: RayIntegrator {

    private int _maxDepth;

    public RandomWalkIntegrator(ICamera camera, Primitive aggregate, RGBColorSpace colorspace, int maxDepth) : base(camera, aggregate, colorspace) {
        _maxDepth = maxDepth;
    }

    public override SampledSpectrum Li(Ray ray, SampledWavelengths lambda) {
        return LiRandomWalk(ray, lambda, 0);
    }

    private SampledSpectrum LiRandomWalk(Ray ray, SampledWavelengths lambda, int depth) {

        SurfaceInteraction interaction = new SurfaceInteraction();

        if (depth >= _maxDepth) {
            return new RGBAlbedoSpectrum(_colorspace, new(0, 0, 0)).Sample(lambda);
        }

        interaction = Aggregate.Intersect(ray, interaction);
        if (!interaction.HasIntersection) {
            return background.Sample(lambda);
        }
        ISpectrum emitted = interaction.Primitive.GetMaterial().Emitted(interaction.UCoordinate, interaction.VCoordinate, interaction.P);

        interaction = interaction.Primitive.GetMaterial().Scatter(ray, interaction);

        if (!interaction.HasScattered) {
            return emitted.Sample(lambda);
        }

        return emitted.Sample(lambda) + interaction.Attenuation.Sample(lambda) * LiRandomWalk(interaction.ScatteredRay, lambda, depth++);
    }
}
