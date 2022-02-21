using System.Collections.Generic;
using System.Linq;
using Math_lib;

namespace Raytracing
{
    public class hittable_list : hittable
    {
        public hittable_list()
        {

        }
        public hittable_list(hittable obj)
        {
            objects.Add(obj);
        }
        public void Add(hittable obj)
        {
            objects.Add(obj);
        }

        public List<hittable> objects = new List<hittable>();

        public override bool TryHit(Ray r, double t_min, double t_max, ref SurfaceInteraction isect)
        {
            SurfaceInteraction temp_rec = new SurfaceInteraction();
            bool hitAnything = false;
            var closest_so_far = t_max;

            foreach (var Object in objects)
            {
                if (Object.TryHit(r, t_min, closest_so_far, ref temp_rec))
                {
                    hitAnything = true;
                    closest_so_far = temp_rec.t;
                    isect = temp_rec;
                }
            }

            return hitAnything;
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            if (!objects.Any())
            {
                return false;
            }

            Bounds3D currentBoundingBox = new Bounds3D();
            bool isFirstBox = true;

            foreach(var _object in objects)
            {
                if (!_object.bounding_box(time0, time1, ref currentBoundingBox))
                {
                    bound = isFirstBox ? currentBoundingBox : Bounds3D.Union(bound, currentBoundingBox);
                    isFirstBox = false;
                }
            }

            return true;
        }
    }
}
