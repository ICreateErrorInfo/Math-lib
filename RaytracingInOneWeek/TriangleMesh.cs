using Math_lib;
using Raytracing.Materials;
using Raytracing.Spectrum;
using System.Collections.Generic;

namespace Raytracing
{
    public struct TriangleMesh
    {
        public int NTriangles, NVertices;
        public List<int> VertexIndices;
        public List<Point3D> Point;
        public List<Normal3D> Normal;
        public List<Point2D> UV;
        public Material Material;

        public TriangleMesh(Transform ObjectToWorld, int nTriangles, List<int> vertexIndices, int nVertices, List<Point3D> points, Material material, List<Normal3D> normal = null, List<Point2D> uv = null)
        {
            NTriangles = nTriangles;
            NVertices = nVertices;
            VertexIndices = vertexIndices;
            Material = material;

            Point = new List<Point3D>();
            for (int i = 0; i < nVertices; i++)
            {
                Point.Add(ObjectToWorld.m * points[i]);
            }

            UV = uv;
            Normal = null;

            if(normal != null)
            {
                for(int i = 0; i < nVertices; i++)
                {
                    Normal.Add(ObjectToWorld.m * Normal[i]);
                }
            }
        }

        public static TriangleMesh Import(SpectrumFactory factory, string path)
        {
            return Importer.Obj(factory, path);
        }
    }
}
