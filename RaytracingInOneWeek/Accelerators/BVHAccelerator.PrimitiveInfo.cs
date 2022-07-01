using Math_lib;

namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        struct PrimitiveInfo
        {
            public int PrimitiveNumber;
            public Bounds3D Bounds;
            public Point3D Centroid;

            public PrimitiveInfo(int primitiveNumber, Bounds3D bounds)
            {
                PrimitiveNumber = primitiveNumber;
                Bounds = bounds;
                Centroid = 0.5 * bounds.pMin + 0.5 * bounds.pMax;
            }
        }
    }
}
