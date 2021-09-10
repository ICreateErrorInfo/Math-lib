using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_lib;

namespace RaytracingInOneWeek
{
    public class Raytracer
    {
        int width;
        int height;

        public Raytracer(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public DirectBitmap Render()
        {
            DirectBitmap bmp = new DirectBitmap(width, height);

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    double r = (double)x / (width - 1);
                    double g = (double)y / (height - 1);
                    double b = 0.25;

                    int ir = Convert.ToInt32(255 * r);
                    int ig = Convert.ToInt32(255 * g);
                    int ib = Convert.ToInt32(255 * b);

                    bmp.SetPixel(x,-(y - 255), Color.FromArgb(ir,ig,ib));
                }
            }

            return bmp;
        }
    }
}
