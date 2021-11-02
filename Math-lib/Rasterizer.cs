using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Math_lib
{
    public class Rasterizer
    {
        private DirectBitmap _bmp = null;
        public int width;
        public int height;

        public Rasterizer(int width, int height)
        {
            _bmp = new DirectBitmap(width, height);
            this.width = width;
            this.height = height;
        }

        public void DrawLine(Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            var x0 = (int)p1.X;
            var y0 = (int)p1.Y;

            var x1 = (int)p2.X;
            var y1 = (int)p2.Y;

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
                if (y0 < 0)
                {
                    ynew = 0;
                }
                if (x0 > width - 1)
                {
                    xnew = width - 1;
                }
                if (y0 > height - 1)
                {
                    ynew = height - 1;
                }

                _bmp.SetPixel(xnew, ynew, c);

                if (x0 == x1 && y0 == y1)
                {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > dy)
                {
                    err += dy;
                    x0 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }

            }
        }
        public void DrawLine(Point2D p1, Point2D p2, System.Drawing.Color c, int thickness)
        {
            Vector2D p1p2 = new Vector2D(p2.X - p1.X, p2.Y - p1.Y);
            p1p2 = Vector2D.Normalize(p1p2);
            Vector2D oP1P2 = new Vector2D(p1p2.Y, -p1p2.X);
            oP1P2 *= thickness;

            Point2D p1New = new Point2D(p1.X + oP1P2.X, p1.Y + oP1P2.Y);
            Point2D p2New = new Point2D(p2.X + oP1P2.X, p2.Y + oP1P2.Y);

            Point2D p3 = new Point2D(p2.X - oP1P2.X, p2.Y - oP1P2.Y);
            Point2D p4 = new Point2D(p1.X - oP1P2.X, p1.Y - oP1P2.Y);

            DrawRectangle(p1New, p2New, p3, p4, c, true);
        }
        private List<Point2D> DrawLineWithOut(Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            List<Point2D> points = new List<Point2D>();
            var x0 = (int)p1.X;
            var y0 = (int)p1.Y;

            var x1 = (int)p2.X;
            var y1 = (int)p2.Y;

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
                if (y0 < 0)
                {
                    ynew = 0;
                }
                if (x0 > width - 1)
                {
                    xnew = width - 1;
                }
                if (y0 > height - 1)
                {
                    ynew = height - 1;
                }

                _bmp.SetPixel(xnew, ynew, c);
                points.Add(new Point2D(xnew, ynew));

                if (x0 == x1 && y0 == y1)
                {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > dy)
                {
                    err += dy;
                    x0 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }

            }

            return points;
        }
        public void DrawRectangle(Point2D p1, Point2D p3, System.Drawing.Color c, bool fill = false)
        {
            Point2D p2 = new Point2D(p3.X, p1.Y);
            Point2D p4 = new Point2D(p1.X, p3.Y);

            if(fill == false)
            {
                DrawLine(p1, p2, c);
                DrawLine(p2, p3, c);
                DrawLine(p3, p4, c);
                DrawLine(p4, p1, c);
            }
            else
            {
                List<Point2D> points = new List<Point2D>();
                points = DrawLineWithOut(p1, p2, c);
                points.AddRange(DrawLineWithOut(p2, p3, c));
                points.AddRange(DrawLineWithOut(p3, p4, c));
                points.AddRange(DrawLineWithOut(p4, p1, c));

                var k = points.OrderBy(p => p.Y);

                Point2D[] p = k.ToArray();

                for (int i = 0; i < points.Count; i++)
                {
                    if (i + 1 > points.Count - 1)
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
                            _bmp.SetPixel(j, (int)p[i].Y, c);
                        }
                    }
                    else
                    {
                        for (int j = anf; j > end; j--)
                        {
                            _bmp.SetPixel(j, (int)p[i].Y, c);
                        }
                    }

                }
            }
        }
        public void DrawRectangle(Point2D p1, Point2D p2, Point2D p3, Point2D p4, System.Drawing.Color c, bool fill = false)
        {
            if (fill == false)
            {
                DrawLine(p1, p2, c);
                DrawLine(p2, p3, c);
                DrawLine(p3, p4, c);
                DrawLine(p4, p1, c);
            }
            else
            {
                List<Point2D> points = new List<Point2D>();
                points = DrawLineWithOut(p1, p2, c);
                points.AddRange(DrawLineWithOut(p2, p3, c));
                points.AddRange(DrawLineWithOut(p3, p4, c));
                points.AddRange(DrawLineWithOut(p4, p1, c));

                var k = points.OrderBy(p => p.Y);

                Point2D[] p = k.ToArray();

                for (int i = 0; i < points.Count; i++)
                {
                    if (i + 1 > points.Count - 1)
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
                            _bmp.SetPixel(j, (int)p[i].Y, c);
                        }
                    }
                    else
                    {
                        for (int j = anf; j > end; j--)
                        {
                            _bmp.SetPixel(j, (int)p[i].Y, c);
                        }
                    }

                }
            }       
        }    
        public ImageSource GetSource()
        {
            return ToImageSource(_bmp);
        }

        private ImageSource ToImageSource(DirectBitmap bitmap)
        {

            var bs = BitmapSource.Create(
                pixelWidth: bitmap.Width,
                pixelHeight: bitmap.Height,
                dpiX: 96,
                dpiY: 96,
                pixelFormat: PixelFormats.Bgr24,
                palette: null,
                pixels: bitmap.Bits,
                stride: bitmap.Stride);

            return bs;

        }
    }
}
