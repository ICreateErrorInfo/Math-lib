using Moarx.Math;
using Raytracing.Materials;
using Raytracing.Spectrum;
using System.Collections.Generic;

namespace Raytracing.Primitives {
    public struct TriangleMesh {
        public int NTriangles, NVertices;
        public List<int> VertexIndices;
        public List<Point3D<double>> Point;
        public List<Normal3D<double>> Normal;
        public List<Point2D<double>> UV;
        public Material Material;

        public TriangleMesh(Transform ObjectToWorld, int nTriangles, List<int> vertexIndices, int nVertices, List<Point3D<double>> points, Material material, List<Normal3D<double>> normal = null, List<Point2D<double>> uv = null) {
            NTriangles = nTriangles;
            NVertices = nVertices;
            VertexIndices = vertexIndices;
            Material = material;

            Point = new List<Point3D<double>>();
            for (var i = 0; i < nVertices; i++) {
                Point.Add(ObjectToWorld * points[i]);
            }

            UV = uv;
            Normal = null;

            if (normal != null) {
                for (var i = 0; i < nVertices; i++) {
                    Normal.Add(ObjectToWorld * Normal[i]);
                }
            }
        }

        public static TriangleMesh Import(string path) {
            return Importer.Obj(path);
        }
    }
}
