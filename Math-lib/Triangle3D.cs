namespace Math_lib
{

    public class Triangle3D {

        //Constructors
        public Triangle3D() {

        }
        public Triangle3D(Point3D p)
        {
            Points[0] = p;
            Points[1] = p;
            Points[2] = p;
        }
        public Triangle3D(Point3D p1, Point3D p2, Point3D p3) {
            Points[0] = p1;
            Points[1] = p2;
            Points[2] = p3;
        }  

        //Properties
        public Point3D[] Points { get; } = new Point3D[3];

        //Methods


        //overrides +
        public static Triangle3D operator +(Triangle3D t, Point3D p)
        {
            return new Triangle3D(new Point3D(t.Points[0] + p),
                                 new Point3D(t.Points[1] + p),
                                 new Point3D(t.Points[2] + p));
        }

        //overrides -
        public static Triangle3D operator -(Triangle3D t, Point3D p)
        {
            return new Triangle3D(new Point3D(t.Points[0] - p),
                                 new Point3D(t.Points[1] - p),
                                 new Point3D(t.Points[2] - p));
        }

        //overrides *
        public static Triangle3D operator *(Triangle3D t, Point3D p)
        {
            return new Triangle3D(new Point3D(t.Points[0] * p),
                                 new Point3D(t.Points[1] * p),
                                 new Point3D(t.Points[2] * p));
        }

        //overrides /
        public static Triangle3D operator /(Triangle3D t, Point3D p)
        {
            return new Triangle3D(new Point3D(t.Points[0] / p),
                                 new Point3D(t.Points[1] / p),
                                 new Point3D(t.Points[2] / p));
        }
    }

}