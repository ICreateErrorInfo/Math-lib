using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Math_lib.DrawingObjects {

    public class Rectangle : DrawingObject
    {

        private readonly Point2D _p1;
        private readonly Point2D _p2;
        private readonly Point2D _p3;
        private readonly Point2D _p4;
        private readonly Color   _color;
        private readonly bool    _fill;

        public Rectangle(Point2D p1, Point2D p3, Color color, bool fill = false) 
        {
            _p1 = p1;
            _p2 = new Point2D(p3.X, p1.Y);
            _p3 = p3;
            _p4 = new Point2D(p1.X, p3.Y);

            _color = color;
            _fill  = fill;
        }

        public Rectangle(Point2D p1, Point2D p2, Point2D p3, Point2D p4, Color color, bool fill = false) 
        {
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
            _p4 = p4;

            _color = color;
            _fill  = fill;
        }

        public override DirectBitmap Draw(DirectBitmap bmp)
        {
            Line l1 = new Line(_p1, _p2, _color);
            Line l2 = new Line(_p2, _p3, _color);
            Line l3 = new Line(_p3, _p4, _color);
            Line l4 = new Line(_p4, _p1, _color);

            if (_fill == false)
            {
                bmp = l1.Draw(bmp);
                bmp = l2.Draw(bmp);
                bmp = l3.Draw(bmp);
                bmp = l4.Draw(bmp);
            }
            else
            {
                // TODO Moritz: wird gefüllt aber nicht gebraucht, auch das "k" unten nicht
                List<Point2D> points = new List<Point2D>();

                var o1 = l1.DrawLineWithOutput(ref bmp);
                var o2 = l2.DrawLineWithOutput(ref bmp);
                var o3 = l3.DrawLineWithOutput(ref bmp);
                var o4 = l4.DrawLineWithOutput(ref bmp);

                points = o1;
                points.AddRange(o2);
                points.AddRange(o3);
                points.AddRange(o4);

                var       k = points.OrderBy(p => p.Y);
                Point2D[] p = k.ToArray();

                for (int i = 0; i < p.Length - 1; i++)
                {
                    if (i + 1 > p.Length - 1)
                    {
                        break;
                    }
                    if (p[i].Y != p[i + 1].Y)
                    {
                        continue;
                    }

                    int anf = (int)p[i].X;
                    int end = (int)p[i + 1].X;

                    if (end - anf > 0)
                    {
                        for (int j = anf; j < end; j++)
                        {
                            bmp.SetPixel(j, (int)p[i].Y, _color);
                        }
                    }
                    else
                    {
                        for (int j = anf; j > end; j--)
                        {
                            bmp.SetPixel(j, (int)p[i].Y, _color);
                        }
                    }
                }
            }

            return bmp;
        }
    }

}