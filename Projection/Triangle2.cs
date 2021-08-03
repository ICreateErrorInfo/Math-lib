
using Math_lib;

namespace Projection
{
    public class Triangle2
    {

        public Triangle2()
        {

        }

        public Triangle2(Point2 p1, Point2 p2, Point2 p3)
        {
            Points[0] = p1;
            Points[1] = p2;
            Points[2] = p3;
        }
        public Triangle2(Point3 p1, Point3 p2, Point3 p3)
        {
            Points[0] = new(p1);
            Points[1] = new(p2);
            Points[2] = new(p3);
        }

        public Point2[] Points { get; } = new Point2[3];

    }

}