
using Math_lib;

namespace Projection
{

    public class Triangle3 {

        public Triangle3() {

        }

        public Triangle3(Point3 p1, Point3 p2, Point3 p3) {
            Points[0] = p1;
            Points[1] = p2;
            Points[2] = p3;
        }

        public Point3[] Points { get; } = new Point3[3];

    }

}