using System.Collections.Generic;

using Math_lib;

namespace Projection { 
    class Triangle 
    {
        public Triangle(Point p1, Point p2, Point p3) 
        {
            p[0] = p1;
            p[1] = p2;
            p[2] = p3;
        }
        public Triangle() 
        {

        }

        public Point[] p = new Point[3];
    }
    class Mesh 
    {

        public Mesh() {
            tris = new();
        }
        public List<Triangle> tris;
    }
}
