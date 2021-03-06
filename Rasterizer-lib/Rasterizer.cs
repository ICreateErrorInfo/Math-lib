using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Rasterizer_lib.DrawingObjects;
using Math_lib;

namespace Rasterizer_lib
{

    public class Rasterizer
    {

        readonly         List<DrawingObject> _drawingObjs;
        public           int              Width;
        public           int              Height;
        private readonly bool             _cooMi;
        private readonly double           _scale;
        private readonly bool _showCoo;

        public Rasterizer(int width, int height, int scale = 1, bool cooMi = true, bool showCoo = true)
        {
            Width = width;
            Height = height;
            _cooMi = cooMi;
            _scale = (double)1 / scale;
            _drawingObjs = new List<DrawingObject>();
            _showCoo = showCoo;
        }

        public void Clear() => _drawingObjs.Clear();

        public void UpdateSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
        private void DrawCoo()
        {
            double scale = 1 / _scale;
            double arrowLength = 0.1;
            double arrowHeight = 0.07;
            double thickness = 0.01;
            double arrowThickness = 0.01;
            System.Drawing.Color c = System.Drawing.Color.Red;

            System.Drawing.Color cL = System.Drawing.Color.FromArgb(120, 120, 120);
            for (int i = 1; i < scale; i++)
            {
                DrawLine(new(i, -scale  / ((double)Width / Height)), new(i, scale          / ((double)Width / Height)), cL);
                DrawLine(new(-i, -scale / ((double)Width / Height)), new(-i, scale / ((double)Width / Height)), cL);
            }
            for (int i = 1; i < scale / ((double)Width / Height); i++)
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

            Point2D p2 = new Point2D(0, scale / ((double)Width / Height));
            DrawLine(new(0, 0), p2, c, thickness);

            Point2D p3 = new Point2D(0, -scale / ((double)Width / Height));
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

            _drawingObjs.Add(new Line(p1, p2, c));
        }
        public void DrawLine(Point2D p1, Point2D p2, System.Drawing.Color c, double thickness)
        {
            p1 = ConvertToCoo(p1);
            p2 = ConvertToCoo(p2);
            thickness = ConvertDouble(thickness, Width);

            _drawingObjs.Add(new Line(p1, p2, c, thickness));
        }

        public void DrawRectangle(Point2D p1, Point2D p3, System.Drawing.Color c, bool fill = false)
        {
            p1 = ConvertToCoo(p1);
            p3 = ConvertToCoo(p3);

            _drawingObjs.Add(new Rectangle(p1, p3, c, fill));
        }
        public void DrawRectangle(Point2D p1, Point2D p2, Point2D p3, Point2D p4, System.Drawing.Color c, bool fill = false)
        {
            p1 = ConvertToCoo(p1);
            p2 = ConvertToCoo(p2);
            p3 = ConvertToCoo(p3);
            p4 = ConvertToCoo(p4);

            _drawingObjs.Add(new Rectangle(p1, p2, p3, p4, c, fill));
        }
        public void DrawPoint(Point2D p, System.Drawing.Color c)
        {
            Point2D p1 = new Point2D(p.X + 0.5, p.Y + 0.5);
            Point2D p2 = new Point2D(p.X - 0.5, p.Y - 0.5);

            DrawRectangle(p1, p2, c, true);
        }

        public void DrawCircle(Point2D p1, double radius, System.Drawing.Color c, bool fill = false)
        {
            p1 = ConvertToCoo(p1);
            radius = ConvertDouble(radius, Width);

            _drawingObjs.Add(new Circle(p1, radius, c, fill));
        }
        public void DrawEllipse(Point2D m, int a, int b, System.Drawing.Color c, bool fill = false)
        {
            m = ConvertToCoo(m);
            a = (int)ConvertDouble(a, Width);
            b = (int)ConvertDouble(b, Width);

            _drawingObjs.Add(new Ellipse(m, a, b, c, fill));
        }

        public void DrawQuadBezier(Point2D p, Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            p = ConvertToCoo(p);
            p1 = ConvertToCoo(p1);
            p2 = ConvertToCoo(p2);

            _drawingObjs.Add(new QuadBezier(p, p1, p2, c));
        }

        public void DrawTriangle(Point2D p, Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            p = ConvertToCoo(p);
            p1 = ConvertToCoo(p1);
            p2 = ConvertToCoo(p2);

            _drawingObjs.Add(new Triangle(p, p1, p2, c));
        }

        public ImageSource GetSource()
        {
            DirectBitmap dBmp = new DirectBitmap(Width, Height);
            if (_showCoo)
            {
                DrawCoo();
            }

            for(int i = _drawingObjs.Count -1; i >= 0; i--)
            {
                dBmp = _drawingObjs[i].Draw(dBmp);
            }

            _drawingObjs.Clear();
            return ToImageSource(dBmp);
        }

        private Point2D ConvertToCoo(Point2D p)
        {         
            double aspectRatio = (double)Width / Height;
            if (_cooMi)
            {
                p = new Point2D(p.X, -p.Y);
                return new Point2D(x: ConvertDouble(p.X, Width) + Width / 2.0,
                                   y: ConvertDouble(p.Y, (int)(Height * aspectRatio)) + Height / 2.0);
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
