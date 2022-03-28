﻿using System.Collections.Generic;
using System.Linq;
using Math_lib;
using Raytracing.Shapes;

namespace Raytracing
{
    public class HittableList : Shape
    {
        public List<Shape> Objects = new List<Shape>();

        public HittableList()
        {

        }
        public HittableList(Shape obj)
        {
            Objects.Add(obj);
        }
        public void Add(Shape obj)
        {
            Objects.Add(obj);
        }

        public override bool Intersect(Ray r, double tMin, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();

            SurfaceInteraction tempIsect = new SurfaceInteraction();
            bool hitAnything = false;

            foreach (var Object in Objects)
            {
                if (Object.Intersect(r, tMin, out tempIsect))
                {
                    hitAnything = true;
                    isect = tempIsect;
                }
            }

            return hitAnything;
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            if (!Objects.Any())
            {
                return false;
            }

            Bounds3D currentBoundingBox = new Bounds3D();
            bool isFirstBox = true;

            foreach(var _object in Objects)
            {
                if (!_object.BoundingBox(time0, time1, ref currentBoundingBox))
                {
                    bound = isFirstBox ? currentBoundingBox : Bounds3D.Union(bound, currentBoundingBox);
                    isFirstBox = false;
                }
            }

            return true;
        }
    }
}
