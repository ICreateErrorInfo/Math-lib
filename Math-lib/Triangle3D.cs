namespace Math_lib
{

    public class Triangle3D {

        //Properties
        public Vertex v0;
        public Vertex v1;
        public Vertex v2;

        //Constructors
        public Triangle3D() {

        }
        public Triangle3D(Point3D p)
        {
            v0 = new(p);
            v1 = new(p);
            v2 = new(p);
        }
        public Triangle3D(Triangle2D p)
        {
            v0 = new(new(p.Points[0]));
            v1 = new(new(p.Points[1]));
            v2 = new(new(0));
        }
        public Triangle3D(Point3D p1, Point3D p2, Point3D p3) 
        {
            v0 = new(p1);
            v1 = new(p2);
            v2 = new(p3);
        }  
        public Triangle3D(Vertex v0, Vertex v1, Vertex v2)
        {
            this.v0 = v0;
            this.v1 = v1;
            this.v2 = v2;
        }

        //Methods


        //overrides +
        public static Triangle3D operator +(Triangle3D t, Point3D p)
        {
            return new Triangle3D(t.v0 + new Vertex(p),
                                  t.v1 + new Vertex(p),
                                  t.v2 + new Vertex(p));
        }

        //overrides -
        public static Triangle3D operator -(Triangle3D t, Point3D p)
        {
            return new Triangle3D(t.v0 - new Vertex(p),
                                  t.v1 - new Vertex(p),
                                  t.v2 - new Vertex(p));
        }

        //overrides *
        public static Triangle3D operator *(Triangle3D t, Point3D p)
        {
            return new Triangle3D(t.v0 * new Vertex(p),
                                  t.v1 * new Vertex(p),
                                  t.v2 * new Vertex(p));
        }

        //overrides /
        public static Triangle3D operator /(Triangle3D t, Point3D p)
        {
            return new Triangle3D(t.v0 / new Vertex(p),
                                  t.v1 / new Vertex(p),
                                  t.v2 / new Vertex(p));
        }
    }

}