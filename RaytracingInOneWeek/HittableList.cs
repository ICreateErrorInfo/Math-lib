using System.Collections.Generic;
using System.Linq;
using Math_lib;

namespace Raytracing
{
    public class HittableList : Hittable
    {
        public List<Hittable> Objects = new List<Hittable>();

        public HittableList()
        {

        }
        public HittableList(Hittable obj)
        {
            Objects.Add(obj);
        }
        public void Add(Hittable obj)
        {
            Objects.Add(obj);
        }

        public override bool TryHit(Ray r, double tMin, double tMax, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();

            SurfaceInteraction tempIsect = new SurfaceInteraction();
            bool hitAnything = false;
            var closestSoFar = tMax;

            foreach (var Object in Objects)
            {
                if (Object.TryHit(r, tMin, closestSoFar, out tempIsect))
                {
                    hitAnything = true;
                    closestSoFar = tempIsect.T;
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
