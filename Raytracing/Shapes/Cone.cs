using Moarx.Math;
using Raytracing.Mathmatic;
using System;

namespace Raytracing.Shapes {
    public class Cone : Shape
    {
        private readonly double _phiMax;
        private readonly double _height;
        private readonly double _radius;

        public Cone(Point3D<double> center, double radius, double height, double phiMax)
        {
            _height = height;
            _phiMax = MathmaticMethods.ConvertToRadians(Math.Clamp(phiMax, 0, 360));
            _radius = radius;
            WorldToObject = Transform.Translate(new Point3D<double>(0, 0, 0) - center);
            ObjectToWorld = Transform.Translate(center - new Point3D<double>(0, 0, 0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction interaction)
        {
            tMax = 0;
            interaction = new SurfaceInteraction();

            Ray rTransformed = WorldToObject * ray;

            double ox = rTransformed.Origin.X, oy = rTransformed.Origin.Y, oz = rTransformed.Origin.Z;
            double dx = rTransformed.Direction.X, dy = rTransformed.Direction.Y, dz = rTransformed.Direction.Z;

            double k = _radius / _height;
            k = k * k;
            double a = dx * dx + dy * dy - k * dz * dz;
            double b = 2 * (dx * ox + dy * oy - k * dz * (oz - _height));
            double c = ox * ox + oy * oy - k * (oz - _height) * (oz - _height);

            double t0, t1;
            if (!MathmaticMethods.SolveQuadratic(a, b, c, out t0, out t1, 0.01, ray.TMax))
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
            interaction.P = ray.At(t0);
           Point3D<double>intersectionPointTransformed = rTransformed.At(t0);
            double hNew = _height - intersectionPointTransformed.Z;
            double XY = Math.Sqrt((hNew * hNew)/2);
            Normal3D<double> outwardNormal = new Normal3D<double>((new Vector3D<double>(XY, XY, Math.Sqrt(intersectionPointTransformed.X * intersectionPointTransformed.X + intersectionPointTransformed.Y * intersectionPointTransformed.Y))).Normalize());
            interaction.SetFaceNormal(ray, outwardNormal);

            return true;
        }

        public override Bounds3D<double> GetObjectBound()
        {
            return new Bounds3D<double>(new Point3D<double>(-_radius, -_radius, 0),
                                new Point3D<double>( _radius,  _radius, _height));
        }
    }
}
