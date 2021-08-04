namespace Math_lib
{

    public class Triangle3 {

        //Constructors
        public Triangle3() {

        }
        public Triangle3(Point3 p)
        {
            Points[0] = p;
            Points[1] = p;
            Points[2] = p;
        }
        public Triangle3(Point3 p1, Point3 p2, Point3 p3) {
            Points[0] = p1;
            Points[1] = p2;
            Points[2] = p3;
        }  

        //Properties
        public Point3[] Points { get; } = new Point3[3];

        //Methods


        //overrides +
        public static Triangle3 operator +(Triangle3 t, Point3 p)
        {
            return new Triangle3(new Point3(t.Points[0] + p),
                                 new Point3(t.Points[1] + p),
                                 new Point3(t.Points[2] + p));
        }

        //overrides -
        public static Triangle3 operator -(Triangle3 t, Point3 p)
        {
            return new Triangle3(new Point3(t.Points[0] - p),
                                 new Point3(t.Points[1] - p),
                                 new Point3(t.Points[2] - p));
        }

        //overrides *
        public static Triangle3 operator *(Triangle3 t, Point3 p)
        {
            return new Triangle3(new Point3(t.Points[0] * p),
                                 new Point3(t.Points[1] * p),
                                 new Point3(t.Points[2] * p));
        }

        //overrides /
        public static Triangle3 operator /(Triangle3 t, Point3 p)
        {
            return new Triangle3(new Point3(t.Points[0] / p),
                                 new Point3(t.Points[1] / p),
                                 new Point3(t.Points[2] / p));
        }
    }

}