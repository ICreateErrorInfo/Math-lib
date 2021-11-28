using System;
using System.Collections.Generic;
using Math_lib;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_lib.VertexAttributes;

namespace Projection.Effects
{
    class SolidGeoEffect : Effect
    {
        public override Color GetColor(Vertex v) {
            if (v.TryGetValue<ColorVertexAttribute>(out var color)) {
                return color.Color;
            }

            throw new InvalidOperationException();
        }

        public override Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex) 
        {
            v0 = new(v0.Pos, v0.Attributes);
            v1 = new(v1.Pos, v1.Attributes);
            v2 = new(v2.Pos, v2.Attributes);

            v0.AddAttribute(new ColorVertexAttribute(triangleColors[triangleIndex / 2]));
            v1.AddAttribute(new ColorVertexAttribute(triangleColors[triangleIndex / 2]));
            v2.AddAttribute(new ColorVertexAttribute(triangleColors[triangleIndex / 2]));

            return new Triangle3D(v0,
                                  v1,
                                  v2);
        }

        public override Vertex Translate(Vertex vIn)
        {
            return new(rotation * vIn.Pos + translation, vIn.Attributes);
        }

        public void BindColors(List<Color> colors)
        {
            triangleColors = colors;
        }

        private List<Color> triangleColors;
    }
}
