using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public abstract class Vertex
    {
        public Point3D pos;

        public Vertex(Point3D pos)
        {
            this.pos = pos;
        }


        public static Vertex operator +(Vertex v0, Vertex v1)
        {
            return v0.Plus(v1);
        }
        public static Vertex operator -(Vertex v0, Vertex v1)
        {
            return v0.Minus(v1);
        }
        public static Vertex operator *(Vertex v0, Vertex v1)
        {
            return v0.Mul(v1);
        }
        public static Vertex operator /(Vertex v0, Vertex v1)
        {
            return v0.Div(v1);
        }

        //overrides ToString
        public override string ToString()
        {
            return $"[{pos}]";
        }
        protected abstract Vertex Minus( Vertex v1);
        protected abstract Vertex Plus(Vertex v1);
        protected abstract Vertex Mul(Vertex v1);
        protected abstract Vertex Div(Vertex v1);
    }
}
