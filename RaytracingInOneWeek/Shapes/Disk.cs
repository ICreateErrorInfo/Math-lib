using Math_lib;
using Raytracing.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Shapes
{
    public class Disk : Shape
    {
        private readonly double _height;
        private readonly double _radius;
        private readonly double _innerRadius;
        private readonly double _phiMax;
        private readonly Material _material;
        private readonly Transform _worldToObject;
        private readonly Transform _objectToWorld;

        public Disk(Point3D center, double height, double radius, double innerRadius, double phiMax, Material material)
        {
            _height = height;
            _radius = radius;
            _innerRadius = innerRadius;
            _material = material;
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            _worldToObject = Transform.Translate(new Point3D(0, 0, 0) - center);
            _objectToWorld = Transform.Translate(center - new Point3D(0, 0, 0));
        }

        public override Bounds3D GetBoundingBox()
        {
            return new Bounds3D(new(-_radius, -_radius, _height),
                                 new(_radius, _radius, _height));
        }

        public override bool Intersect(Ray ray, double tMin, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();

            Ray rTransformed = _worldToObject.m * ray;

            double t0 = (_height - rTransformed.O.Z) / rTransformed.D.Z;
            if(t0 <= 0 || t0 >= rTransformed.TMax || rTransformed.D.Z == 0)
            {
                return false;
            }

            Point3D pHit = rTransformed.At(t0);
            double dist2 = pHit.X * pHit.X + pHit.Y * pHit.Y;
            if(dist2 > _radius * _radius || dist2 < _innerRadius * _innerRadius)
            {
                return false;
            }

            double phi = Math.Atan2(pHit.Y, pHit.X);
            if(phi < 0) phi += 2 * Math.PI;
            if (phi > _phiMax) return false;

            ray.TMax = t0;
            isect.T = t0;
            isect.P = ray.At(t0);
            Normal3D outwardNormal = new(pHit.X,pHit.Y,_height + 1);
            isect.SetFaceNormal(ray, outwardNormal);
            isect.Material = _material;

            return true;
        }
    }
}
