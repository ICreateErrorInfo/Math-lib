using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Point = Math_lib.Point;

namespace Projection {

    class Rasterizer {

        public Rasterizer(int width, int height) {
            Bmp = new DirectBitmap(width, height);
        }

        public DirectBitmap Bmp { get; }

        private readonly List<Point> _points = new();

        public int Width  => Bmp.Width;
        public int Height => Bmp.Height;

        public void Clear() {
            Bmp.Clear();
        }

        public void DrawLine(Point p1, Point p2, Color c) {

            if (p1.X == p2.X) {

                for (int y = (int) Math.Min(p1.Y, p2.Y); y < Math.Max(p1.Y, p2.Y); y++) {

                    _points.Add(new Point(p1.X, y, 0));

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

                for (double x = pMin.X; x < Math.Floor(pMax.X); x += 0.5) {
                    int y = (int) (pMin.Y + (x - pMin.X) * (dy / dx));

                    Bmp.SetPixel((int) x, y, c);
                    _points.Add(new Point(x, y, 0));
                }
            }
        }

        public void DrawTriangle(Triangle tri, Color c, bool fill = false) {

            DrawLine(tri.p[0], tri.p[1], c);
            DrawLine(tri.p[1], tri.p[2], c);
            DrawLine(tri.p[0], tri.p[2], c);

            if (fill) {
                Point minPoint = new Point();
                Point maxPoint = new Point();
                for (int i = 0; i < _points.Count(); i++) {
                    if (_points[i].Y < minPoint.Y) {
                        minPoint = _points[i];
                    }

                    if (_points[i].Y > maxPoint.Y) {
                        maxPoint = _points[i];
                    }
                }

                for (int y = (int) minPoint.Y; y < maxPoint.Y; y++) {
                    Point minX = new Point();
                    Point maxX = new Point();

                    for (int i = 0; i < _points.Count(); i++) {
                        if (_points[i].Y == y) {

                            if (_points[i].X < minX) {
                                minX = _points[i];
                            }

                            if (_points[i].X > maxX) {
                                maxX = _points[i];
                            }
                        }
                    }

                    for (int x = (int) minX.X; x < maxX.X; x++) {
                        Bmp.SetPixel(x, y, c);
                    }
                }
            }

            _points.Clear();
        }

    }

}