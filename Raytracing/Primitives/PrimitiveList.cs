using System.Collections.Generic;
using System.Linq;
using Moarx.Math;
using Raytracing.Materials;
using Raytracing.Mathmatic;

namespace Raytracing.Primitives {
    public class PrimitiveList: Primitive {
        public List<Primitive> Objects = new List<Primitive>();

        public PrimitiveList() {

        }
        public PrimitiveList(Primitive obj) {
            Objects.Add(obj);
        }
        public void Add(Primitive obj) {
            Objects.Add(obj);
        }

        public override SurfaceInteraction Intersect(Ray ray, SurfaceInteraction interaction) {
            interaction = new SurfaceInteraction();

            var tempIsect = new SurfaceInteraction();
            var hitAnything = false;

            foreach (var Object in Objects) {
                tempIsect = Object.Intersect(ray, tempIsect);
                if (tempIsect.HasIntersection) {
                    hitAnything = true;
                    interaction = tempIsect;
                }
            }

            interaction.HasIntersection = hitAnything;
            return interaction;
        }
        public override Bounds3D<double> GetWorldBound() {
            var bound = new Bounds3D<double>();
            if (!Objects.Any()) {
                return bound;
            }

            var currentBoundingBox = new Bounds3D<double>();
            var isFirstBox = true;

            foreach (var _object in Objects) {
                currentBoundingBox = _object.GetWorldBound();
                bound = isFirstBox ? currentBoundingBox : Bounds3D<double>.Union(bound, currentBoundingBox);
                isFirstBox = false;

            }

            return bound;
        }

        public override Material GetMaterial() {
            throw new System.NotImplementedException();
        }
    }
}
