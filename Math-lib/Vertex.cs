using Math_lib.VertexAttributes;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Math_lib
{
    public class Vertex
    {
        public Point3D Pos { get; init; }
        public Dictionary<Type, VertexAttribute> Attributes { get; set; }

        public Vertex(double d): this(new(d), null)
        {

        }
        public Vertex(double p1, double p2, double p3) : this(new(p1, p2, p3), null)
        {

        }
        public Vertex(Point3D pos):this(pos, null)
        {

        }
        public Vertex(Point3D pos, Dictionary<Type, VertexAttribute> attributes = null)
        {
            this.Pos = pos;
            Attributes = attributes??new();
        }


        public bool TryGetValue<T>(out T value) where T : VertexAttribute 
        {
            if(Attributes.TryGetValue(typeof(T),out var v))
            {
                value = (T)v;
                return true;
            }
            value = default(T);
            return false;
        }
        public void AddAttribute(VertexAttribute vertexAttribute) 
        {
            Attributes[vertexAttribute.GetType()] = vertexAttribute;
        }

        public static Vertex operator +(Vertex v0, Vertex v1) 
        {
            Vertex vertexOut = new(v0.Pos + v1.Pos);

            foreach (var vaKey in v0.Attributes.Keys) {

                var attr0 = v0.Attributes[vaKey];
                if(v1.Attributes.TryGetValue(vaKey, out var attr1))
                {
                    var attribute = attr0.Add(attr1);
                    vertexOut.AddAttribute(attribute);
                }else
                {
                    vertexOut.AddAttribute(attr0);
                }
            }

            return vertexOut;
        }
        public static Vertex operator -(Vertex v0, Vertex v1)
        {
            Vertex vertexOut = new((Point3D)(v0.Pos - v1.Pos));

            foreach (var vaKey in v0.Attributes.Keys) {

                var attr0 = v0.Attributes[vaKey];
                if(v1.Attributes.TryGetValue(vaKey, out var attr1))
                {
                    var attribute = attr0.Sub(attr1);
                    vertexOut.AddAttribute(attribute);
                }else
                {
                    vertexOut.AddAttribute(attr0);
                }
            }

            return vertexOut;
        }
        public static Vertex operator *(Vertex v0, Vertex v1)
        {
            Vertex vertexOut = new(v0.Pos * v1.Pos);

            foreach (var vaKey in v0.Attributes.Keys) {

                var attr0 = v0.Attributes[vaKey];
                if(v1.Attributes.TryGetValue(vaKey, out var attr1))
                {
                    var attribute = attr0.Mul(attr1);
                    vertexOut.AddAttribute(attribute);
                }else
                {
                    vertexOut.AddAttribute(attr0);
                }
                
            }

            return vertexOut;
        }
        public static Vertex operator /(Vertex v0, Vertex v1)
        {
            Vertex vertexOut = new(v0.Pos / v1.Pos);

            foreach (var vaKey in v0.Attributes.Keys) {

                var attr0 = v0.Attributes[vaKey];
                if(v1.Attributes.TryGetValue(vaKey, out var attr1))
                {
                    var attribute = attr0.Div(attr1);
                    vertexOut.AddAttribute(attribute);
                }else
                {
                    vertexOut.AddAttribute(attr0);
                }
            }

            return vertexOut;
        }

        //overrides ToString
        public override string ToString()
        {
            return $"[{Pos}]";
        }        
    }
}
