using System;
using Math_lib;

namespace Raytracing.Shapes
{
    public class Sphere : Shape
    {
        private readonly double _zMin, _zMax;
        private readonly double _thetaMin, _thetaMax, _phiMax;
        private readonly Point3D  _center;
        private readonly double   _radius;
        private readonly Material _material;
        public double Radius => _radius;
        public Point3D Center => _center;
        public Material Material => _material;

        public Sphere() { }
        public Sphere(Point3D center, double radius, Material material)
        {
            _center = center;
            _radius = radius;
            _material = material;
            _zMin = -radius;
            _zMax = radius;
            _thetaMax = 1;
            _thetaMin = -1;
            _phiMax = 360;
        }
        public Sphere(Point3D center, double radius, double zMin, double zMax, double phiMax, Material material) 
        {
            _center = center;
            _radius = radius;
            _zMin = Math.Clamp(Math.Min(zMin, zMax), -radius, radius);
            _zMax = Math.Clamp(Math.Max(zMin, zMax), -radius, radius);
            _thetaMin = Math.Acos(Math.Clamp(zMin / radius, -1, 1));
            _thetaMax = Math.Acos(Math.Clamp(zMax / radius, -1, 1));
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            _material = material;
        }

        public override bool TryHit(Ray r, double tMin, double tMax, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();

            Vector3D oc = r.O - _center;
            var a = r.D.GetLengthSqrt();
            var halfB = Vector3D.Dot(oc, r.D);
            var c = oc.GetLengthSqrt() - _radius * _radius;
            var discriminant = halfB * halfB - a * c;

            if (discriminant < 0)
            {
                return false;
            }
            var sqrtd = Math.Sqrt(discriminant);

            var root = (-halfB - sqrtd) / a;
            if (root < tMin || tMax < root)
            {
                root = (-halfB + sqrtd) / a;
                if (root < tMin || tMax < root)
                {
                    return false;
                }
            }

            var pHit = r.At(root);

            var phi = Math.Atan2(pHit.Y - _center.Y, pHit.X - _center.X);
            if (phi < 0) phi += 2 * Math.PI;

            if ((_zMin > -Radius && pHit.Z < _zMin) ||
                (_zMax <  Radius && pHit.Z > _zMax) || phi > _phiMax)
            {
                if (root == tMax) return false;
                root = tMax;

                if ((_zMin > -Radius && pHit.Z < _zMin) ||
                    (_zMax <  Radius && pHit.Z > _zMax) || phi > _phiMax)
                    return false;
            }

            isect.T = root;
            isect.P = r.At(root);
            Normal3D outwardNormal = (Normal3D)((isect.P - _center) / _radius);
            isect.SetFaceNormal(r, outwardNormal);
            (isect.U, isect.V) = GetSphereUV(outwardNormal);
            isect.Material = _material;

            return true;
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(_center - new Vector3D(_radius, _radius, _radius),
                                 _center + new Vector3D(_radius, _radius, _radius));
            return true;
        }
        private (double u, double v) GetSphereUV(Normal3D p)
        {
            var theta = Math.Acos(-p.Y);
            var phi = Math.Atan2(-p.Z, p.X) + Math.PI;

            var u = phi / (2 * Math.PI);
            var v = theta / Math.PI;

            return (u, v);
        }
    }
}
