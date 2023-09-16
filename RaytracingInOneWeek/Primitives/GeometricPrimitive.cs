﻿using Moarx.Math;
using Raytracing.Materials;
using Raytracing.Mathmatic;
using Raytracing.Shapes;

namespace Raytracing.Primitives {
    public class GeometricPrimitive: Primitive {
        private Shape _shape;
        private Material _material;

        public GeometricPrimitive(Shape shape, Material material) {
            _shape = shape;
            _material = material;
        }

        public override SurfaceInteraction Intersect(Ray r, SurfaceInteraction interaction) {
            double tHit = 0;
            if (!_shape.Intersect(r, out tHit, out interaction)) {
                interaction.HasIntersection = false; //TODO theoretisch unnötig
                return interaction;
            }
            r.TMax = tHit;
            interaction.Primitive = this;
            interaction.HasIntersection = true;
            return interaction;
        }
        public override Material GetMaterial() {
            return _material;
        }
        public override Bounds3D<double> GetWorldBound() {
            return _shape.ObjectToWorld * _shape.GetObjectBound();
        }
    }
}