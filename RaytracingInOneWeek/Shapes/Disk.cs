using Math_lib;
using System;

namespace Raytracing.Shapes
{
    public class Disk : Shape
    {
        private readonly double _height;
        private readonly double _radius;
        private readonly double _innerRadius;
        private readonly double _phiMax;

        public Disk(Point3D center, double height, double radius, double innerRadius, double phiMax)
        {
            _height = height;
            _radius = radius;
            _innerRadius = innerRadius;
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            WorldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            ObjectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction interaction)
        {
            tMax = 0;
            interaction = new SurfaceInteraction();

            Ray rTransformed = WorldToObject.m * ray;

            double t0 = (_height - rTransformed.O.Z) / rTransformed.D.Z;
            if(t0 <= 0 || t0 >= rTransformed.TMax || rTransformed.D.Z == 0)
            {
                return false;
            }

            Point3D pHit = rTransformed.At(t0);
            double dist2 = pHit.X * pHit.X + pHit.Y * pHit.Y;
            if(dist2 > _radius * _radius || dist2 < _innerRadius * _innerRadius)
            {
                return false;
            }

            double phi = Math.Atan2(pHit.Y, pHit.X);
            if(phi < 0) phi += 2 * Math.PI;
            if (phi > _phiMax) return false;

            tMax = t0;
            interaction.P = ray.At(t0);
            Normal3D outwardNormal = new(pHit.X,pHit.Y,_height + 1);
            interaction.SetFaceNormal(ray, outwardNormal);

            return true;
        }
        public override Bounds3D GetObjectBound()
        {
            return new Bounds3D(new(-_radius, -_radius, _height),
                                 new(_radius, _radius, _height));
        }
    }
}
