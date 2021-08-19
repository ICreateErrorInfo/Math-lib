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
            Mesh cube = new Mesh();

            cube.vertices.Add(new Vertex(new Point3D(-side, -side, -side), ConvertTexCoord(1, 0)));
            cube.vertices.Add(new Vertex(new Point3D(side, -side, -side), ConvertTexCoord(0, 0)));

            cube.vertices.Add(new Vertex(new Point3D(-side, side, -side), ConvertTexCoord(1, 1)));
            cube.vertices.Add(new Vertex(new Point3D(side, side, -side), ConvertTexCoord(0, 1)));

            cube.vertices.Add(new Vertex(new Point3D(-side, -side, side), ConvertTexCoord(1, 3)));
            cube.vertices.Add(new Vertex(new Point3D(side, -side, side), ConvertTexCoord(0, 3)));

            cube.vertices.Add(new Vertex(new Point3D(-side, side, side), ConvertTexCoord(1, 2)));
            cube.vertices.Add(new Vertex(new Point3D(side, side, side), ConvertTexCoord(0, 2)));

            cube.vertices.Add(new Vertex(new Point3D(-side, -side, -side), ConvertTexCoord(1, 4)));
            cube.vertices.Add(new Vertex(new Point3D(side, -side, -side), ConvertTexCoord(0, 4)));

            cube.vertices.Add(new Vertex(new Point3D(-side, -side, -side), ConvertTexCoord(2, 1)));
            cube.vertices.Add(new Vertex(new Point3D(-side, -side, side), ConvertTexCoord(2, 2)));

            cube.vertices.Add(new Vertex(new Point3D(side, -side, -side), ConvertTexCoord(-1, 1)));
            cube.vertices.Add(new Vertex(new Point3D(side, -side, side), ConvertTexCoord(-1, 2)));


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
        private static Point2D ConvertTexCoord(double u, double v)
        {
            return new Point2D((u + 1) / 3, v / 4);
        }

        public static Mesh GetPlainIndependentFaces(double size = 1)
        {
            double side = size / 2;

            Mesh cube = new Mesh();

            cube.vertices.Add(new Vertex(-side, -side, -side)); // 0 near side
            cube.vertices.Add(new Vertex(side, -side, -side)); // 1
            cube.vertices.Add(new Vertex(-side, side, -side)); // 2
            cube.vertices.Add(new Vertex(side, side, -side)); // 3
            cube.vertices.Add(new Vertex(-side, -side, side)); // 4 far side
            cube.vertices.Add(new Vertex(side, -side, side)); // 5
            cube.vertices.Add(new Vertex(-side, side, side)); // 6
            cube.vertices.Add(new Vertex(side, side, side)); // 7
            cube.vertices.Add(new Vertex(-side, -side, -side)); // 8 left side
            cube.vertices.Add(new Vertex(-side, side, -side)); // 9
            cube.vertices.Add(new Vertex(-side, -side, side)); // 10
            cube.vertices.Add(new Vertex(-side, side, side)); // 11
            cube.vertices.Add(new Vertex(side, -side, -side)); // 12 right side
            cube.vertices.Add(new Vertex(side, side, -side)); // 13
            cube.vertices.Add(new Vertex(side, -side, side)); // 14
            cube.vertices.Add(new Vertex(side, side, side)); // 15
            cube.vertices.Add(new Vertex(-side, -side, -side)); // 16 bottom side
            cube.vertices.Add(new Vertex(side, -side, -side)); // 17
            cube.vertices.Add(new Vertex(-side, -side, side)); // 18
            cube.vertices.Add(new Vertex(side, -side, side)); // 19
            cube.vertices.Add(new Vertex(-side, side, -side)); // 20 top side
            cube.vertices.Add(new Vertex(side, side, -side)); // 21
            cube.vertices.Add(new Vertex(-side, side, side)); // 22
            cube.vertices.Add(new Vertex(side, side, side)); // 23

            List<int> ind = new List<int>{
                0,2, 1,    2,3,1,
                4,5, 7,    4,7,6,
                8,10, 9,  10,11,9,
                12,13,15, 12,15,14,
                16,17,18, 18,17,19,
                20,23,21, 20,22,23
            };

            cube.indices = ind;

            return cube;
        }

        public static Mesh GetIndependentFacesNormals(double size = 1)
        {
            Mesh cube = GetPlainIndependentFaces(size);


            cube.vertices[0].n  = new(0.0f, 0.0f, -1.0f);
            cube.vertices[1].n  = new(0.0f, 0.0f, -1.0f);
            cube.vertices[2].n  = new(0.0f, 0.0f, -1.0f);
            cube.vertices[3].n  = new(0.0f, 0.0f, -1.0f);

            cube.vertices[4].n  = new(0.0f, 0.0f, 1.0f);
            cube.vertices[5].n  = new(0.0f, 0.0f, 1.0f);
            cube.vertices[6].n  = new(0.0f, 0.0f, 1.0f);
            cube.vertices[7].n  = new(0.0f, 0.0f, 1.0f);

            cube.vertices[8].n  = new(-1.0f, 0.0f, 0.0f);
            cube.vertices[9].n  = new(-1.0f, 0.0f, 0.0f);
            cube.vertices[10].n = new(-1.0f, 0.0f, 0.0f);
            cube.vertices[11].n = new(-1.0f, 0.0f, 0.0f);

            cube.vertices[12].n = new(1.0f, 0.0f, 0.0f);
            cube.vertices[13].n = new(1.0f, 0.0f, 0.0f);
            cube.vertices[14].n = new(1.0f, 0.0f, 0.0f);
            cube.vertices[15].n = new(1.0f, 0.0f, 0.0f);

            cube.vertices[16].n = new(0.0f, -1.0f, 0.0f);
            cube.vertices[17].n = new(0.0f, -1.0f, 0.0f);
            cube.vertices[18].n = new(0.0f, -1.0f, 0.0f);
            cube.vertices[19].n = new(0.0f, -1.0f, 0.0f);

            cube.vertices[20].n = new(0.0f, 1.0f, 0.0f);
            cube.vertices[21].n = new(0.0f, 1.0f, 0.0f);
            cube.vertices[22].n = new(0.0f, 1.0f, 0.0f);
            cube.vertices[23].n = new(0.0f, 1.0f, 0.0f);

            return cube;
        }
    }
}
