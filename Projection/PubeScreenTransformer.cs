using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    class PubeScreenTransformer
    {
        public PubeScreenTransformer()
        {
            xFactor = Options.screenWidth / 2;
            yFactor = Options.screenHeight / 2;
        }

        public Vertex Transform(Vertex v)
        {
            double zInv = 1 / v.pos.Z;

            v = v * new Vertex(zInv);

            v.pos = new Point3D((v.pos.X + 1) * xFactor, (-v.pos.Y + 1) * yFactor, zInv);

            return v;
        }
        public Vertex GetTransformed(Vertex p)
        {
            return Transform(p);
        }

        private readonly double xFactor;
        private readonly double yFactor;
    }
}
