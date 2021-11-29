using Math_lib;
using Math_lib.VertexAttributes;
using System.Collections.Generic;

namespace Projection.Primitives
{
    class Cube
    {
        public static Mesh GetPlain(double size = 1)
        {
            double side = size / 2;

            List<Vertex> vertices = new List<Vertex>
            {
                new Vertex(new Point3D(-side,-side,-side)),
                new Vertex(new Point3D( side,-side,-side)),

                new Vertex(new Point3D(-side,side,-side)),
                new Vertex(new Point3D( side,side,-side)),

                new Vertex(new Point3D(-side,-side,side)),
                new Vertex(new Point3D( side,-side,side)),

                new Vertex(new Point3D(-side,side,side)),
                new Vertex(new Point3D( side,side,side)),
            };

            List<int> ind = new List<int>{
                0,2,1, 2,3,1,
                1,3,5, 3,7,5,
                2,6,3, 3,6,7,
                4,5,7, 4,7,6,
                0,4,2, 2,4,6,
                0,1,4, 1,5,4
            };

            Mesh cube = new Mesh(vertices, ind);

            return cube;
        }
        public static Mesh GetPlainIndependentFaces(double size = 1)
        {
            double side = size / 2;

            List<Vertex> vertices = new List<Vertex>();

            vertices.Add(new Vertex(new(-side, -side, -side))); // 0 near side
            vertices.Add(new Vertex(new( side, -side, -side))); // 1
            vertices.Add(new Vertex(new(-side,  side, -side))); // 2
            vertices.Add(new Vertex(new( side,  side, -side))); // 3
            vertices.Add(new Vertex(new(-side, -side,  side))); // 4 far side
            vertices.Add(new Vertex(new( side, -side,  side))); // 5
            vertices.Add(new Vertex(new(-side,  side,  side))); // 6
            vertices.Add(new Vertex(new( side,  side,  side))); // 7
            vertices.Add(new Vertex(new(-side, -side, -side))); // 8 left side
            vertices.Add(new Vertex(new(-side,  side, -side))); // 9
            vertices.Add(new Vertex(new(-side, -side,  side))); // 10
            vertices.Add(new Vertex(new(-side,  side,  side))); // 11
            vertices.Add(new Vertex(new( side, -side, -side))); // 12 right side
            vertices.Add(new Vertex(new( side,  side, -side))); // 13
            vertices.Add(new Vertex(new( side, -side,  side))); // 14
            vertices.Add(new Vertex(new( side,  side,  side))); // 15
            vertices.Add(new Vertex(new(-side, -side, -side))); // 16 bottom side
            vertices.Add(new Vertex(new( side, -side, -side))); // 17
            vertices.Add(new Vertex(new(-side, -side,  side))); // 18
            vertices.Add(new Vertex(new( side, -side,  side))); // 19
            vertices.Add(new Vertex(new(-side,  side, -side))); // 20 top side
            vertices.Add(new Vertex(new( side,  side, -side))); // 21
            vertices.Add(new Vertex(new(-side,  side,  side))); // 22
            vertices.Add(new Vertex(new( side,  side,  side))); // 23

            List<int> ind = new List<int>{
                0,2, 1,    2,3,1,
                4,5, 7,    4,7,6,
                8,10, 9,  10,11,9,
                12,13,15, 12,15,14,
                16,17,18, 18,17,19,
                20,23,21, 20,22,23
            };

            Mesh cube = new Mesh(vertices, ind);

            return cube;
        }
        public static Mesh GetIndependentFacesNormals(double size = 1)
        {
            Mesh cube = GetPlainIndependentFaces(size);

           cube.vertices[0] .AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 0.0f, -1.0f)));
           cube.vertices[1] .AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 0.0f, -1.0f)));
           cube.vertices[2] .AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 0.0f, -1.0f)));
           cube.vertices[3] .AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 0.0f, -1.0f)));
                                        
           cube.vertices[4] .AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 0.0f, 1.0f)));
           cube.vertices[5] .AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 0.0f, 1.0f)));
           cube.vertices[6] .AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 0.0f, 1.0f)));
           cube.vertices[7] .AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 0.0f, 1.0f)));
                                         
           cube.vertices[8] .AddAttribute(new NormalVertexAttribute(new Vector3D(-1.0f, 0.0f, 0.0f)));
           cube.vertices[9] .AddAttribute(new NormalVertexAttribute(new Vector3D(-1.0f, 0.0f, 0.0f)));
           cube.vertices[10].AddAttribute(new NormalVertexAttribute(new Vector3D(-1.0f, 0.0f, 0.0f)));
           cube.vertices[11].AddAttribute(new NormalVertexAttribute(new Vector3D(-1.0f, 0.0f, 0.0f)));
                                      
           cube.vertices[12].AddAttribute(new NormalVertexAttribute(new Vector3D(1.0f, 0.0f, 0.0f)));
           cube.vertices[13].AddAttribute(new NormalVertexAttribute(new Vector3D(1.0f, 0.0f, 0.0f)));
           cube.vertices[14].AddAttribute(new NormalVertexAttribute(new Vector3D(1.0f, 0.0f, 0.0f)));
           cube.vertices[15].AddAttribute(new NormalVertexAttribute(new Vector3D(1.0f, 0.0f, 0.0f)));
                                      
           cube.vertices[16].AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, -1.0f, 0.0f)));
           cube.vertices[17].AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, -1.0f, 0.0f)));
           cube.vertices[18].AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, -1.0f, 0.0f)));
           cube.vertices[19].AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, -1.0f, 0.0f)));

           cube.vertices[20].AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 1.0f, 0.0f)));
           cube.vertices[21].AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 1.0f, 0.0f)));
           cube.vertices[22].AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 1.0f, 0.0f)));
           cube.vertices[23].AddAttribute(new NormalVertexAttribute(new Vector3D(0.0f, 1.0f, 0.0f)));

            return cube;
        }
        public static Mesh GetSkinned(double size = 1)
        {
            double side = size / 2;

            List<Vertex> vertices = new List<Vertex>
            {
                new Vertex(new Point3D(-side,-side,-side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(1,0))),
                new Vertex(new Point3D( side,-side,-side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(0,0))),
                                                                                                               
                new Vertex(new Point3D(-side, side,-side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(1,1))),
                new Vertex(new Point3D( side, side,-side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(0,1))),

                new Vertex(new Point3D(-side,-side, side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(1,3))),
                new Vertex(new Point3D( side,-side, side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(0,3))),

                new Vertex(new Point3D(-side, side, side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(1,2))),
                new Vertex(new Point3D( side, side, side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(0,2))),

                new Vertex(new Point3D(-side,-side,-side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(1,4))),
                new Vertex(new Point3D( side,-side,-side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(0,4))),

                new Vertex(new Point3D(-side,-side,-side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(2,1))),
                new Vertex(new Point3D(-side,-side, side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(2,2))),

                new Vertex(new Point3D( side,-side,-side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(-1,1))),
                new Vertex(new Point3D( side,-side, side)).GetAddAttribute(new TextureCoordinateVertexAttribute(ConvertTexCoord(-1,2))),
            };

            List<int> ind = new List<int>{
                0,2,1,   2,3,1,
                4,8,5,   5,8,9,
                2,6,3,   3,6,7,
                4,5,7,   4,7,6,
                2,10,11, 2,11,6,
                12,3,7,  12,7,13
            };

            Mesh cube = new Mesh(vertices, ind);

            return cube;
        }

        private static Point2D ConvertTexCoord(double u, double v)
        {
            return new Point2D((u + 1) / 3, v / 4);
        }
    }
}
