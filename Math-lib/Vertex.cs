using Math_lib.VertexAttributes;
using System;
using System.Collections.Generic;

namespace Math_lib
{
    public readonly struct Vertex
    {
        public Point3D Pos { get; init; }
        public Dictionary<Type, VertexAttribute> Attributes { get; }


        public Vertex(Point3D pos) : this(pos, null)
        {

        }
        public Vertex(Point3D pos, Dictionary<Type, VertexAttribute> attributes = null)
        {
            this.Pos = pos;
            Attributes = attributes ?? new();
        }


        public bool TryGetValue<T>(out T value) where T : VertexAttribute
        {
            if (Attributes.TryGetValue(typeof(T), out var v))
            {
                value = (T)v;
                return true;
            }
            value = default;
            return false;
        }
        public void AddAttribute(VertexAttribute vertexAttribute)
        {
            Attributes[vertexAttribute.GetType()] = vertexAttribute;
        }
        public Vertex GetAddAttribute(VertexAttribute vertexAttribute)
        {
            Attributes[vertexAttribute.GetType()] = vertexAttribute;
            return this;
        }

        public static Vertex operator +(Vertex v0, Vertex v1)
        {
            Vertex vertexOut = new(v0.Pos + v1.Pos);

            var attrv0 = v0.Attributes;
            var attrv1 = v1.Attributes;

            foreach (var vaKey in attrv0.Keys)
            {
                vertexOut.AddAttribute(attrv0[vaKey].Add(attrv1[vaKey]));
            }

            return vertexOut;
        }
        public static Vertex operator -(Vertex v0, Vertex v1)
        {
            Vertex vertexOut = new((Point3D)(v0.Pos - v1.Pos));

            var attrv0 = v0.Attributes;
            var attrv1 = v1.Attributes;

            foreach (var vaKey in attrv0.Keys)
            {
                vertexOut.AddAttribute(attrv0[vaKey].Sub(attrv1[vaKey]));
            }

            return vertexOut;
        }
        public static Vertex operator *(Vertex v0, Vertex v1)
        {
            Vertex vertexOut = new(v0.Pos * v1.Pos);

            var attrv0 = v0.Attributes;
            var attrv1 = v1.Attributes;

            foreach (var vaKey in attrv0.Keys)
            {
                vertexOut.AddAttribute(attrv0[vaKey].Mul(attrv1[vaKey]));
            }

            return vertexOut;
        }
        public static Vertex operator /(Vertex v0, Vertex v1)
        {
            Vertex vertexOut = new(v0.Pos / v1.Pos);

            var attrv0 = v0.Attributes;
            var attrv1 = v1.Attributes;

            foreach (var vaKey in attrv0.Keys)
            {
                vertexOut.AddAttribute(attrv0[vaKey].Div(attrv1[vaKey]));
            }

            return vertexOut;
        }

        public static Vertex operator +(Vertex v0, double v1)
        {
            Vertex vertexOut = new(v0.Pos + v1);

            foreach (var vaVal in v0.Attributes.Values)
            {
                var attribute = vaVal.AddDouble(v1);
                vertexOut.AddAttribute(attribute);
            }

            return vertexOut;
        }
        public static Vertex operator -(Vertex v0, double v1)
        {
            Vertex vertexOut = new(v0.Pos - v1);

            foreach (var vaVal in v0.Attributes.Values)
            {
                var attribute = vaVal.SubDouble(v1);
                vertexOut.AddAttribute(attribute);
            }

            return vertexOut;
        }
        public static Vertex operator *(Vertex v0, double v1)
        {
            Vertex vertexOut = new(v0.Pos * v1);

            foreach (var vaVal in v0.Attributes.Values)
            {
                var attribute = vaVal.MulDouble(v1);
                vertexOut.AddAttribute(attribute);
            }

            return vertexOut;
        }
        public static Vertex operator /(Vertex v0, double v1)
        {
            Vertex vertexOut = new(v0.Pos / v1);

            foreach (var vaVal in v0.Attributes.Values)
            {
                var attribute = vaVal.DivDouble(v1);
                vertexOut.AddAttribute(attribute);
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
