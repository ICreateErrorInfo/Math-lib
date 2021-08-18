using Math_lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    class SolidGeometryEffect : Effect
    {
        public override Color GetColor(Vertex v)
        {
            return v.col;
        }

        public override Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex)
        {
            return new Triangle3D(new(v0.pos, v0.t, triangleColors[triangleIndex / 2]),
                                  new(v1.pos, v1.t, triangleColors[triangleIndex / 2]),
                                  new(v2.pos, v2.t, triangleColors[triangleIndex / 2]));
        }

        public override Vertex Translate(Vertex vIn)
        {
            return new(rotation * vIn.pos + translation, vIn.t);
        }

        public void BindColors(List<Color> colors)
        {
            triangleColors = colors;
        }

        private List<Color> triangleColors;
    }
}
