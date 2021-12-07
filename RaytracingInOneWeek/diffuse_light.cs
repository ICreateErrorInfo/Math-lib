using System;
using System.Collections.Generic;
using System.Text;
using Math_lib;

namespace RaytracingInOneWeek
{
    class diffuse_light : material
    {
        public diffuse_light(Texture a)
        {
            emit = a;
        }
        public diffuse_light(Vector3D c)
        {
            emit = new solid_color(c);
        }
        public Texture emit;

        public override zwischenSpeicher scatter(Ray r_in, hit_record rec, Vector3D attenuation, Ray scattered)
        {
            zwischenSpeicher zw = new zwischenSpeicher();
            zw.IsTrue = false;
            return zw;
        }

        public override Vector3D emitted(double u, double v, Point3D p)
        {
            return emit.value(u, v, p);
        }
    }
}
