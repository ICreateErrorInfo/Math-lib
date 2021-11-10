using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.DrawingObjects
{
    public class QuadBezier : DrawingObject
    {
        private Point2D _p;
        private Point2D _p1;
        private Point2D _p2;
        private System.Drawing.Color _color;

        public QuadBezier(Point2D p, Point2D p1, Point2D p2, System.Drawing.Color c)
        {
            _p = p;
            _p1 = p1;
            _p2 = p2;
            _color = c;
        }

        public override DirectBitmap Draw(DirectBitmap bmp)
        {
            int x0 = (int)_p.X;
            int y0 = (int)_p.Y;
            int x1 = (int)_p1.X; 
            int y1 = (int)_p1.Y;
            int x2 = (int)_p2.X;
            int y2 = (int)_p2.Y;

            int sx = x0 < x2 ? 1 : -1, sy = y0 < y2 ? 1 : -1; /* step direction */
            double x = x0 - 2 * x1 + x2, y = y0 - 2 * y1 + y2, xy = 2 * x * y * sx * sy;
            double cur = sx * sy * (x * (y2 - y0) - y * (x2 - x0)) / 2; /* curvature */
            /* compute error increments of P0 */
            double dx = (1 - 2 * Math.Abs(x0 - x1)) * y * y + Math.Abs(y0 - y1) * xy - 2 * cur * Math.Abs(y0 - y2);
            double dy = (1 - 2 * Math.Abs(y0 - y1)) * x * x + Math.Abs(x0 - x1) * xy + 2 * cur * Math.Abs(x0 - x2);
            /* compute error increments of P2 */
            double ex = (1 - 2 * Math.Abs(x2 - x1)) * y * y + Math.Abs(y2 - y1) * xy + 2 * cur * Math.Abs(y0 - y2);
            double ey = (1 - 2 * Math.Abs(y2 - y1)) * x * x + Math.Abs(x2 - x1) * xy - 2 * cur * Math.Abs(x0 - x2);

            /* sign of gradient must not change */
            //assert((x0 - x1) * (x2 - x1) <= 0 && (y0 - y1) * (y2 - y1) <= 0);

            if (cur == 0) { return new Line(new(x0, y0), new(x2, y2), _color).Draw(bmp); } /* straight line */

            x *= 2 * x; y *= 2 * y;
            if (cur < 0)
            { /* negated curvature */
                x = -x; dx = -dx; ex = -ex; xy = -xy;
                y = -y; dy = -dy; ey = -ey;
            }
            /* algorithm fails for almost straight line, check error values */
            if (dx >= -y || dy <= -x || ex <= -y || ey >= -x) {
                x1 = (x0 + 4 * x1 + x2) / 6; y1 = (y0 + 4 * y1 + y2) / 6; /* approximation */
                bmp = new Line(new(x0, y0), new(x1, y1), _color).Draw(bmp);
                bmp = new Line(new(x1, y1), new(x2, y2), _color).Draw(bmp);
                return bmp;
            }
            dx -= xy; ex = dx + dy; dy -= xy; /* error of 1.step */

            for (; ; )
            { /* plot curve */
                bmp.SetPixel(x0, y0, _color);
                ey = 2 * ex-dy; /* save value for test of y step */
                if (2 * ex >= dx)
                { /* x step */
                    if (x0 == x2) break;
                    x0 += sx; dy -= xy; ex += dx += y;
                }
                if (ey <= 0)
                { /* y step */
                    if (y0 == y2) break;
                    y0 += sy; dx -= xy; ex += dy += x;
                }
            }

            return bmp;
        }
    }
}
