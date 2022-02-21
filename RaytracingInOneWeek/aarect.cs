﻿using Math_lib;

namespace Raytracing
{
    class xy_rect : hittable
    {
        public xy_rect()
        {

        }
        public xy_rect(double _x0, double _x1, double _y0, double _y1, double _k, material mat)
        {
            x0 = _x0;
            x1 = _x1;
            y0 = _y0;
            y1 = _y1;
            k = _k;
            mp = mat;
        }
        public double x0, x1, y0, y1, k;
        public material mp;
        public override bool Hit(Ray r, double t_min, double t_max, ref SurfaceInteraction insec)
        {
            var t = (k - r.o.Z) / r.d.Z;
            if(t < t_min || t > t_max)
            {
                return false;
            }

            var x = r.o.X + t * r.d.X;
            var y = r.o.Y + t * r.d.Y;
            if(x < x0 || x > x1 || y < y0 || y > y1)
            {
                return false;
            }

            insec.u = (x - x0) / (x1 - x0);
            insec.v = (y - y0) / (y1 - y0);
            insec.t = t;
            var outward_normal = new Normal3D(0,0,1);
            insec.set_face_normal(r, outward_normal);
            insec.mat_ptr = mp;
            insec.p = r.At(t);

            return true;
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(new Point3D(x0, y0, k - 0.0001), new Point3D(x1, y1, k + 0.0001));
            return true;
        }
    }
    class xz_rect : hittable
    {
        public xz_rect()
        {

        }
        public xz_rect(double _x0, double _x1, double _z0, double _z1, double _k, material mat)
        {
            x0 = _x0;
            x1 = _x1;
            z0 = _z0;
            z1 = _z1;
            k = _k;
            mp = mat;
        }
        double x0, x1, z0, z1, k;
        material mp;

        public override bool Hit(Ray r, double t_min, double t_max, ref SurfaceInteraction insec)
        {
            var t = (k - r.o.Y) / r.d.Y;
            if (t < t_min || t > t_max)
            {
                return false;
            }

            var x = r.o.X + t * r.d.X;
            var z = r.o.Z + t * r.d.Z;

            if (x < x0 || x > x1 || z < z0 || z > z1)
            {
                return false;
            }

            insec.u = (x - x0) / (x1 - x0);
            insec.v = (z - z0) / (z1 - z0);
            insec.t = t;
            var outward_normal = new Normal3D(0, 1, 0);
            insec.set_face_normal(r, outward_normal);
            insec.mat_ptr = mp;
            insec.p = r.At(t);

            return true;
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(new Point3D(x0, k - 0.0001, z0), new Point3D(x1, k + 0.0001, z1));
            return true;
        }
    }
    class yz_rect : hittable
    {
        public yz_rect()
        {

        }
        public yz_rect(double _y0, double _y1, double _z0, double _z1, double _k, material mat)
        {
            y0 = _y0;
            y1 = _y1;
            z0 = _z0;
            z1 = _z1;
            k = _k;
            mp = mat;
        }
        double y0, y1, z0, z1, k;
        material mp;

        public override bool Hit(Ray r, double t_min, double t_max, ref SurfaceInteraction insec)
        {
            var t = (k - r.o.X) / r.d.X;
            if (t < t_min || t > t_max)
            {
                return false;
            }

            var y = r.o.Y + t * r.d.Y;
            var z = r.o.Z + t * r.d.Z;

            if (y < y0 || y > y1 || z < z0 || z > z1)
            {
                return false;
            }

            insec.u = (y - y0) / (y1 - y0);
            insec.v = (z - z0) / (z1 - z0);
            insec.t = t;
            var outward_normal = new Normal3D(1, 0, 0);
            insec.set_face_normal(r, outward_normal);
            insec.mat_ptr = mp;
            insec.p = r.At(t);

            return true;
        }
        public override bool bounding_box(double time0, double time1, ref Bounds3D bound)
        {
            bound = new Bounds3D(new Point3D(k - 0.0001, y0, z0), new Point3D(k + 0.0001,y1, z1));
            return true;
        }
    }
}
