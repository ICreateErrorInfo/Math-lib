using Math_lib;
using System;

namespace Raytracing.Shapes
{
    public class Cylinder : Shape
    {
        private readonly double _radius;
        private readonly double _zMin;
        private readonly double _zMax;
        private readonly double _phiMax;

        public Cylinder(Point3D center, double radius, double zMin, double zMax, double phiMax)
        {
            WorldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            ObjectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
            _radius = radius;
            _zMin = Math.Min(zMin, zMax);
            _zMax = Math.Max(zMin, zMax);
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction isect)
        {
            tMax = 0;
            isect = new SurfaceInteraction();

            Ray rTransformed = WorldToObject.m * ray;

            double a = Math.Pow(rTransformed.D.X, 2) + Math.Pow(rTransformed.D.Y, 2);
            double b = rTransformed.D.X * rTransformed.O.X + rTransformed.D.Y * rTransformed.O.Y;
            double c = Math.Pow(rTransformed.O.X, 2) + Math.Pow(rTransformed.O.Y, 2) - Math.Pow(_radius, 2);

            double t0, t1;
            if (!Mathe.SolveQuadratic(a, b, c, out t0, 0.01, ray.TMax))
            {
                return false;
            }

            var pHit = rTransformed.At(t0);

            var phi = Math.Atan2(pHit.Y, pHit.X);

            if (phi < 0) phi += 2 * Math.PI;

            if (pHit.Z < _zMin || pHit.Z > _zMax || phi > _phiMax)
            {
                if (pHit.Z < _zMin ||
                    pHit.Z > _zMax || phi > _phiMax)
                    return false;
            }

            tMax = 0;
            isect.P = ray.At(t0);
            Point3D intersectionPointTransformed = rTransformed.At(t0);
            Normal3D outwardNormal = new((intersectionPointTransformed.X) / _radius, (intersectionPointTransformed.Y) / _radius, 0);
            isect.SetFaceNormal(ray, outwardNormal);

            return true;
        }
        public override Bounds3D GetObjectBound()
        {
            return new Bounds3D(new Point3D(-_radius, -_radius, _zMin),
                                 new Point3D(_radius, _radius, _zMax));
        }
    }
}
