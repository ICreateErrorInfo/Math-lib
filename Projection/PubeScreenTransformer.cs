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
            Vertex v1 = new Vertex(Matrix4x4.Projection(Options.screenWidth, Options.screenHeight, Options.Fov, 0.1, 1000) * v.pos);

            return new(new Point3D((v1.pos.X + 1) * xFactor, (-v1.pos.Y + 1) * yFactor, v1.pos.Z), v.t, v.col, v.n);
        }
        public Vertex GetTransformed(Vertex p)
        {
            return Transform(p);
        }

        private readonly double xFactor;
        private readonly double yFactor;
    }
}
