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
        private readonly Material _material;
        private readonly Transform _worldToObject;
        private readonly Transform _objectToWorld;
        public double Radius => _radius;
        public Material Material => _material;

        public Sphere() { }
        public Sphere(Point3D center, double radius, Material material)
        {
            _radius = radius;
            _material = material;
            _zMin = -radius;
            _zMax = radius;
            _thetaMax = 1;
            _thetaMin = -1;
            _phiMax = 360;
            _worldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            _objectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
        }
        public Sphere(Point3D center, double radius, double zMin, double zMax, double phiMax, Material material) 
        {
            _radius = radius;
            _zMin = Math.Clamp(Math.Min(zMin, zMax), -radius, radius);
            _zMax = Math.Clamp(Math.Max(zMin, zMax), -radius, radius);
            _thetaMin = Math.Acos(Math.Clamp(zMin / radius, -1, 1));
            _thetaMax = Math.Acos(Math.Clamp(zMax / radius, -1, 1));
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            _material = material;
            _worldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            _objectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
        }

        public override bool Intersect(Ray ray, double tMin, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();

            Ray rTransformed = _worldToObject.m * ray;

            var a = rTransformed.D.GetLengthSqrt();
            var halfB = Vector3D.Dot((Vector3D)rTransformed.O, rTransformed.D);
            var c = ((Vector3D)rTransformed.O).GetLengthSqrt() - _radius * _radius;

            double t0;
            if(!Mathe.SolveQuadratic(a, halfB, c, out t0, tMin, ray.TMax))
            {
                return false;
            }

            var pHit = rTransformed.At(t0);

            var phi = Math.Atan2(pHit.Y, pHit.X);
            if (phi < 0) phi += 2 * Math.PI;

            if ((_zMin > -Radius && pHit.Z < _zMin) ||
                (_zMax < Radius && pHit.Z > _zMax) || phi > _phiMax)
            {
                if (t0 == ray.TMax) return false;
                t0 = ray.TMax;

                if ((_zMin > -Radius && pHit.Z < _zMin) ||
                    (_zMax < Radius && pHit.Z > _zMax) || phi > _phiMax)
                    return false;
            }

            ray.TMax = t0;
            insec.T = t0;
            insec.P = ray.At(t0);
            Point3D _center = _objectToWorld.m * new Point3D(0,0,0);
            Normal3D outwardNormal = (Normal3D)((insec.P - _center) / Radius);
            insec.SetFaceNormal(ray, outwardNormal);
            (insec.U, insec.V) = GetSphereUV(outwardNormal);
            insec.Material = _material;

            return true;
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(new Point3D(_radius, _radius, _radius),
                                 new Point3D(_radius, _radius, _radius));
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
