using System;
using System.Diagnostics;

namespace Math_lib
{
    public class Bounds3D
    {
        //Properties
        public Point3D pMin, pMax;


        //Ctors
        public Bounds3D()
        {
            double minNum = double.MinValue;
            double maxNum = double.MaxValue;
            pMin = new Point3D(maxNum, maxNum, maxNum);
            pMax = new Point3D(minNum, minNum, minNum);
        }
        public Bounds3D(Point3D p)
        {
            pMin = p;
            pMax = p;
        }
        public Bounds3D(Point3D p1, Point3D p2)
        {
            pMin = Point3D.Min(p1, p2);
            pMax = Point3D.Max(p1, p2);
        }


        //Mehtods
        public static bool IsNaN(Bounds3D b)
        {
            if (Point3D.IsNaN(b.pMax) || Point3D.IsNaN(b.pMin))
            {
                return false;
                throw new ArgumentOutOfRangeException("NaN", "Number cant be NaN");
            }
            return true;
        }
        public Point3D Corner(int corner)
        {
            Debug.Assert(corner >= 0 && corner < 8);
            Debug.Assert(!double.IsNaN(corner));

            return new Point3D(this[corner & 1].X,
                               this[(corner & 2) == 2 ? 1 : 0].Y,
                               this[(corner & 4) == 4 ? 1 : 0].Y);
        }
        public static  Bounds3D Union(Bounds3D b, Point3D p)
        {
            Debug.Assert(Point3D.IsNaN(p));
            Debug.Assert(IsNaN(b));

            return new Bounds3D(Point3D.Min(b.pMin, p),
                                Point3D.Max(b.pMax, p));
        }
        public static Bounds3D Union(Bounds3D b, Bounds3D b1)
        {
            Debug.Assert(IsNaN(b));
            Debug.Assert(IsNaN(b1));

            return new Bounds3D(Point3D.Min(b.pMin, b1.pMin),
                                Point3D.Max(b.pMax, b1.pMax));
        }
        public Bounds3D Intersect(Bounds3D b, Bounds3D b1)
        {
            Debug.Assert(IsNaN(b));
            Debug.Assert(IsNaN(b1));

            return new Bounds3D(Point3D.Max(b.pMin, b1.pMin),
                                Point3D.Min(b.pMax, b1.pMax));
        }
        public bool Overlaps(Bounds3D b, Bounds3D b1)
        {
            Debug.Assert(IsNaN(b));
            Debug.Assert(IsNaN(b1));

            return b.pMax >= b1.pMin && b.pMin <= b1.pMax;
        }
        public bool Inside(Point3D p, Bounds3D b)
        {
            Debug.Assert(Point3D.IsNaN(p));
            Debug.Assert(IsNaN(b));

            return p >= b.pMin && p <= b.pMax;
        }
        public bool INsideExclusive(Point3D p, Bounds3D b)
        {
            Debug.Assert(Point3D.IsNaN(p));
            Debug.Assert(IsNaN(b));

            return p >= pMin && p < b.pMax;
        }
        public Bounds3D Expand(Bounds3D b, double delta)
        {
            Debug.Assert(IsNaN(b));
            Debug.Assert(!double.IsNaN(delta));

            return new Bounds3D(b.pMin - delta,
                                b.pMax + delta);
        }
        public Vector3D Diagonal()
        {
            Debug.Assert(IsNaN(this));

            return pMax - pMin;
        }
        public double Volume()
        {
            Vector3D d = Diagonal();
            return d.X * d.Y * d.Z;
        }
        public double SurfaceArea()
        {
            Vector3D d = Diagonal();
            return d.X * d.Y * d.Z;
        }
        public int MaximumExtent()
        {
            Vector3D d = Diagonal();
            if(d.X > d.Y && d.X > d.Z)
            {
                return 0;
            }
            else if(d.Y > d.Z)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public Point3D Lerp(Point3D p)
        {
            Debug.Assert(IsNaN(this));
            Debug.Assert(Point3D.IsNaN(p));


            return new Point3D(Math1.Lerp(p.X, pMin.X, pMax.X),
                               Math1.Lerp(p.Y, pMin.Y, pMax.Y),
                               Math1.Lerp(p.Z, pMin.Z, pMax.Z));
        }
        public Vector3D Offset(Point3D p)
        {
            Debug.Assert(IsNaN(this));
            Debug.Assert(Point3D.IsNaN(p));

            Vector3D o = p - pMin;

            if (pMax.X > pMin.X) o /= new Vector3D(pMax.X - pMin.X, 1, 1);
            if (pMax.Y > pMin.Y) o /= new Vector3D(1, pMax.Y - pMin.Y, 1);
            if (pMax.Z > pMin.Z) o /= new Vector3D(1, 1, pMax.Z - pMin.Z);

            return o;
        }
        public void BoundingSphere(ref Point3D center, ref double radius)
        {
            Debug.Assert(Point3D.IsNaN(center));
            Debug.Assert(!double.IsNaN(radius));
            Debug.Assert(IsNaN(this));

            center = (pMin + pMax) / 2;
            radius = Inside(center, this) ? Point3D.Distance(center, pMax) : 0;
        }


        //Override
        public Point3D this[int i]
        {
            get
            {
                if (i == 0)
                {
                    Debug.Assert(Point3D.IsNaN(pMin));
                    return pMin;
                }
                if (i == 1)
                {
                    Debug.Assert(Point3D.IsNaN(pMax));
                    return pMax;
                }

                throw new IndexOutOfRangeException();
            }
        }
    }
}
