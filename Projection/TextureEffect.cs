using Math_lib;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace Projection
{
    class TextureEffect : Effect
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
            for(int x = 0; x < img.Width; x++)
            {
                for(int y = 0; y < img.Height; y++)
                {
                    pTex.SetPixel(x,y, img.GetPixel(x,y));
                }
            }

            texWidth = img.Width;
            texHeigh = img.Height;
            texClampX = texWidth - 1;
            texClampY = texHeigh - 1;
        }

        public override Vertex Translate(Vertex vIn)
        {
            return new(rotation * vIn.pos + translation, vIn.t);
        }

        public DirectBitmap pTex;
        double texWidth;
        double texHeigh;
        double texClampX;
        double texClampY;
    }
}
