﻿using System;
using Moarx.Math;
using Raytracing.Mathmatic;

namespace Raytracing.Shapes {
    public class MovingSphere : Shape
    {
        private readonly Point3D<double>  _center0;
        private readonly Point3D<double>  _center1;
        private readonly double   _time0;
        private readonly double   _time1;
        private readonly double   _radius;

        public MovingSphere()
        {

        }
        public MovingSphere(Point3D<double>  center0,Point3D<double> center1, double time0, double time1, double radius)
        {
            _center0 = center0;
            _center1 = center1;
            _time0 = time0;
            _time1 = time1;
            _radius = radius;
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction interaction)
        {
            tMax = 0;
            interaction = new SurfaceInteraction();

            Vector3D<double> oc = ray.Origin - Center(ray.Time);
            var a = ray.Direction.GetLengthSquared();
            var halfB = oc * ray.Direction;
            var c = oc.GetLengthSquared() - _radius * _radius;
            var discriminant = halfB * halfB - a * c;

            if (discriminant < 0)
            {
                return false;
            }
            var sqrtd = Math.Sqrt(discriminant);

            var root = (-halfB - sqrtd) / a;
            if (root < 0.01 || ray.TMax < root)
            {
                root = (-halfB + sqrtd) / a;
                if (root < 0.01 || ray.TMax < root)
                {
                    return false;
                }
            }

            tMax = root;
            interaction.P = ray.At(root);
            Normal3D<double> outward_normal = new((interaction.P - Center(ray.Time)) / _radius);
            interaction.SetFaceNormal(ray, outward_normal);

            return true;
        }
        public override Bounds3D<double> GetObjectBound()
        {
            Bounds3D<double> box0 = new Bounds3D<double>(Center(_time0) - new Vector3D<double>(_radius, _radius, _radius),
                                         Center(_time0) + new Vector3D<double>(_radius, _radius, _radius));
            Bounds3D<double> box1 = new Bounds3D<double>(Center(_time1) - new Vector3D<double>(_radius, _radius, _radius),
                                         Center(_time1) + new Vector3D<double>(_radius, _radius, _radius));
            return Bounds3D<double>.Union(box0, box1);
        }
        public virtual Point3D<double> Center(double time)
        {
            return _center0 + ((time - _time0) / (_time1 - _time0)) * (_center1 - _center0);
        }
    }
}
