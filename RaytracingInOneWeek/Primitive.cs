using Math_lib;
using Raytracing.Materials;

namespace Raytracing
{
    public abstract class Primitive
    {
        public abstract Bounds3D GetWorldBound();
        public abstract SurfaceInteraction Intersect(Ray r, SurfaceInteraction intersection);
        public abstract Material GetMaterial();
    }
}
