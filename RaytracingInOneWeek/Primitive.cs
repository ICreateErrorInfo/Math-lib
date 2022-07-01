using Math_lib;
using Raytracing.Materials;

namespace Raytracing
{
    public abstract class Primitive
    {
        public abstract Bounds3D GetWorldBound();
        public abstract bool Intersect(Ray r, out SurfaceInteraction intersection);
        public abstract Material GetMaterial();
    }
}
