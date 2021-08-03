using System.Collections.Generic;

namespace Projection
{
    class Mesh
    {

        public Mesh() {
            Triangles = new();
        }

        public List<Triangle3> Triangles { get; }

    }

}