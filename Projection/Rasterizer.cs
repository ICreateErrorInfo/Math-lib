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

        public DirectBitmap Bmp { get; private set; }

        public int Width  => Bmp.Width;
        public int Height => Bmp.Height;

        public void OverrideBitmap(DirectBitmap bmp)
        {
            Bmp = bmp;
        }
        public void Clear() {
            Bmp.Clear();
        }
        public abstract void DrawLine(Point3D p1, Point3D p2, Color c);

        public virtual void DrawTriangle(Triangle3D tri, Color c, bool fill) {

            DrawLine(tri.Points[0], tri.Points[1], c);
            DrawLine(tri.Points[1], tri.Points[2], c);
            DrawLine(tri.Points[0], tri.Points[2], c);

            if (fill) {
                Point2D p = new Point2D((tri.Points[0].X + tri.Points[1].X + tri.Points[2].X) /3,
                                      (tri.Points[0].Y + tri.Points[1].Y + tri.Points[2].Y) /3);

                Bmp.FloodFill((int)p.X, (int)p.Y, c);
            }

        }

    }

}