using System.Collections.Generic;

namespace Math_lib
{
    public class Mesh
    {
        public Mesh()
        {
            vertices = new List<Vertex>();
            indices = new List<int>();
        }
        public Mesh(List<Vertex> vertsIn, List<int> indicesIn)
        {
            vertices = vertsIn;
            indices = indicesIn;
        }

        public readonly List<Vertex> vertices;
        public readonly List<int> indices;
    }

}