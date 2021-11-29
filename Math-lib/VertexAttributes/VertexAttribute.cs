using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.VertexAttributes
{
    public abstract class VertexAttribute 
    {
        public abstract VertexAttribute Add(VertexAttribute vertexAttribute2);
        public abstract VertexAttribute Sub(VertexAttribute vertexAttribute2);
        public abstract VertexAttribute Mul(VertexAttribute vertexAttribute2);
        public abstract VertexAttribute Div(VertexAttribute vertexAttribute2);

        public abstract VertexAttribute AddDouble(double d);
        public abstract VertexAttribute SubDouble(double d);
        public abstract VertexAttribute MulDouble(double d);
        public abstract VertexAttribute DivDouble(double d);

        public abstract void SetValue(double d);
        public abstract VertexAttribute CopyThis();

    }
}
