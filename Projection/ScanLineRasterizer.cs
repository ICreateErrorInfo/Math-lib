using Math_lib;
using System;
using System.Drawing;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Projection
{
    class ScanLineRasterizer : Rasterizer
    {
        readonly ZBuffer _zb;

        public ScanLineRasterizer(Rasterizer rasterizer) : base(rasterizer)
        {
            _zb = new ZBuffer(rasterizer.Width, rasterizer.Height);
        }

        public ScanLineRasterizer(int width, int height) : base(width, height)
        {
            _zb = new ZBuffer(width, height);
        }


        public override void Clear() {
            base.Clear();
            _zb.Clear();
        }

        public override void DrawLine(Point3D p1, Point3D p2, Color c)
        {
            
        }
        public override void DrawTriangle(Triangle3D tri, Color c, bool fill)
        {
            Point3D p0 = tri.Points[0];
            Point3D p1 = tri.Points[1];
            Point3D p2 = tri.Points[2];

            if (p1.Y < p0.Y) { (p0, p1) = (p1, p0); }
            if (p2.Y < p1.Y) { (p1, p2) = (p2, p1); }
            if (p1.Y < p0.Y) { (p0, p1) = (p1, p0); }

            if(p0.Y == p1.Y) //top flat
            {
                //sort
                if(p1.X < p0.X) { (p0, p1) = (p1, p0); }

                DrawFlatTopTriangle(p0, p1, p2, c);
            }
            else if(p1.Y == p2.Y) // bottom flat
            {
                //sort
                if(p2.X < p1.X) { (p1, p2) = (p2, p1); }
                DrawFlatBottomTriangle(p0, p1, p2, c);
            }
            else // general tri
            {
                //find Splitting Point
                double alphaSplit = (p1.Y - p0.Y) / (p2.Y - p0.Y);
                Point3D vi = p0 + (p2 - p0) * alphaSplit;

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

        private void DrawFlatTopTriangle(Point3D p0, Point3D p1, Point3D p2, Color c)
        {
            double deltaY = p2.Y - p0.Y;
            Point3D dit0 = new((p2 - p0) / deltaY);
            Point3D dit1 = new((p2 - p1) / deltaY);

            var itEdge1 = p1;

            DrawFlatTriangle(p0, p1, p2, dit0, dit1, itEdge1, c);
        }
        private void DrawFlatBottomTriangle(Point3D p0, Point3D p1, Point3D p2, Color c)
        {
            double deltaY = p2.Y - p0.Y;
            Point3D dit0 = new((p1 - p0) / deltaY);
            Point3D dit1 = new((p2 - p0) / deltaY);

            var itEdge1 = p0;

            DrawFlatTriangle(p0, p1, p2, dit0, dit1, itEdge1, c);
        }
        private void DrawFlatTriangle(Point3D it0, Point3D it1, Point3D it2, Point3D dv0, Point3D dv1, Point3D itEdge, Color c)
        {
            var itEdge0 = it0;

            int yStart = (int)Math.Ceiling(it0.Y - 0.5);
            int yEnd = (int)Math.Ceiling(it2.Y - 0.5);

            itEdge0 += dv0 * (yStart + 0.5 - it0.Y);
            itEdge  += dv1 * (yStart + 0.5 - it0.Y);

            for(int y = yStart; y < yEnd; y++, itEdge0 += dv0, itEdge += dv1)
            {
                int xStart = (int)Math.Ceiling(itEdge0.X - 0.5);
                int xEnd = (int)Math.Ceiling(itEdge.X - 0.5);

                var iLine = itEdge0;

                double dx = itEdge.X - itEdge0.X;
                var diLine = (itEdge - iLine) / dx;

                iLine += diLine * (xStart + 0.5 - itEdge0.X);

                for (int x = xStart; x < xEnd; x++, iLine += diLine)
                {
                    double z = 1 / iLine.Z;

                    if (_zb.TestAndSet(x, y, z))
                    {
                        Bmp.SetPixel(x, y, c);
                    }
                }
            }
        }
    }
}
