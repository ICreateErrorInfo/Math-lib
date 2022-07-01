using Math_lib;
using System;

namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        struct LinearBVHNode
        {
            public Bounds3D Bounds;
            public int PrimitivesOffset;
            public int SecondChildOffset;
            public Int16 NPrimitives;
            public Int16 Axis;
        }
    }
}
