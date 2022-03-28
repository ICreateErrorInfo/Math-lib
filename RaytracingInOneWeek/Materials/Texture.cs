using System;
using System.Collections.Generic;
using System.Text;
using Math_lib;

namespace Raytracing.Materials
{
    abstract class Texture
    {
        public abstract Vector3D Value(double u, double v, Point3D p);
    }
    class SolidColor : Texture
    {
        private readonly Vector3D _colorValue;

        public SolidColor()
        {

        }
        public SolidColor(Vector3D c)
        {
            _colorValue = c;
        }
        public SolidColor(double red, double green, double blue)
        {
            _colorValue = new Vector3D(red, green, blue);
        }

        public override Vector3D Value(double u, double v, Point3D p)
        {
            return _colorValue;
        }
    }
    class CheckerTexture : Texture
    {
        private readonly Texture _odd;
        private readonly Texture _even;

        public CheckerTexture()
        {

        }
        public CheckerTexture(Texture even, Texture odd)
        {
            _even = even;
            _odd = odd;
        }
        public CheckerTexture(Vector3D c1, Vector3D c2)
        {
            _even = new SolidColor(c1);
            _odd = new SolidColor(c2);
        }

        public override Vector3D Value(double u, double v, Point3D p)
        {
            var sines = Math.Sin(10 * p.X) * Math.Sin(10 * p.Y) * Math.Sin(10 * p.Z);
            if(sines < 0)
            {
                return _odd.Value(u, v, p);
            }
            else
            {
                return _even.Value(u, v, p);
            }
        }
    }
    class NoiseTexture : Texture
    {
        readonly public Perlin _noise = new Perlin();
        private readonly double _scale;

        public NoiseTexture()
        {

        }
        public NoiseTexture(double scale)
        {
            _scale = scale;
        }

        public override Vector3D Value(double u, double v, Point3D p)
        {
            return new Vector3D(1,1,1) * 0.5 * (1 + Math.Sin(_scale * p.Z + 10*_noise.Turb(_scale * (Vector3D)p)));
        }
    }
}
