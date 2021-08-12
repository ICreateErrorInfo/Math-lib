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

        public override Vertex Translate(Vertex vIn)
        {
            return new(rotation * vIn.pos + translation, vIn.t);
        }

        Color c = Color.Red;
    }
}
