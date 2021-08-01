using System.Collections.Generic;

using Math_lib;

namespace Projection {

    public class Triangle {

        public Triangle() {

        }

        public Triangle(Point p1, Point p2, Point p3) {
            Points[0] = p1;
            Points[1] = p2;
            Points[2] = p3;
        }

        public Point[] Points { get; } = new Point[3];

    }

    class Mesh {

        public Mesh() {
            Triangles = new();
        }

        public List<Triangle> Triangles { get; }

    }

}