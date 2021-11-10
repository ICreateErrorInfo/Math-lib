using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.DrawingObjects
{
    public class Ellipse : DrawingObject
    {
        private int _a;
        private int _b;
        private Point2D _m;
        private System.Drawing.Color _color;
        private bool _fill;

        public Ellipse(Point2D m, int a, int b, System.Drawing.Color c, bool fill = false)
        {
            _m = m;
            _a = a;
            _b = b;
            _color = c;
            _fill = fill;
        }

        public override DirectBitmap Draw(DirectBitmap bmp)
        {
            List<Point2D> points = new List<Point2D>();
            int width = bmp.Width - 1;
            int height = bmp.Height - 1;

            int xm = (int)_m.X;
            int ym = (int)_m.Y;

            int x = -_a, y = 0;
            int e2 = _b;
            int dx = (1 + 2 * x) * e2 * e2;
            int dy = x * x;
            int err = dx + dy;

            while (x <= 0)
            {
                bmp.SetPixel(Math.Clamp(xm - x, 0, width), Math.Clamp(ym + y, 0, height), _color);
                bmp.SetPixel(Math.Clamp(xm + x, 0, width), Math.Clamp(ym + y, 0, height), _color);
                bmp.SetPixel(Math.Clamp(xm + x, 0, width), Math.Clamp(ym - y, 0, height), _color);
                bmp.SetPixel(Math.Clamp(xm - x, 0, width), Math.Clamp(ym - y, 0, height), _color);

                if (_fill)
                {
                    points.Add(new(Math.Clamp(xm - x, 0, width), Math.Clamp(ym + y, 0, height)));
                    points.Add(new(Math.Clamp(xm + x, 0, width), Math.Clamp(ym + y, 0, height)));
                    points.Add(new(Math.Clamp(xm + x, 0, width), Math.Clamp(ym - y, 0, height)));
                    points.Add(new(Math.Clamp(xm - x, 0, width), Math.Clamp(ym - y, 0, height)));
                }

                e2 = 2 * err;
                if (e2 >= dx) 
                {
                    x++; err += dx += 2 * _b * _b; 
                }
                if (e2 <= dy) 
                {
                    y++; err += dy += 2 * _a * _a; 
                }
            }
            while (y++ < _b)
            {
                bmp.SetPixel(Math.Clamp(xm, 0, width), Math.Clamp(ym + y, 0, height), _color);
                bmp.SetPixel(Math.Clamp(xm, 0, width), Math.Clamp(ym - y, 0, height), _color);

                if (_fill)
                {
                    points.Add(new(Math.Clamp(xm, 0, width), Math.Clamp(ym + y, 0, height)));
                    points.Add(new(Math.Clamp(xm, 0, width), Math.Clamp(ym - y, 0, height)));
                }
            }

            if (!_fill)
            {
                return bmp;
            }
            else
            {
                var k = points.OrderBy(p => p.Y);

                Point2D[] p = k.ToArray();

                for (int i = 0; i < p.Length - 1; i++)
                {
                    if (i + 1 > p.Length - 1)
                    {
                        break;
                    }
                    if (p[i].Y != p[i + 1].Y)
                    {
                        continue;
                    }

                    int anf = (int)p[i].X;
                    int end = (int)p[i + 1].X;

                    if (end - anf > 0)
                    {
                        for (int j = anf; j < end; j++)
                        {
                            bmp.SetPixel(j, (int)p[i].Y, _color);
                        }
                    }
                    else
                    {
                        for (int j = anf; j > end; j--)
                        {
                            bmp.SetPixel(j, (int)p[i].Y, _color);
                        }
                    }
                }

                return bmp;
            }
        }
    }
}
