using Math_lib;
using System.Drawing;

namespace Projection
{
    abstract class Effect : DefaultVertexShader
    {
        public abstract Color GetColor(Vertex v);
        public abstract Vertex Translate(Vertex vIn);
    }
}
