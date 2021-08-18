using System.Collections.Generic;

namespace Math_lib
{
    public class Mesh
    {
        public Mesh()
        {

        }
        public Mesh(List<Vertex> vertsIn, List<int> indicesIn)
        {
            vertices = vertsIn;
            indices = indicesIn;
        }

        public List<Vertex> vertices = new List<Vertex>();
        public List<int> indices = new List<int>();
    }

}