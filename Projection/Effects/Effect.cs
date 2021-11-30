using Math_lib;
using System.Drawing;

namespace Projection
{
    abstract class Effect
    {
        public abstract Color GetColor(Vertex v);
        public abstract Vertex Translate(Vertex vIn);
        public abstract Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex);

        public void BindRotation(Matrix rot)
        {
            rotation = rot;
        }
        public void BindTranslation(Vector3D translate)
        {
            translation = translate;
        }

        public Matrix rotation;
        public Vector3D translation;
    }
}
