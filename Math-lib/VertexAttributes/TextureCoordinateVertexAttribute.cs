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

        public override VertexAttribute Add(VertexAttribute vertexAttribute2) => new TextureCoordinateVertexAttribute(T + ((TextureCoordinateVertexAttribute)vertexAttribute2).T);

        public override VertexAttribute Div(VertexAttribute vertexAttribute2) => new TextureCoordinateVertexAttribute(new Point2D(T.X / ((TextureCoordinateVertexAttribute)vertexAttribute2).T.X, T.Y / ((TextureCoordinateVertexAttribute)vertexAttribute2).T.Y));

        public override VertexAttribute Mul(VertexAttribute vertexAttribute2) => new TextureCoordinateVertexAttribute(T * ((TextureCoordinateVertexAttribute)vertexAttribute2).T);

        public override VertexAttribute Sub(VertexAttribute vertexAttribute2) => new TextureCoordinateVertexAttribute((Point2D)(T - ((TextureCoordinateVertexAttribute)vertexAttribute2).T));


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
            return new TextureCoordinateVertexAttribute(T + new Point2D(d));
        }

        public override VertexAttribute SubDouble(double d)
        {
            return new TextureCoordinateVertexAttribute(T - new Vector2D(d));
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
