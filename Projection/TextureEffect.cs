using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Math_lib;

namespace Projection
{
    public class TextureEffect : Effect
    {
        public class VertexFactory : Math_lib.IFactory<Vertex>
        {
            public Vertex CreateVertex(Point3D p)
            {
                return new Vertex(p);
            }

            public Vertex CreateVertex(Vector3D p)
            {
                return new Vertex(new(p));
            }
        }
        public class Vertex : Math_lib.Vertex
        {
            public Vector2D t;


            public Vertex(Point3D pos) : base(pos)
            {
                t = new Vector2D();
            }
            public Vertex(Point3D pos, Vector2D t) : base(pos)
            {
                this.t = t;
            }


            public static Vertex operator +(Vertex v0, Vertex v1)
            {
                return new(v0.pos + v1.pos, v0.t + v1.t);
            }
            public static Vertex operator -(Vertex v0, Vertex v1)
            {
                return new(new(v0.pos - v1.pos), v0.t - v1.t);
            }
            public static Vertex operator *(Vertex v0, Vertex v1)
            {
                return new(v0.pos * v1.pos, v0.t * v1.t);
            }
            public static Vertex operator /(Vertex v0, Vertex v1)
            {
                return new(v0.pos / v1.pos, v0.t / v1.t);
            }


            //overrides ToString
            public override string ToString()
            {
                return $"[{pos} , {t}]";
            }

            protected override Math_lib.Vertex Minus(Math_lib.Vertex v1)
            {
                throw new NotImplementedException();
            }

            protected override Math_lib.Vertex Plus(Math_lib.Vertex v1)
            {
                throw new NotImplementedException();
            }

            protected override Math_lib.Vertex Mul(Math_lib.Vertex v1)
            {
                throw new NotImplementedException();
            }

            protected override Math_lib.Vertex Div(Math_lib.Vertex v1)
            {
                throw new NotImplementedException();
            }
        }


        public void BindTexture()
        {

        }

        public override Color PixelShader(Math_lib.Vertex iLine)
        {
            throw new NotImplementedException();
        }
    }
}
