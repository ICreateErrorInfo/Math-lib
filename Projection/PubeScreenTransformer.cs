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

        public Vertex Transform(Vertex p)
        {
            double zInv = 1 / p.pos.Z;

            return new Vertex( 
            new Point3D((p.pos.X * zInv + 1.0f) * xFactor,
                       (-p.pos.Y * zInv + 1.0f) * yFactor, 0));
        }
        public Vertex GetTransformed(Vertex p)
        {
            return Transform(p);
        }

        private readonly double xFactor;
        private readonly double yFactor;
    }
}
