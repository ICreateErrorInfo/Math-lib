using System;
using System.Drawing;

using Math_lib;

namespace Projection {

    public class BresenhamRasterizer: Rasterizer {

        public BresenhamRasterizer(Rasterizer rasterizer): base(rasterizer) {
        }

        public BresenhamRasterizer(int width, int height): base(width, height) {
        }

        public override void DrawLine(Point2D p1, Point2D p2, Color c) {

            var x0 = (int) p1.X;
            var y0 = (int) p1.Y;

            var x1 = (int) p2.X;
            var y1 = (int) p2.Y;

            int dx = Math.Abs(x1 - x0);
            int dy = -Math.Abs(y1 - y0);

            var sx = x0 < x1 ? 1 : -1;
            var sy = y0 < y1 ? 1 : -1;

            int err = dx + dy;

            while (true) {

                Bmp.SetPixel(x0, y0, c);

                if (x0 == x1 && y0 == y1) {
                    break;
                }

                var e2 = 2 * err;
                if (e2 > dy) {
                    err += dy;
                    x0  += sx;
                }

                if (e2 < dx) {
                    err += dx;
                    y0  += sy;
                }

            }

        }

        public override void DrawTriangle(Triangle2D tri, Color c, bool fill)
        {

            DrawLine(tri.Points[0], tri.Points[1], c);
            DrawLine(tri.Points[1], tri.Points[2], c);
            DrawLine(tri.Points[0], tri.Points[2], c);

            if (fill)
            {
                Point3D p = new Point3D((tri.Points[0].X + tri.Points[1].X + tri.Points[2].X) / 3,
                (tri.Points[0].Y + tri.Points[1].Y + tri.Points[2].Y) / 3, 0);

                Bmp.FloodFill((int)p.X, (int)p.Y, c);
            }
        }
    }

}