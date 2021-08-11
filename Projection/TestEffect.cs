using Math_lib;
using System.Drawing;

namespace Projection
{
    public class TestEffect : Effect
    {
        public TestEffect()
        {
        }

        public override Color PixelShader(Vertex iLine)
        {
            return Color.Red;
        }
    }
}
