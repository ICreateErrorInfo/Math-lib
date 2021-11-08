using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Math_lib
{
    public abstract class DrawingObj
    {
        public abstract DirectBitmap Draw(DirectBitmap _bmp);
    }

    public class Line : DrawingObj
    {
        private Point2D p1;
        private Point2D p2;
        private System.Drawing.Color c;
        private int Width;
        private int Height;
        private double thickness;

        public Line(Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.c = c;
            this.thickness = 0;
        }
        public Line(Point2D p1, Point2D p2, System.Drawing.Color c, double thickness)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.c = c;
            this.thickness = thickness;
        }

        public override DirectBitmap Draw(DirectBitmap _bmp)
        {
            Width = _bmp.Width;
            Height = _bmp.Height;

            if (thickness == 0)
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
                    else
                    if (y0 < 0)
                    {
                        ynew = 0;
                    }
                    else
                    if (x0 > Width - 1)
                    {
                        xnew = Width - 1;
                    }
                    else
                    if (y0 > Height - 1)
                    {
                        ynew = Height - 1;
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
            else
            {
                Vector2D p1p2 = new Vector2D(p2.X - p1.X, p2.Y - p1.Y);
                Vector2D oP1P2 = new Vector2D(p1p2.Y, -p1p2.X);
                oP1P2 = Vector2D.Normalize(oP1P2);
                oP1P2 *= thickness;

                Point2D p1New = new Point2D(p1.X + oP1P2.X, p1.Y + oP1P2.Y);
                Point2D p2New = new Point2D(p2.X + oP1P2.X, p2.Y + oP1P2.Y);

                Point2D p3 = new Point2D(p2.X - oP1P2.X, p2.Y - oP1P2.Y);
                Point2D p4 = new Point2D(p1.X - oP1P2.X, p1.Y - oP1P2.Y);

                Rectangle rec = new Rectangle(p1New, p2New, p3, p4, c, true);
                _bmp = rec.Draw(_bmp);
            }

            return _bmp;
        }
        public List<Point2D> DrawLineWithOutput(ref DirectBitmap _bmp)
        {
            Width = _bmp.Width;
            Height = _bmp.Height;
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
                else
                if (y0 < 0)
                {
                    ynew = 0;
                }
                else
                if (x0 > Width - 1)
                {
                    xnew = Width - 1;
                }
                else
                if (y0 > Height - 1)
                {
                    ynew = Height - 1;
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
    }
    public class Rectangle : DrawingObj
    {
        private Point2D p1;
        private Point2D p2;
        private Point2D p3;
        private Point2D p4;
        private System.Drawing.Color c;
        private bool fill;

        public Rectangle(Point2D p1, Point2D p3, System.Drawing.Color c, bool fill = false)
        {
            this.p1 = p1;
            this.p2 = new Point2D(p3.X, p1.Y);
            this.p3 = p3;
            this.p4 = new Point2D(p1.X, p3.Y);

            this.c = c;
            this.fill = fill;
        }
        public Rectangle(Point2D p1, Point2D p2, Point2D p3, Point2D p4, System.Drawing.Color c, bool fill = false)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;

            this.c = c;
            this.fill = fill;
        }

        public override DirectBitmap Draw(DirectBitmap _bmp)
        {
            Line l1 = new Line(p1, p2, c);
            Line l2 = new Line(p2, p3, c);
            Line l3 = new Line(p3, p4, c);
            Line l4 = new Line(p4, p1, c);

            if (fill == false)
            {
                _bmp = l1.Draw(_bmp);
                _bmp = l2.Draw(_bmp);
                _bmp = l3.Draw(_bmp);
                _bmp = l4.Draw(_bmp);
            }
            else
            {
                List<Point2D> points = new List<Point2D>();

                var o1 = l1.DrawLineWithOutput(ref _bmp);
                var o2 = l2.DrawLineWithOutput(ref _bmp);
                var o3 = l3.DrawLineWithOutput(ref _bmp);
                var o4 = l4.DrawLineWithOutput(ref _bmp);

                points =        o1;
                points.AddRange(o2);
                points.AddRange(o3);
                points.AddRange(o4);

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

            return _bmp;
        }
    }
    public class Circle : DrawingObj
    {
        private Point2D p1;
        private double radius;
        private System.Drawing.Color c;
        private bool fill;
        private int Width;
        private int Height;

        public Circle(Point2D p1, double radius, System.Drawing.Color c, bool fill = false)
        {
            this.p1 = p1;
            this.radius = radius;
            this.c = c;
            this.fill = fill;
        }
        public override DirectBitmap Draw(DirectBitmap _bmp)
        {
            Width = _bmp.Width;
            Height = _bmp.Height;

            int radiusInt = (int) radius;

            List<Point2D> points = new List<Point2D>();
            int x0 = (int)p1.X;
            int y0 = (int)p1.Y;

            int f = 1 - radiusInt;
            int ddfX = 0;
            int ddfY = -2 * radiusInt;
            int x = 0;
            int y = radiusInt;

            _bmp.SetPixel(Math.Clamp(x0, 0, Width - 1), Math.Clamp(y0 + radiusInt, 0, Height - 1), c);
            _bmp.SetPixel(Math.Clamp(x0, 0, Width - 1), Math.Clamp(y0 - radiusInt, 0, Height - 1), c);
            _bmp.SetPixel(Math.Clamp(x0 + radiusInt, 0, Width - 1), Math.Clamp(y0, 0, Height - 1), c);
            _bmp.SetPixel(Math.Clamp(x0 - radiusInt, 0, Width - 1), Math.Clamp(y0, 0, Height - 1), c);

            if (fill)
            {
                points.Add(new(Math.Clamp(x0, 0, Width - 1), Math.Clamp(y0 + radiusInt, 0, Height - 1)));
                points.Add(new(Math.Clamp(x0, 0, Width - 1), Math.Clamp(y0 - radiusInt, 0, Height - 1)));
                points.Add(new(Math.Clamp(x0 + radiusInt, 0, Width - 1), Math.Clamp(y0, 0, Height - 1)));
                points.Add(new(Math.Clamp(x0 - radiusInt, 0, Width - 1), Math.Clamp(y0, 0, Height - 1)));
            }

            while (x < y)
            {
                if (f >= 0)
                {
                    y--;
                    ddfY += 2;
                    f += ddfY;
                }
                x++;
                ddfX += 2;
                f += ddfX + 1;

                _bmp.SetPixel(Math.Clamp(x0 + x, 0, Width - 1), Math.Clamp(y0 + y, 0, Height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 - x, 0, Width - 1), Math.Clamp(y0 + y, 0, Height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 + x, 0, Width - 1), Math.Clamp(y0 - y, 0, Height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 - x, 0, Width - 1), Math.Clamp(y0 - y, 0, Height - 1), c);

                if (fill)
                {
                    points.Add(new(Math.Clamp(x0 + x, 0, Width - 1), Math.Clamp(y0 + y, 0, Height - 1)));
                    points.Add(new(Math.Clamp(x0 - x, 0, Width - 1), Math.Clamp(y0 + y, 0, Height - 1)));
                    points.Add(new(Math.Clamp(x0 + x, 0, Width - 1), Math.Clamp(y0 - y, 0, Height - 1)));
                    points.Add(new(Math.Clamp(x0 - x, 0, Width - 1), Math.Clamp(y0 - y, 0, Height - 1)));
                }

                _bmp.SetPixel(Math.Clamp(x0 + y, 0, Width - 1), Math.Clamp(y0 + x, 0, Height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 - y, 0, Width - 1), Math.Clamp(y0 + x, 0, Height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 + y, 0, Width - 1), Math.Clamp(y0 - x, 0, Height - 1), c);
                _bmp.SetPixel(Math.Clamp(x0 - y, 0, Width - 1), Math.Clamp(y0 - x, 0, Height - 1), c);

                if (fill)
                {
                    points.Add(new(Math.Clamp(x0 + y, 0, Width - 1), Math.Clamp(y0 + x, 0, Height - 1)));
                    points.Add(new(Math.Clamp(x0 - y, 0, Width - 1), Math.Clamp(y0 + x, 0, Height - 1)));
                    points.Add(new(Math.Clamp(x0 + y, 0, Width - 1), Math.Clamp(y0 - x, 0, Height - 1)));
                    points.Add(new(Math.Clamp(x0 - y, 0, Width - 1), Math.Clamp(y0 - x, 0, Height - 1)));
                }
            }

            if (fill)
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

            return _bmp;
        }
    }

    public class Rasterizer
    {
        List<DrawingObj> drawingObjs;
        public int Width;
        public int Height;
        private readonly bool _cooMi;
        private readonly double _scale;

        public Rasterizer(int width, int height, int scale = 1, bool cooMi = true, bool showCoo = true)
        {
            Width = width;
            Height = height;
            _cooMi = cooMi;
            _scale = (double)1 / scale;
            drawingObjs = new List<DrawingObj>();

            if (showCoo)
            {
                DrawCoo();
            }
        }

        private void DrawCoo()
        {
            double scale = 1 / _scale;
            double arrowLength = 0.1 / 2 / 6 * scale;
            double arrowHeight = 0.07 / 2 / 6 * scale;
            double thickness = 0.005 / 2 / 3 * scale;
            double arrowThickness = 0.005 / 2 / 3 * scale;
            System.Drawing.Color c = System.Drawing.Color.Red;

            System.Drawing.Color cL = System.Drawing.Color.FromArgb(120, 120, 120);
            for (int i = 1; i < scale; i++)
            {
                DrawLine(new(i, (double)-scale / ((double)Width / Height)), new(i, (double)scale / ((double)Width / Height)), cL);
                DrawLine(new(-i, (double)-scale / ((double)Width / Height)), new(-i, (double)scale / ((double)Width / Height)), cL);
            }
            for (int i = 1; i < (double)scale / ((double)Width / Height); i++)
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

            Point2D p2 = new Point2D(0, (double)scale / ((double)Width / Height));
            DrawLine(new(0, 0), p2, c, thickness);

            Point2D p3 = new Point2D(0, (double)-scale / ((double)Width / Height));
            DrawLine(new(0, 0), p3, c, thickness);
            if (_cooMi)
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

            drawingObjs.Add(new Line(p1, p2, c));
        }
        public void DrawLine(Point2D p1, Point2D p2, System.Drawing.Color c, double thickness)
        {
            p1 = ConvertToCoo(p1);
            p2 = ConvertToCoo(p2);
            thickness = ConvertDouble(thickness, Width);

            drawingObjs.Add(new Line(p1, p2, c, thickness));
        }

        public void DrawRectangle(Point2D p1, Point2D p3, System.Drawing.Color c, bool fill = false)
        {
            p1 = ConvertToCoo(p1);
            p3 = ConvertToCoo(p3);

            drawingObjs.Add(new Rectangle(p1, p3, c, fill));
        }
        public void DrawRectangle(Point2D p1, Point2D p2, Point2D p3, Point2D p4, System.Drawing.Color c, bool fill = false)
        {
            p1 = ConvertToCoo(p1);
            p2 = ConvertToCoo(p2);
            p3 = ConvertToCoo(p3);
            p4 = ConvertToCoo(p4);

            drawingObjs.Add(new Rectangle(p1, p2, p3, p4, c, fill));
        }

        public void DrawCircle(Point2D p1, double radius, System.Drawing.Color c, bool fill = false)
        {
            p1 = ConvertToCoo(p1);
            radius = ConvertDouble(radius, Width);

            drawingObjs.Add(new Circle(p1, radius, c, fill));
        }

        public ImageSource GetSource()
        {
            DirectBitmap dBmp = new DirectBitmap(Width, Height);
            foreach (DrawingObj element in drawingObjs)
            {
                dBmp = element.Draw(dBmp);
            }

            return ToImageSource(dBmp);
        }

        private Point2D ConvertToCoo(Point2D p)
        {
            p = new Point2D(p.X, -p.Y);
            double aspectRatio = (double)Width / Height;
            if (_cooMi)
            {
                return new Point2D(ConvertDouble(p.X, Width) + Width / 2, ConvertDouble(p.Y, (int)(Height * aspectRatio)) + Height / 2);
            }
            else
            {
                return new Point2D(ConvertDouble(p.X, Width), ConvertDouble(p.Y, (int)(Height * aspectRatio)));
            }

        }
        private double ConvertDouble(double d, int l)
        {
            if (_cooMi)
            {
                return d * l * _scale / 2;
            }
            else
            {
                return d * l * _scale;
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
