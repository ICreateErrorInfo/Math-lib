using System;
using System.Collections.Generic;
using System.Text;
using Math_lib;

namespace Raytracing
{
    class DiffuseLight : Material
    {
        private readonly Texture _emit;

        public DiffuseLight(Texture a)
        {
            _emit = a;
        }
        public DiffuseLight(Vector3D c)
        {
            _emit = new SolidColor(c);
        }

        public override bool Scatter(Ray rIn, ref SurfaceInteraction isect, out  Vector3D attenuation, out Ray scattered)
        {
            attenuation = new Vector3D();
            scattered = new Ray();
            return false;
        }

        public override Vector3D Emitted(double u, double v, Point3D p)
        {
            return _emit.Value(u, v, p);
        }
    }
}
