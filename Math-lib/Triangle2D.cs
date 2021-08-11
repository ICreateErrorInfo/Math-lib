namespace Math_lib
{
    public class Triangle2D
    {

        //Constructors
        public Triangle2D()
        {

        }
        public Triangle2D(Point2D p1, Point2D p2, Point2D p3)
        {
            Points[0] = p1;
            Points[1] = p2;
            Points[2] = p3;
        }
        public Triangle2D(Point3D p1, Point3D p2, Point3D p3)
        {
            Points[0] = new(p1);
            Points[1] = new(p2);
            Points[2] = new(p3);
        }

        //Properties
        public Point2D[] Points { get; } = new Point2D[3];

        //Methods


        //overrides +
        public static Triangle2D operator +(Triangle2D t, Point2D p)
        {
            return new Triangle2D(new Point2D(t.Points[0] + p),
                                 new Point2D(t.Points[1] + p),
                                 new Point2D(t.Points[2] + p));
        }

        //overrides -
        public static Triangle2D operator -(Triangle2D t, Point2D p)
        {
            return new Triangle2D(new Point2D(t.Points[0] - p),
                                 new Point2D(t.Points[1] - p),
                                 new Point2D(t.Points[2] - p));
        }

        //overrides *
        public static Triangle2D operator *(Triangle2D t, Point2D p)
        {
            return new Triangle2D(new Point2D(t.Points[0] * p),
                                 new Point2D(t.Points[1] * p),
                                 new Point2D(t.Points[2] * p));
        }

        //overrides /
        public static Triangle2D operator /(Triangle2D t, Point2D p)
        {
            return new Triangle2D(new Point2D(t.Points[0] / p),
                                 new Point2D(t.Points[1] / p),
                                 new Point2D(t.Points[2] / p));
        }
    }

}