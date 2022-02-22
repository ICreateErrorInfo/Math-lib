using System;
using Math_lib;

namespace Raytracing
{
    public class sphere : Hittable
    {
        private readonly Point3D  _center;
        private readonly double   _radius;
        private readonly Material _material;
        public double Radius => _radius;
        public Point3D Center => _center;
        public Material MatPtr => _material;

        public sphere() { }
        public sphere(Point3D center, double radius, Material material)
        {
            _center = center;
            _radius = radius;
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
