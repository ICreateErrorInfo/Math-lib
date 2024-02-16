using Moarx.Graphics.Color;
using Moarx.Math;
using Raytracing.Mathmatic;
using Raytracing.Primitives;
using static Raytracing.Raytracer;
using System;

namespace Raytracing.Integrators;
public abstract class IIntegrator {

    public Primitive Aggregate;
    protected RGBColorSpace _colorspace;

    public IIntegrator(Primitive aggregate, RGBColorSpace colorspace) {
        Aggregate = aggregate;
        _colorspace = colorspace;
    }

    public abstract void Render(Scene scene, IProgress<ProgressData> progress);

    public SurfaceInteraction Intersect(Ray ray) {
        return Aggregate.Intersect(ray, new());
    }
}
