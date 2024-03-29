﻿using Moarx.Math;

namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        struct BuildNode
        {
            public Bounds3D<double> Bounds;
            public BuildNode[] Children;
            public int SplitAxis, FirstPrimOffset, NPrimitives;

            public void InitLeaf(int first, int n, Bounds3D<double> b)
            {
                FirstPrimOffset = first;
                NPrimitives = n;
                Bounds = b;
            }
            public void InitInterior(int axis, BuildNode c0, BuildNode c1)
            {
                Children = new BuildNode[2];
                Children[0] = c0;
                Children[1] = c1;
                Bounds = Bounds3D<double>.Union(c0.Bounds, c1.Bounds);
                SplitAxis = axis;
                NPrimitives = 0;
            }
        }
    }
}
