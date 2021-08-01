using System.Drawing;

using Point = Math_lib.Point;

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

        public abstract void DrawLine(Point p1, Point p2, Color c);

        public void DrawTriangle(Triangle tri, Color c, bool fill = false) {

            DrawLine(tri.Points[0], tri.Points[1], c);
            DrawLine(tri.Points[1], tri.Points[2], c);
            DrawLine(tri.Points[0], tri.Points[2], c);
        }

    }

}