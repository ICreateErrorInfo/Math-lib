using System;
using System.Collections.Generic;
using System.Drawing;

namespace Math_lib.DrawingObjects {

    public class Line : DrawingObject
    {

        private readonly Point2D _p1;
        private readonly Point2D _p2;
        private readonly Color   _color;
        private readonly double  _thickness;

        public Line(Point2D p1, Point2D p2, Color color)
        {
            _p1        = p1;
            _p2        = p2;
            _color     = color;
            _thickness = 0;
        }

        public Line(Point2D p1, Point2D p2, Color color, double thickness)
        {
            _p1        = p1;
            _p2        = p2;
            _color         = color;
            _thickness = thickness;
        }

        public override DirectBitmap Draw(DirectBitmap bmp)
        {
            int width  = bmp.Width;
            int height = bmp.Height;

            if (_thickness == 0)
            {
                var x0 = (int)_p1.X;
                var y0 = (int)_p1.Y;

                var x1 = (int)_p2.X;
                var y1 = (int)_p2.Y;

                int dx = Math.Abs(x1 - x0);
                int dy = -Math.Abs(y1 - y0);

                var sx = x0 < x1 ? 1 : -1;
                var sy = y0 < y1 ? 1 : -1;

                int err = dx + dy;

                while (true)
                {
                    var xnew = x0;
                    var ynew = y0;
                    if (x0 < 0)
                    {
                        xnew = 0;
                    }
                    else
                    if (y0 < 0)
                    {
                        ynew = 0;
                    }
                    else
                    if (x0 > width - 1)
                    {
                        xnew = width - 1;
                    }
                    else
                    if (y0 > height - 1)
                    {
                        ynew = height - 1;
                    }
                    else
                    {
                        bmp.SetPixel(xnew, ynew, _color);
                    }

                    if (x0 == x1 && y0 == y1)
                    {
                        break;
                    }

                    var e2 = 2 * err;
                    if (e2 > dy)
                    {
                        err += dy;
                        x0  += sx;
                    }

                    if (e2 < dx)
                    {
                        err += dx;
                        y0  += sy;
                    }
                }
            }
            else
            {
                Vector2D p1P2  = new Vector2D(_p2.X - _p1.X, _p2.Y - _p1.Y);
                Vector2D oP1P2 = new Vector2D(p1P2.Y,      -p1P2.X);
                oP1P2 =  Vector2D.Normalize(oP1P2);
                oP1P2 *= _thickness;

                Point2D p1New = new Point2D(_p1.X + oP1P2.X, _p1.Y + oP1P2.Y);
                Point2D p2New = new Point2D(_p2.X + oP1P2.X, _p2.Y + oP1P2.Y);

                Point2D p3 = new Point2D(_p2.X - oP1P2.X, _p2.Y - oP1P2.Y);
                Point2D p4 = new Point2D(_p1.X - oP1P2.X, _p1.Y - oP1P2.Y);

                Rectangle rec = new Rectangle(p1New, p2New, p3, p4, _color, true);
                bmp = rec.Draw(bmp);
            }

            return bmp;
        }
        public List<Point2D> DrawLineWithOutput(ref DirectBitmap bmp)
        {
            int width  = bmp.Width;
            int height = bmp.Height;
            List<Point2D> points = new List<Point2D>();

            var x0 = (int)_p1.X;
            var y0 = (int)_p1.Y;

            var x1 = (int)_p2.X;
            var y1 = (int)_p2.Y;

            int dx = Math.Abs(x1 - x0);
            int dy = -Math.Abs(y1 - y0);

            var sx = x0 < x1 ? 1 : -1;
            var sy = y0 < y1 ? 1 : -1;

            int err = dx + dy;

            while (true)
            {
                var xnew = x0;
                var ynew = y0;
                if (x0 < 0)
                {
                    xnew = 0;
                }
                else
                if (y0 < 0)
                {
                    ynew = 0;
                }
                else
                if (x0 > width - 1)
                {
                    xnew = width - 1;
                }
                else
                if (y0 > height - 1)
                {
                    ynew = height - 1;
                }
                else
                {
                    bmp.SetPixel(xnew, ynew, _color);
                }

                points.Add(new Point2D(xnew, ynew));

                if (x0 == x1 && y0 == y1)
                {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > dy)
                {
                    err += dy;
                    x0  += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y0  += sy;
                }

            }

            return points;
        }
    }

}