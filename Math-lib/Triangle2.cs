namespace Math_lib
{
    public class Triangle2
    {

        //Constructors
        public Triangle2()
        {

        }
        public Triangle2(Triangle3 p)
        {
            Points[0] = new Point2(p.Points[0]);
            Points[1] = new Point2(p.Points[1]);
            Points[2] = new Point2(p.Points[2]);
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

        //Properties
        public Point2[] Points { get; } = new Point2[3];

        //Methods


        //overrides +
        public static Triangle2 operator +(Triangle2 t, Point2 p)
        {
            return new Triangle2(new Point2(t.Points[0] + p),
                                 new Point2(t.Points[1] + p),
                                 new Point2(t.Points[2] + p));
        }

        //overrides -
        public static Triangle2 operator -(Triangle2 t, Point2 p)
        {
            return new Triangle2(new Point2(t.Points[0] - p),
                                 new Point2(t.Points[1] - p),
                                 new Point2(t.Points[2] - p));
        }

        //overrides *
        public static Triangle2 operator *(Triangle2 t, Point2 p)
        {
            return new Triangle2(new Point2(t.Points[0] * p),
                                 new Point2(t.Points[1] * p),
                                 new Point2(t.Points[2] * p));
        }

        //overrides /
        public static Triangle2 operator /(Triangle2 t, Point2 p)
        {
            return new Triangle2(new Point2(t.Points[0] / p),
                                 new Point2(t.Points[1] / p),
                                 new Point2(t.Points[2] / p));
        }
    }

}