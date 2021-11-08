using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using Math_lib;

namespace Projection
{
    class Importer
    {
        public static Mesh mesh = new Mesh();

        public static void Obj(string filename, bool isTex)
        {

            var stringList = File.ReadAllLines(filename);

            var provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            List<Point3D> v = new List<Point3D>();
            List<Point2D> vt = new List<Point2D>();

            for (int i = 0; i < stringList.Length; i++)
            {
                if (i > 1)
                {
                    string[] zeile = stringList[i].Split(' ');

                    if (zeile[0] == "v")
                    {
                        v.Add(new Point3D(Convert.ToDouble(zeile[1], provider), Convert.ToDouble(zeile[2], provider), Convert.ToDouble(zeile[3], provider)));
                    }
                    if(zeile[0] == "vt")
                    {
                        vt.Add(new Point2D(Convert.ToDouble(zeile[1], provider), Convert.ToDouble(zeile[2], provider)));
                    }
                }
            }

            if (isTex == false)
            {
                for (int i = 0; i < stringList.Length; i++)
                {
                    if (i > 1)
                    {
                        string[] zeile = stringList[i].Split(' ');

                        if (zeile[0] == "f")
                        {
                            mesh.indices.Add(Convert.ToInt32(zeile[1]) - 1);
                            mesh.indices.Add(Convert.ToInt32(zeile[2]) - 1);
                            mesh.indices.Add(Convert.ToInt32(zeile[3]) - 1);
                        }
                    }
                }
            }
            else
            {
                char[] ch = new char[] { ' ', '/' };
                int index = 0;
                for (int i = 0; i < stringList.Length; i++)
                {
                    if (i > 1)
                    {
                        string[] zeile = stringList[i].Split(ch);

                        if (zeile[0] == "f")
                        {
                            mesh.indices.Add(index);
                            mesh.vertices.Add(new Vertex(v[Convert.ToInt32(zeile[1]) - 1], vt[Convert.ToInt32(zeile[2]) - 1]));

                            mesh.indices.Add(index + 1);
                            mesh.vertices.Add(new Vertex(v[Convert.ToInt32(zeile[3]) - 1], vt[Convert.ToInt32(zeile[4]) - 1]));

                            mesh.indices.Add(index + 2);
                            mesh.vertices.Add(new Vertex(v[Convert.ToInt32(zeile[5]) - 1], vt[Convert.ToInt32(zeile[6]) - 1]));

                            index += 3;
                        }
                    }
                }
            }
        }
    }
}
