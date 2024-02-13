using Moarx.Math;
using Raytracing.Mathmatic;

namespace Raytracing.Shapes {
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
            WorldToObject = Transform.Translate(new(0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction interaction)
        {
            tMax = 0;
            interaction = new SurfaceInteraction();
            var t = (_k - ray.Origin.Z) / ray.Direction.Z;

            if(t < 0.01 || t > ray.TMax)
            {
                return false;
            }

            var x = ray.Origin.X + t * ray.Direction.X;
            var y = ray.Origin.Y + t * ray.Direction.Y;

            if(x < _x0 || x > _x1 || y < _y0 || y > _y1)
            {
                return false;
            }

            interaction.UCoordinate = (x - _x0) / (_x1 - _x0);
            interaction.VCoordinate = (y - _y0) / (_y1 - _y0);
            tMax = t;

            var outwardNormal = new Normal3D<double>(0,0,1);
            interaction.SetFaceNormal(ray, outwardNormal);
            interaction.P = ray.At(t);

            return true;
        }
        public override Bounds3D<double> GetObjectBound()
        {
            return new Bounds3D<double>(new Point3D<double>(_x0, _y0, _k - 0.0001), new Point3D<double>(_x1, _y1, _k + 0.0001));
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
            ObjectToWorld = Transform.Translate(new(0));
            WorldToObject = Transform.Translate(new(0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction interaction)
        {
            tMax = 0;
            interaction = new SurfaceInteraction();
            var t = (_k - ray.Origin.Y) / ray.Direction.Y;

            if (t < 0.01 || t > ray.TMax)
            {
                return false;
            }

            var x = ray.Origin.X + t * ray.Direction.X;
            var z = ray.Origin.Z + t * ray.Direction.Z;

            if (x < _x0 || x > _x1 || z < _z0 || z > _z1)
            {
                return false;
            }

            interaction.UCoordinate = (x - _x0) / (_x1 - _x0);
            interaction.VCoordinate = (z - _z0) / (_z1 - _z0);
            tMax = t;
            var outwardNormal = new Normal3D<double>(0, 1, 0);
            interaction.SetFaceNormal(ray, outwardNormal);
            interaction.P = ray.At(t);

            return true;
        }
        public override Bounds3D<double> GetObjectBound()
        {
            return new Bounds3D<double>(new Point3D<double>(_x0, _k - 0.0001, _z0), new Point3D<double>(_x1, _k + 0.0001, _z1));
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
            ObjectToWorld = Transform.Translate(new(0));
            WorldToObject = Transform.Translate(new(0));
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction isect)
        {
            tMax = 0;
            isect = new SurfaceInteraction();
            var t = (_k - ray.Origin.X) / ray.Direction.X;
            if (t < 0.01 || t > ray.TMax)
            {
                return false;
            }

            var y = ray.Origin.Y + t * ray.Direction.Y;
            var z = ray.Origin.Z + t * ray.Direction.Z;

            if (y < _y0 || y > _y1 || z < _z0 || z > _z1)
            {
                return false;
            }

            isect.UCoordinate = (y - _y0) / (_y1 - _y0);
            isect.VCoordinate = (z - _z0) / (_z1 - _z0);
            tMax = t;
            var outwardNormal = new Normal3D<double>(1, 0, 0);
            isect.SetFaceNormal(ray, outwardNormal);
            isect.P = ray.At(t);

            return true;
        }
        public override Bounds3D<double> GetObjectBound()
        {
            return new Bounds3D<double>(new Point3D<double>(_k - 0.0001, _y0, _z0), new Point3D<double>(_k + 0.0001,_y1, _z1));
        }
    }
}
