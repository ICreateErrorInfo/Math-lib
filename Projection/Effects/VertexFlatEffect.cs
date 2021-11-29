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
    internal class VertexFlatEffect : Effect
    {
        //Vertex Shader
        public override Vertex Translate(Vertex vIn)
        {
            if (vIn.TryGetValue<NormalVertexAttribute>(out var vT))
            {
                var d = diffuse * Math.Max(0, -Vector3D.Dot((Vector3D)(rotation * vT.N), dir));

                var c = (color * (d + ambient)).Saturate() * 255;
                return new Vertex(rotation * vIn.Pos + translation, vIn.Attributes).GetAddAttribute(new ColorVertexAttribute(Color.FromArgb((int)c.X, (int)c.Y, (int)c.Z)));
            }
            throw new InvalidOperationException();
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

        private Vector3D dir = new(0, 0, 1);
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
            if (v.TryGetValue<ColorVertexAttribute>(out var vT))
            {
                return vT.Color;
            }
            throw new InvalidOperationException();
        }
    }
}
