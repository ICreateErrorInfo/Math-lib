using Math_lib;
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
        private readonly Point3D _center;

        public Disk(Point3D center, double height, double radius, double innerRadius, double phiMax, Material material)
        {
            _height = height;
            _radius = radius;
            _innerRadius = innerRadius;
            _material = material;
            _phiMax = Mathe.ToRad(Math.Clamp(phiMax, 0, 360));
            _center = center;
        }

        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(new(-_radius, -_radius, _height),
                                 new(_radius, _radius, _height));
            return true;
        }

        public override bool Intersect(Ray r, double tMin, double tMax, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();

            Vector3D oc = r.O - _center;

            double tShapeHit = (_height - oc.Z) / r.D.Z;
            if(tShapeHit <= 0 || tShapeHit >= r.TMax || r.D.Z == 0)
            {
                return false;
            }

            Point3D pHit = r.At(tShapeHit);
            double dist2 = pHit.X * pHit.X + pHit.Y * pHit.Y;
            if(dist2 > _radius * _radius || dist2 < _innerRadius * _innerRadius)
            {
                return false;
            }

            double phi = Math.Atan2(pHit.Y, pHit.X);
            if(phi < 0) phi += 2 * Math.PI;
            if (phi > _phiMax) return false;

            isect.T = tShapeHit;
            isect.P = pHit;
            Normal3D outwardNormal = new(pHit.X,pHit.Y,_height + 1);
            isect.SetFaceNormal(r, outwardNormal);
            isect.Material = _material;

            return true;
        }
    }
}
