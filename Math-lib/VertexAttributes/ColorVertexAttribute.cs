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
        public Color Color {get; init; }


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
    }
}
