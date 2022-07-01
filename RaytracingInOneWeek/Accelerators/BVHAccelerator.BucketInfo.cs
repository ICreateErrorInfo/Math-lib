using Math_lib;

namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        struct BucketInfo
        {
            public int Count;
            public Bounds3D Bounds;
        }
    }
}
