using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public class BasicVertex : Vertex
    {
        public BasicVertex(Point3D p) : base(p)
        {

        }

        public override Vertex ConvertFrom(Point3D p)
        {
            return new BasicVertex(p);
        }

        public override Vertex ConvertFrom(double p)
        {
            return new BasicVertex(new(p));
        }

        public override Vertex Create(Point3D p)
        {
            return new BasicVertex(p);
        }

        protected override Vertex Div(Vertex v1)
        {
            return new BasicVertex(this.pos / v1.pos);
        }

        protected override Vertex Minus(Vertex v1)
        {
            return new BasicVertex(new(this.pos - v1.pos));
        }

        protected override Vertex Mul(Vertex v1)
        {
            return new BasicVertex(this.pos * v1.pos);
        }

        protected override Vertex Plus(Vertex v1)
        {
            return new BasicVertex(this.pos + v1.pos);
        }
    }
}
