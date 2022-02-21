using System;
using System.Collections.Generic;
using System.Text;
using Math_lib;

namespace Raytracing
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

        public override bool scatter(Ray r_in, ref SurfaceInteraction isect, ref  Vector3D attenuation, ref Ray scattered)
        {
            return false;
        }

        public override Vector3D emitted(double u, double v, Point3D p)
        {
            return emit.value(u, v, p);
        }
    }
}
