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
        public RayDifferential(Point3D o, Vector3D d, double tMax = double.PositiveInfinity, double time = 0)
        {
            this.o = o;
            this.d = d;
            this.tMax = tMax;
            this.time = time;
            hasDifferentials = false;
        }
        public RayDifferential(Ray r) 
        {
            o = r.o;
            d = r.d;
            tMax = r.tMax;
            time = r.time;
            hasDifferentials = false;
        }

        //Methods
        public void ScaleDifferentials(double s)
        {
            rxOrigin = o + (rxOrigin - o) * s;
            ryOrigin = o + (ryOrigin - o) * s;
            rxDirection = d + (rxDirection - d) * s;
            ryDirection = d + (ryDirection - d) * s;
        }


        public override string ToString()
        {
            return $"[o={o}, d={d}, tMax={tMax}, time={time}, has differentials:{hasDifferentials}, xo={rxOrigin}, xd={rxDirection}, yo={ryOrigin}, yd={ryDirection}]";
        }
    }
}
