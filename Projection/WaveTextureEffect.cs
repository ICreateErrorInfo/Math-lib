using Math_lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    class WaveTextureEffect : Effect
    {
        public override Color GetColor(Vertex v)
        {
            return pTex.GetPixel((int)Math.Min(v.t.X * texWidth + 0.5, texClampX),
                                 (int)Math.Min(v.t.Y * texHeigh + 0.5, texClampY));
        }

        public void BindTexture(string filename)
        {
            Bitmap img = (Bitmap)Image.FromFile(filename);

            pTex = new DirectBitmap(img.Width, img.Height);
            for (int x = 0; x < img.Width; x++)
            {
                for (int y = 0; y < img.Height; y++)
                {
                    pTex.SetPixel(x, y, img.GetPixel(x, y));
                }
            }

            texWidth = img.Width;
            texHeigh = img.Height;
            texClampX = texWidth - 1;
            texClampY = texHeigh - 1;
        }

        public override Vertex Translate(Vertex vIn)
        {
            Point3D pos = rotation * vIn.pos + translation;
            pos += new Point3D(0, amplitude * Math.Sin(time * freqScroll + pos.X * freqWave), 0);
            return new Vertex(pos, vIn.t);
        }
        public void SetTime(double t)
        {
            time = t;
        }

        public override Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex)
        {
            return new(v0, v1, v2);
        }

        //Texture
        public DirectBitmap pTex;
        double texWidth;
        double texHeigh;
        double texClampX;
        double texClampY;

        //Wave
        double time = 0;
        double freqWave = 10;
        double freqScroll = 5;
        double amplitude = 0.05;
    }
}
