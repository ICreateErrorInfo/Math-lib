using Math_lib;

namespace Raytracing.Shapes
{
    class XYRect : Shape
    {
        double _x0, _x1, _y0, _y1, _k;

        public XYRect()
        {

        }
        public XYRect(double x0, double x1, double y0, double y1, double k)
        {
            _x0 = x0;
            _x1 = x1;
            _y0 = y0;
            _y1 = y1;
            _k = k;
            ObjectToWorld = Transform.Translate(new(0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction isect)
        {
            tMax = 0;
            isect = new SurfaceInteraction();
            var t = (_k - ray.O.Z) / ray.D.Z;

            if(t < 0.01 || t > ray.TMax)
            {
                return false;
            }

            var x = ray.O.X + t * ray.D.X;
            var y = ray.O.Y + t * ray.D.Y;

            if(x < _x0 || x > _x1 || y < _y0 || y > _y1)
            {
                return false;
            }

            isect.UCoordinate = (x - _x0) / (_x1 - _x0);
            isect.VCoordinate = (y - _y0) / (_y1 - _y0);
            tMax = t;

            var outwardNormal = new Normal3D(0,0,1);
            isect.SetFaceNormal(ray, outwardNormal);
            isect.P = ray.At(t);

            return true;
        }
        public override Bounds3D GetObjectBound()
        {
            return new Bounds3D(new Point3D(_x0, _y0, _k - 0.0001), new Point3D(_x1, _y1, _k + 0.0001));
        }
    }
    class XZRect : Shape
    {
        double _x0, _x1, _z0, _z1, _k;

        public XZRect()
        {

        }
        public XZRect(double x0, double x1, double z0, double z1, double k)
        {
            _x0 = x0;
            _x1 = x1;
            _z0 = z0;
            _z1 = z1;
            _k = k;
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction isect)
        {
            tMax = 0;
            isect = new SurfaceInteraction();
            var t = (_k - ray.O.Y) / ray.D.Y;

            if (t < 0.01 || t > ray.TMax)
            {
                return false;
            }

            var x = ray.O.X + t * ray.D.X;
            var z = ray.O.Z + t * ray.D.Z;

            if (x < _x0 || x > _x1 || z < _z0 || z > _z1)
            {
                return false;
            }

            isect.UCoordinate = (x - _x0) / (_x1 - _x0);
            isect.VCoordinate = (z - _z0) / (_z1 - _z0);
            tMax = t;
            var outwardNormal = new Normal3D(0, 1, 0);
            isect.SetFaceNormal(ray, outwardNormal);
            isect.P = ray.At(t);

            return true;
        }
        public override Bounds3D GetObjectBound()
        {
            return new Bounds3D(new Point3D(_x0, _k - 0.0001, _z0), new Point3D(_x1, _k + 0.0001, _z1));
        }
    }
    class YZRect : Shape
    {
        double _y0, _y1, _z0, _z1, _k;

        public YZRect()
        {

        }
        public YZRect(double y0, double y1, double z0, double z1, double k)
        {
            _y0 = y0;
            _y1 = y1;
            _z0 = z0;
            _z1 = z1;
            _k = k;
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction isect)
        {
            tMax = 0;
            isect = new SurfaceInteraction();
            var t = (_k - ray.O.X) / ray.D.X;
            if (t < 0.01 || t > ray.TMax)
            {
                return false;
            }

            var y = ray.O.Y + t * ray.D.Y;
            var z = ray.O.Z + t * ray.D.Z;

            if (y < _y0 || y > _y1 || z < _z0 || z > _z1)
            {
                return false;
            }

            isect.UCoordinate = (y - _y0) / (_y1 - _y0);
            isect.VCoordinate = (z - _z0) / (_z1 - _z0);
            tMax = t;
            var outwardNormal = new Normal3D(1, 0, 0);
            isect.SetFaceNormal(ray, outwardNormal);
            isect.P = ray.At(t);

            return true;
        }
        public override Bounds3D GetObjectBound()
        {
            return new Bounds3D(new Point3D(_k - 0.0001, _y0, _z0), new Point3D(_k + 0.0001,_y1, _z1));
        }
    }
}
