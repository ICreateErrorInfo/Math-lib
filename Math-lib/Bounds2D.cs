using System;
using System.Diagnostics;

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
            Debug.Assert(Point2D.IsNaN(p));

            pMin = p;
            pMax = p;
        }
        public Bounds2D(Point2D p1, Point2D p2)
        {
            Debug.Assert(Point2D.IsNaN(p1));
            Debug.Assert(Point2D.IsNaN(p2));

            pMin = Point2D.Min(p1, p2);
            pMax = Point2D.Max(p1, p2);
        }

        //Methods
        public bool IntersectP(Ray ray, ref double hitt0, ref double hitt1)
        {
            double t0 = 0;
            double t1 = ray.TMax;
            for (int i = 0; i < 2; i++)
            {
                double invRayDir = 1 / ray.D[i];
                double tNear = (pMin[i] - ray.O[i]) * invRayDir;
                double tFar = (pMax[i] - ray.O[i]) * invRayDir;
                if (tNear > tFar)
                {
                    Mathe.Swap(ref tNear, ref tFar);
                }
                t0 = tNear > t0 ? tNear : t0;
                t1 = tFar < t1 ? tFar : t1;
                if (t0 > t1) return false;
            }
            hitt0 = t0;
            hitt1 = t1;
            return true;
        }
        public bool IntersectP(Ray ray, Vector3D invDir, int[] dirIsNeg)
        {
            double tMin = (this[dirIsNeg[0]].X - ray.O.X) * invDir.X;
            double tMax = (this[1 - dirIsNeg[0]].X - ray.O.X) * invDir.X;
            double tyMin = (this[dirIsNeg[1]].Y - ray.O.Y) * invDir.Y;
            double tyMax = (this[1 - dirIsNeg[1]].Y - ray.O.Y) * invDir.Y;

            if (tMin > tyMax || tyMin > tMax) return false;
            if (tyMin > tMin) tMin = tyMin;
            if (tyMax < tMax) tMax = tyMax;

            return (tMin < ray.TMax) && (tMax > 0);
        }
        public static bool IsNaN(Bounds2D b)
        {
            if (!Point2D.IsNaN(b.pMax) || !Point2D.IsNaN(b.pMin))
            {
                return false;
            }
            return true;
        }
        public Point2D Corner(int corner)
        {
            Debug.Assert(corner >= 0 && corner < 8);
            Debug.Assert(!double.IsNaN(corner));

            return new Point2D(this[corner & 1].X,
                               this[(corner & 2) == 2 ? 1 : 0].Y);
        }
        public Vector2D Diagonal()
        {
            Debug.Assert(IsNaN(this));

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
            Debug.Assert(Point2D.IsNaN(p));
            Debug.Assert(IsNaN(this));

            return new Point2D(Mathe.Lerp(p.X, pMin.X, pMax.X),
                               Mathe.Lerp(p.Y, pMin.Y, pMax.Y));
        }                          
        public Vector2D Offset(Point2D p)
        {
            Debug.Assert(Point2D.IsNaN(p));
            Debug.Assert(IsNaN(this));

            Vector2D o = p - pMin;

            if (pMax.X > pMin.X) o /= new Vector2D(pMax.X - pMin.X, 1);
            if (pMax.Y > pMin.Y) o /= new Vector2D(1, pMax.Y - pMin.Y);

            return o;
        }
        public static Bounds2D Union(Bounds2D b, Bounds2D b2)
        {
            Debug.Assert(IsNaN(b));
            Debug.Assert(IsNaN(b2));

            return new Bounds2D(Point2D.Min(b.pMin, b2.pMin),
                                Point2D.Max(b.pMax, b2.pMax));
        }
        public static Bounds2D Union(Bounds2D b, Point2D p)
        {
            Debug.Assert(IsNaN(b));
            Debug.Assert(Point2D.IsNaN(p));

            return new Bounds2D(Point2D.Min(b.pMin, p),
                                Point2D.Max(b.pMax, p));
        }
        public static Bounds2D Intersect(Bounds2D b1, Bounds2D b2)
        {
            Debug.Assert(IsNaN(b1));
            Debug.Assert(IsNaN(b2));

            return new Bounds2D(Point2D.Max(b1.pMin, b2.pMin),
                                Point2D.Min(b1.pMax, b2.pMax));
        }
        public static bool Overlaps(Bounds2D b1, Bounds2D b2)
        {
            Debug.Assert(IsNaN(b1));
            Debug.Assert(IsNaN(b2));

            return b1.pMax >= b2.pMin && b1.pMin <= b2.pMax;
        }
        public static bool Inside(Point2D p, Bounds2D b)
        {
            Debug.Assert(Point2D.IsNaN(p));
            Debug.Assert(IsNaN(b));

            return p >= b.pMin && p <= b.pMax;
        }
        public bool InsideExclusive(Point2D p, Bounds2D b)
        {
            Debug.Assert(Point2D.IsNaN(p));
            Debug.Assert(IsNaN(b));

            return p >= pMin && p < b.pMax;
        }
        public static Bounds2D Expand(Bounds2D b, double delta)
        {
            Debug.Assert(IsNaN(b));
            Debug.Assert(!double.IsNaN(delta));

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
            Debug.Assert(Point2D.IsNaN(c));
            Debug.Assert(double.IsNaN(rad));
            Debug.Assert(IsNaN(this));

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
