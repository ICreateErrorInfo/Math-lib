using Math_lib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    class VertexFlatEffect : Effect
    {
        //Vertex Shader
        public override Vertex Translate(Vertex vIn)
        {
            var d = diffuse * Math.Max(0, -Vector3D.Dot(rotation * vIn.n, dir));

            var c = (color * (d + ambient)).Saturate() * 255;
            return new(rotation * vIn.pos + translation, vIn.t, Color.FromArgb((int)c.X, (int)c.Y, (int)c.Z));
        }

        public void SetDiffuseLight(Vector3D c)
        {
            diffuse = c;
        }
        public void SetAmbientLight(Vector3D c)
        {
            ambient = c;
        }
        public void SetLightDir(Vector3D dl)
        {
            dir = dl.Normalize();
        }
        public void SetMaterialColor(Color c)
        {
            color = new(c.R / 255, c.G / 255, c.B / 255);
        }

        private Vector3D dir = new(0, .5, .5);
        private Vector3D diffuse = new(1, 1, 1);
        private Vector3D ambient = new(0.05, 0.05, 0.05);
        private Vector3D color = new(0.8, 0.85, 1);

        //Geo Shader
        public override Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex)
        {
            return new(v0, v1, v2);
        }


        //Pixel Shader
        public override Color GetColor(Vertex v)
        {
            return v.col;
        }

    }
}
