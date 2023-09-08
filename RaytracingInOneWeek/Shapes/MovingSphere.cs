using System;
using Math_lib;
using Raytracing.Mathmatic;

namespace Raytracing.Shapes {
    public class MovingSphere : Shape
    {
        private readonly Point3D  _center0;
        private readonly Point3D  _center1;
        private readonly double   _time0;
        private readonly double   _time1;
        private readonly double   _radius;

        public MovingSphere()
        {

        }
        public MovingSphere(Point3D center0, Point3D center1, double time0, double time1, double radius)
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

            Vector3D oc = ray.O - Center(ray.Time);
            var a = ray.D.GetLengthSqrt();
            var halfB = Vector3D.Dot(oc, ray.D);
            var c = oc.GetLengthSqrt() - _radius * _radius;
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
            Normal3D outward_normal = (Normal3D)(Vector3D)((interaction.P - Center(ray.Time)) / _radius);
            interaction.SetFaceNormal(ray, outward_normal);

            return true;
        }
        public override Bounds3D GetObjectBound()
        {
            Bounds3D box0 = new Bounds3D(Center(_time0) - new Vector3D(_radius, _radius, _radius),
                                         Center(_time0) + new Vector3D(_radius, _radius, _radius));
            Bounds3D box1 = new Bounds3D(Center(_time1) - new Vector3D(_radius, _radius, _radius),
                                         Center(_time1) + new Vector3D(_radius, _radius, _radius));
            return Bounds3D.Union(box0, box1);
        }
        public virtual Point3D Center(double time)
        {
            return _center0 + ((time - _time0) / (_time1 - _time0)) * (_center1 - _center0);
        }
    }
}
