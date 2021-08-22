using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    class Cube
    {
        public static Mesh GetPlain(double size = 1)
        {
            double side = size / 2;

            List<Vertex> vertices = new List<Vertex>();

            vertices.Add(new Vertex(new Point3D(-side, -side, -side), ConvertTexCoord(1, 0)));
            vertices.Add(new Vertex(new Point3D(side, -side, -side), ConvertTexCoord(0, 0)));

            vertices.Add(new Vertex(new Point3D(-side, side, -side), ConvertTexCoord(1, 1)));
            vertices.Add(new Vertex(new Point3D(side, side, -side), ConvertTexCoord(0, 1)));

            vertices.Add(new Vertex(new Point3D(-side, -side, side), ConvertTexCoord(1, 3)));
            vertices.Add(new Vertex(new Point3D(side, -side, side), ConvertTexCoord(0, 3)));

            vertices.Add(new Vertex(new Point3D(-side, side, side), ConvertTexCoord(1, 2)));
            vertices.Add(new Vertex(new Point3D(side, side, side), ConvertTexCoord(0, 2)));

            vertices.Add(new Vertex(new Point3D(-side, -side, -side), ConvertTexCoord(1, 4)));
            vertices.Add(new Vertex(new Point3D(side, -side, -side), ConvertTexCoord(0, 4)));

            vertices.Add(new Vertex(new Point3D(-side, -side, -side), ConvertTexCoord(2, 1)));
            vertices.Add(new Vertex(new Point3D(-side, -side, side), ConvertTexCoord(2, 2)));

            vertices.Add(new Vertex(new Point3D(side, -side, -side), ConvertTexCoord(-1, 1)));
            vertices.Add(new Vertex(new Point3D(side, -side, side), ConvertTexCoord(-1, 2)));


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

        public static Mesh GetPlainIndependentFaces(double size = 1)
        {
            double side = size / 2;

            List<Vertex> vertices = new List<Vertex>();

            vertices.Add(new Vertex(-side, -side, -side)); // 0 near side
            vertices.Add(new Vertex(side, -side, -side)); // 1
            vertices.Add(new Vertex(-side, side, -side)); // 2
            vertices.Add(new Vertex(side, side, -side)); // 3
            vertices.Add(new Vertex(-side, -side, side)); // 4 far side
            vertices.Add(new Vertex(side, -side, side)); // 5
            vertices.Add(new Vertex(-side, side, side)); // 6
            vertices.Add(new Vertex(side, side, side)); // 7
            vertices.Add(new Vertex(-side, -side, -side)); // 8 left side
            vertices.Add(new Vertex(-side, side, -side)); // 9
            vertices.Add(new Vertex(-side, -side, side)); // 10
            vertices.Add(new Vertex(-side, side, side)); // 11
            vertices.Add(new Vertex(side, -side, -side)); // 12 right side
            vertices.Add(new Vertex(side, side, -side)); // 13
            vertices.Add(new Vertex(side, -side, side)); // 14
            vertices.Add(new Vertex(side, side, side)); // 15
            vertices.Add(new Vertex(-side, -side, -side)); // 16 bottom side
            vertices.Add(new Vertex(side, -side, -side)); // 17
            vertices.Add(new Vertex(-side, -side, side)); // 18
            vertices.Add(new Vertex(side, -side, side)); // 19
            vertices.Add(new Vertex(-side, side, -side)); // 20 top side
            vertices.Add(new Vertex(side, side, -side)); // 21
            vertices.Add(new Vertex(-side, side, side)); // 22
            vertices.Add(new Vertex(side, side, side)); // 23

            List<int> ind = new List<int>{
                0,2, 1,    2,3,1,
                4,5, 7,    4,7,6,
                8,10, 9,  10,11,9,
                12,13,15, 12,15,14,
                16,17,18, 18,17,19,
                20,23,21, 20,22,23
            };

            Mesh cube= new Mesh(vertices, ind);

            return cube;
        }

        public static Mesh GetIndependentFacesNormals(double size = 1)
        {
            Mesh cube = GetPlainIndependentFaces(size);

            List<Vertex> vertices = new List<Vertex>();

            vertices.Add(new Vertex(cube.vertices[0].pos, cube.vertices[0].t, cube.vertices[0].col, new Vector3D(0.0f, 0.0f, -1.0f)));
            vertices.Add(new Vertex(cube.vertices[1].pos, cube.vertices[1].t, cube.vertices[1].col, new Vector3D(0.0f, 0.0f, -1.0f)));
            vertices.Add(new Vertex(cube.vertices[2].pos, cube.vertices[2].t, cube.vertices[2].col, new Vector3D(0.0f, 0.0f, -1.0f)));
            vertices.Add(new Vertex(cube.vertices[3].pos, cube.vertices[3].t, cube.vertices[3].col, new Vector3D(0.0f, 0.0f, -1.0f)));

            vertices.Add(new Vertex(cube.vertices[4].pos, cube.vertices[4].t, cube.vertices[4].col, new Vector3D(0.0f, 0.0f, 1.0f)));
            vertices.Add(new Vertex(cube.vertices[5].pos, cube.vertices[5].t, cube.vertices[5].col, new Vector3D(0.0f, 0.0f, 1.0f)));
            vertices.Add(new Vertex(cube.vertices[6].pos, cube.vertices[6].t, cube.vertices[6].col, new Vector3D(0.0f, 0.0f, 1.0f)));
            vertices.Add(new Vertex(cube.vertices[7].pos, cube.vertices[7].t, cube.vertices[7].col, new Vector3D(0.0f, 0.0f, 1.0f)));

            vertices.Add(new Vertex(cube.vertices[8 ].pos, cube.vertices[8 ].t, cube.vertices[8 ].col, new Vector3D(-1.0f, 0.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[9 ].pos, cube.vertices[9 ].t, cube.vertices[9 ].col, new Vector3D(-1.0f, 0.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[10].pos, cube.vertices[10].t, cube.vertices[10].col, new Vector3D(-1.0f, 0.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[11].pos, cube.vertices[11].t, cube.vertices[11].col, new Vector3D(-1.0f, 0.0f, 0.0f)));

            vertices.Add(new Vertex(cube.vertices[12].pos, cube.vertices[12].t, cube.vertices[12].col, new Vector3D(1.0f, 0.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[13].pos, cube.vertices[13].t, cube.vertices[13].col, new Vector3D(1.0f, 0.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[14].pos, cube.vertices[14].t, cube.vertices[14].col, new Vector3D(1.0f, 0.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[15].pos, cube.vertices[15].t, cube.vertices[15].col, new Vector3D(1.0f, 0.0f, 0.0f)));

            vertices.Add(new Vertex(cube.vertices[16].pos, cube.vertices[16].t, cube.vertices[16].col, new Vector3D(0.0f, -1.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[17].pos, cube.vertices[17].t, cube.vertices[17].col, new Vector3D(0.0f, -1.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[18].pos, cube.vertices[18].t, cube.vertices[18].col, new Vector3D(0.0f, -1.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[19].pos, cube.vertices[19].t, cube.vertices[19].col, new Vector3D(0.0f, -1.0f, 0.0f)));

            vertices.Add(new Vertex(cube.vertices[20].pos, cube.vertices[20].t, cube.vertices[20].col, new Vector3D(0.0f, 1.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[21].pos, cube.vertices[21].t, cube.vertices[21].col, new Vector3D(0.0f, 1.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[22].pos, cube.vertices[22].t, cube.vertices[22].col, new Vector3D(0.0f, 1.0f, 0.0f)));
            vertices.Add(new Vertex(cube.vertices[23].pos, cube.vertices[23].t, cube.vertices[23].col, new Vector3D(0.0f, 1.0f, 0.0f)));

            Mesh cube2 = new Mesh(vertices, cube.indices);
            return cube2;
        }
    }
}
