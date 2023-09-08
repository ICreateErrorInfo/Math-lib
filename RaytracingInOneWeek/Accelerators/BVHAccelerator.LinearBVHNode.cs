using Moarx.Math;
using System;

namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        struct LinearBVHNode
        {
            public Bounds3D<double> Bounds;
            public int PrimitivesOffset;
            public int SecondChildOffset;
            public Int16 NPrimitives;
            public Int16 Axis;
        }
    }
}
