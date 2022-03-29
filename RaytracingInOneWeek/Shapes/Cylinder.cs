using Math_lib;
using Raytracing.Materials;
using System;

namespace Raytracing.Shapes
{
    public class Cylinder : Shape
    {
        private readonly Transform _worldToObject;
        private readonly Transform _objectToWorld;
        private readonly double _radius;
        private readonly double _zMin;
        private readonly double _zMax;
        private readonly double _phiMax;
        private readonly Material _material;

        public Cylinder(Point3D center, double radius, double zMin, double zMax, double phiMax, Material material)
        {
            _worldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            _objectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
            _radius = radius;
            _zMin = Math.Min(zMin, zMax);
            _zMax = Math.Max(zMin, zMax);
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            _material = material;
        }

        public override Bounds3D GetObjectBound()
        {
            return new Bounds3D(new Point3D(-_radius, -_radius, _zMin),
                                 new Point3D( _radius,  _radius, _zMax));
        }

        public override bool Intersect(Ray ray, double tMin, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();

            Ray rTransformed = _worldToObject.m * ray;

            double a = Math.Pow(rTransformed.D.X, 2) + Math.Pow(rTransformed.D.Y, 2);
            double b = rTransformed.D.X * rTransformed.O.X + rTransformed.D.Y * rTransformed.O.Y;
            double c = Math.Pow(rTransformed.O.X, 2) + Math.Pow(rTransformed.O.Y, 2) - Math.Pow(_radius, 2);

            double t0, t1;
            if (!Mathe.SolveQuadratic(a, b, c, out t0, tMin, ray.TMax))
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

            ray.TMax = t0;
            insec.T = t0;
            insec.P = ray.At(t0);
            Point3D intersectionPointTransformed = rTransformed.At(t0);
            Normal3D outwardNormal = new((intersectionPointTransformed.X) / _radius, (intersectionPointTransformed.Y) / _radius, 0);
            insec.SetFaceNormal(ray, outwardNormal);
            insec.Material = _material;

            return true;
        }
    }
}
