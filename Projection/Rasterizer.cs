using Math_lib;
using System.Drawing;

namespace Projection {

    public abstract class Rasterizer {

        protected Rasterizer(Rasterizer rasterizer) {
            Bmp = rasterizer.Bmp;
        }

        protected Rasterizer(int width, int height) {
            Bmp = new DirectBitmap(width, height);
        }

        public DirectBitmap Bmp { get; }

        public int Width  => Bmp.Width;
        public int Height => Bmp.Height;

        public void Clear() {
            Bmp.Clear();
        }

        public abstract void DrawLine(Point2 p1, Point2 p2, Color c);

        public virtual void DrawTriangle(Triangle2 tri, Color c, bool fill) {

            DrawLine(tri.Points[0], tri.Points[1], c);
            DrawLine(tri.Points[1], tri.Points[2], c);
            DrawLine(tri.Points[0], tri.Points[2], c);

            if (fill) {
                Point2 p = new Point2((tri.Points[0].X + tri.Points[1].X + tri.Points[2].X) /3,
                                      (tri.Points[0].Y + tri.Points[1].Y + tri.Points[2].Y) /3);

                Bmp.FloodFill((int)p.X, (int)p.Y, c);
            }

        }

    }

}