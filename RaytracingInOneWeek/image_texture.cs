using Math_lib;
using System;
using System.Drawing;
using System.IO;

namespace Raytracing
{
    class image_texture : Texture
    {
        public static int bytes_per_pixel = 3;

        private byte[] data;
        private int width, height;
        private int bytes_per_scanline;

        public image_texture()
        {
            data = null;
            width = 0;
            height = 0;
            bytes_per_scanline = 0;
        }

        public image_texture(string filename)
        {
            Image img = Image.FromFile(filename);
            Bitmap bit = new Bitmap(img);
            width = img.Width;
            height = img.Height;

            data = new byte[width * height * bytes_per_pixel];

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    var p = y * width + x;
                    var c = bit.GetPixel(x, y);

                    //data[p * bytes_per_pixel   ] = c.A;
                    data[p * bytes_per_pixel ] = c.R;
                    data[p * bytes_per_pixel +1] = c.G;
                    data[p * bytes_per_pixel +2] = c.B;
                }
            }

            bytes_per_scanline = bytes_per_pixel * width;
        }

        public override Vector3D value(double u, double v, Point3D p)
        {
            // If we have no texture data, then return solid cyan as a debugging aid.
            if (data == null)
            {
                return new Vector3D(0, 1, 1);
            }

            // Clamp input texture coordinates to [0,1] x [1,0]
            u = Math.Clamp(u, 0.0f, 1);
            v = 1.0f - Math.Clamp(v, 0.0f, 1);  // Flip V to image coordinates

            var i = (int)(u * width);
            var j = (int)(v * height);

            // Clamp integer mapping, since actual coordinates should be less than 1.0
            if (i >= width) i = width - 1;
            if (j >= height) j = height - 1;

            var color_scale = 1.0 / 255;

            var index =  j * bytes_per_scanline + i * bytes_per_pixel;
            Vector3D pixel = new Vector3D(data[index] * color_scale, data[index + 1] * color_scale, data[index + 2] * color_scale);

            return new Vector3D(pixel.X, pixel.Y, pixel.Z);
        }
        public Vector3D GetPixel(int x, int y)
        {
            var index = y * bytes_per_scanline + x * bytes_per_pixel;
            Vector3D pixel = new Vector3D(data[index], data[index + 1], data[index + 2]);

            return new Vector3D(pixel.X, pixel.Y, pixel.Z);
        }
        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var p = y * width + x;
                    Vector3D c = GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255, (int)c.X, (int)c.Y, (int)c.Z));
                }
            }
            return bmp;
        }
    }
}
