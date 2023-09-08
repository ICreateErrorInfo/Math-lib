using Moarx.Math;

namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        struct PrimitiveInfo
        {
            public int PrimitiveNumber;
            public Bounds3D<double> Bounds;
            public Point3D<double> Centroid;

            public PrimitiveInfo(int primitiveNumber, Bounds3D<double> bounds)
            {
                PrimitiveNumber = primitiveNumber;
                Bounds = bounds;
                Centroid = 0.5 * bounds.PMin + 0.5 * bounds.PMax;
            }
        }
    }
}
