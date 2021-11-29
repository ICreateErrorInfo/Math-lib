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
    class TextureEffect : Effect
    {
        public override Color GetColor(Vertex v)
        {
            if(v.TryGetValue<TextureCoordinateVertexAttribute>(out var vT))
            {
                return pTex.GetPixel((int)Math.Min(vT.T.X * texWidth + 0.5, texClampX),
                                     (int)Math.Min(vT.T.Y * texHeigh + 0.5, texClampY));
            }
            throw new InvalidOperationException();
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
            return new(rotation * vIn.Pos + translation, vIn.Attributes);
        }

        public override Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex)
        {
            return new(v0, v1, v2);
        }

        public DirectBitmap pTex;
        double texWidth;
        double texHeigh;
        double texClampX;
        double texClampY;
    }
}
