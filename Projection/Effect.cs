using Math_lib;
using System.Drawing;

namespace Projection
{
    public abstract class Effect
    {
        public abstract Color PixelShader(Vertex iLine);

        public abstract Vertex CreateVertex(Point3D p);
        public abstract Vertex CreateVertex(Vector3D p);
    }
}
