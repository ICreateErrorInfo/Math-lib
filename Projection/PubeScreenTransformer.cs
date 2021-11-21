using Math_lib;

namespace Projection
{
    class PubeScreenTransformer
    {
        public PubeScreenTransformer()
        {
            xFactor = Options.ScreenWidth / 2;
            yFactor = Options.ScreenHeight / 2;
        }

        public Vertex Transform(Vertex v)
        {
            Vertex v1 = new Vertex(Matrix.Projection(Options.ScreenWidth, Options.ScreenHeight, Options.Fov, Options.Nplane, Options.Fplane) * v.pos);

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
