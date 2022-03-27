using Math_lib;
using System;
using System.Collections.Generic;

namespace Raytracing.Shapes
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
    }
    public class Triangle : Shape
    {
        private readonly TriangleMesh _mesh;
        private readonly int _firstVertexIndex;
        private readonly Transform _worldToObject;
        private readonly Transform _objectToWorld;

        public Triangle(Transform objectToWorld, Transform worldToObject, TriangleMesh mesh, int triangleNumber)
        {
            _objectToWorld = objectToWorld;
            _worldToObject = worldToObject;
            _mesh = mesh;

            _firstVertexIndex = mesh.VertexIndices[3 * triangleNumber];
        }

        public override bool BoundingBox(double time0, double time1, ref Bounds3D bound)
        {
            Point3D p0 = _worldToObject.m * _mesh.Point[_firstVertexIndex];
            Point3D p1 = _worldToObject.m * _mesh.Point[_firstVertexIndex + 1];
            Point3D p2 = _worldToObject.m * _mesh.Point[_firstVertexIndex + 2];

            bound = Bounds3D.Union(new Bounds3D(p0, p1), p2);
            return true;
        }

        public override bool Intersect(Ray ray, double tMin, double tMax, out SurfaceInteraction isect)
        {
            isect = new SurfaceInteraction();

            //Get Triangle Points
            Point3D p0 = _mesh.Point[_firstVertexIndex];
            Point3D p1 = _mesh.Point[_firstVertexIndex + 1];
            Point3D p2 = _mesh.Point[_firstVertexIndex + 2];

            //Translate
            Point3D p0t = p0 - (Vector3D)ray.O;
            Point3D p1t = p1 - (Vector3D)ray.O;
            Point3D p2t = p2 - (Vector3D)ray.O;

            //Permute
            int kz = Vector3D.MaxDimension(Vector3D.Abs(ray.D));
            int kx = kz + 1; if (kx == 3) kx = 0;
            int ky = kx + 1; if (ky == 3) ky = 0;
            Vector3D d = Vector3D.Permute(ray.D, kx, ky, kz);
            p0t = Point3D.Permute(p0t, kx, ky, kz);
            p1t = Point3D.Permute(p1t, kx, ky, kz);
            p2t = Point3D.Permute(p2t, kx, ky, kz);

            //Shear
            double sx = -d.X / d.Z;
            double sy = -d.Y / d.Z;
            double sz = 1.0  / d.Z;
            p0t += new Point3D(sx * p0t.Z,
                               sy * p0t.Z, 0);
            p1t += new Point3D(sx * p1t.Z,
                               sy * p1t.Z, 0);
            p2t += new Point3D(sx * p2t.Z,
                               sy * p2t.Z, 0);

            //edge function coefficients
            double e0 = p1t.X * p2t.Y - p1t.Y * p2t.X;
            double e1 = p2t.X * p0t.Y - p2t.Y * p0t.X;
            double e2 = p0t.X * p1t.Y - p0t.Y * p1t.X;

            if((e0 < 0 || e1 < 0 || e2 < 0) && (e0 > 0 || e1 > 0 || e2 > 0))
            {
                return false;
            }
            double det = e0 + e1 + e2;
            if(det == 0)
            {
                return false;
            }

            p0t *= new Point3D(1, 1, sz);
            p1t *= new Point3D(1, 1, sz);
            p2t *= new Point3D(1, 1, sz);
            double tScaled = e0 * p0t.Z + e1 * p1t.Z + e2 * p2t.Z;
            if(det < 0 && (tScaled >= 0 || tScaled < ray.TMax * det))
            {
                return false;
            }
            else if (det > 0 && (tScaled <= 0 || tScaled > ray.TMax * det))
            {
                return false;
            }

            double invDet = 1 / det;
            double b0 = e0 * invDet;
            double b1 = e1 * invDet;
            double b2 = e2 * invDet;
            double t = tScaled * invDet;

            isect.T = t;
            isect.P = b0 * p0 + b1 * p1 + b2 * p2;
            Normal3D outwardNormal = (Normal3D)Vector3D.Normalize(Vector3D.Cross(p1 - p0, p2 - p0));
            isect.SetFaceNormal(ray, outwardNormal);
            isect.Material = _mesh.Material;

            return true;
        }
    }
}
