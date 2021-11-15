using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_lib;

namespace Rasterizer_lib.DrawingObjects
{
    public class Triangle : DrawingObject
    {
        private Point2D _p0;
        private Point2D _p1;
        private Point2D _p2;
        private System.Drawing.Color _color;

        private int _Height;
        private int _Width;

        public Triangle(Point2D p0, Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            _p0 = p0;
            _p1 = p1;   
            _p2 = p2;
            _color = c;
        }

        public override DirectBitmap Draw(DirectBitmap bmp)
        {
            _Height = bmp.Height;
            _Width = bmp.Width;

            if (_p1.Y < _p0.Y) { (_p0, _p1) = (_p1, _p0); }
            if (_p2.Y < _p1.Y) { (_p1, _p2) = (_p2, _p1); }
            if (_p1.Y < _p0.Y) { (_p0, _p1) = (_p1, _p0); }

            if (_p0.Y == _p1.Y)
            {
                if (_p1.X < _p0.X) { (_p0, _p1) = (_p1, _p0); }

                bmp = DrawFlatTopTriangle(_p0, _p1, _p2, bmp);
            }
            else if (_p1.Y == _p2.Y)
            {
                if (_p2.X < _p1.X) { (_p1, _p2) = (_p2, _p1); }

                bmp = DrawFlatBottomTriangle(_p0, _p1, _p2, bmp);
            }
            else
            {
                double alphaSplit = (_p1.Y - _p0.Y) / (_p2.Y - _p0.Y);

                Point2D vi = _p0 + (_p2 - _p0) * alphaSplit;

                if (_p1.X < vi.X)
                {
                    bmp = DrawFlatBottomTriangle(_p0, _p1, vi, bmp);
                    bmp = DrawFlatTopTriangle(_p1, vi, _p2, bmp);
                }
                else
                {
                    bmp = DrawFlatBottomTriangle(_p0, vi, _p1, bmp);
                    bmp = DrawFlatTopTriangle(vi, _p1, _p2, bmp);
                }
            }

            return bmp;
        }

        private DirectBitmap DrawFlatTopTriangle(Point2D p0, Point2D p1, Point2D p2, DirectBitmap bmp)
        {
            double m0 = (p2.X - p0.X) / (p2.Y - p0.Y);
            double m1 = (p2.X - p1.X) / (p2.Y - p1.Y);

            int yStart = (int)Math.Ceiling(p0.Y - 0.5);
            int yEnd   = (int)Math.Ceiling(p2.Y - 0.5);

            for(int y = yStart; y < yEnd; y++)
            {
                double px0 = m0 * (y + 0.5 - p0.Y) + p0.X;
                double px1 = m1 * (y + 0.5 - p1.Y) + p1.X;

                int xStart = (int)Math.Ceiling(px0 - 0.5);
                int xEnd   = (int)Math.Ceiling(px1 - 0.5);

                for(int x = xStart; x < xEnd; x++)
                {
                    if (   x > 0
                        && x < _Width
                        && y > 0
                        && y < _Height)
                    {
                        bmp.SetPixel(x, y, _color);
                    }
                }
            }

            return bmp;
        }
        private DirectBitmap DrawFlatBottomTriangle(Point2D p0, Point2D p1, Point2D p2, DirectBitmap bmp)
        {
            double m0 = (p1.X - p0.X) / (p1.Y - p0.Y);
            double m1 = (p2.X - p0.X) / (p2.Y - p0.Y);

            int yStart = (int)Math.Ceiling(p0.Y - 0.5);
            int yEnd   = (int)Math.Ceiling(p2.Y - 0.5);

            for (int y = yStart; y < yEnd; y++)
            {
                double px0 = m0 * (y + 0.5 - p0.Y) + p0.X;
                double px1 = m1 * (y + 0.5 - p0.Y) + p0.X;

                int xStart = (int)Math.Ceiling(px0 - 0.5);
                int xEnd = (int)Math.Ceiling(px1 - 0.5);

                for (int x = xStart; x < xEnd; x++)
                {
                    if (   x > 0
                        && x < _Width
                        && y > 0
                        && y < _Height)
                    {
                        bmp.SetPixel(x, y, _color);
                    }
                }
            }

            return bmp;
        }
    }
}
