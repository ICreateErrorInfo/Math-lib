using Math_lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Point3 = Math_lib.Point3;

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

        public override void DrawLine(Point2 p1, Point2 p2, Color c)
        {
            
        }
        public override void DrawTriangle(Triangle2 tri, Color c, bool fill = true)
        {
            fill = true;

            Point2 p0 = tri.Points[0];
            Point2 p1 = tri.Points[1];
            Point2 p2 = tri.Points[2];

            if (p1.Y < p0.Y) { Point2 tempswap = p0; p0 = p1; p1 = tempswap; }
            if (p2.Y < p1.Y) { Point2 tempswap = p1; p1 = p2; p2 = tempswap; }
            if (p1.Y < p0.Y) { Point2 tempswap = p0; p0 = p1; p1 = tempswap; }

            if(p0.Y == p1.Y) //top flat
            {
                //sort
                if(p1.X < p0.X) { Point2 tempswap = p0; p0 = p1; p1 = tempswap; }
                DrawFlatTopTriangle(p0, p1, p2, c);
            }
            else if(p1.Y == p2.Y) // bottom flat
            {
                //sort
                if(p2.X < p1.X) { Point2 tempswap = p1; p1 = p2; p2 = tempswap; }
                DrawFlatBottomTriangle(p0, p1, p2, c);
            }
            else // general tri
            {
                //find Splitting Point
                double alphaSplit = (p1.Y - p0.Y) / (p2.Y - p0.Y);
                Point2 vi = p0 + (p2 - p0) * alphaSplit;

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

        private void DrawFlatTopTriangle(Point2 p0, Point2 p1, Point2 p2, Color c)
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
        private void DrawFlatBottomTriangle(Point2 p0, Point2 p1, Point2 p2, Color c)
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
