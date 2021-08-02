using Math_lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Point = Math_lib.Point;

namespace Projection
{
    class ScannLineRasterizer : Rasterizer
    {
        public ScannLineRasterizer(Rasterizer rasterizer) : base(rasterizer)
        {
        }

        public ScannLineRasterizer(int width, int height) : base(width, height)
        {
        }

        public override void DrawLine(Point p1, Point p2, Color c)
        {
            
        }
        public override void DrawTriangle(Triangle tri, Color c, bool fill = true)
        {
            fill = true;

            Point p0 = tri.Points[0];
            Point p1 = tri.Points[1];
            Point p2 = tri.Points[2];

            if (p1.Y < p0.Y) { Point tempswap = p0; p0 = p1; p1 = tempswap; }
            if (p2.Y < p1.Y) { Point tempswap = p1; p1 = p2; p2 = tempswap; }
            if (p1.Y < p0.Y) { Point tempswap = p0; p0 = p1; p1 = tempswap; }

            if(p0.Y == p1.Y) //top flat
            {
                //sort
                if(p1.X < p0.X) { Point tempswap = p0; p0 = p1; p1 = tempswap; }
                DrawFlatTopTriangle(p0, p1, p2, c);
            }
            else if(p1.Y == p2.Y) // bottom flat
            {
                //sort
                if(p2.X < p1.X) { Point tempswap = p1; p1 = p2; p2 = tempswap; }
                DrawFlatBottomTriangle(p0, p1, p2, c);
            }
            else // general tri
            {
                //find Splitting Point
                double alphaSplit = (p1.Y - p0.Y) / (p2.Y - p0.Y);
                Point vi = p0 + (p2 - p0) * alphaSplit;

                if(p1.X < vi.X)
                {
                    DrawFlatBottomTriangle(p0, p1, vi, c);
                    DrawFlatTopTriangle(p1, vi, p2, c);
                }
                else
                {
                    DrawFlatBottomTriangle(p0, vi, p1, c);
                    DrawFlatTopTriangle(vi, p1, p2, c);
                }
            }
        }

        private void DrawFlatTopTriangle(Point p0, Point p1, Point p2, Color c)
        {
            double m0 = (p2.X - p0.X) / (p2.Y - p0.Y);
            double m1 = (p2.X - p1.X) / (p2.Y - p1.Y);

            int yStart = (int)Math.Ceiling(p0.Y - 0.5);
            int yEnd = (int)Math.Ceiling(p2.Y - 0.5);

            for(int y = yStart; y < yEnd; y++)
            {
                double px0 = m0 * (double)(y + 0.5 - p0.Y) + p0.X;
                double px1 = m1 * (double)(y + 0.5 - p1.Y) + p1.X;

                int xStart = (int)Math.Ceiling(px0 - 0.5);
                int xEnd   = (int)Math.Ceiling(px1 - 0.5);

                for(int x = xStart; x < xEnd; x++)
                {
                    Bmp.SetPixel(x, y, c);
                }
            }
        }
        private void DrawFlatBottomTriangle(Point p0, Point p1, Point p2, Color c)
        {
            double m0 = (p1.X - p0.X) / (p1.Y - p0.Y);
            double m1 = (p2.X - p0.X) / (p2.Y - p0.Y);

            int yStart = (int)Math.Ceiling(p0.Y - 0.5);
            int yEnd   = (int)Math.Ceiling(p2.Y - 0.5);

            for(int y = yStart; y < yEnd; y++)
            {
                double px0 = m0 * (double)(y + 0.5 - p0.Y) + p0.X;
                double px1 = m1 * (double)(y + 0.5 - p0.Y) + p0.X;

                int xStart = (int)Math.Ceiling(px0 - 0.5);
                int xEnd   = (int)Math.Ceiling(px1 - 0.5);

                for(int x = xStart; x < xEnd; x++)
                {
                    Bmp.SetPixel(x, y, c);
                }
            }
        }
    }
}
