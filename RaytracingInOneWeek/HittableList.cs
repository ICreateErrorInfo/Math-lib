using System.Collections.Generic;
using System.Linq;
using Math_lib;
using Raytracing.Materials;

namespace Raytracing
{
    public class HittableList : Primitive
    {
        public List<Primitive> Objects = new List<Primitive>();

        public HittableList()
        {

        }
        public HittableList(Primitive obj)
        {
            Objects.Add(obj);
        }
        public void Add(Primitive obj)
        {
            Objects.Add(obj);
        }

        public override SurfaceInteraction Intersect(Ray ray, SurfaceInteraction interaction)
        {
            interaction = new SurfaceInteraction();

            SurfaceInteraction tempIsect = new SurfaceInteraction();
            bool hitAnything = false;

            foreach (var Object in Objects)
            {
                tempIsect = Object.Intersect(ray, tempIsect);
                if (tempIsect.HasIntersection)
                {
                    hitAnything = true;
                    interaction = tempIsect;
                }
            }

            interaction.HasIntersection = hitAnything;
            return interaction;
        }
        public override Bounds3D GetWorldBound()
        {
            Bounds3D bound = new Bounds3D();
            if (!Objects.Any())
            {
                return bound;
            }

            Bounds3D currentBoundingBox = new Bounds3D();
            bool isFirstBox = true;

            foreach(var _object in Objects)
            {
                currentBoundingBox = _object.GetWorldBound();
                bound = isFirstBox ? currentBoundingBox : Bounds3D.Union(bound, currentBoundingBox);
                isFirstBox = false;
                
            }

            return bound;
        }

        public override Material GetMaterial()
        {
            throw new System.NotImplementedException();
        }
    }
}
