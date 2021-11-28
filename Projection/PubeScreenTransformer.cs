using Math_lib;

namespace Projection
{
    class PubeScreenTransformer
    {
        public PubeScreenTransformer()
        {
            _xFactor = (double)Options.ScreenWidth  / 2;
            _yFactor = (double)Options.ScreenHeight / 2;
        }

        public Vertex Transform(Vertex v) {
            Matrix projectionMatrix = Matrix.Projection(Options.ScreenWidth, Options.ScreenHeight, Options.Fov, Options.Nplane, Options.Fplane);

            Vertex v1         = new Vertex(projectionMatrix * v.Pos, v.Attributes);

            return new(new Point3D((v1.Pos.X + 1) * _xFactor, (-v1.Pos.Y + 1) * _yFactor, v1.Pos.Z), v.Attributes);
        }

        private readonly double _xFactor;
        private readonly double _yFactor;
    }
}
