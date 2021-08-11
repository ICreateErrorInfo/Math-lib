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

        public static Mesh GetCube(double size)
        {
            double side = size / 2;

            Mesh meshCube = new Mesh();

            meshCube.vertices.Add(new Vertex(new Point3D(-side, -side, -side)));
            meshCube.vertices.Add(new Vertex(new Point3D( side, -side, -side)));
            meshCube.vertices.Add(new Vertex(new Point3D(-side,  side, -side)));
            meshCube.vertices.Add(new Vertex(new Point3D( side,  side, -side)));
            meshCube.vertices.Add(new Vertex(new Point3D(-side, -side,  side)));
            meshCube.vertices.Add(new Vertex(new Point3D( side, -side,  side)));
            meshCube.vertices.Add(new Vertex(new Point3D(-side,  side,  side)));
            meshCube.vertices.Add(new Vertex(new Point3D( side,  side,  side)));

            //Todo indices
            meshCube.indices.Add(0); meshCube.indices.Add(2); meshCube.indices.Add(1);
            meshCube.indices.Add(2); meshCube.indices.Add(3); meshCube.indices.Add(1);
            meshCube.indices.Add(4); meshCube.indices.Add(3); meshCube.indices.Add(1);

            return meshCube;
        }

    }

}