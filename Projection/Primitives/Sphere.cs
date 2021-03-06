using Math_lib;
using Math_lib.VertexAttributes;
using System;

namespace Projection
{
    class Sphere
    {
        public static Mesh GetPlain(double radius = 1, int latDiv = 12, int longDiv = 24)
        {
            Mesh sphere = new Mesh();

            Point3D baseP = new Point3D(0, 0, radius);
            double lattitudeAngle = Math.PI / latDiv;
            double longitudeAngle = 2 * Math.PI / longDiv;

            for (int iLat = 1; iLat < latDiv; iLat++)
            {
                var latBase = Matrix.RotateXMarix(lattitudeAngle * iLat) * baseP;
                for (int iLong = 0; iLong < longDiv; iLong++)
                {
                    sphere.vertices.Add(new Vertex(Matrix.RotateZMarix(longitudeAngle * iLong) * latBase));
                }
            }

            var iNorthPole = sphere.vertices.Count;
            sphere.vertices.Add(new Vertex(baseP));
            var iSouthPole = sphere.vertices.Count;
            sphere.vertices.Add(new Vertex(-baseP));

            for(int iLat = 0; iLat < latDiv - 2; iLat++)
            {
                for(int iLong = 0; iLong < longDiv - 1; iLong++)
                {
                    sphere.indices.Add(calcIdx(iLat + 1, iLong + 1, longDiv));
                    sphere.indices.Add(calcIdx(iLat + 1, iLong    , longDiv));
                    sphere.indices.Add(calcIdx(iLat    , iLong + 1, longDiv));
                    sphere.indices.Add(calcIdx(iLat    , iLong + 1, longDiv));
                    sphere.indices.Add(calcIdx(iLat + 1, iLong    , longDiv));
                    sphere.indices.Add(calcIdx(iLat    , iLong    , longDiv));
                }

                sphere.indices.Add(calcIdx(iLat + 1, 0          , longDiv));
                sphere.indices.Add(calcIdx(iLat + 1, longDiv - 1, longDiv));
                sphere.indices.Add(calcIdx(iLat    , 0          , longDiv));
                sphere.indices.Add(calcIdx(iLat    , 0          , longDiv));
                sphere.indices.Add(calcIdx(iLat + 1, longDiv - 1, longDiv));
                sphere.indices.Add(calcIdx(iLat    , longDiv - 1, longDiv));
            }

            for(int iLong = 0; iLong < longDiv - 1; iLong++)
            {
                sphere.indices.Add(calcIdx(0, iLong + 1, longDiv));
                sphere.indices.Add(calcIdx(0, iLong, longDiv));
                sphere.indices.Add(iNorthPole);

                sphere.indices.Add(iSouthPole);
                sphere.indices.Add(calcIdx(latDiv - 2, iLong, longDiv));
                sphere.indices.Add(calcIdx(latDiv - 2, iLong + 1, longDiv));
            }

            sphere.indices.Add(calcIdx(0, 0, longDiv));
            sphere.indices.Add(calcIdx(0, longDiv - 1, longDiv));
            sphere.indices.Add(iNorthPole);

            sphere.indices.Add(iSouthPole);
            sphere.indices.Add(calcIdx(latDiv - 2, longDiv - 1, longDiv));
            sphere.indices.Add(calcIdx(latDiv - 2, 0, longDiv));

            return sphere;
        }

        public static Mesh GetPlainNormals(double radius = 1, int latDiv = 12, int longDiv = 24)
        {
            var sphere = GetPlain(radius, latDiv, longDiv);
            foreach(var element in sphere.vertices)
            {
                element.AddAttribute(new NormalVertexAttribute(Vector3D.Normalize(((Vector3D)element.Pos))));
            }
            return sphere;
        }

        private static int calcIdx(int iLat, int iLong, int longDiv)
        {
            return iLat * longDiv + iLong;
        }
    }
}
