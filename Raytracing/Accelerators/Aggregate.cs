using Raytracing.Materials;
using Raytracing.Primitives;
using System;

namespace Raytracing.Accelerators {
    public abstract class Aggregate : Primitive
    {
        public override Material GetMaterial()
        {
            throw new Exception("Can not call GetMaterial");
        }
    }
}
