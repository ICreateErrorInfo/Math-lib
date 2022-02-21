using System;
using System.Collections.Generic;
using System.Text;
using Math_lib;

namespace Raytracing
{
    abstract class Texture
    {
        public abstract Vector3D value(double u, double v, Point3D p);
    }
    class solid_color : Texture
    {
        public solid_color()
        {

        }
        public solid_color(Vector3D c)
        {
            color_value = c;
        }

        public solid_color(double red, double green, double blue)
        {
            color_value = new Vector3D(red, green, blue);
        }
        private Vector3D color_value;

        public override Vector3D value(double u, double v, Point3D p)
        {
            return color_value;
        }
    }
    class checker_texture : Texture
    {
        public checker_texture()
        {

        }
        public checker_texture(Texture _even, Texture _odd)
        {
            even = _even;
            odd = _odd;
        }
        public checker_texture(Vector3D c1, Vector3D c2)
        {
            even = new solid_color(c1);
            odd = new solid_color(c2);
        }
        public Texture odd;
        public Texture even;

        public override Vector3D value(double u, double v, Point3D p)
        {
            var sines = Math.Sin(10 * p.X) * Math.Sin(10 * p.Y) * Math.Sin(10 * p.Z);
            if(sines < 0)
            {
                return odd.value(u, v, p);
            }
            else
            {
                return even.value(u, v, p);
            }
        }
    }
    class noise_texture : Texture
    {
        public noise_texture()
        {

        }
        public noise_texture(double sc)
        {
            scale = sc;
        }
        public override Vector3D value(double u, double v, Point3D p)
        {
            return new Vector3D(1,1,1) * 0.5 * (1 + Math.Sin(scale * p.Z + 10*noise.turb(scale * (Vector3D)p)));
        }

        public Perlin noise = new Perlin();
        double scale;
    }
}
