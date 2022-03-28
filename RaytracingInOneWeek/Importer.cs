﻿using Math_lib;
using Raytracing.Shapes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Raytracing
{
    public class Importer
    {
        public static TriangleMesh Obj(string path)
        {
            var stringList = File.ReadAllLines(path);

            List<int> indices = new List<int>();
            List<Point3D> points = new List<Point3D>();
            int nTriangles = 0;
            int nVertices  = 0;

            var provider = new NumberFormatInfo
            {
                NumberDecimalSeparator = "."
            };

            char[] charsForSplit = new char[] { ' ', '/' };
            for (int i = 1; i < stringList.Length; i++)
            {
                string[] zeile = stringList[i].Split(charsForSplit);

                if (zeile[0] == "f")
                {
                    indices.Add(Convert.ToInt32(zeile[1]) - 1);
                    indices.Add(Convert.ToInt32(zeile[3]) - 1);
                    indices.Add(Convert.ToInt32(zeile[5]) - 1);
                    nTriangles++;
                }
                if (zeile[0] == "v")
                {
                    points.Add(new Point3D(Convert.ToDouble(zeile[1], provider), Convert.ToDouble(zeile[2], provider), Convert.ToDouble(zeile[3], provider)));
                    nVertices++;
                }
            }

            TriangleMesh mesh = new TriangleMesh(Transform.Translate(new(0)), nTriangles, indices, nVertices, points, new Metal(new Vector3D(.61, .61, .61), 0.7));

            return mesh;
        }
    }
}
