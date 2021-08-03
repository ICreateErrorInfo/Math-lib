using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Math_lib;

namespace Projection
{
    class Importer
    {
        readonly IReadOnlyList<string> _stringList;

        public Importer(IReadOnlyList<String> stringList, IReadOnlyList<Vector> verts)
        {
            _stringList = stringList;
            Verts = verts;
        }

        public IReadOnlyList<Vector> Verts { get; }

        public static Importer Obj(string filename)
        {

            var stringList = File.ReadAllLines(filename);
            var verts = new List<Vector>();

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
                        verts.Add(new Vector(Convert.ToDouble(zeile[1], provider), Convert.ToDouble(zeile[2], provider), Convert.ToDouble(zeile[3], provider)));
                    }
                }
            }

            return new Importer(stringList, verts);
        }

        public Mesh CreateMesh()
        {
            var triangles = new Mesh();
            for (int i = 0; i < _stringList.Count; i++)
            {
                if (i > 1)
                {
                    string[] zeile = _stringList[i].Split(' ');

                    if (zeile[0] == "f")
                    {
                        triangles.Triangles.Add(new Triangle(new(Verts[Convert.ToInt32(zeile[1]) - 1]), new(Verts[Convert.ToInt32(zeile[2]) - 1]), new(Verts[Convert.ToInt32(zeile[3]) - 1])));
                    }
                }
            }

            return triangles;
        }
    }
}
