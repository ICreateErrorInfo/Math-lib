using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;
using Raytracing.Materials;
using Raytracing.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Raytracing {
    public class Importer
    {
        public static TriangleMesh Obj(string path, RGBColorSpace colorSpace)
        {
            var stringList = File.ReadAllLines(path);

            List<int> indices = new List<int>();
            List<Point3D<double>> points = new List<Point3D<double>>();
            int nTriangles = 0;
            int nVertices  = 0;

            var provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            for (int i = 1; i < stringList.Length; i++)
            {
                string[] zeile = stringList[i].Split(' ');

                if (zeile[0] == "f")
                {
                    if (zeile[1].Contains('/'))
                    {
                        string[] splittedWithSlash = stringList[i].Split(new char[] {' ', '/'});
                        indices.Add(Convert.ToInt32(splittedWithSlash[1]) - 1);
                        indices.Add(Convert.ToInt32(splittedWithSlash[3]) - 1);
                        indices.Add(Convert.ToInt32(splittedWithSlash[5]) - 1);
                    }
                    else
                    {
                        indices.Add(Convert.ToInt32(zeile[1]) - 1);
                        indices.Add(Convert.ToInt32(zeile[2]) - 1);
                        indices.Add(Convert.ToInt32(zeile[3]) - 1);
                    }
                    nTriangles++;
                }
                if (zeile[0] == "v")
                {
                    points.Add(new Point3D<double>(Convert.ToDouble(zeile[1], provider), Convert.ToDouble(zeile[2], provider), Convert.ToDouble(zeile[3], provider)));
                    nVertices++;
                }
            }
            
            TriangleMesh mesh = new TriangleMesh(Transform.Translate(new(0)), nTriangles, indices, nVertices, points, new Metal(new RGBAlbedoSpectrum(colorSpace, new(.61, .61, .61)), 0.7, colorSpace));

            return mesh;
        }
    }
}
