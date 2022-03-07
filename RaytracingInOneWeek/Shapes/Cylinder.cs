using Math_lib;
using System;

namespace Raytracing.Shapes
{
    public class Cylinder : Shape
    {
        private readonly Point3D _center;
        private readonly double _radius;
        private readonly double _zMin;
        private readonly double _zMax;
        private readonly double _phiMax;
        private readonly Material _material;

        public Cylinder(Point3D center, double radius, double zMin, double zMax, double phiMax, Material material)
        {
            _center = center;
            _radius = radius;
            _zMin = Math.Min(zMin, zMax);
            _zMax = Math.Max(zMin, zMax);
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            _material = material;
        }

        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(_center + new Point3D(-_radius, -_radius, _zMin),
                                 _center + new Point3D( _radius,  _radius, _zMax));
            return true;
        }

        public override bool Intersect(Ray r, double tMin, double tMax, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();

            Vector3D oc = r.O - _center;
            double a = Math.Pow(r.D.X, 2) + Math.Pow(r.D.Y, 2);
            double b = r.D.X * oc.X + r.D.Y * oc.Y;
            double c = Math.Pow(oc.X, 2) + Math.Pow(oc.Y, 2) - Math.Pow(_radius, 2);
            double discriminant = Math.Pow(b, 2) - a * c;

            if (discriminant < 0)
            {
                return false;
            }

            var sqrtd = Math.Sqrt(discriminant);

            var root = (-b - sqrtd) / a;
            if (root < tMin || tMax < root)
            {
                root = (-b + sqrtd) / a;
                if (root < tMin || tMax < root)
                {
                    return false;
                }
            }

            var pHit = r.At(root);
            var phi = Math.Atan2(pHit.Y - _center.Y, pHit.X - _center.X);
            if (phi < 0) phi += 2 * Math.PI;

            if (pHit.Z < _zMin ||  pHit.Z > _zMax || phi > _phiMax)
            {
                if (pHit.Z < _zMin ||
                    pHit.Z > _zMax || phi > _phiMax)
                    return false;
            }

            insec.T = root;
            insec.P = r.At(root);
            Normal3D outwardNormal = new((insec.P.X - _center.X) / _radius, (insec.P.Y - _center.X) / _radius, insec.P.Z);
            insec.SetFaceNormal(r, outwardNormal);
            insec.Material = _material;

            return true;
        }
    }
}
