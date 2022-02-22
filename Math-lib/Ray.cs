namespace Math_lib
{
    public class Ray
    {
        //properties
        public readonly Point3D O;
        public readonly Vector3D D;
        public double TMax;
        public double Time;

        //Ctors
        public Ray()
        {
            TMax = double.PositiveInfinity;
            Time = 0;
        }
        public Ray(Point3D o, Vector3D d, double tMax = double.PositiveInfinity, double time = 0)
        {
            O = o;
            D = d;
            TMax = tMax;
            Time = time;
        }
        

        //Methods
        public Point3D At(double t)
        {
            return O + D * t;
        }


        public override string ToString()
        {
            return $"[o={O}, d={D}, tMax={TMax}, time={Time}]";
        }
    }
}
