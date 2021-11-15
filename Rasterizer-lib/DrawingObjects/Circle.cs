using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Math_lib;

namespace Rasterizer_lib.DrawingObjects {

    public class Circle : DrawingObject
    {

        private readonly Point2D _p1;
        private readonly double  _radius;
        private readonly Color   _color;
        private readonly bool    _fill;

        public Circle(Point2D p1, double radius, Color color, bool fill = false) 
        {
            _p1     = p1;
            _radius = radius;
            _color  = color;
            _fill   = fill;
        }

        public override DirectBitmap Draw(DirectBitmap bmp)
        {
            int width  = bmp.Width;
            int height = bmp.Height;

            int radiusInt = (int) _radius;

            List<Point2D> points = new List<Point2D>();
            int           x0     = (int)_p1.X;
            int           y0     = (int)_p1.Y;

            int f    = 1 - radiusInt;
            int ddfX = 0;
            int ddfY = -2 * radiusInt;
            int x    = 0;
            int y    = radiusInt;

            bmp.SetPixel(Math.Clamp(x0, 0, width - 1), Math.Clamp(y0 + radiusInt, 0, height - 1), _color);
            bmp.SetPixel(Math.Clamp(x0, 0, width - 1), Math.Clamp(y0 - radiusInt, 0, height - 1), _color);
            bmp.SetPixel(Math.Clamp(x0           + radiusInt,                     0, width  - 1), Math.Clamp(y0, 0, height - 1), _color);
            bmp.SetPixel(Math.Clamp(x0           - radiusInt,                     0, width  - 1), Math.Clamp(y0, 0, height - 1), _color);

            if (_fill)
            {
                points.Add(new(Math.Clamp(x0, 0, width - 1), Math.Clamp(y0 + radiusInt, 0, height - 1)));
                points.Add(new(Math.Clamp(x0, 0, width - 1), Math.Clamp(y0 - radiusInt, 0, height - 1)));
                points.Add(new(Math.Clamp(x0           + radiusInt,                     0, width  - 1), Math.Clamp(y0, 0, height - 1)));
                points.Add(new(Math.Clamp(x0           - radiusInt,                     0, width  - 1), Math.Clamp(y0, 0, height - 1)));
            }

            while (x < y)
            {
                if (f >= 0)
                {
                    y--;
                    ddfY += 2;
                    f    += ddfY;
                }
                x++;
                ddfX += 2;
                f    += ddfX + 1;

                bmp.SetPixel(Math.Clamp(x0 + x, 0, width - 1), Math.Clamp(y0 + y, 0, height - 1), _color);
                bmp.SetPixel(Math.Clamp(x0 - x, 0, width - 1), Math.Clamp(y0 + y, 0, height - 1), _color);
                bmp.SetPixel(Math.Clamp(x0 + x, 0, width - 1), Math.Clamp(y0 - y, 0, height - 1), _color);
                bmp.SetPixel(Math.Clamp(x0 - x, 0, width - 1), Math.Clamp(y0 - y, 0, height - 1), _color);

                if (_fill)
                {
                    points.Add(new(Math.Clamp(x0 + x, 0, width - 1), Math.Clamp(y0 + y, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 - x, 0, width - 1), Math.Clamp(y0 + y, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 + x, 0, width - 1), Math.Clamp(y0 - y, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 - x, 0, width - 1), Math.Clamp(y0 - y, 0, height - 1)));
                }

                bmp.SetPixel(Math.Clamp(x0 + y, 0, width - 1), Math.Clamp(y0 + x, 0, height - 1), _color);
                bmp.SetPixel(Math.Clamp(x0 - y, 0, width - 1), Math.Clamp(y0 + x, 0, height - 1), _color);
                bmp.SetPixel(Math.Clamp(x0 + y, 0, width - 1), Math.Clamp(y0 - x, 0, height - 1), _color);
                bmp.SetPixel(Math.Clamp(x0 - y, 0, width - 1), Math.Clamp(y0 - x, 0, height - 1), _color);

                if (_fill)
                {
                    points.Add(new(Math.Clamp(x0 + y, 0, width - 1), Math.Clamp(y0 + x, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 - y, 0, width - 1), Math.Clamp(y0 + x, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 + y, 0, width - 1), Math.Clamp(y0 - x, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 - y, 0, width - 1), Math.Clamp(y0 - x, 0, height - 1)));
                }
            }

            if (_fill)
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
            }

            return bmp;
        }

        public override string ToString()
        {
            return $"{Point2D.Floor(_p1).ToString()} | {_radius} | {_color.ToString()}";
        }
    }

}