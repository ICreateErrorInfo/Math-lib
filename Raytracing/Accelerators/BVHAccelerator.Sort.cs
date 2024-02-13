using System;
using System.Collections.Generic;

namespace Raytracing.Accelerators
{
    public partial class BVHAccelerator
    {
        class Sort : IComparer<PrimitiveInfo>
        {
            int _dim = 0;

            public Sort(int dim)
            {
                _dim = dim;
            }

            public int Compare(PrimitiveInfo x, PrimitiveInfo y)
            {
                bool value = x.Centroid[_dim] < y.Centroid[_dim];
                if (value == true)
                {
                    return -1;
                }
                if (value == false)
                {
                    return 1;
                }
                throw new NullReferenceException("value can't be null");
            }
        }
    }
}
