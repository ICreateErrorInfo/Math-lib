using Math_lib;
using System.Drawing;

namespace Projection
{
    abstract class Effect : DefaultShader
    {
        public abstract Color GetColor(Vertex v);
        public abstract Vertex Translate(Vertex vIn);
        public abstract Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex);
    }
}
