﻿using System.Diagnostics;

namespace Math_lib
{
    public class Bounds2D
    {
        //Properties
        public Point2D pMin, pMax;


        //Ctors
        public Bounds2D()
        {
            double minNum = double.MinValue;
            double maxNum = double.MaxValue;
            pMin = new Point2D(maxNum, maxNum);
            pMax = new Point2D(minNum, minNum);
        }
        public Bounds2D(Point2D p)
        {
            pMin = p;
            pMax = p;
        }
        public Bounds2D(Point2D p1, Point2D p2)
        {
            pMin = Point2D.Min(p1, p2);
            pMax = Point2D.Max(p1, p2);
        }

        //Methods
        public Point2D Corner(int corner)
        {
            Debug.Assert(corner >= 0 && corner < 8);

            return new Point2D(this[corner & 1].X,
                               this[(corner & 2) == 2 ? 1 : 0].Y);
        }
        public Vector2D Diagonal()
        {
            return pMax - pMin;
        }
        public int MaximumExtent()
        {
            Vector2D d = Diagonal();
            if (d.X > d.Y)
                return 0;
            else
                return 1;
        }
        public Point2D Lerp(Point2D p)
        {
            return new Point2D(Math1.Lerp(p.X, pMin.X, pMax.X),
                               Math1.Lerp(p.Y, pMin.Y, pMax.Y));
        }                          
        public Vector2D Offset(Point2D p)
        {
            Vector2D o = p - pMin;

            if (pMax.X > pMin.X) o /= new Vector2D(pMax.X - pMin.X, 1);
            if (pMax.Y > pMin.Y) o /= new Vector2D(1, pMax.Y - pMin.Y);

            return o;
        }
        public Bounds2D Union(Bounds2D b, Bounds2D b2)
        {
            return new Bounds2D(Point2D.Min(b.pMin, b2.pMin),
                                Point2D.Max(b.pMax, b2.pMax));
        }
        public Bounds2D Union(Bounds2D b, Point2D p)
        {
            return new Bounds2D(Point2D.Min(b.pMin, p),
                                Point2D.Max(b.pMax, p));
        }
        public Bounds2D Intersect(Bounds2D b1, Bounds2D b2)
        {
            return new Bounds2D(Point2D.Max(b1.pMin, b2.pMin),
                                Point2D.Min(b1.pMax, b2.pMax));
        }
        public bool Overlaps(Bounds2D b1, Bounds2D b2)
        {
            return b1.pMax >= b2.pMin && b1.pMin <= b2.pMax;
        }
        public bool Inside(Point2D p, Bounds2D b)
        {
            return p >= b.pMin && p <= b.pMax;
        }
        public bool INsideExclusive(Point2D p, Bounds2D b)
        {
            return p >= pMin && p < b.pMax;
        }
        public Bounds2D Expand(Bounds2D b, double delta)
        {
            return new Bounds2D(b.pMin - delta,
                                b.pMax + delta);
        }
        public double Volume()
        {
            Vector2D d = Diagonal();
            return d.X * d.Y;
        }
        public double SurfaceArea()
        {
            Vector2D d = Diagonal();
            return d.X * d.Y;
        }
        public void BoundingSphere(ref Point2D c, ref double rad)
        {
            c = (pMin + pMax) / 2;
            rad = Inside(c, this) ? Point2D.Distance(c, pMax) : 0;
        }


        //overrides
        public Point2D this[int i]
        {
            get
            {
                return (i == 0) ? pMin : pMax;
            }
        }
    }
}