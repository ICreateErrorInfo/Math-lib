using Math_lib;
using System;

namespace Raytracing.Shapes
{
    public class Cone : Shape
    {
        private readonly double _phiMax;
        private readonly double _height;
        private readonly double _radius;

        public Cone(Point3D center, double radius, double height, double phiMax)
        {
            _height = height;
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            _radius = radius;
            WorldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            ObjectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction isect)
        {
            tMax = 0;
            isect = new SurfaceInteraction();

            Ray rTransformed = WorldToObject.m * ray;

            double ox = rTransformed.O.X, oy = rTransformed.O.Y, oz = rTransformed.O.Z;
            double dx = rTransformed.D.X, dy = rTransformed.D.Y, dz = rTransformed.D.Z;

            double k = _radius / _height;
            k = k * k;
            double a = dx * dx + dy * dy - k * dz * dz;
            double b = 2 * (dx * ox + dy * oy - k * dz * (oz - _height));
            double c = ox * ox + oy * oy - k * (oz - _height) * (oz - _height);

            double t0, t1;
            if (!Mathe.SolveQuadratic(a, b, c, out t0, out t1, 0.01, ray.TMax))
            {
                return false;
            }

            var pHit = rTransformed.At(t0);

            var phi = Math.Atan2(pHit.Y, pHit.X);
            if (phi < 0) phi += 2 * Math.PI;

            if (pHit.Z < 0 || pHit.Z > _height || phi > _phiMax)
            {
                return false;
            }

            tMax = t0;
            isect.P = ray.At(t0);
            Point3D intersectionPointTransformed = rTransformed.At(t0);
            double hNew = _height - intersectionPointTransformed.Z;
            double XY = Math.Sqrt((hNew * hNew)/2);
            Normal3D outwardNormal = (Normal3D)Vector3D.Normalize(new Vector3D(XY, XY, Math.Sqrt(intersectionPointTransformed.X * intersectionPointTransformed.X + intersectionPointTransformed.Y * intersectionPointTransformed.Y)));
            isect.SetFaceNormal(ray, outwardNormal);

            return true;
        }

        public override Bounds3D GetObjectBound()
        {
            return new Bounds3D(new Point3D(-_radius, -_radius, 0),
                                new Point3D( _radius,  _radius, _height));
        }
    }
}
