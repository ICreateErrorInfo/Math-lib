using Math_lib;

using System.Collections.Generic;
using System.Linq;

namespace Projection
{
    class Plane
    {
        public static Mesh GetPlain(int divisions = 7, double size = 1)
        {
            int nVerticesSide = divisions + 1;

            Vertex[] vertices = new Vertex[(nVerticesSide) * (nVerticesSide)];

            double side = size / 2;
            double divisionSize = size / divisions;
            Point3D bottomLeft = new Point3D(-side, -side, 0);

            for(int y = 0, i = 0; y < nVerticesSide; y++)
            {
                double yPos = (double)y * divisionSize;
                for (int x = 0; x < nVerticesSide; x++, i++)
                {
                    vertices[i] = new(bottomLeft + new Point3D(x * divisionSize, yPos, 0));
                }
            }


            List<int> indices = new List<int>();
            for(int y = 0; y < divisions; y++)
            {
                for(int x = 0; x < divisions; x++)
                {
                    int[] indexArray = new int[] { vxy2i(x,y, nVerticesSide), vxy2i(x + 1, y, nVerticesSide), vxy2i(x, y + 1, nVerticesSide), vxy2i(x + 1, y + 1, nVerticesSide) };
                    indices.Add(indexArray[0]);
                    indices.Add(indexArray[2]);
                    indices.Add(indexArray[1]);
                    indices.Add(indexArray[1]);
                    indices.Add(indexArray[2]);
                    indices.Add(indexArray[3]);
                }
            }

            return new Mesh(vertices.ToList<Vertex>(), indices);

        }
        private static int vxy2i(int x, int y, int nVerticesSide)
        {
            return y * nVerticesSide + x;
        }

        public static Mesh GetSkinned(int divisions = 7, double size = 1)
        {
            Mesh itList = GetPlain(divisions, size);
            int nVerticesSide = divisions + 1;
            double tDivisionSize = (double)1 / divisions;
            Point2D tBottomLeft = new Point2D(0, 1);

            for(int y = 0, i = 0; y < nVerticesSide; y++)
            {
                double yT = -y * tDivisionSize;
                for(int x = 0; x < nVerticesSide; x++, i++)
                {
                    //Todo
                    //itList.vertices[i].t = tBottomLeft + new Point2D(x * tDivisionSize, yT);
                }
            }

            return itList;
        }
    }
}
