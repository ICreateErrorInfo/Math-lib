using Math_lib;
using Raytracing.Materials;

namespace Raytracing.Shapes
{
    class XYRect : Shape
    {
        double _x0, _x1, _y0, _y1, _k;
        Material _material;

        public XYRect()
        {

        }
        public XYRect(double x0, double x1, double y0, double y1, double k, Material material)
        {
            _x0 = x0;
            _x1 = x1;
            _y0 = y0;
            _y1 = y1;
            _k = k;
            _material = material;
        }

        public override bool Intersect(Ray ray, double tMin, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();
            var t = (_k - ray.O.Z) / ray.D.Z;

            if(t < tMin || t > ray.TMax)
            {
                return false;
            }

            var x = ray.O.X + t * ray.D.X;
            var y = ray.O.Y + t * ray.D.Y;

            if(x < _x0 || x > _x1 || y < _y0 || y > _y1)
            {
                return false;
            }

            insec.U = (x - _x0) / (_x1 - _x0);
            insec.V = (y - _y0) / (_y1 - _y0);
            insec.T = t;

            var outwardNormal = new Normal3D(0,0,1);
            insec.SetFaceNormal(ray, outwardNormal);
            insec.Material = _material;
            insec.P = ray.At(t);

            return true;
        }
        public override Bounds3D GetBoundingBox()
        {
            return new Bounds3D(new Point3D(_x0, _y0, _k - 0.0001), new Point3D(_x1, _y1, _k + 0.0001));
        }
    }
    class XZRect : Shape
    {
        double _x0, _x1, _z0, _z1, _k;
        Material _material;

        public XZRect()
        {

        }
        public XZRect(double x0, double x1, double z0, double z1, double k, Material material)
        {
            _x0 = x0;
            _x1 = x1;
            _z0 = z0;
            _z1 = z1;
            _k = k;
            _material = material;
        }

        public override bool Intersect(Ray ray, double tMin, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();
            var t = (_k - ray.O.Y) / ray.D.Y;

            if (t < tMin || t > ray.TMax)
            {
                return false;
            }

            var x = ray.O.X + t * ray.D.X;
            var z = ray.O.Z + t * ray.D.Z;

            if (x < _x0 || x > _x1 || z < _z0 || z > _z1)
            {
                return false;
            }

            insec.U = (x - _x0) / (_x1 - _x0);
            insec.V = (z - _z0) / (_z1 - _z0);
            insec.T = t;
            var outwardNormal = new Normal3D(0, 1, 0);
            insec.SetFaceNormal(ray, outwardNormal);
            insec.Material = _material;
            insec.P = ray.At(t);

            return true;
        }
        public override Bounds3D GetBoundingBox()
        {
            return new Bounds3D(new Point3D(_x0, _k - 0.0001, _z0), new Point3D(_x1, _k + 0.0001, _z1));
        }
    }
    class YZRect : Shape
    {
        double _y0, _y1, _z0, _z1, _k;
        Material _material;

        public YZRect()
        {

        }
        public YZRect(double y0, double y1, double z0, double z1, double k, Material material)
        {
            _y0 = y0;
            _y1 = y1;
            _z0 = z0;
            _z1 = z1;
            _k = k;
            _material = material;
        }

        public override bool Intersect(Ray ray, double tMin, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();
            var t = (_k - ray.O.X) / ray.D.X;
            if (t < tMin || t > ray.TMax)
            {
                return false;
            }

            var y = ray.O.Y + t * ray.D.Y;
            var z = ray.O.Z + t * ray.D.Z;

            if (y < _y0 || y > _y1 || z < _z0 || z > _z1)
            {
                return false;
            }

            insec.U = (y - _y0) / (_y1 - _y0);
            insec.V = (z - _z0) / (_z1 - _z0);
            insec.T = t;
            var outwardNormal = new Normal3D(1, 0, 0);
            insec.SetFaceNormal(ray, outwardNormal);
            insec.Material = _material;
            insec.P = ray.At(t);

            return true;
        }
        public override Bounds3D GetBoundingBox()
        {
            return new Bounds3D(new Point3D(_k - 0.0001, _y0, _z0), new Point3D(_k + 0.0001,_y1, _z1));
        }
    }
}
