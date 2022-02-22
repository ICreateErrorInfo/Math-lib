namespace Math_lib
{
    public class RayDifferential : Ray
    {
        //Properties
        public bool hasDifferentials;
        public Point3D rxOrigin, ryOrigin;
        public Vector3D rxDirection, ryDirection;

        //Ctors
        public RayDifferential() { hasDifferentials = false; }
        public RayDifferential(Point3D o, Vector3D d, double tMax = double.PositiveInfinity, double time = 0) : base(o, d, tMax, time)
        {
            hasDifferentials = false;
        }
        public RayDifferential(Ray r) :base(r.O, r.D, r.TMax, r.Time)
        {
            hasDifferentials = false;
        }

        //Methods
        public void ScaleDifferentials(double s)
        {
            rxOrigin = O + (rxOrigin - O) * s;
            ryOrigin = O + (ryOrigin - O) * s;
            rxDirection = D + (rxDirection - D) * s;
            ryDirection = D + (ryDirection - D) * s;
        }


        public override string ToString()
        {
            return $"[o={O}, d={D}, tMax={TMax}, time={Time}, has differentials:{hasDifferentials}, xo={rxOrigin}, xd={rxDirection}, yo={ryOrigin}, yd={ryDirection}]";
        }
    }
}
