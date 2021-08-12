﻿using Math_lib;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Projection
{
    class Pipeline
    {
        public Pipeline()
        {

        }
        public void Draw(Mesh triList, Effect e)
        {
            _Effect = e;
            ProcessVertices(triList.vertices, triList.indices);
        }
        public void BindRotation(Matrix4x4 rotationIn)
        {
            rotation = rotationIn;
        }
        public void BindTranslation(Vector3D translationIn)
        {
            translation = translationIn;
        }


        private void ProcessVertices(List<Vertex> vertices, List<int> indices)
        {
            List<Vertex> verticesOut = new List<Vertex>();

            foreach(var v in vertices)
            {
                //Translation
                verticesOut.Add(v.ConvertFrom(rotation * v.pos + translation));
            }

            AssembleTriangles(verticesOut, indices);
        }
        private void AssembleTriangles(List<Vertex> vertieces, List<int> indices)
        {
            for(int i = 0, end = indices.Count / 3; i < end; i++)
            {
                //Determine Triangle Vertices 
                var v0 = vertieces[indices[i * 3]];
                var v1 = vertieces[indices[i * 3 + 1]];
                var v2 = vertieces[indices[i * 3 + 2]];

                //Backface Culling
                if((Vector3D.Dot(Vector3D.Cross(v1.pos - v0.pos, v2.pos - v0.pos), new(v0.pos)) <= 0))
                {
                    //process 3 verts into a triangle
                    ProcessTriangle(v0, v1, v2);
                }
            }
        }
        private void ProcessTriangle(Vertex v0, Vertex v1, Vertex v2)
        {
            PostProcessTriangleVertices(v0, v1, v2);
        }
        private void PostProcessTriangleVertices(Vertex v0, Vertex v1, Vertex v2)
        {
            v0 =  pst.Transform(v0);
            v1 =  pst.Transform(v1);
            v2 =  pst.Transform(v2);

            DrawTriangle(v0, v1, v2);
        }
        private void DrawTriangle(Vertex v0, Vertex v1, Vertex v2)
        {
            Vertex p0 = v0;
            Vertex p1 = v1;
            Vertex p2 = v2;

            if (p1.pos.Y < p0.pos.Y) { (p0, p1) = (p1, p0); }
            if (p2.pos.Y < p1.pos.Y) { (p1, p2) = (p2, p1); }
            if (p1.pos.Y < p0.pos.Y) { (p0, p1) = (p1, p0); }

            if (p0.pos.Y == p1.pos.Y) //top flat
            {
                //sort
                if (p1.pos.X < p0.pos.X) { (p0, p1) = (p1, p0); }

                DrawFlatTopTriangle(p0, p1, p2);
            }
            else if (p1.pos.Y == p2.pos.Y) // bottom flat
            {
                //sort
                if (p2.pos.X < p1.pos.X) { (p1, p2) = (p2, p1); }
                DrawFlatBottomTriangle(p0, p1, p2);
            }
            else // general tri
            {
                //find Splitting Point
                double alphaSplit = (p1.pos.Y - p0.pos.Y) / (p2.pos.Y - p0.pos.Y);
                Vertex vi = p0 + (p2 - p0) * p0.ConvertFrom(alphaSplit);

                if (p1.pos.X < vi.pos.X)
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
            double deltaY = p2.pos.Y - p0.pos.Y;
            Vertex dit0 = (p2 - p0) / p1.ConvertFrom(deltaY);
            Vertex dit1 = (p2 - p1) / p2.ConvertFrom(deltaY);

            var itEdge1 = p1;

            DrawFlatTriangle(p0, p1, p2, dit0, dit1, itEdge1);
        }
        private void DrawFlatBottomTriangle(Vertex p0, Vertex p1, Vertex p2)
        {
            double deltaY = p2.pos.Y - p0.pos.Y;
            Vertex dit0 = (p1 - p0) / p1.ConvertFrom(deltaY);
            Vertex dit1 = (p2 - p0) / p2.ConvertFrom(deltaY);

            var itEdge1 = p0;

            DrawFlatTriangle(p0, p1, p2, dit0, dit1, itEdge1);
        }
        private void DrawFlatTriangle(Vertex it0, Vertex it1, Vertex it2, Vertex dv0, Vertex dv1, Vertex itEdge)
        {
            var itEdge0 = it0;

            int yStart = (int)Math.Ceiling(it0.pos.Y - 0.5);
            int yEnd = (int)Math.Ceiling(it2.pos.Y - 0.5);

            itEdge0 += dv0 * it0.ConvertFrom((yStart + 0.5 - it0.pos.Y));
            itEdge += dv1 * it0.ConvertFrom((yStart + 0.5 - it0.pos.Y));

            for (int y = yStart; y < yEnd; y++, itEdge0 += dv0, itEdge += dv1)
            {
                int xStart = (int)Math.Ceiling(itEdge0.pos.X - 0.5);
                int xEnd = (int)Math.Ceiling(itEdge.pos.X - 0.5);

                var iLine = itEdge0;

                double dx = itEdge.pos.X - itEdge0.pos.X;
                var diLine = (itEdge - iLine) / itEdge.ConvertFrom(dx);

                iLine += diLine * diLine.ConvertFrom(xStart + 0.5 - itEdge0.pos.X);

                for (int x = xStart; x < xEnd; x++, iLine += diLine)
                {
                    double z = 1 / iLine.pos.Z;
                    var attr = iLine * iLine.ConvertFrom(z);

                    if (zb.TestAndSet(x, y, z))
                    {
                        Bmp.SetPixel(x, y, _Effect.GetColor(attr));
                    }
                }
            }
        }

        private Effect _Effect;
        public DirectBitmap Bmp = new DirectBitmap(Options.screenWidth, Options.screenHeight);

        private ZBuffer zb = new ZBuffer(Options.screenWidth, Options.screenHeight);
        private PubeScreenTransformer pst = new PubeScreenTransformer();
        private Matrix4x4 rotation = Matrix4x4.Identity();
        private Vector3D translation = new Vector3D();
    }
}
