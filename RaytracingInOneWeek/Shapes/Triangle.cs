using Math_lib;
using System;

namespace Raytracing.Shapes
{
    public class Triangle : Shape
    {
        public Triangle()
        {
            throw new NotImplementedException();
        }

        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            throw new NotImplementedException();
        }

        public override bool Intersect(Ray r, double tMin, double tMax, out SurfaceInteraction insec)
        {
            throw new NotImplementedException();
        }
    }
}
