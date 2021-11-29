using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.VertexAttributes
{
    public class ColorVertexAttribute : VertexAttribute
    {
        public Color Color {get; private set; }


        public ColorVertexAttribute(Color color) 
        {
            Color = color;
        }


        public override VertexAttribute Add(VertexAttribute vertexAttribute2) 
        {
            return this;
        }

        public override VertexAttribute Div(VertexAttribute vertexAttribute2)
        {
            return this;
        }

        public override VertexAttribute Mul(VertexAttribute vertexAttribute2)
        {
            return this;
        }

        public override VertexAttribute Sub(VertexAttribute vertexAttribute2)
        {
            return this;
        }

        public override void SetValue(double d)
        {
            this.Color = Color.FromArgb((int)d, (int)d, (int)d);
        }

        public override VertexAttribute CopyThis()
        {
            return new ColorVertexAttribute(Color);
        }

        public override VertexAttribute AddDouble(double d)
        {
            return this;
        }

        public override VertexAttribute SubDouble(double d)
        {
            return this;
        }

        public override VertexAttribute MulDouble(double d)
        {
            return this;
        }

        public override VertexAttribute DivDouble(double d)
        {
            return this;
        }
    }
}
