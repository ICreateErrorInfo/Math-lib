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
        public Vector2D t;


        public Vertex(Point3D pos)
        {
            this.pos = pos;
        }
        public Vertex(Point3D pos, Vertex src)
        {
            t = src.t;
            this.pos = pos;
        }
        public Vertex(Point3D pos, Vector2D t)
        {
            this.pos = pos;
            this.t = t;
        }


        public static Vertex operator +(Vertex v0, Vertex v1)
        {
            return new(v0.pos + v1.pos, v0.t + v1.t);
        }
        public static Vertex operator -(Vertex v0, Vertex v1)
        {
            return new(new(v0.pos - v1.pos), v0.t - v1.t);
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
            return $"[{pos}, {t}]";
        }
    }
}
