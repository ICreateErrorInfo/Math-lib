using System;

namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        struct MortonPrimitive
        {
            public int PrimitiveIndex;
            public Int32 MortonCode;
        }
    }
}
