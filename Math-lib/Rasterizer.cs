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
        private bool CooMi;
        private double scale;
        public int width;
        public int height;

        public Rasterizer(int width, int height, int scale = 1, bool CooMi = true)
        {
            _bmp = new DirectBitmap(width, height);
            this.width = width;
            this.height = height;
            this.CooMi = CooMi;
            this.scale = (double)1 / scale;

            DrawCoo();
        }                                             

        private void DrawCoo()
        {
            double scale = 1 / this.scale;
            double arrowLength = 0.1 / 2 / 6 * scale;
            double arrowHeight = 0.07 / 2 / 6 * scale;
            double thickness = 0.005 / 2 / 3 * scale;
            double arrowThickness = 0.005 / 2 / 3 * scale;
            System.Drawing.Color c = System.Drawing.Color.Red;

            System.Drawing.Color cL = System.Drawing.Color.FromArgb(120, 120, 120);
            for (int i = 1; i < scale; i++)
            {
                DrawLine(new(i, (double)-scale / ((double)width / height)), new(i, (double)scale / ((double)width / height)), cL);
                DrawLine(new(-i, (double)-scale / ((double)width / height)), new(-i, (double)scale / ((double)width / height)), cL);
            }
            for (int i = 1; i < (double)scale / ((double)width / height); i++)
            {
                DrawLine(new(-scale, i), new(scale, i), cL);
                DrawLine(new(-scale, -i), new(scale, -i), cL);
            }

            //Lines
            Point2D p = new Point2D(scale, 0);
            DrawLine(new(0, 0), p, c, thickness);
            DrawLine(p, new(p.X - arrowLength, p.Y + arrowHeight), c, arrowThickness);
            DrawLine(p, new(p.X - arrowLength, p.Y - arrowHeight), c, arrowThickness);

            Point2D p1 = new Point2D(-scale, 0);
            DrawLine(new(0, 0), p1, c, thickness);

            Point2D p2 = new Point2D(0, (double)scale / ((double)width / height));
            DrawLine(new(0, 0), p2, c, thickness);

            Point2D p3 = new Point2D(0, (double)-scale / ((double)width / height));
            DrawLine(new(0, 0), p3, c, thickness);
            if (CooMi)
            {
                DrawLine(p2, new(p2.X + arrowHeight, p2.Y - arrowLength), c, arrowThickness);
                DrawLine(p2, new(p2.X - arrowHeight, p2.Y - arrowLength), c, arrowThickness);
            }
            else
            {
                DrawLine(p3, new(p3.X + arrowHeight, p3.Y + arrowLength), c, arrowThickness);
                DrawLine(p3, new(p3.X - arrowHeight, p3.Y + arrowLength), c, arrowThickness);
            }


            //Center
            DrawCircle(new(0, 0), arrowHeight, c, true);
        }
        public void DrawLine(Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            p1 = ConvertToCoo(p1);
            p2 = ConvertToCoo(p2);        

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
                }else
                if (y0 < 0)
                {
                    ynew = 0;
                }else
                if (x0 > width - 1)
                {
                    xnew = width - 1;
                }else
                if (y0 > height - 1)
                {
                    ynew = height - 1;
                }
                else
                {
                    _bmp.SetPixel(xnew, ynew, c);
                }

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
        public void DrawLine(Point2D p1, Point2D p2, System.Drawing.Color c, double thickness)
        {
            Vector2D p1p2 = new Vector2D(p2.X - p1.X, p2.Y - p1.Y);
            Vector2D oP1P2 = new Vector2D(p1p2.Y, -p1p2.X);
            oP1P2 = Vector2D.Normalize(oP1P2);
            oP1P2 *= thickness;

            Point2D p1New = new Point2D(p1.X + oP1P2.X, p1.Y + oP1P2.Y);
            Point2D p2New = new Point2D(p2.X + oP1P2.X, p2.Y + oP1P2.Y);

            Point2D p3 = new Point2D(p2.X - oP1P2.X, p2.Y - oP1P2.Y);
            Point2D p4 = new Point2D(p1.X - oP1P2.X, p1.Y - oP1P2.Y);

            DrawRectangle(p1New, p2New, p3, p4, c, true);
        }
        private List<Point2D> DrawLineWithOut(Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            p1 = ConvertToCoo(p1);
            p2 = ConvertToCoo(p2);      

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
                }else
                if (y0 < 0)
                {
                    ynew = 0;
                }else
                if (x0 > width - 1)
                {
                    xnew = width - 1;
                }else
                if (y0 > height - 1)
                {
                    ynew = height - 1;
                }
                else
                {
                    _bmp.SetPixel(xnew, ynew, c);
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

                Fill(p, c);
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

                Fill(p, c);
            }       
        }    

        public void DrawCircle(Point2D p1, double radius, System.Drawing.Color c, bool fill = false)
        {
            int radiusInt;

            radiusInt = (int)ConvertDouble(radius, width);
            p1 = ConvertToCoo(p1);

            List<Point2D> points = new List<Point2D>();
            int x0 = (int)p1.X;
            int y0 = (int)p1.Y;

            int f = 1 - radiusInt;
            int ddfX = 0;
            int ddfY = -2 * radiusInt;
            int x = 0;
            int y = radiusInt;

            _bmp.SetPixel(Math.Clamp(x0, 0, width - 1),Math.Clamp(y0 + radiusInt, 0, height - 1), c);
            _bmp.SetPixel(Math.Clamp(x0, 0, width - 1), Math.Clamp(y0 - radiusInt, 0, height - 1), c);
            _bmp.SetPixel(Math.Clamp(x0 + radiusInt, 0, width - 1), Math.Clamp(y0, 0, height - 1), c);
            _bmp.SetPixel(Math.Clamp(x0 - radiusInt, 0, width - 1), Math.Clamp(y0, 0, height - 1), c);

            if (fill)
            {
                points.Add(new(Math.Clamp(x0, 0, width - 1),Math.Clamp(y0 + radiusInt, 0, height - 1)));
                points.Add(new(Math.Clamp(x0, 0, width - 1), Math.Clamp(y0 - radiusInt, 0, height - 1)));
                points.Add(new(Math.Clamp(x0 + radiusInt, 0, width - 1), Math.Clamp(y0, 0, height - 1)));
                points.Add(new(Math.Clamp(x0 - radiusInt, 0, width - 1), Math.Clamp(y0, 0, height - 1)));
            }

            while (x < y)
            {
                if(f >= 0)
                {
                    y--;
                    ddfY += 2;
                    f += ddfY;
                }
                x++;
                ddfX += 2;
                f += ddfX + 1;

                _bmp.SetPixel(Math.Clamp(x0 + x, 0, width - 1), Math.Clamp(y0 + y, 0, height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 - x, 0, width - 1), Math.Clamp(y0 + y, 0, height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 + x, 0, width - 1), Math.Clamp(y0 - y, 0, height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 - x, 0, width - 1), Math.Clamp(y0 - y, 0, height - 1), c);

                if (fill)
                {
                    points.Add(new(Math.Clamp(x0 + x, 0, width - 1), Math.Clamp(y0 + y, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 - x, 0, width - 1), Math.Clamp(y0 + y, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 + x, 0, width - 1), Math.Clamp(y0 - y, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 - x, 0, width - 1), Math.Clamp(y0 - y, 0, height - 1)));
                }

                _bmp.SetPixel(Math.Clamp(x0 + y, 0, width - 1), Math.Clamp(y0 + x, 0, height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 - y, 0, width - 1), Math.Clamp(y0 + x, 0, height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 + y, 0, width - 1), Math.Clamp(y0 - x, 0, height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 - y, 0, width - 1), Math.Clamp(y0 - x, 0, height - 1), c);

                if (fill)
                {
                    points.Add(new(Math.Clamp(x0 + y, 0, width - 1), Math.Clamp(y0 + x, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 - y, 0, width - 1), Math.Clamp(y0 + x, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 + y, 0, width - 1), Math.Clamp(y0 - x, 0, height - 1)));
                    points.Add(new(Math.Clamp(x0 - y, 0, width - 1), Math.Clamp(y0 - x, 0, height - 1)));
                }
            }

            if (fill)
            {
                var k = points.OrderBy(p => p.Y);

                Point2D[] p = k.ToArray();

                Fill(p, c);
            }
        }

        public ImageSource GetSource()
        {
            return ToImageSource(_bmp);
        }

        private void Fill(Point2D[] p, System.Drawing.Color c)
        {
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

        private Point2D ConvertToCoo(Point2D p)
        {
            p = new Point2D(p.X, -p.Y);
            double aspectRatio = (double)width / height;
            if(CooMi == true)
            {
                return new Point2D(ConvertDouble(p.X, width) + width / 2, ConvertDouble(p.Y, (int)(height * aspectRatio)) + height / 2);
            }
            else
            {
                return new Point2D(ConvertDouble(p.X, width), ConvertDouble(p.Y, (int)(height * aspectRatio)));
            }

        }
        private double ConvertDouble(double d, int l)
        {
            if (CooMi)
            {
                return d * l * scale / 2;
            }
            else
            {
                return d * l * scale;
            }
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
