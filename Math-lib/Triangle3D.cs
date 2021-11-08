namespace Math_lib
{
    public class Triangle3D
    {

        //Constructors
        public Triangle3D()
        {

        }
        public Triangle3D(Vertex p)
        {
            Points[0] = p;
            Points[1] = p;
            Points[2] = p;
        }
        public Triangle3D(Vertex p1, Vertex p2, Vertex p3)
        {
            Points[0] = p1;
            Points[1] = p2;
            Points[2] = p3;
        }

        //Properties
        public Vertex[] Points { get; } = new Vertex[3];

        //Methods


        //overrides +
        public static Triangle3D operator +(Triangle3D t, Vertex p)
        {
            return new Triangle3D(t.Points[0] + p,
                                  t.Points[1] + p,
                                  t.Points[2] + p);
        }

        //overrides -
        public static Triangle3D operator -(Triangle3D t, Vertex p)
        {
            return new Triangle3D(t.Points[0] - p,
                                  t.Points[1] - p,
                                  t.Points[2] - p);
        }

        //overrides *
        public static Triangle3D operator *(Triangle3D t, Vertex p)
        {
            return new Triangle3D(t.Points[0] * p,
                                  t.Points[1] * p,
                                  t.Points[2] * p);
        }

        //overrides /
        public static Triangle3D operator /(Triangle3D t, Vertex p)
        {
            return new Triangle3D(t.Points[0] / p,
                                  t.Points[1] / p,
                                  t.Points[2] / p);
        }
    }
}
