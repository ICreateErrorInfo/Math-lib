using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.VertexAttributes
{
    public class NormalVertexAttribute : VertexAttribute
    {
        public Vector3D N { get; private set; }


        public NormalVertexAttribute(Vector3D n)
        {
            N = n;
        }

        public override VertexAttribute Add(VertexAttribute vertexAttribute2)
        {
            var other = (NormalVertexAttribute)vertexAttribute2;
            return new NormalVertexAttribute(this.N + other.N);
        }

        public override VertexAttribute Div(VertexAttribute vertexAttribute2)
        {
            var other = (NormalVertexAttribute)vertexAttribute2;
            return new NormalVertexAttribute(new Vector3D(
                N.X / other.N.X,
                N.Y / other.N.Y,
                N.Z / other.N.Z
                ));
        }

        public override VertexAttribute Mul(VertexAttribute vertexAttribute2)
        {
            var other = (NormalVertexAttribute)vertexAttribute2;
            return new NormalVertexAttribute(this.N * other.N);
        }

        public override VertexAttribute Sub(VertexAttribute vertexAttribute2)
        {
            var other = (NormalVertexAttribute)vertexAttribute2;
            return new NormalVertexAttribute(this.N - other.N);
        }

        public override void SetValue(double d)
        {
            this.N = new(d);
        }
        public override VertexAttribute CopyThis()
        {
            return new NormalVertexAttribute(N);
        }

        public override VertexAttribute AddDouble(double d)
        {
            return new NormalVertexAttribute(N + new Vector3D(d));
        }

        public override VertexAttribute SubDouble(double d)
        {
            return new NormalVertexAttribute(N - new Vector3D(d));
        }

        public override VertexAttribute MulDouble(double d)
        {
            return new NormalVertexAttribute(N * d);
        }

        public override VertexAttribute DivDouble(double d)
        {
            return new NormalVertexAttribute(N / d);
        }
    }
}
