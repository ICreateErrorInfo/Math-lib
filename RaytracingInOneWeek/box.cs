using System;
using Math_lib;
using System.Collections.Generic;
using System.Text;

namespace Raytracing
{
    class Box : Shape
    {
        private Point3D boxMin;
        private Point3D boxMax;
        public readonly HittableList Sides = new HittableList();

        public Box()
        {

        }
        public Box(Point3D p0, Point3D p1, Material material)
        {
            boxMin = p0;
            boxMax = p1;

            Sides.Add(new XYRect(p0.X, p1.X, p0.Y, p1.Y, p1.Z, material));
            Sides.Add(new XYRect(p0.X, p1.X, p0.Y, p1.Y, p0.Z, material));
                                 
            Sides.Add(new XZRect(p0.X, p1.X, p0.Z, p1.Z, p1.Y, material));
            Sides.Add(new XZRect(p0.X, p1.X, p0.Z, p1.Z, p0.Y, material));
                                   
            Sides.Add(new YZRect(p0.Y, p1.Y, p0.Z, p1.Z, p1.X, material));
            Sides.Add(new YZRect(p0.Y, p1.Y, p0.Z, p1.Z, p0.X, material));

        }

        public override bool TryHit(Ray r, double tMin, double tMax, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();
            return Sides.TryHit(r, tMin, tMax, out isect);
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(boxMin, boxMax);
            return true;
        }
    }
}
