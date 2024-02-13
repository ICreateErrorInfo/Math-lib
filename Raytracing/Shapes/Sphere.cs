using System;
using Moarx.Math;
using Raytracing.Mathmatic;

namespace Raytracing.Shapes {
    public class Sphere : Shape
    {
        private readonly double _zMin, _zMax;
        private readonly double _thetaMin, _thetaMax, _phiMax;
        private readonly double   _radius;

        public Sphere() { }
        public Sphere(Point3D<double> center, double radius)
        {
            _radius = radius;
            _zMin = -radius;
            _zMax = radius;
            _thetaMax = 1;
            _thetaMin = -1;
            _phiMax = 360;
            WorldToObject = Transform.Translate(new Point3D<double>(0, 0, 0) - center);
            ObjectToWorld = Transform.Translate(center - new Point3D<double>(0, 0, 0));
        }
        public Sphere(Point3D<double> center, double radius, double zMin, double zMax, double phiMax) 
        {
            _radius = radius;
            _zMin = Math.Clamp(Math.Min(zMin, zMax), -radius, radius);
            _zMax = Math.Clamp(Math.Max(zMin, zMax), -radius, radius);
            _thetaMin = Math.Acos(Math.Clamp(zMin / radius, -1, 1));
            _thetaMax = Math.Acos(Math.Clamp(zMax / radius, -1, 1));
            _phiMax = MathmaticMethods.ConvertToRadians(Math.Clamp(phiMax, 0, 360));
            WorldToObject = Transform.Translate(new Point3D<double>(0, 0, 0) - center);
            ObjectToWorld = Transform.Translate(center - new Point3D<double>(0, 0, 0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction interaction)
        {
            tMax = 0;
            interaction = new SurfaceInteraction();

            Ray rTransformed = WorldToObject * ray;

            var a = rTransformed.Direction.GetLengthSquared();
            var halfB = rTransformed.Origin.ToVector() * rTransformed.Direction;
            var c = (rTransformed.Origin.ToVector()).GetLengthSquared() - _radius * _radius;

            double t0;
            if(!MathmaticMethods.SolveQuadratic(a, halfB, c, out t0, 0.01, ray.TMax))
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
            interaction.P = ray.At(t0);
            Point3D<double> _center = ObjectToWorld * new Point3D<double>(0,0,0);
            Normal3D<double> outwardNormal = new(((interaction.P - _center) / _radius));
            interaction.SetFaceNormal(ray, outwardNormal);
            (interaction.UCoordinate, interaction.VCoordinate) = GetSphereUV(outwardNormal);

            return true;
        }
        public override Bounds3D<double> GetObjectBound()
        {
            return new Bounds3D<double>(new Point3D<double>(-_radius, -_radius, _zMin),
                                new Point3D<double>( _radius,  _radius, _zMax));
        }
        private (double u, double v) GetSphereUV(Normal3D<double> p)
        {
            var theta = Math.Acos(-p.Y);
            var phi = Math.Atan2(-p.Z, p.X) + Math.PI;

            var u = phi / (2 * Math.PI);
            var v = theta / Math.PI;

            return (u, v);
        }
    }
}
