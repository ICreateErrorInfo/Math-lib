using System.Collections.Generic;

namespace Math_lib
{
    public class Mesh<TVertex> where TVertex:Vertex
    {
        public Mesh()
        {

        }
        public Mesh(List<TVertex> vertsIn, List<int> indicesIn)
        {
            vertices = vertsIn;
            indices = indicesIn;
        }

        public List<TVertex> vertices = new List<TVertex>();
        public List<int> indices = new List<int>();

    }

}