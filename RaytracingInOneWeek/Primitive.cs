using Math_lib;
using Raytracing.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing
{
    public abstract class Primitive
    {
        public abstract Bounds3D GetWorldBound();
        public abstract bool Intersect(Ray r, out SurfaceInteraction intersection);
        public abstract Material GetMaterial();
    }
}
