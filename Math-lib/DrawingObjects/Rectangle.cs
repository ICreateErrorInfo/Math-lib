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
            bmp = new Triangle(_p1, _p2, _p3, _color).Draw(bmp);
            bmp = new Triangle(_p3, _p4, _p1, _color).Draw(bmp);

            return bmp;
        }
    }

}