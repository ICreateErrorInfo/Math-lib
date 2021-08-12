using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public class TexVertex : Vertex
    {
        public TexVertex(Point3D pos, Vector2D t) : base(pos)
        {
            this.pos = pos;
            this.t = t;
        }


        public override Vertex ConvertFrom(Point3D p)
        {
            return new TexVertex(p, t);
        }

        public override Vertex ConvertFrom(double p)
        {
            return new TexVertex(new(p), new Vector2D(p));
        }

        public override Vertex Create(Point3D p)
        {
            return new TexVertex(p, t);
        }

        protected override Vertex Div(Vertex v1)
        {
            return new TexVertex(this.pos / v1.pos, t / v1.t);
        }

        protected override Vertex Minus(Vertex v1)
        {
            return new TexVertex(new(this.pos - v1.pos), t - v1.t);
        }

        protected override Vertex Mul(Vertex v1)
        {
            return new TexVertex(this.pos * v1.pos, t * v1.t);
        }

        protected override Vertex Plus(Vertex v1)
        {
            return new TexVertex(this.pos + v1.pos, t + v1.t);
        }
    }
}
