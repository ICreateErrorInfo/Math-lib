using Math_lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    class SolidColorEffect : Effect
    {
        public void SetColor(Color c)
        {
            this.c = c;
        }

        public override Color GetColor(Vertex v)
        {
            return c;
        }

        Color c = Color.Red;
    }
}
