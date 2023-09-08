using Moarx.Math;
using Raytracing.Materials;
using Raytracing.Mathmatic;

namespace Raytracing.Primitives {
    public abstract class Primitive {
        public abstract Bounds3D<double> GetWorldBound();
        public abstract SurfaceInteraction Intersect(Ray r, SurfaceInteraction interaction);
        public abstract Material GetMaterial();
    }
}
