using Math_lib;

namespace Raytracing
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

        public override bool Intersect(Ray r, double tMin, double tMax, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();
            var t = (_k - r.O.Z) / r.D.Z;

            if(t < tMin || t > tMax)
            {
                return false;
            }

            var x = r.O.X + t * r.D.X;
            var y = r.O.Y + t * r.D.Y;

            if(x < _x0 || x > _x1 || y < _y0 || y > _y1)
            {
                return false;
            }

            insec.U = (x - _x0) / (_x1 - _x0);
            insec.V = (y - _y0) / (_y1 - _y0);
            insec.T = t;

            var outwardNormal = new Normal3D(0,0,1);
            insec.SetFaceNormal(r, outwardNormal);
            insec.Material = _material;
            insec.P = r.At(t);

            return true;
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(new Point3D(_x0, _y0, _k - 0.0001), new Point3D(_x1, _y1, _k + 0.0001));
            return true;
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

        public override bool Intersect(Ray r, double tMin, double tMax, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();
            var t = (_k - r.O.Y) / r.D.Y;

            if (t < tMin || t > tMax)
            {
                return false;
            }

            var x = r.O.X + t * r.D.X;
            var z = r.O.Z + t * r.D.Z;

            if (x < _x0 || x > _x1 || z < _z0 || z > _z1)
            {
                return false;
            }

            insec.U = (x - _x0) / (_x1 - _x0);
            insec.V = (z - _z0) / (_z1 - _z0);
            insec.T = t;
            var outwardNormal = new Normal3D(0, 1, 0);
            insec.SetFaceNormal(r, outwardNormal);
            insec.Material = _material;
            insec.P = r.At(t);

            return true;
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(new Point3D(_x0, _k - 0.0001, _z0), new Point3D(_x1, _k + 0.0001, _z1));
            return true;
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

        public override bool Intersect(Ray r, double t_min, double t_max, out SurfaceInteraction insec)
        {
            insec = new SurfaceInteraction();
            var t = (_k - r.O.X) / r.D.X;
            if (t < t_min || t > t_max)
            {
                return false;
            }

            var y = r.O.Y + t * r.D.Y;
            var z = r.O.Z + t * r.D.Z;

            if (y < _y0 || y > _y1 || z < _z0 || z > _z1)
            {
                return false;
            }

            insec.U = (y - _y0) / (_y1 - _y0);
            insec.V = (z - _z0) / (_z1 - _z0);
            insec.T = t;
            var outwardNormal = new Normal3D(1, 0, 0);
            insec.SetFaceNormal(r, outwardNormal);
            insec.Material = _material;
            insec.P = r.At(t);

            return true;
        }
        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(new Point3D(_k - 0.0001, _y0, _z0), new Point3D(_k + 0.0001,_y1, _z1));
            return true;
        }
    }
}
