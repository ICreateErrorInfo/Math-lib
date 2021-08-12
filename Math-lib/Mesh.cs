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


        public static Mesh GetCube(double size = 1)
        {
            double side = size / 2;
            Mesh cube = new Mesh();

            cube.vertices.Add(new TexVertex(new Point3D(-side, -side, -side), ConvertTexCoord(1, 0)));
            cube.vertices.Add(new TexVertex(new Point3D( side, -side, -side), ConvertTexCoord(0, 0)));

            cube.vertices.Add(new TexVertex(new Point3D(-side,  side, -side), ConvertTexCoord(1, 1)));
            cube.vertices.Add(new TexVertex(new Point3D( side,  side, -side), ConvertTexCoord(0, 1)));

            cube.vertices.Add(new TexVertex(new Point3D(-side, -side,  side), ConvertTexCoord(1, 3)));
            cube.vertices.Add(new TexVertex(new Point3D( side, -side,  side), ConvertTexCoord(0, 3)));

            cube.vertices.Add(new TexVertex(new Point3D(-side,  side,  side), ConvertTexCoord(1, 2)));
            cube.vertices.Add(new TexVertex(new Point3D( side,  side,  side), ConvertTexCoord(0, 2)));

            cube.vertices.Add(new TexVertex(new Point3D(-side, -side, -side), ConvertTexCoord(1, 4)));
            cube.vertices.Add(new TexVertex(new Point3D( side, -side, -side), ConvertTexCoord(0, 4)));

            cube.vertices.Add(new TexVertex(new Point3D(-side, -side, -side), ConvertTexCoord(2, 1)));
            cube.vertices.Add(new TexVertex(new Point3D(-side, -side,  side), ConvertTexCoord(2, 2)));

            cube.vertices.Add(new TexVertex(new Point3D( side, -side, -side), ConvertTexCoord(-1, 1)));
            cube.vertices.Add(new TexVertex(new Point3D( side, -side,  side), ConvertTexCoord(-1, 2)));


            List<int> ind = new List<int>{
                0,2,1,   2,3,1,
                4,8,5,   5,8,9,
                2,6,3,   3,6,7,
                4,5,7,   4,7,6,
                2,10,11, 2,11,6,
                12,3,7,  12,7,13
            };

            cube.indices = ind;

            return cube;
        }

        private static Vector2D ConvertTexCoord(double u, double v)
        {
            return new Vector2D((u + 1) / 3, v / 4);
        }
    }

}