﻿using Moarx.Math;
using Raytracing.Mathmatic;
using Raytracing.Primitives;

namespace Raytracing.Shapes {
    public class Triangle : Shape
    {
        private readonly TriangleMesh _mesh;
        private readonly int _firstVertexIndex;

        public Triangle(Transform objectToWorld, Transform worldToObject, TriangleMesh mesh, int triangleNumber)
        {
            ObjectToWorld = objectToWorld;
            WorldToObject = worldToObject;
            _mesh = mesh;

            _firstVertexIndex = 3 * triangleNumber;
        }

        public override bool Intersect(Ray ray, out double tMax, out SurfaceInteraction interaction)
        {
            tMax = 0;
            interaction = new SurfaceInteraction();

            //Get Triangle Points
           Point3D<double> p0 = _mesh.Point[_mesh.VertexIndices[_firstVertexIndex]];
           Point3D<double> p1 = _mesh.Point[_mesh.VertexIndices[_firstVertexIndex + 1]];
           Point3D<double> p2 = _mesh.Point[_mesh.VertexIndices[_firstVertexIndex + 2]];

            //Translate
           Point3D<double> p0t = p0 - ray.Origin.ToVector();
           Point3D<double> p1t = p1 - ray.Origin.ToVector();
           Point3D<double> p2t = p2 - ray.Origin.ToVector();

            //Permute
            int kz = Vector3D<double>.MaxDimension(Vector3D<double>.Abs(ray.Direction));
            int kx = kz + 1; if (kx == 3) kx = 0;
            int ky = kx + 1; if (ky == 3) ky = 0;
            Vector3D<double> d = Vector3D<double>.Permute(ray.Direction, kx, ky, kz);
            p0t = Point3D<double>.Permute(p0t, kx, ky, kz);
            p1t = Point3D<double>.Permute(p1t, kx, ky, kz);
            p2t = Point3D<double>.Permute(p2t, kx, ky, kz);

            //Shear
            double sx = -d.X / d.Z;
            double sy = -d.Y / d.Z;
            double sz = 1.0 / d.Z;
            p0t += new Point3D<double>(sx * p0t.Z,
                               sy * p0t.Z, 0);
            p1t += new Point3D<double>(sx * p1t.Z,
                               sy * p1t.Z, 0);
            p2t += new Point3D<double>(sx * p2t.Z,
                               sy * p2t.Z, 0);

            //edge function coefficients
            double e0 = p1t.X * p2t.Y - p1t.Y * p2t.X;
            double e1 = p2t.X * p0t.Y - p2t.Y * p0t.X;
            double e2 = p0t.X * p1t.Y - p0t.Y * p1t.X;

            if ((e0 < 0 || e1 < 0 || e2 < 0) && (e0 > 0 || e1 > 0 || e2 > 0))
            {
                return false;
            }
            double det = e0 + e1 + e2;
            if (det == 0)
            {
                return false;
            }

            p0t *= new Point3D<double>(1, 1, sz);
            p1t *= new Point3D<double>(1, 1, sz);
            p2t *= new Point3D<double>(1, 1, sz);
            double tScaled = e0 * p0t.Z + e1 * p1t.Z + e2 * p2t.Z;
            if (det < 0 && (tScaled >= 0 || tScaled < ray.TMax * det))
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

            tMax = t;
            interaction.P = b0 * p0 + b1 * p1 + b2 * p2;
            Normal3D<double> outwardNormal = new((Vector3D<double>.CrossProduct(p1 - p0, p2 - p0)).Normalize());
            interaction.SetFaceNormal(ray, outwardNormal);

            return true;
        }
        public override Bounds3D<double> GetObjectBound()
        {
           Point3D<double> p0 = WorldToObject * _mesh.Point[_mesh.VertexIndices[_firstVertexIndex]];
           Point3D<double> p1 = WorldToObject * _mesh.Point[_mesh.VertexIndices[_firstVertexIndex + 1]];
           Point3D<double> p2 = WorldToObject * _mesh.Point[_mesh.VertexIndices[_firstVertexIndex + 2]];

            return Bounds3D<double>.Union(new Bounds3D<double>(p0, p1), p2);
        }

        public double Area()
        {
           Point3D<double> p0 = _mesh.Point[_mesh.VertexIndices[_firstVertexIndex]];
           Point3D<double> p1 = _mesh.Point[_mesh.VertexIndices[_firstVertexIndex + 1]];
           Point3D<double> p2 = _mesh.Point[_mesh.VertexIndices[_firstVertexIndex + 2]];

            return 0.5 * Vector3D<double>.CrossProduct(p1 - p0, p2 - p0).GetLength();
        }
    }
}