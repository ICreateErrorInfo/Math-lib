using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public class Vertex
    {
        public Point3D pos;
        public Point2D t;

        public Vertex(Point3D pos)
        {
            this.pos = pos;
        }
        public Vertex(Point3D pos, Point2D t)
        {
            this.pos = pos;
            this.t = t;
        }
        public Vertex(double d)
        {
            this.pos = new Point3D(d);
            if(t != null)
            {
                this.t = new Point2D(d);
            }
        }


        public static Vertex operator +(Vertex v0, Vertex v1)
        {
            return new(v0.pos + v1.pos, v0.t + v1.t);
        }
        public static Vertex operator -(Vertex v0, Vertex v1)
        {
            return new(new(v0.pos - v1.pos), new(v0.t - v1.t));
        }
        public static Vertex operator *(Vertex v0, Vertex v1)
        {
            return new(v0.pos * v1.pos, v0.t * v1.t);
        }
        public static Vertex operator /(Vertex v0, Vertex v1)
        {
            return new(v0.pos / v1.pos, v0.t / v1.t);
        }

        //overrides ToString
        public override string ToString()
        {
            return $"[{pos}]";
        }        
    }
}
