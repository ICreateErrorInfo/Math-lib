using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public class Ray
    {
        //properties
        public Point3D o { get; init; }
        public Vector3D d { get; init; }
        public double tMax { get; init; }
        public double time { get; init; }


        //Ctors
        public Ray()
        {
            tMax = double.PositiveInfinity;
            time = 0;
        }
        public Ray(Point3D o, Vector3D d, double tMax = double.PositiveInfinity, double time = 0)
        {
            this.o = o;
            this.d = d;
            this.tMax = tMax;
            this.time = time;
        }
        

        //Methods
        public Point3D At(double t)
        {
            return o + d * t;
        }


        public override string ToString()
        {
            return $"[o={o}, d={d}, tMax={tMax}, time={time}]";
        }
    }
}
