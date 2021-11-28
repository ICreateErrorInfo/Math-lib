using Math_lib;

using System.Drawing;

namespace Projection
{
    class SolidColorEffect : Effect
    {
        public void SetColor(Color c)
        {
            _c = c;
        }

        public override Color GetColor(Vertex v)
        {
            return _c;
        }

        public override Vertex Translate(Vertex vIn)
        {
            return new Vertex(rotation * vIn.Pos + translation, vIn.Attributes);
        }

        public override Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex)
        {
            return new(v0, v1, v2);
        }

        Color _c = Color.Red;
    }
}
