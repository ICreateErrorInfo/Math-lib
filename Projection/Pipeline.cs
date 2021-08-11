using Math_lib;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Projection
{

    class Pipeline<TEffect, TVertex> where TEffect:Effect, new() where TVertex:Vertex
    {
        public Pipeline()
        {
            _effect = new TEffect();
        }
        public Effect Effect => _effect;
        public void Draw(Mesh<TVertex> triList)
        {
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


        private void ProcessVertices(List<TVertex> vertices, List<int> indices)
        {
            List<Vertex> verticesOut = new List<Vertex>();

            foreach(var v in vertices)
            {
                //Translation
                verticesOut.Add(Effect.CreateVertex(rotation * v.pos + translation));
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
                    //Illumination
                    Vector3D lightDirection = new Vector3D(0,0,1);
                    lightDirection = Vector3D.UnitVector(lightDirection);

                    double dp = Vector3D.Dot(Vector3D.UnitVector(Vector3D.Cross(v1.pos - v0.pos, v2.pos - v0.pos)), lightDirection);
                    var grayValue = Convert.ToByte(Math.Abs(dp * Byte.MaxValue));
                    c = System.Drawing.Color.FromArgb(255, grayValue, grayValue, grayValue);

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
                Vertex vi = p0 + (p2 - p0) * Effect.CreateVertex(new Point3D(alphaSplit));

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
            Point3D dit0 = new((p2.pos - p0.pos) / deltaY);
            Point3D dit1 = new((p2.pos - p1.pos) / deltaY);

            var itEdge1 = p1;

            DrawFlatTriangle(p0, p1, p2, dit0, dit1, itEdge1);
        }
        private void DrawFlatBottomTriangle(Vertex p0, Vertex p1, Vertex p2)
        {
            double deltaY = p2.pos.Y - p0.pos.Y;
            Point3D dit0 = new((p1.pos - p0.pos) / deltaY);
            Point3D dit1 = new((p2.pos - p0.pos) / deltaY);

            var itEdge1 = p0;

            DrawFlatTriangle(p0, p1, p2, dit0, dit1, itEdge1);
        }
        private void DrawFlatTriangle(Vertex it0, Vertex it1, Vertex it2, Point3D dv0, Point3D dv1, Vertex itEdge)
        {
            var itEdge0 = it0;

            int yStart = (int)Math.Ceiling(it0.pos.Y - 0.5);
            int yEnd = (int)Math.Ceiling(it2.pos.Y - 0.5);

            itEdge0 += Effect.CreateVertex(dv0 * (double)(yStart + 0.5 - it0.pos.Y));
            itEdge += Effect.CreateVertex(dv1 * (double)(yStart + 0.5 - it0.pos.Y));

            for (int y = yStart; y < yEnd; y++, itEdge0.pos += dv0, itEdge.pos += dv1)
            {
                int xStart = (int)Math.Ceiling(itEdge0.pos.X - 0.5);
                int xEnd = (int)Math.Ceiling(itEdge.pos.X - 0.5);

                var iLine = itEdge0;

                double dx = itEdge.pos.X - itEdge0.pos.X;
                var diLine = (itEdge.pos - iLine.pos) / dx;

                iLine += Effect.CreateVertex(diLine * (double)(xStart + 0.5 - itEdge0.pos.X));

                for (int x = xStart; x < xEnd; x++, iLine.pos += diLine)
                {
                    double z = 1 / iLine.pos.Z;

                    if (zb.TestAndSet(x, y, z))
                    {
                        Bmp.SetPixel(x, y, _effect.PixelShader(iLine));
                    }
                }
            }
        }

        public Color c;
        public DirectBitmap Bmp = new DirectBitmap(Options.screenWidth, Options.screenHeight);

        Effect _effect;
        private ZBuffer zb = new ZBuffer(Options.screenWidth, Options.screenHeight);
        private PubeScreenTransformer pst = new PubeScreenTransformer();
        private Matrix4x4 rotation = Matrix4x4.Identity();
        private Vector3D translation = new Vector3D();
    }
}
