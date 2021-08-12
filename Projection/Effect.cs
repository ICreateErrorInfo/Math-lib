using Math_lib;
using System.Drawing;

namespace Projection
{
    abstract class Effect
    {
        public abstract Color GetColor(Vertex v = null);
    }
}
