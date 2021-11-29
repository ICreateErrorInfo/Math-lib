using Math_lib;
using Math_lib.VertexAttributes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection.Effects
{
    class GeometryFlatEffect : Effect
    {
        //Vertex Shader
        public override Vertex Translate(Vertex vIn)
        {
            return new(rotation * vIn.Pos + translation, vIn.Attributes);
        }

        //Geometry Shader
        public override Triangle3D ProcessTri(Vertex v0, Vertex v1, Vertex v2, int triangleIndex)
        {
            var n = Vector3D.Normalize(Vector3D.Cross(v1.Pos - v0.Pos, v2.Pos - v0.Pos));
            var d = diffuse * Math.Max(0, Vector3D.Dot(-n, dir));
            var c = (color * (d + ambient)).Saturate() * 255;

            v0.AddAttribute(new ColorVertexAttribute(Color.FromArgb((int)c.X, (int)c.Y, (int)c.Z)));
            v1.AddAttribute(new ColorVertexAttribute(Color.FromArgb((int)c.X, (int)c.Y, (int)c.Z)));
            v2.AddAttribute(new ColorVertexAttribute(Color.FromArgb((int)c.X, (int)c.Y, (int)c.Z)));

            return new(v0, v1, v2);
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
            dir = Vector3D.Normalize(dl);
        }
        public void SetMaterialColor(Color c)
        {
            color = new(c.R / 255, c.G / 255, c.B / 255);
        }
        private Vector3D dir = new(0, -.5, .5);
        private Vector3D diffuse = new(1, 1, 1);
        private Vector3D ambient = new(0.05, 0.05, 0.05);
        private Vector3D color = new(0.8, 0.85, 1);

        //Pixel Shader
        public override Color GetColor(Vertex v)
        {
            if (v.TryGetValue<ColorVertexAttribute>(out var vT))
            {
                return vT.Color;
            }
            throw new InvalidOperationException();
        }
    }
}
