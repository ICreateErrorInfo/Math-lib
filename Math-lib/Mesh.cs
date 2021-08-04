using System.Collections.Generic;

namespace Math_lib
{
    public class Mesh
    {

        public Mesh() {
            Triangles = new();
        }

        public List<Triangle3> Triangles { get; }

    }

}