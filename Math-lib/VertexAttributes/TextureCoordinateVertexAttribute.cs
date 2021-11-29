using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.VertexAttributes
{
    public class TextureCoordinateVertexAttribute : VertexAttribute
    {
        public Point2D T { get; private set; }

        public TextureCoordinateVertexAttribute(Point2D t)
        {
            T = t;
        }

        public override VertexAttribute Add(VertexAttribute vertexAttribute2)
        {
            var other = (TextureCoordinateVertexAttribute) vertexAttribute2;
            return new TextureCoordinateVertexAttribute(this.T + other.T);
        }

        public override VertexAttribute Div(VertexAttribute vertexAttribute2)
        {
            var other = (TextureCoordinateVertexAttribute)vertexAttribute2;
            return new TextureCoordinateVertexAttribute(this.T / other.T);
        }

        public override VertexAttribute Mul(VertexAttribute vertexAttribute2)
        {
            var other = (TextureCoordinateVertexAttribute)vertexAttribute2;
            return new TextureCoordinateVertexAttribute(this.T * other.T);
        }

        public override VertexAttribute Sub(VertexAttribute vertexAttribute2)
        {
            var other = (TextureCoordinateVertexAttribute)vertexAttribute2;
            return new TextureCoordinateVertexAttribute((Point2D)(this.T - other.T));
        }

        public override void SetValue(double d)
        {
            this.T = new(d);
        }

        public override VertexAttribute CopyThis()
        {
            return new TextureCoordinateVertexAttribute(T);
        }

        public override VertexAttribute AddDouble(double d)
        {
            return new TextureCoordinateVertexAttribute(T + d);
        }

        public override VertexAttribute SubDouble(double d)
        {
            return new TextureCoordinateVertexAttribute(T - d);
        }

        public override VertexAttribute MulDouble(double d)
        {
            return new TextureCoordinateVertexAttribute(T * d);
        }

        public override VertexAttribute DivDouble(double d)
        {
            return new TextureCoordinateVertexAttribute(T / d);
        }
    }
}
