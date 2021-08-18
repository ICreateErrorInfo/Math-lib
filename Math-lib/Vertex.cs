﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public class Vertex
    {
        public Point3D pos;
        public Point2D t;
        public Color col;

        public Vertex(Point3D pos)
        {
            this.pos = pos;
        }
        public Vertex(Point3D pos, Point2D t)
        {
            this.pos = pos;
            this.t = t;
        }
        public Vertex(Point3D pos, Point2D t, Color col)
        {
            this.pos = pos;
            this.t = t;
            this.col = col;
        }
        public Vertex(double d)
        {
            this.pos = new Point3D(d);
            this.t = new Point2D(d);
        }


        public static Vertex operator +(Vertex v0, Vertex v1)
        {
            return new(v0.pos + v1.pos, v0.t + v1.t, v0.col);
        }
        public static Vertex operator -(Vertex v0, Vertex v1)
        {
            return new(new(v0.pos - v1.pos), new(v0.t - v1.t), v0.col);
        }
        public static Vertex operator *(Vertex v0, Vertex v1)
        {
            return new(v0.pos * v1.pos, v0.t * v1.t, v0.col);
        }
        public static Vertex operator /(Vertex v0, Vertex v1)
        {
            return new(v0.pos / v1.pos, v0.t / v1.t, v0.col);
        }

        //overrides ToString
        public override string ToString()
        {
            return $"[{pos}]";
        }        
    }
}
