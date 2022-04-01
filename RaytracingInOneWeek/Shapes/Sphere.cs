using System;
using Math_lib;
using Raytracing.Materials;

namespace Raytracing.Shapes
{
    public class Sphere : Shape
    {
        private readonly double _zMin, _zMax;
        private readonly double _thetaMin, _thetaMax, _phiMax;
        private readonly double   _radius;

        public Sphere() { }
        public Sphere(Point3D center, double radius)
        {
            _radius = radius;
            _zMin = -radius;
            _zMax = radius;
            _thetaMax = 1;
            _thetaMin = -1;
            _phiMax = 360;
            WorldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            ObjectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
        }
        public Sphere(Point3D center, double radius, double zMin, double zMax, double phiMax) 
        {
            _radius = radius;
            _zMin = Math.Clamp(Math.Min(zMin, zMax), -radius, radius);
            _zMax = Math.Clamp(Math.Max(zMin, zMax), -radius, radius);
            _thetaMin = Math.Acos(Math.Clamp(zMin / radius, -1, 1));
            _thetaMax = Math.Acos(Math.Clamp(zMax / radius, -1, 1));
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            WorldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            ObjectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction isect)
        {
            tMax = 0;
            isect = new SurfaceInteraction();

            Ray rTransformed = WorldToObject.m * ray;

            var a = rTransformed.D.GetLengthSqrt();
            var halfB = Vector3D.Dot((Vector3D)rTransformed.O, rTransformed.D);
            var c = ((Vector3D)rTransformed.O).GetLengthSqrt() - _radius * _radius;

            double t0;
            if(!Mathe.SolveQuadratic(a, halfB, c, out t0, 0.01, ray.TMax))
            {
                return false;
            }

            var pHit = rTransformed.At(t0);

            var phi = Math.Atan2(pHit.Y, pHit.X);
            if (phi < 0) phi += 2 * Math.PI;

            if ((_zMin > -_radius && pHit.Z < _zMin) ||
                (_zMax < _radius && pHit.Z > _zMax) || phi > _phiMax)
            {
                if (t0 == ray.TMax) return false;
                t0 = ray.TMax;

                if ((_zMin > -_radius && pHit.Z < _zMin) ||
                    (_zMax < _radius && pHit.Z > _zMax) || phi > _phiMax)
                    return false;
            }

            tMax = t0;
            isect.P = ray.At(t0);
            Point3D _center = ObjectToWorld.m * new Point3D(0,0,0);
            Normal3D outwardNormal = (Normal3D)((isect.P - _center) / _radius);
            isect.SetFaceNormal(ray, outwardNormal);
            (isect.U, isect.V) = GetSphereUV(outwardNormal);

            return true;
        }
        public override Bounds3D GetObjectBound()
        {
            return new Bounds3D(new Point3D(-_radius, -_radius, _zMin),
                                new Point3D( _radius,  _radius, _zMax));
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
