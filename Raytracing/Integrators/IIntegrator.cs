using Moarx.Graphics.Color;
using Moarx.Math;
using Raytracing.Mathmatic;
using Raytracing.Primitives;
using static Raytracing.Raytracer;
using System;
using System.Threading;

namespace Raytracing.Integrators;
public abstract class IIntegrator {

    public Primitive Aggregate;
    protected RGBColorSpace _colorspace;

    public IIntegrator(Primitive aggregate, RGBColorSpace colorspace) {
        Aggregate = aggregate;
        _colorspace = colorspace;
    }

    // Cancellation is cooperative: implementations must poll token.IsCancellationRequested and
    // return early instead of throwing, so debugging a render isn't interrupted by first-chance exceptions.
    // Returns the number of samples per pixel actually completed (equals scene.SamplesPerPixel unless canceled early).
    public abstract int Render(Scene scene, IProgress<ProgressData> progress, CancellationToken token);

    public SurfaceInteraction Intersect(Ray ray) {
        return Aggregate.Intersect(ray, new());
    }
}
