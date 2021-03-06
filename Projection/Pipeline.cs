using Math_lib;
using System;
using System.Collections.Generic;

namespace Projection
{
    class Pipeline
    {
        public Pipeline()
        {

        }
        private void Clear()
        {
            Bmp = new DirectBitmap(Options.ScreenWidth, Options.ScreenHeight);
            zb  = new ZBuffer(Options.ScreenWidth, Options.ScreenHeight);
            pst = new PubeScreenTransformer();
        }
        public void Draw(Mesh triList, Effect e)
        {
            Clear();
            _effect = e;
            ProcessVertices(triList.vertices, triList.indices);
        }


        private void ProcessVertices(List<Vertex> vertices, List<int> indices)
        {
            List<Vertex> verticesOut = new List<Vertex>();

            foreach (var v in vertices)
            {
                //Translation
                verticesOut.Add(_effect.Translate(v));
            }

            AssembleTriangles(verticesOut, indices);
        }
        private void AssembleTriangles(List<Vertex> vertieces, List<int> indices)
        {
            for (int i = 0, end = indices.Count / 3; i < end; i++)
            {
                //Determine Triangle Vertices 
                var v0 = vertieces[indices[i * 3]];
                var v1 = vertieces[indices[i * 3 + 1]];
                var v2 = vertieces[indices[i * 3 + 2]];

                //Backface Culling
                if ((Vector3D.Dot(Vector3D.Cross(v1.Pos - v0.Pos, v2.Pos - v0.Pos), (Vector3D)v0.Pos) < 0))
                {
                    ProcessTriangle(new(v0, v1, v2), i);
                }
            }
        }
        private void ProcessTriangle(Triangle3D tri, int triangleIndex)
        {
            PostProcessTriangleVertices(_effect.ProcessTri(tri.Points[0], tri.Points[1], tri.Points[2], triangleIndex));
        }
        private void PostProcessTriangleVertices(Triangle3D triangle)
        {
            var v0 = pst.Transform(triangle.Points[0]);
            var v1 = pst.Transform(triangle.Points[1]);
            var v2 = pst.Transform(triangle.Points[2]);

            DrawTriangle(v0, v1, v2);
        }
        private void DrawTriangle(Vertex p0, Vertex p1, Vertex p2)
        {
            if (p1.Pos.Y < p0.Pos.Y) { (p0, p1) = (p1, p0); }
            if (p2.Pos.Y < p1.Pos.Y) { (p1, p2) = (p2, p1); }
            if (p1.Pos.Y < p0.Pos.Y) { (p0, p1) = (p1, p0); }

            if (p0.Pos.Y == p1.Pos.Y) //top flat
            {
                //sort
                if (p1.Pos.X < p0.Pos.X) { (p0, p1) = (p1, p0); }

                DrawFlatTopTriangle(p0, p1, p2);
            }
            else if (p1.Pos.Y == p2.Pos.Y) // bottom flat
            {
                //sort
                if (p2.Pos.X < p1.Pos.X) { (p1, p2) = (p2, p1); }
                DrawFlatBottomTriangle(p0, p1, p2);
            }
            else // general tri
            {
                //find Splitting Point
                double alphaSplit = (p1.Pos.Y - p0.Pos.Y) / (p2.Pos.Y - p0.Pos.Y);
                Vertex vi = p0 + (p2 - p0) * alphaSplit;

                if (p1.Pos.X < vi.Pos.X)
                {
                    DrawFlatBottomTriangle(p0, p1, vi);
                    DrawFlatTopTriangle(p1, vi, p2);
                }
                else
                {
                    DrawFlatBottomTriangle(p0, vi, p1);
                    DrawFlatTopTriangle(vi, p1, p2);
                }
            }
        }
        private void DrawFlatTopTriangle(Vertex p0, Vertex p1, Vertex p2)
        {
            double deltaY = p2.Pos.Y - p0.Pos.Y;
            Vertex dit0 = (p2 - p0) / deltaY;
            Vertex dit1 = (p2 - p1) / deltaY;

            var itEdge1 = p1;

            DrawFlatTriangle(p0, p1, p2, dit0, dit1, itEdge1);
        }
        private void DrawFlatBottomTriangle(Vertex p0, Vertex p1, Vertex p2)
        {
            double deltaY = p2.Pos.Y - p0.Pos.Y;
            Vertex dit0 = (p1 - p0) / deltaY;
            Vertex dit1 = (p2 - p0) / deltaY;

            var itEdge1 = p0;

            DrawFlatTriangle(p0, p1, p2, dit0, dit1, itEdge1);
        }
        private void DrawFlatTriangle(Vertex it0, Vertex it1, Vertex it2, Vertex dv0, Vertex dv1, Vertex itEdge1)
        {
            var itEdge0 = it0;

            int yStart = (int)Math.Ceiling(it0.Pos.Y - 0.5);
            int yEnd = (int)Math.Ceiling(it2.Pos.Y - 0.5);

            itEdge0 += dv0 * (yStart + 0.5 - it0.Pos.Y);
            itEdge1 += dv1 * (yStart + 0.5 - it0.Pos.Y);

            for (int y = yStart; y < yEnd; y++, itEdge0 += dv0, itEdge1 += dv1)
            {
                int xStart = (int)Math.Ceiling(itEdge0.Pos.X - 0.5);
                int xEnd = (int)Math.Ceiling(itEdge1.Pos.X - 0.5);

                var iLine = itEdge0;

                double dx = itEdge1.Pos.X - itEdge0.Pos.X;
                var diLine = (itEdge1 - iLine) / dx;

                iLine += diLine * (xStart + 0.5 - itEdge0.Pos.X);

                for (int x = xStart; x < xEnd; x++, iLine += diLine)
                {
                    double z = 1 / iLine.Pos.Z;

                    if (zb.TestAndSet(x, y, z))
                    {
                        var attr = iLine * z;

                        Bmp.SetPixel(x, y, _effect.GetColor(attr));
                    }
                }
            }
        }

        private Effect _effect;
        public DirectBitmap Bmp;

        private ZBuffer zb;
        private PubeScreenTransformer pst;
    }
}
