using Moarx.Math;
using System.Drawing;
using System.Numerics;
using System.Security;

namespace Moarx.Rasterizer;

public class DirectGraphics {

    readonly DirectBitmap _bitmap;

    DirectGraphics(DirectBitmap bitmap) {
        _bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
    }

    public static DirectGraphics Create(DirectBitmap bitmap) => new(bitmap);

    public void FloodFill(int x, int y, DirectColor newColor) {


        var replaceColor = _bitmap.GetPixel(x, y);
        if (newColor == replaceColor) {
            return;
        }

        FloodFillmpl(x, y, newColor, replaceColor);

    }
    void FloodFillmpl(int x1, int y1, DirectColor newColor, DirectColor replaceColor) {

        Stack<(int X, int Y)> stack = new();

        ProcessPixel(x1, y1);

        while (stack.Any()) {

            var (x, y) = stack.Pop();

            ProcessPixel(x, y + 1);
            ProcessPixel(x, y - 1);
            ProcessPixel(x + 1, y);
            ProcessPixel(x - 1, y);

        }

        void ProcessPixel(int x, int y) {

            var currentColor = SafeGetPixel(x, y);

            if (currentColor == replaceColor) {
                stack.Push((x, y));
                _bitmap.SetPixel(x, y, newColor);
            }
        }

        DirectColor? SafeGetPixel(int x, int y) {

            if (x < 0 || x >= _bitmap.Width ||
                y < 0 || y >= _bitmap.Height) {
                return null;
            }

            return _bitmap.GetPixel(x, y);
        }
    }

    public void DrawTriangle(Triangle2D<int> triangle, DirectAttributes attributes) {
        if (attributes.IsFilled) {
            DrawTriangleFilled(triangle, attributes.LineColor, attributes.FillColor);
        } else {
            DrawTriangle(triangle, attributes.LineColor);
        }
    }
    public void DrawEllipse(Ellipse2D<int> ellipse, DirectAttributes attributes) {
        if (!attributes.IsFilled) {
            DrawEllipse(ellipse, attributes.LineColor);
            return;
        }

        int a = ellipse.HorizontalStretch;
        int b = ellipse.VerticalStretch;

        int x = 0, y = b;
        double d2 = b*b + a*a*(-b + 0.25);

        while (b * b * x <= a * a * y) {
            if (d2 < 0) {
                d2 += b * b * (2 * x + 3);
            } else {
                d2 += b * b * (2 * x + 3) + a * a * (-2 * y + 2);
                y--;
            }
            x++;

            for (int i = ellipse.MidPoint.X - x + 1; i < ellipse.MidPoint.X + x; i++) {
                _bitmap.SetPixel(i, ellipse.MidPoint.Y - y, attributes.FillColor);
                _bitmap.SetPixel(i, ellipse.MidPoint.Y + y, attributes.FillColor);
            }
        }
        d2 = b * b * x * x + a * a * (y - 1) * (y - 1) - a * a * b * b;
        while (y >= 0) {
            if (d2 < 0) {
                d2 += b * b * (2 * x + 2) + a * a * (-2 * y + 3);
                x++;
            } else {
                d2 += a * a * (-2 * y + 3);
            }
            y--;
            for (int i = ellipse.MidPoint.X - x + 1; i < ellipse.MidPoint.X + x; i++) {
                _bitmap.SetPixel(i, ellipse.MidPoint.Y - y, attributes.FillColor);
                _bitmap.SetPixel(i, ellipse.MidPoint.Y + y, attributes.FillColor);
            }
        }

        DrawEllipse(ellipse, attributes.LineColor);
    }
    public void DrawRectangle(Rectangle2D<int> rectangle, DirectAttributes attributes) {
        if (!attributes.IsFilled) {
            DrawRectangle(rectangle, attributes.LineColor);
            return;
        }

        DrawRectangleFilled(rectangle, attributes.FillColor);
        DrawRectangle(rectangle, attributes.LineColor);

    }
    public void DrawLine(Line2D<int> line, DirectAttributes attributes) {
        if (attributes.LineThickness == 1) {
            DrawLine(line, attributes.LineColor);
            return;
        }

        DrawThickLine(line, attributes.LineThickness, attributes.LineColor);
    }
    public void DrawQuadBezier(QuadBezierCurve2D<int> curve, DirectAttributes attributes) {
        DrawQuadBezier(curve, attributes.LineColor);
    }
    public void DrawCubicBezier(CubicBezierCurve2D<int> curve, DirectAttributes attributes) {
        DrawCubicBezier(curve, attributes.LineColor);
    }


    //Bezier from http://members.chello.at/~easyfilter/bresenham.html
    private void DrawQuadBezier(QuadBezierCurve2D<int> curve, DirectColor color) {
        int x = curve[0].X-curve[1].X, y = curve[0].Y-curve[1].Y;
        double t = curve[0].X-2*curve[1].X+curve[2].X, r;

        Point2D<int> p0Swaped = curve[0], p1Swaped = curve[1], p2Swaped = curve[2];

        if ((double)x * (curve[2].X - curve[1].X) > 0) {

            if ((double)y * (curve[2].Y - curve[1].Y) > 0) {

                if (System.Math.Abs((curve[0].Y - 2 * curve[1].Y + curve[2].Y) / t * x) > System.Math.Abs(y)) {
                    p0Swaped = new(curve[2].X, curve[2].Y);
                    p2Swaped = new(x + curve[1].X, y + curve[1].Y);
                }
            }

            t = (p0Swaped.X - p1Swaped.X) / t;
            r = (1 - t) * ((1 - t) * p0Swaped.Y + 2.0 * t * p1Swaped.Y) + t * t * p2Swaped.Y;
            t = (p0Swaped.X * p2Swaped.X - p1Swaped.X * p1Swaped.X) * t / (p0Swaped.X - p1Swaped.X);
            x = (int)System.Math.Floor(t + 0.5);
            y = (int)System.Math.Floor(r + 0.5);
            r = (p1Swaped.Y - p0Swaped.Y) * (t - p0Swaped.X) / (p1Swaped.X - p0Swaped.X) + p0Swaped.Y;
            DrawQuadBezierSeg(new(new(p0Swaped.X, p0Swaped.Y), new(x, (int)System.Math.Floor(r + 0.5)), new(x, y)), color);

            r = (p1Swaped.Y - p2Swaped.Y) * (t - p2Swaped.X) / (p1Swaped.X - p2Swaped.X) + p2Swaped.Y;
            p0Swaped = new(x, y);
            p1Swaped = new(x, (int)System.Math.Floor(r + 0.5));
        }
        if ((double)(p0Swaped.Y - p1Swaped.Y) * (p2Swaped.Y - p1Swaped.Y) > 0) {
            t = p0Swaped.Y - 2 * p1Swaped.Y + p2Swaped.Y;
            t = (p0Swaped.Y - p1Swaped.Y) / t;
            r = (1 - t) * ((1 - t) * p0Swaped.X + 2.0 * t * p1Swaped.X) + t * t * p2Swaped.X;
            t = (p0Swaped.Y * p2Swaped.Y - p1Swaped.Y * p1Swaped.Y) * t / (p0Swaped.Y - p1Swaped.Y);
            x = (int)System.Math.Floor(r + 0.5);
            y = (int)System.Math.Floor(t + 0.5);
            r = (p1Swaped.X - p0Swaped.X) * (t - p0Swaped.Y) / (p1Swaped.Y - p0Swaped.Y) + p0Swaped.X;
            DrawQuadBezierSeg(new(p0Swaped, new((int)System.Math.Floor(r + 0.5), y), new(x, y)), color);

            r = (p1Swaped.X - p2Swaped.X) * (t - p2Swaped.Y) / (p1Swaped.Y - p2Swaped.Y) + p2Swaped.X;

            p0Swaped = new(x, y);
            p1Swaped = new((int)System.Math.Floor(r + 0.5), y);
        }
        DrawQuadBezierSeg(new(p0Swaped, p1Swaped, p2Swaped), color);
    }
    private void DrawQuadBezierSeg(QuadBezierCurve2D<int> curve, DirectColor color) {
        int sx = curve[2].X-curve[1].X, sy = curve[2].Y-curve[1].Y;
        long xx = curve[0].X-curve[1].X, yy = curve[0].Y-curve[1].Y, xy;
        double dx, dy, err, cur = xx*sy-yy*sx;

        if (!(xx * sx <= 0 && yy * sy <= 0))
            throw new Exception("sign of gradient must not change");

        Point2D<int> p1Swaped = curve[0], p2Swaped = curve[1], p3Swaped = curve[2];

        if (sx * (long)sx + sy * (long)sy > xx * xx + yy * yy) {
            p3Swaped = new(curve[0].X, curve[0].Y);
            p1Swaped = new(sx + curve[1].X, sy + curve[1].Y);
            cur = -cur;
        }

        if (cur != 0) {
            xx += sx;
            xx *= sx = p1Swaped.X < p3Swaped.X ? 1 : -1;
            yy += sy;
            yy *= sy = p1Swaped.Y < p3Swaped.Y ? 1 : -1;
            xy = 2 * xx * yy;
            xx *= xx;
            yy *= yy;
            if (cur * sx * sy < 0) {
                xx = -xx;
                yy = -yy;
                xy = -xy;
                cur = -cur;
            }
            dx = 4.0 * sy * cur * (p2Swaped.X - p1Swaped.X) + xx - xy;
            dy = 4.0 * sx * cur * (p1Swaped.Y - p2Swaped.Y) + yy - xy;
            xx += xx;
            yy += yy;
            err = dx + dy + xy;
            do {
                _bitmap.SetPixel(p1Swaped.X, p1Swaped.Y, color);
                if (p1Swaped.X == p3Swaped.X && p1Swaped.Y == p3Swaped.Y)
                    return;
                bool y = 2 * err < dx;
                if (2 * err > dy) { p1Swaped += new Vector2D<int>(sx, 0); dx -= xy; err += dy += yy; }
                if (y) { p1Swaped += new Vector2D<int>(0, sy); dy -= xy; err += dx += xx; }
            } while (dy < dx);
        }
        DrawLine(new(p1Swaped, p3Swaped), color);
    }
    private void DrawFineQuadBezierSeg(QuadBezierCurve2D<int> curve, DirectColor color) {
        int x0 = curve[0].X, x1 = curve[1].X, x2 = curve[2].X;
        int y0 = curve[0].Y, y1 = curve[1].Y, y2 = curve[2].Y;

        int sx = x0<x2 ? 1 : -1, sy = y0<y2 ? 1 : -1;

        long f = 1, fx = x0-2 * x1 + x2, fy = y0-2 * y1 + y2;

        long x = 2*fx*fx, y = 2*fy*fy, xy = 2*fx*fy*sx*sy;
        long cur = sx*sy*(fx*(y2-y0)-fy * (x2-x0));
        /* compute error increments of P0 */
        long dx = System.Math.Abs(y0-y1)*xy-System.Math.Abs(x0-x1) * y-cur* System.Math.Abs(y0-y2);
        long dy = System.Math.Abs(x0-x1)*xy-System.Math.Abs(y0-y1) * x + cur * System.Math.Abs(x0-x2);
        /* compute error increments of P2 */
        long ex = System.Math.Abs(y2-y1)*xy-System.Math.Abs(x2-x1) * y + cur * System.Math.Abs(y0-y2);
        long ey = System.Math.Abs(x2-x1)*xy-System.Math.Abs(y2-y1) * x-cur* System.Math.Abs(x0-x2);
        /* sign of gradient must not change */
        if (!((x0 - x1) * (x2 - x1) <= 0 && (y0 - y1) * (y2 - y1) <= 0))
            throw new Exception("sign of gradient must not change");

        if (cur == 0) { DrawLine(new(x0, y0), new(x2, y2), color); return; } /* straight line */
        /* compute required minimum resolution factor */
        if (dx == 0 || dy == 0 || ex == 0 || ey == 0)
            f = System.Math.Abs(xy / cur) / 2 + 1; /* division by zero: use curvature */
        else {
            fx = 2 * y / dx;
            if (fx > f)
                f = fx; /* increase resolution */
            fx = 2 * x / dy;
            if (fx > f)
                f = fx;
            fx = 2 * y / ex;
            if (fx > f)
                f = fx;
            fx = 2 * x / ey;
            if (fx > f)
                f = fx;
        } /* negated curvature? */
        if (cur < 0) { x = -x; y = -y; dx = -dx; dy = -dy; xy = -xy; }
        dx = f * dx + y / 2 - xy;
        dy = f * dy + x / 2 - xy;
        ex = dx + dy + xy; /* error 1.step */
        for (fx = fy = f; ;) { /* plot curve */
            _bitmap.SetPixel(x0, y0, color);
            if (x0 == x2 && y0 == y2)
                break;
            do { /* move f sub-pixel */
                ey = 2 * ex - dy; /* save value for test of y step */
                if (2 * ex >= dx) { fx--; dy -= xy; ex += dx += y; } /* x step */
                if (ey <= 0) { fy--; dx -= xy; ex += dy += x; } /* y step */
            } while (fx > 0 && fy > 0); /* pixel complete? */
            if (2 * fx <= f) { x0 += sx; fx += f; } /* sufficient sub-steps.. */
            if (2 * fy <= f) { y0 += sy; fy += f; } /* .. for a pixel? */
        }
    }

    private void DrawCubicBezier(CubicBezierCurve2D<int> curve, DirectColor color) {
        DirectAttributes attributes = new DirectAttributes(DirectColors.Black, 10, DirectColors.Yellow);
        int x0 = curve[0].X, x1 = curve[1].X, x2 = curve[2].X, x3 = curve[3].X;
        int y0 = curve[0].Y, y1 = curve[1].Y, y2 = curve[2].Y, y3 = curve[3].Y;

        int n = 0, i = 0;
        double xc = x0+x1-x2-x3, xa = xc-4*(x1-x2);
        double xb = x0-x1-x2+x3, xd = xb+4*(x1+x2);
        double yc = y0+y1-y2-y3, ya = yc-4*(y1-y2);
        double yb = y0-y1-y2+y3, yd = yb+4*(y1+y2);
        double fx0 = x0, fx1, fx2, fx3, fy0 = y0, fy1, fy2, fy3;
        double t1 = xb*xb-xa*xc, t2;
        double[] t = new double[5];

        if (xa == 0) {
            if (System.Math.Abs(xc) < 2 * System.Math.Abs(xb))
                t[n++] = xc / (2.0 * xb);
        } else if (t1 > 0.0) {
            t2 = System.Math.Sqrt(t1);
            t1 = (xb - t2) / xa;
            if (System.Math.Abs(t1) < 1.0)
                t[n++] = t1;
            t1 = (xb + t2) / xa;
            if (System.Math.Abs(t1) < 1.0)
                t[n++] = t1;
        }
        t1 = yb * yb - ya * yc;
        if (ya == 0) {
            if (System.Math.Abs(yc) < 2 * System.Math.Abs(yb))
                t[n++] = yc / (2.0 * yb);
        } else if (t1 > 0.0) {
            t2 = System.Math.Sqrt(t1);
            t1 = (yb - t2) / ya;
            if (System.Math.Abs(t1) < 1.0)
                t[n++] = t1;
            t1 = (yb + t2) / ya;
            if (System.Math.Abs(t1) < 1.0)
                t[n++] = t1;
        }
        for (i = 1; i < n; i++)
            if ((t1 = t[i - 1]) > t[i]) { t[i - 1] = t[i]; t[i] = t1; i = 0; }
        t1 = -1.0;
        t[n] = 1.0;
        for (i = 0; i <= n; i++) {
            t2 = t[i];
            fx1 = (t1 * (t1 * xb - 2 * xc) - t2 * (t1 * (t1 * xa - 2 * xb) + xc) + xd) / 8 - fx0;
            fy1 = (t1 * (t1 * yb - 2 * yc) - t2 * (t1 * (t1 * ya - 2 * yb) + yc) + yd) / 8 - fy0;
            fx2 = (t2 * (t2 * xb - 2 * xc) - t1 * (t2 * (t2 * xa - 2 * xb) + xc) + xd) / 8 - fx0;
            fy2 = (t2 * (t2 * yb - 2 * yc) - t1 * (t2 * (t2 * ya - 2 * yb) + yc) + yd) / 8 - fy0;
            fx0 -= fx3 = (t2 * (t2 * (3 * xb - t2 * xa) - 3 * xc) + xd) / 8;
            fy0 -= fy3 = (t2 * (t2 * (3 * yb - t2 * ya) - 3 * yc) + yd) / 8;
            x3 = (int)System.Math.Floor(fx3 + 0.5);
            y3 = (int)System.Math.Floor(fy3 + 0.5);
            if (fx0 != 0.0) { fx1 *= fx0 = (x0 - x3) / fx0; fx2 *= fx0; }
            if (fy0 != 0.0) { fy1 *= fy0 = (y0 - y3) / fy0; fy2 *= fy0; }
            if (x0 != x3 || y0 != y3)
                DrawCubicBezierSeg(new(new(x0, y0), new((int)(x0 + fx1), (int)(y0 + fy1)),new((int)(x0 + fx2), (int)(y0 + fy2)),new( x3, y3)), color);
            x0 = x3;
            y0 = y3;
            fx0 = fx3;
            fy0 = fy3;
            t1 = t2;
        }
    }
    private void DrawCubicBezierSeg(CubicBezierCurve2D<int> curve, DirectColor color) {

        int x0 = curve[0].X, x1 = curve[1].X, x2 = curve[2].X, x3 = curve[3].X;
        int y0 = curve[0].Y, y1 = curve[1].Y, y2 = curve[2].Y, y3 = curve[3].Y;

        int f, fx, fy, leg = 1;
        int sx = x0 < x3 ? 1 : -1, sy = y0 < y3 ? 1 : -1;
        double xc = -System.Math.Abs(x0+x1-x2-x3), xa = xc-4*sx*(x1-x2), xb = sx*(x0-x1-x2+x3);
        double yc = -System.Math.Abs(y0+y1-y2-y3), ya = yc-4*sy*(y1-y2), yb = sy*(y0-y1-y2+y3);
        double ab, ac, bc, cb, xx, xy, yy, dx, dy, ex, pxy, EP = 0.01;


        //if (!((x1 - x0) * (x2 - x3) < EP && ((x3 - x0) * (x1 - x2) < EP || xb * xb < xa * xc + EP)))
        //    throw new Exception();
        //if(!((y1 - y0) * (y2 - y3) < EP && ((y3 - y0) * (y1 - y2) < EP || yb * yb < ya * yc + EP)))
        //    throw new Exception();


        if (xa == 0 && ya == 0) {
            sx = (int)System.Math.Floor((double)(3 * x1 - x0 + 1) / 2);
            sy = (int)System.Math.Floor((double)(3 * y1 - y0 + 1) / 2);
            DrawQuadBezierSeg(new(new(x0, y0),new(sx, sy),new( x3, y3)), color);
            return;
        }
        x1 = (x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0) + 1;
        x2 = (x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3) + 1;
        do {
            ab = xa * yb - xb * ya;
            ac = xa * yc - xc * ya;
            bc = xb * yc - xc * yb;
            ex = ab * (ab + ac - 3 * bc) + ac * ac;
            f = ex > 0 ? 1 : (int)System.Math.Sqrt(1 + 1024 / x1);
            ab *= f;
            ac *= f;
            bc *= f;
            ex *= f * f;
            xy = 9 * (ab + ac + bc) / 8;
            cb = 8 * (xa - ya);
            dx = 27 * (8 * ab * (yb * yb - ya * yc) + ex * (ya + 2 * yb + yc)) / 64 - ya * ya * (xy - ya);
            dy = 27 * (8 * ab * (xb * xb - xa * xc) - ex * (xa + 2 * xb + xc)) / 64 - xa * xa * (xy + xa);
            xx = 3 * (3 * ab * (3 * yb * yb - ya * ya - 2 * ya * yc) - ya * (3 * ac * (ya + yb) + ya * cb)) / 4;
            yy = 3 * (3 * ab * (3 * xb * xb - xa * xa - 2 * xa * xc) - xa * (3 * ac * (xa + xb) + xa * cb)) / 4;
            xy = xa * ya * (6 * ab + 6 * ac - 3 * bc + cb);
            ac = ya * ya;
            cb = xa * xa;
            xy = 3 * (xy + 9 * f * (cb * yb * yc - xb * xc * ac) - 18 * xb * yb * ab) / 8;
            if (ex < 0) {
                dx = -dx;
                dy = -dy;
                xx = -xx;
                yy = -yy;
                xy = -xy;
                ac = -ac;
                cb = -cb;
            }
            ab = 6 * ya * ac;
            ac = -6 * xa * ac;
            bc = 6 * ya * cb;
            cb = -6 * xa * cb;
            dx += xy;
            ex = dx + dy;
            dy += xy;
            for (pxy = xy, fx = fy = f; x0 != x3 && y0 != y3;) {
                _bitmap.SetPixel(x0, y0, color);
                do {
                    if (dx > pxy || dy < pxy)
                        goto exit;
                    if (2 * ex >= dx) {
                        fx--;
                        ex += dx += xx;
                        dy += xy += ac;
                        yy += bc;
                        xx += ab;
                    }
                    if (2 * ex - dy <= 0) {
                        fy--;
                        ex += dy += yy;
                        dx += xy += bc;
                        xx += ac;
                        yy += cb;
                    }
                } while (fx > 0 && fy > 0);
                if (2 * fx <= f) { x0 += sx; fx += f; }
                if (2 * fy <= f) { y0 += sy; fy += f; }
                if (pxy == xy && dx < 0 && dy > 0)
                    pxy = EP;
            }
            exit:
            xx = x0;
            x0 = x3;
            x3 = (int)xx;
            sx = -sx;
            xb = -xb;
            yy = y0;
            y0 = y3;
            y3 = (int)yy;
            sy = -sy;
            yb = -yb;
            x1 = x2;
        } while (leg-- >= 0);
        DrawLine(new(x0, y0), new(x3, y3), color);
    }


    private void DrawRectangle(Rectangle2D<int> rectangle, DirectColor color) {
        DrawLine(rectangle.TopLeft, rectangle.TopRight, color);
        DrawLine(rectangle.TopRight, rectangle.BottomRight, color);
        DrawLine(rectangle.BottomLeft, rectangle.BottomRight, color);
        DrawLine(rectangle.BottomLeft, rectangle.TopLeft, color);
    }
    private void DrawRectangleFilled(Rectangle2D<int> rectangle, DirectColor color) {
        DrawTriangleFilled(new(rectangle.TopLeft, rectangle.TopRight, rectangle.BottomRight), color, color);
        DrawTriangleFilled(new(rectangle.TopLeft, rectangle.BottomLeft, rectangle.BottomRight), color, color);
    }

    private void DrawLine(Point2D<int> start, Point2D<int> end, DirectColor color) {
        DrawLine(new(start, end), color);
    }
    private void DrawLine(Line2D<int> line, DirectColor color) {
        Vector2D<int> slope = line.EndPoint - line.StartPoint;

        int dx = System.Math.Abs(slope.X);
        int dy = -System.Math.Abs(slope.Y);

        int sx = line.StartPoint.X < line.EndPoint.X ? 1 : -1;
        int sy = line.StartPoint.Y < line.EndPoint.Y ? 1 : -1;

        int error = dx + dy;

        int newX = line.StartPoint.X;
        int newY = line.StartPoint.Y;

        while (true) {
            _bitmap.SetPixel(newX, newY, color);

            if (newX == line.EndPoint.X && newY == line.EndPoint.Y)
                break;

            var errorDoubled = 2 * error;
            if (errorDoubled > dy) {
                error += dy;
                newX += sx;
            }

            if (errorDoubled < dx) {
                error += dx;
                newY += sy;
            }
        }
    }
    private void DrawThickLine(Line2D<int> line, int thickness, DirectColor color) {
        Vector2D<int> lineVector = line.EndPoint - line.StartPoint;
        Vector2D<double> perpendicentVector = new(-lineVector.Y, lineVector.X);
        perpendicentVector = perpendicentVector / System.Math.Sqrt(perpendicentVector.GetLengthSquared());

        perpendicentVector = new(System.Math.Floor(perpendicentVector.X * thickness / 2), System.Math.Floor(perpendicentVector.Y * thickness / 2));

        Point2D<int> topLeft     = line.StartPoint + (Vector2D<int>)(perpendicentVector);
        Point2D<int> bottomLeft  = line.StartPoint - (Vector2D<int>)(perpendicentVector);
        Point2D<int> topRight    = line.EndPoint + (Vector2D<int>)(perpendicentVector);
        Point2D<int> bottomRight = line.EndPoint - (Vector2D<int>)(perpendicentVector);

        DrawRectangleFilled(new(topLeft, bottomLeft, bottomRight, topRight), color);
        DrawRectangle(new(topLeft, bottomLeft, bottomRight, topRight), color);
    }
    public void DrawSSAALine(Line2D<int> line, int thickness, int samples, DirectColor color) {
        Rectangle2D<int> rec = new Bounds2D<int>(line.GetBoundingBox().PMin - new Vector2D<int>(1), line.GetBoundingBox().PMax + new Vector2D<int>(1)).ToRectangle();
        DirectBitmap bitmapSliced = _bitmap.Slice(rec);

        DirectBitmap supersampledBitmap = DirectBitmap.Create(bitmapSliced.Width * samples, bitmapSliced.Height * samples);

        Line2D<int> scaledTriangle = (new Line2D<int>(line.StartPoint * samples, line.EndPoint * samples)).Transform(new(-rec.Left * samples, -rec.Top * samples));

        DirectGraphics g = DirectGraphics.Create(supersampledBitmap);
        g.DrawThickLine(scaledTriangle, thickness * samples, color);

        DownSample(bitmapSliced, supersampledBitmap, samples);
    }

    public void DrawAntiAliasedLine(Point2D<int> start, Point2D<int> end, DirectColor color) {
        DrawAntiAliasedLine(new(start, end), color);
    }
    public void DrawAntiAliasedLine(Line2D<int> line, DirectColor color) {
        bool steep = System.Math.Abs(line.EndPoint.Y - line.StartPoint.Y) > System.Math.Abs(line.EndPoint.X - line.StartPoint.X);

        int x0 = line.StartPoint.X, y0 = line.StartPoint.Y;
        int x1 = line.EndPoint.X, y1 = line.EndPoint.Y;

        if (steep) {
            (x0, y0) = (y0, x0);
            (x1, y1) = (y1, x1);
        }

        if (x0 > x1) {
            (x0, x1) = (x1, x0);
            (y0, y1) = (y1, y0);
        }

        Vector2D<int> slope = new Point2D<int>(x0, y0) -  new Point2D<int>(x1, y1);

        double gradient = slope.X switch {
            0 => 1,
            _ => (double)slope.Y / slope.X
        };

        double xend = (int)(x0+0.5);
        double yend = y0 + gradient * (xend - x0);
        double xgap = rfpart(x0 + 1);
        double xpxl1 = xend;
        double ypxl1 = (int)yend;

        if (steep) {
            _bitmap.BlendPixel((int)ypxl1, (int)xpxl1, GetColor(color, rfpart(yend) * xgap));
            _bitmap.BlendPixel((int)ypxl1 + 1, (int)xpxl1, GetColor(color, fpart(yend) * xgap));
        } else {
            _bitmap.BlendPixel((int)xpxl1, (int)ypxl1, GetColor(color, rfpart(yend) * xgap));
            _bitmap.BlendPixel((int)xpxl1, (int)ypxl1 + 1, GetColor(color, fpart(yend) * xgap));
        }
        double intery = yend + gradient;

        xend = (int)(x1 + 0.5);
        yend = y1 + gradient * (xend - x1);
        xgap = fpart(x1 + 1);
        double xpxl2 = xend;
        double ypxl2 = (int)yend;

        if (steep) {
            _bitmap.BlendPixel((int)ypxl2, (int)xpxl2, GetColor(color, rfpart(yend) * xgap));
            _bitmap.BlendPixel((int)ypxl2 + 1, (int)xpxl2, GetColor(color, fpart(yend) * xgap));
        } else {
            _bitmap.BlendPixel((int)xpxl2, (int)ypxl2, GetColor(color, rfpart(yend) * xgap));
            _bitmap.BlendPixel((int)xpxl2, (int)ypxl2 + 1, GetColor(color, fpart(yend) * xgap));
        }

        if (steep) {
            for (double x = xpxl1 + 1; x <= xpxl2 - 1; x++) {
                _bitmap.BlendPixel((int)intery, (int)x, GetColor(color, rfpart(intery)));
                _bitmap.BlendPixel((int)intery + 1, (int)x, GetColor(color, fpart(intery)));
                intery = intery + gradient;
            }
        } else {
            for (double x = xpxl1 + 1; x <= xpxl2 - 1; x++) {
                _bitmap.BlendPixel((int)x, (int)intery, GetColor(color, rfpart(intery)));
                _bitmap.BlendPixel((int)x, (int)intery + 1, GetColor(color, fpart(intery)));
                intery = intery + gradient;
            }
        }
    }

    private void DrawEllipse(Ellipse2D<int> ellipse, DirectColor color) {
        //Circle
        if (ellipse.VerticalStretch == ellipse.HorizontalStretch) {
            int radius = ellipse.VerticalStretch;

            int f     = 1 - radius;
            int ddF_x = 0;
            int ddF_y = -2 * radius;
            int x     = 0;
            int y     = radius;

            _bitmap.SetPixel(ellipse.MidPoint.X, ellipse.MidPoint.Y + radius, color);
            _bitmap.SetPixel(ellipse.MidPoint.X, ellipse.MidPoint.Y - radius, color);
            _bitmap.SetPixel(ellipse.MidPoint.X + radius, ellipse.MidPoint.Y, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - radius, ellipse.MidPoint.Y, color);

            while (x < y) {
                if (f >= 0) {
                    y -= 1;
                    ddF_y += 2;
                    f += ddF_y;
                }

                x += 1;
                ddF_x += 2;
                f += ddF_x + 1;

                _bitmap.SetPixel(ellipse.MidPoint.X + x, ellipse.MidPoint.Y + y, color);
                _bitmap.SetPixel(ellipse.MidPoint.X - x, ellipse.MidPoint.Y + y, color);
                _bitmap.SetPixel(ellipse.MidPoint.X + x, ellipse.MidPoint.Y - y, color);
                _bitmap.SetPixel(ellipse.MidPoint.X - x, ellipse.MidPoint.Y - y, color);
                _bitmap.SetPixel(ellipse.MidPoint.X + y, ellipse.MidPoint.Y + x, color);
                _bitmap.SetPixel(ellipse.MidPoint.X - y, ellipse.MidPoint.Y + x, color);
                _bitmap.SetPixel(ellipse.MidPoint.X + y, ellipse.MidPoint.Y - x, color);
                _bitmap.SetPixel(ellipse.MidPoint.X - y, ellipse.MidPoint.Y - x, color);
            }

            return;
        }

        //Ellipse
        int    dx    = 0,                                                     dy = ellipse.VerticalStretch;
        double a2    = ellipse.HorizontalStretch * ellipse.HorizontalStretch, b2 = ellipse.VerticalStretch * ellipse.VerticalStretch;
        double error = b2 - (2 * ellipse.VerticalStretch - 1) * a2,           errorDoubled;

        while (dy >= 0) {
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y - dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y - dy, color);

            errorDoubled = 2 * error;
            if (errorDoubled < (2 * dx + 1) * b2) {
                ++dx;
                error += (2 * dx + 1) * b2;
            }

            if (errorDoubled > -(2 * dy - 1) * a2) {
                --dy;
                error -= (2 * dy - 1) * a2;
            }
        }

        do {
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y - dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y - dy, color);

            errorDoubled = 2 * error;
            if (errorDoubled < (2 * dx + 1) * b2) {
                ++dx;
                error += (2 * dx + 1) * b2;
            }

            if (errorDoubled > -(2 * dy - 1) * a2) {
                --dy;
                error -= (2 * dy - 1) * a2;
            }
        } while (dy >= 0);

        while (dx++ < ellipse.HorizontalStretch) {
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y, color);
        }
    }
    public void DrawAntiAliasedEllipse(Ellipse2D<int> ellipse, DirectColor color) {
        double radiusX = ellipse.HorizontalStretch;
        double radiusY = ellipse.VerticalStretch;
        double radiusXSquared = radiusX * radiusX;
        double radiusYSquared = radiusY * radiusY;

        double maxTransparency = 255;

        double quater = System.Math.Round(radiusXSquared / System.Math.Sqrt(radiusXSquared + radiusYSquared));
        for (int x = 0; x <= quater; x++) {
            double y = radiusY * System.Math.Sqrt(1 - x * x / radiusXSquared);
            double error = y - System.Math.Floor(y);

            double transparency = System.Math.Round(error * maxTransparency);
            int alpha = (int)transparency;
            int alpha2 = (int)(maxTransparency - transparency);

            SetPixel4(ellipse.MidPoint, x, (int)System.Math.Floor(y), DirectColor.FromArgb((byte)alpha, color));
            SetPixel4(ellipse.MidPoint, x, (int)System.Math.Floor(y) - 1, DirectColor.FromArgb((byte)alpha2, color));
        }

        quater = System.Math.Round(radiusYSquared / System.Math.Sqrt(radiusXSquared + radiusYSquared));
        for (int y = 0; y <= quater; y++) {
            double x = radiusX * System.Math.Sqrt(1 - y * y / radiusYSquared);
            double error = x - System.Math.Floor(x);

            double transparency = System.Math.Round(error * maxTransparency);
            int alpha = (int)transparency;
            int alpha2 = (int)(maxTransparency - transparency);

            SetPixel4(ellipse.MidPoint, (int)System.Math.Floor(x), y, DirectColor.FromArgb((byte)alpha, color));
            SetPixel4(ellipse.MidPoint, (int)System.Math.Floor(x) - 1, y, DirectColor.FromArgb((byte)alpha2, color));
        }
    }

    private void DrawTriangle(Triangle2D<int> triangle, DirectColor color) {
        DrawTriangle(triangle.Point1, triangle.Point2, triangle.Point3, color);
    }
    public void DrawAntiAliasedTriangle(Triangle2D<int> triangle, DirectColor color) {
        DrawAntiAliasedLine(new(triangle.Point1, triangle.Point2), color);
        DrawAntiAliasedLine(new(triangle.Point2, triangle.Point3), color);
        DrawAntiAliasedLine(new(triangle.Point3, triangle.Point1), color);
    }
    public void DrawAntiAliasedTriangleFilled(Triangle2D<int> triangle, DirectColor color) {
        DrawAntiAliasedLine(new(triangle.Point1, triangle.Point2), color);
        DrawAntiAliasedLine(new(triangle.Point2, triangle.Point3), color);
        DrawAntiAliasedLine(new(triangle.Point3, triangle.Point1), color);

        Point2D<int> top = triangle.Point1, middle = triangle.Point2, bottom = triangle.Point3;

        //Sort points by Y
        if (middle.Y < top.Y)
            (top, middle) = (middle, top);
        if (middle.Y > bottom.Y)
            (bottom, middle) = (middle, bottom);
        if (middle.Y < top.Y)
            (top, middle) = (middle, top);


        if (top.Y == middle.Y) //top flat
        {
            Point2D<int> right = top, left = middle;
            if (right.X < left.X)
                (left, right) = (right, left);

            DrawTopFlatAnitAliasedTriangle(right, left, bottom, color);
            return;
        }

        if (bottom.Y == middle.Y) //top bottom
        {
            Point2D<int> right = bottom, left = middle;
            if (right.X < left.X)
                (left, right) = (right, left);

            DrawBottomFlatAnitAliasedTriangle(top, left, right, color);
            return;
        }

        Point2D<int> splitPoint = new((int)(top.X + ((middle.Y - top.Y) / (float)(bottom.Y - top.Y)) * (bottom.X - top.X)), middle.Y);
        if (splitPoint.X < middle.X) {
            //split point is left
            DrawBottomFlatAnitAliasedTriangle(top, splitPoint, middle, color);
            DrawTopFlatAnitAliasedTriangle(middle, splitPoint, bottom, color);
        } else {
            //split point is right
            DrawBottomFlatAnitAliasedTriangle(top, middle, splitPoint, color);
            DrawTopFlatAnitAliasedTriangle(splitPoint, middle, bottom, color);
        }
    }
    public void DrawSSAATriangleFilled(Triangle2D<int> triangle, int samples, DirectColor color) {
        Rectangle2D<int> rec = triangle.GetBoundingBox().ToRectangle();
        DirectBitmap bitmapSliced = _bitmap.Slice(rec);

        DirectBitmap supersampledBitmap = DirectBitmap.Create(bitmapSliced.Width * samples, bitmapSliced.Height * samples);

        Triangle2D<int> scaledTriangle = (new Triangle2D<int>(triangle.Point1 * samples, triangle.Point2 * samples, triangle.Point3 * samples)).Transform(new(-rec.Left * samples, -rec.Top * samples));

        DirectGraphics g = DirectGraphics.Create(supersampledBitmap);
        g.DrawTriangleFilled(scaledTriangle, color, color);

        DownSample(bitmapSliced, supersampledBitmap, samples);
    }
    public void DrawMSAATriangleFilled(Triangle2D<int> triangle, int samples, DirectColor color) {
        Rectangle2D<int> rec = triangle.GetBoundingBox().ToRectangle();
        DirectBitmap bitmapSliced = _bitmap.Slice(rec);

        Triangle2D<int> transformedTriangle = triangle.Transform(new(-rec.Left, -rec.Top));

        Random r = new Random();

        for (int x = 0; x < bitmapSliced.Width; x++) {
            for (int y = 0; y < bitmapSliced.Height; y++) {
                Vector3D<double> sampleColor = new Vector3D<double>();
                DirectColor backgroundColor = bitmapSliced.GetPixel(x, y);

                for (int s = 0; s < samples * samples; s++) {

                    Point2D<double> sampledPoint = new Point2D<double>(x + r.NextDouble() - 0.5, y + r.NextDouble() - 0.5);

                    if (IsInsideTriangle(sampledPoint, transformedTriangle)) {
                        sampleColor += new Vector3D<double>(color.R, color.G, color.B);
                    } else {
                        sampleColor += new Vector3D<double>(backgroundColor.R, backgroundColor.G, backgroundColor.B);
                    }
                }
                Vector3D<double> averageColor = sampleColor / (samples * samples);
                bitmapSliced.SetPixel(x, y, DirectColor.FromArgb(255, (byte)(averageColor.X), (byte)(averageColor.Y), (byte)(averageColor.Z)));
            }
        }
    }

    //Triangle
    private void DrawTriangle(Point2D<int> point1, Point2D<int> point2, Point2D<int> point3, DirectColor color) {
        DrawLine(point1, point2, color);
        DrawLine(point2, point3, color);
        DrawLine(point3, point1, color);
    }
    private void DrawTriangleFilled(Triangle2D<int> triangle, DirectColor lineColor, DirectColor fillColor) {

        Point2D<int> top = triangle.Point1, middle = triangle.Point2, bottom = triangle.Point3;

        //Sort points by Y
        if (middle.Y < top.Y)
            (top, middle) = (middle, top);
        if (middle.Y > bottom.Y)
            (bottom, middle) = (middle, bottom);
        if (middle.Y < top.Y)
            (top, middle) = (middle, top);


        if (top.Y == middle.Y) //top flat
        {
            Point2D<int> right = top, left = middle;
            if (right.X < left.X)
                (left, right) = (right, left);

            DrawTopFlatTriangle(right, left, bottom, lineColor, fillColor);
            return;
        }

        if (bottom.Y == middle.Y) //bottom flat
        {
            Point2D<int> right = bottom, left = middle;
            if (right.X < left.X)
                (left, right) = (right, left);

            DrawBottomFlatTriangle(top, left, right, lineColor, fillColor);
            return;
        }

        Point2D<int> splitPoint = new((int)(top.X + ((middle.Y - top.Y) / (float)(bottom.Y - top.Y)) * (bottom.X - top.X)), middle.Y);
        if (splitPoint.X < middle.X) {
            //split point is left
            DrawBottomFlatTriangle(top, splitPoint, middle, lineColor, fillColor);
            DrawTopFlatTriangle(middle, splitPoint, bottom, lineColor, fillColor);
        } else {
            //split point is right
            DrawBottomFlatTriangle(top, middle, splitPoint, lineColor, fillColor);
            DrawTopFlatTriangle(splitPoint, middle, bottom, lineColor, fillColor);
        }

    }
    private void DrawBottomFlatTriangle(Point2D<int> top, Point2D<int> bottomLeft, Point2D<int> bottomRight, DirectColor lineColor, DirectColor fillColor) {
        double inverseSlopeLeft  = (double)(bottomLeft.X - top.X) / (bottomLeft.Y - top.Y);
        double inverseSlopeRight = (double)(bottomRight.X - top.X) / (bottomRight.Y - top.Y);

        double currentXPositionLeft  = top.X;
        double currentXPositionRight = top.X;

        for (int scanlineY = top.Y; scanlineY < bottomLeft.Y; scanlineY++) {

            _bitmap.SetPixel((int)currentXPositionLeft, scanlineY, lineColor);
            _bitmap.SetPixel((int)currentXPositionRight, scanlineY, lineColor);

            for (int x = (int)currentXPositionLeft + 1; x <= currentXPositionRight - 1; x++) {
                _bitmap.SetPixel(x, scanlineY, fillColor);
            }

            currentXPositionLeft += inverseSlopeLeft;
            currentXPositionRight += inverseSlopeRight;
        }
    }
    private void DrawTopFlatTriangle(Point2D<int> topRight, Point2D<int> topLeft, Point2D<int> bottom, DirectColor lineColor, DirectColor fillColor) {
        double inverseSlopeLeft  = (double)(bottom.X - topLeft.X) / (bottom.Y - topLeft.Y);
        double inverseSlopeRight = (double)(bottom.X - topRight.X) / (bottom.Y - topRight.Y);

        double currentXPositionLeft  = bottom.X;
        double currentXPositionRight = bottom.X;

        for (int scanlineY = bottom.Y; scanlineY >= topRight.Y; scanlineY--) {

            _bitmap.SetPixel((int)currentXPositionLeft, scanlineY, lineColor);
            _bitmap.SetPixel((int)currentXPositionRight + 1, scanlineY, lineColor);

            for (int x = (int)currentXPositionLeft + 1; x <= currentXPositionRight; x++) {
                _bitmap.SetPixel(x, scanlineY, fillColor);
            }
            currentXPositionLeft -= inverseSlopeLeft;
            currentXPositionRight -= inverseSlopeRight;
        }
    }

    private void DrawBottomFlatAnitAliasedTriangle(Point2D<int> top, Point2D<int> bottomLeft, Point2D<int> bottomRight, DirectColor color) {
        double inverseSlopeLeft  = (double)(bottomLeft.X - top.X) / (bottomLeft.Y - top.Y);
        double inverseSlopeRight = (double)(bottomRight.X - top.X) / (bottomRight.Y - top.Y);

        double currentXPositionLeft  = top.X;
        double currentXPositionRight = top.X;

        for (int scanlineY = top.Y; scanlineY <= bottomLeft.Y; scanlineY++) {
            //TODO loop
            //TODO not optimal
            if (currentXPositionLeft != currentXPositionRight) {
                DrawLine(new Line2D<int>(new((int)System.Math.Floor(currentXPositionLeft + 1), scanlineY), new((int)System.Math.Floor(currentXPositionRight - 1), scanlineY)), color);
            }
            currentXPositionLeft += inverseSlopeLeft;
            currentXPositionRight += inverseSlopeRight;
        }
    }
    private void DrawTopFlatAnitAliasedTriangle(Point2D<int> topRight, Point2D<int> topLeft, Point2D<int> bottom, DirectColor color) {
        double inverseSlopeLeft  = (double)(bottom.X - topLeft.X) / (bottom.Y - topLeft.Y);
        double inverseSlopeRight = (double)(bottom.X - topRight.X) / (bottom.Y - topRight.Y);

        double currentXPositionLeft  = bottom.X;
        double currentXPositionRight = bottom.X;

        for (int scanlineY = bottom.Y; scanlineY > topRight.Y; scanlineY--) {

            if (currentXPositionLeft != currentXPositionRight) {
                DrawLine(new Line2D<int>(new((int)System.Math.Floor(currentXPositionLeft) + 1, scanlineY), new((int)System.Math.Floor(currentXPositionRight) - 1, scanlineY)), color);
            }
            currentXPositionLeft -= inverseSlopeLeft;
            currentXPositionRight -= inverseSlopeRight;
        }
    }


    //Anit aliased line methods
    private double fpart(double x) {
        if (x < 0)
            return 1 - (x - System.Math.Floor(x));

        return x - System.Math.Floor(x);
    }
    private double rfpart(double x) {
        return 1 - fpart(x);
    }
    private DirectColor GetColor(DirectColor color, double brightness) {
        return DirectColor.FromArgb((byte)(brightness * 255), color);
    }

    //SSAA
    private void DownSample(DirectBitmap originalBitmap, DirectBitmap sampledBitmap, int samples) {
        for (int x = 0; x < originalBitmap.Width; x++) {
            for (int y = 0; y < originalBitmap.Height; y++) {
                Vector3D<int> sampledColor = new Vector3D<int>();
                for (int i = 0; i < samples; i++) {
                    for (int j = 0; j < samples; j++) {

                        Vector3D<int> newColor = new Vector3D<int>((int)(sampledBitmap.GetPixel(x * samples + i, y * samples + j).R),
                                                                   (int)(sampledBitmap.GetPixel(x * samples + i, y * samples + j).G),
                                                                   (int)(sampledBitmap.GetPixel(x * samples + i, y * samples + j).B));

                        if (newColor.X == 0 && newColor.Y == 0 && newColor.Z == 0) {
                            newColor = new(originalBitmap.GetPixel(x, y).R, originalBitmap.GetPixel(x, y).G, originalBitmap.GetPixel(x, y).B);
                        }

                        sampledColor += newColor;
                    }
                }

                originalBitmap.SetPixel(x, y, DirectColor.FromArgb(255,
                                                            (byte)(sampledColor.X / (samples * samples)),
                                                            (byte)(sampledColor.Y / (samples * samples)),
                                                            (byte)(sampledColor.Z / (samples * samples))));
            }
        }
    }
    private bool IsInsideTriangle(Point2D<double> point, Triangle2D<int> triangle) {
        double alpha = ((triangle.Point2.Y - triangle.Point3.Y) * (point.X - triangle.Point3.X) + (triangle.Point3.X - triangle.Point2.X) * (point.Y - triangle.Point3.Y)) / ((triangle.Point2.Y - triangle.Point3.Y) * (triangle.Point1.X - triangle.Point3.X) + (triangle.Point3.X - triangle.Point2.X) * (triangle.Point1.Y - triangle.Point3.Y));
        double beta = ((triangle.Point3.Y - triangle.Point1.Y) * (point.X - triangle.Point3.X) + (triangle.Point1.X - triangle.Point3.X) * (point.Y - triangle.Point3.Y)) / ((triangle.Point2.Y - triangle.Point3.Y) * (triangle.Point1.X - triangle.Point3.X) + (triangle.Point3.X - triangle.Point2.X) * (triangle.Point1.Y - triangle.Point3.Y));
        double gamma = 1 - alpha - beta;

        return alpha > 0 && beta > 0 && gamma > 0;
    }


    //Anti aliased ellipse methods
    private void SetPixel4(Point2D<int> center, int deltaX, int deltaY, DirectColor color) {
        _bitmap.BlendPixel(center.X + deltaX, center.Y + deltaY, color);
        _bitmap.BlendPixel(center.X - deltaX, center.Y + deltaY, color);
        _bitmap.BlendPixel(center.X + deltaX, center.Y - deltaY, color);
        _bitmap.BlendPixel(center.X - deltaX, center.Y - deltaY, color);
    }
}