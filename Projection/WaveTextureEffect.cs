using Math_lib;
using System;
using System.Drawing;

namespace Projection
{
    class WaveTextureEffect : Effect
    {
        //Texture
        DirectBitmap _pTex;
        double       _texWidth;
        double       _texHeigh;
        double       _texClampX;
        double       _texClampY;

        //Wave
        double _time;
        double _freqWave   = 10;
        double _freqScroll = 5;
        double _amplitude  = 0.05;

        public override Color GetColor(Vertex v)
        {
            return _pTex.GetPixel((int)Math.Min(v.t.X * _texWidth + 0.5, _texClampX),
                                 (int)Math.Min(v.t.Y * _texHeigh + 0.5, _texClampY));
        }

        public void BindTexture(string filename)
        {
            Bitmap img = (Bitmap)Image.FromFile(filename);

            _pTex = new DirectBitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    _pTex.SetPixel(x, y, img.GetPixel(x, y));
                }
            }

            _texWidth = img.Width;
            _texHeigh = img.Height;
            _texClampX = _texWidth - 1;
            _texClampY = _texHeigh - 1;
        }

        public override Vertex Translate(Vertex vIn)
        {
            Point3D pos = rotation * vIn.pos + translation;
            pos += new Point3D(0, _amplitude * Math.Sin(_time * _freqScroll + pos.X * _freqWave), 0);
            return new Vertex(pos, vIn.t);
        }
        public void SetTime(double t)
        {
            _time = t;
        }

       
    }
}
