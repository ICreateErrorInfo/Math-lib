namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        struct LBVHTreelet
        {
            public int StartIndex, NPrimitives;
            public BuildNode BuildNodes;
            public LBVHTreelet(int startIndex, int nPrimitives, BuildNode buildNodes)
            {
                StartIndex = startIndex;
                NPrimitives = nPrimitives;
                BuildNodes = buildNodes;
            }
        }
    }
}
