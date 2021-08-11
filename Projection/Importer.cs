using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Math_lib;

namespace Projection
{
    class Importer<TVertex, TVertexFactory> where TVertex : Vertex where TVertexFactory : IFactory<TVertex>, new()
    {
        public Mesh<TVertex> mesh = new Mesh<TVertex>();

        public void Obj(string filename)
        {
            var factory = new TVertexFactory();
            var stringList = File.ReadAllLines(filename);

            var provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            for (int i = 0; i < stringList.Length; i++)
            {
                if (i > 1)
                {
                    string[] zeile = stringList[i].Split(' ');

                    if (zeile[0] == "v")
                    {
                        mesh.vertices.Add(factory.CreateVertex(new Point3D(Convert.ToDouble(zeile[1], provider), Convert.ToDouble(zeile[2], provider), Convert.ToDouble(zeile[3], provider))));
                    }
                    if(zeile[0] == "f")
                    {
                        mesh.indices.Add(Convert.ToInt32(zeile[1]) - 1);
                        mesh.indices.Add(Convert.ToInt32(zeile[2]) - 1);
                        mesh.indices.Add(Convert.ToInt32(zeile[3]) - 1);
                    }
                }
            }
        }
    }
}
