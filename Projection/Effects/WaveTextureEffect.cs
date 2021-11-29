using Math_lib;
using Math_lib.VertexAttributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Effects
{
    class WaveTextureEffect : Effect
    {
        public override Color GetColor(Vertex v)
        {
            if (v.TryGetValue<TextureCoordinateVertexAttribute>(out var vT))
            {
                return _pTex.GetPixel((int)Math.Min(vT.T.X * _texWidth + 0.5, _texClampX),
                                      (int)Math.Min(vT.T.Y * _texHeigh + 0.5, _texClampY));
            }
            throw new InvalidOperationException();
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
            Point3D pos = rotation * vIn.Pos + translation;
            pos += new Point3D(0, _amplitude * Math.Sin(_time * _freqScroll + pos.X * _freqWave), 0);
            return new Vertex(pos, vIn.Attributes);
        }
        public void SetTime(double t)
        {
            _time = t;
        }

        public override Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex)
        {
            return new(v0, v1, v2);
        }

        //Texture
        public DirectBitmap _pTex;
        double _texWidth;
        double _texHeigh;
        double _texClampX;
        double _texClampY;

        //Wave
        double _time = 0;
        double _freqWave = 10;
        double _freqScroll = 5;
        double _amplitude = 0.05;
    }
}
