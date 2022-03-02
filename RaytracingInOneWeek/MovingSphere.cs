using System;
using Math_lib;
using System.Collections.Generic;
using System.Text;

namespace Raytracing
{
    class MovingSphere : Shape
    {
        private readonly Point3D  _center0;
        private readonly Point3D  _center1;
        private readonly double   _time0;
        private readonly double   _time1;
        private readonly double   _radius;
        private readonly Material _material;

        public MovingSphere()
        {

        }
        public MovingSphere(Point3D center0, Point3D center1, double time0, double time1, double radius, Material material)
        {
            _center0 = center0;
            _center1 = center1;
            _time0 = time0;
            _time1 = time1;
            _radius = radius;
            _material = material;
        }

        public override bool TryHit(Ray r, double tMin, double tMax, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();

            Vector3D oc = r.O - Center(r.TMax);
            var a = r.D.GetLengthSqrt();
            var halfB = Vector3D.Dot(oc, r.D);
            var c = oc.GetLengthSqrt() - _radius * _radius;

            var discriminant = halfB * halfB - a * c;
            if(discriminant < 0)
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
            isect.P = r.At(isect.T);
            Normal3D outward_normal = (Normal3D)(Vector3D)((isect.P - Center(r.TMax)) / _radius);
            isect.SetFaceNormal(r, outward_normal);
            isect.Material = _material;

            return true;
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            Bounds3D box0 = new Bounds3D(Center(time0) - new Vector3D(_radius, _radius, _radius),
                                         Center(time0) + new Vector3D(_radius, _radius, _radius));
            Bounds3D box1 = new Bounds3D(Center(time1) - new Vector3D(_radius, _radius, _radius),
                                         Center(time1) + new Vector3D(_radius, _radius, _radius));
            bound = Bounds3D.Union(box0, box1);
            return true;
        }
        public virtual Point3D Center(double time)
        {
            return _center0 + ((time - _time0) / (_time1 - _time0)) * (_center1 - _center0);
        }
    }
}
