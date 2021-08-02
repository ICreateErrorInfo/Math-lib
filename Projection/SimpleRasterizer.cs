using System;
using System.Drawing;

using Point = Math_lib.Point;

namespace Projection {

    public class SimpleRasterizer: Rasterizer {

        public SimpleRasterizer(Rasterizer rasterizer): base(rasterizer) {
        }

        public SimpleRasterizer(int width, int height): base(width, height) {
        }

        public override void DrawLine(Point p1, Point p2, Color c) {

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (p1.X == p2.X) {

                for (int y = (int) Math.Min(p1.Y, p2.Y); y < Math.Max(p1.Y, p2.Y); y++) {

                    Bmp.SetPixel((int) p1.X, y, c);
                }

            } else {
                Point pMin;
                Point pMax;

                if (p1.X < p2.X) {
                    pMin = p1;
                    pMax = p2;
                } else {
                    pMin = p2;
                    pMax = p1;
                }

                double dx = pMax.X - pMin.X;
                double dy = pMax.Y - pMin.Y;

                if (dy == 0) {

                }

                for (double x = pMin.X; x < Math.Floor(pMax.X); x += 0.5) {
                    int y = (int) (pMin.Y + (x - pMin.X) * (dy / dx));

                    Bmp.SetPixel((int) x, y, c);
                }
            }
        }

        public override void DrawTriangle(Triangle tri, Color c, bool fill = false)
        {

            DrawLine(tri.Points[0], tri.Points[1], c);
            DrawLine(tri.Points[1], tri.Points[2], c);
            DrawLine(tri.Points[0], tri.Points[2], c);

            if (fill)
            {
                Point p = new Point((tri.Points[0].X + tri.Points[1].X + tri.Points[2].X) / 3,
                (tri.Points[0].Y + tri.Points[1].Y + tri.Points[2].Y) / 3, 0);

                Bmp.FloodFill((int)p.X, (int)p.Y, c);
            }
        }
    }

}