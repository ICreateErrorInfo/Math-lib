using System;
using System.Diagnostics;
// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Math_lib
{
    public readonly struct Point3D
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }


        //Ctors
        public Point3D(double x, double y, double z)
        {
            Debug.Assert(!double.IsNaN(x) || !double.IsNaN(y) || !double.IsNaN(z));

            X = x;
            Y = y;
            Z = z;
        }
        public Point3D(double i)
        {
            Debug.Assert(!double.IsNaN(i));

            X = Y = Z = i;
        }
        public Point3D(Point3D p)
        {
            Debug.Assert(IsNaN(p));

            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }


        //Methods
        public static bool IsNaN(Point3D v)
        {
            if (!double.IsNaN(v.X) || !double.IsNaN(v.Y) || !double.IsNaN(v.Z))
            {
                return false;
                throw new ArgumentOutOfRangeException("NaN", "Number cant be NaN");
            }
            return true;
        }
        public static double Distance(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p) || IsNaN(p1));

            return (p - p1).GetLength();
        }
        public static double DistanceSqrt(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p) || IsNaN(p1));

            return (p - p1).GetLengthSqrt();
        }
        public static Point3D Lerp(double t, Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p) || IsNaN(p1) || !double.IsNaN(t));

            return (1 - t) * p + t * p1;
        }
        public static Point3D Min(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p) || IsNaN(p1));

            return new Point3D(Math.Min(p.X, p1.X),
                               Math.Min(p.Y, p1.Y),
                               Math.Min(p.Z, p1.Z));
        }
        public static Point3D Max(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p) || IsNaN(p1));

            return new Point3D(Math.Max(p.X, p1.X),
                               Math.Max(p.Y, p1.Y),
                               Math.Max(p.Z, p1.Z));
        }
        public static Point3D Floor(Point3D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point3D(Math.Floor(p.X),
                             Math.Floor(p.Y),
                             Math.Floor(p.Z));
        }
        public static Point3D Ceil(Point3D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point3D(Math.Ceiling(p.X),
                             Math.Ceiling(p.Y),
                             Math.Ceiling(p.Z));
        }
        public static Point3D Abs(Point3D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point3D(Math.Abs(p.X),
                             Math.Abs(p.Y),
                             Math.Abs(p.Z));
        }
        public static Point3D Permute(Point3D p, int x, int y, int z)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(!double.IsNaN(x));
            Debug.Assert(!double.IsNaN(y));
            Debug.Assert(!double.IsNaN(z));

            return new Point3D(p[x], p[y], p[z]);
        }
        public static explicit operator Vector3D(Point3D a)
        {
            Debug.Assert(IsNaN(a));

            return new Vector3D() { X = a.X, Y = a.Y, Z = a.Z };
        }

        //overrides +
        public static Point3D operator +(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            return new Point3D(p.X + p1.X,
                               p.Y + p1.Y,
                               p.Z + p1.Z);
        }
        public static Point3D operator +(Point3D p, double d) 
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(!double.IsNaN(d));

            return new Point3D(p.X + d,
                               p.Y + d,
                               p.Z + d);
        }
        public static Point3D operator +(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(!double.IsNaN(d));

            return new Point3D(p.X + d,
                               p.Y + d,
                               p.Z + d);
        }
        public static Point3D operator +(Point3D p, Vector3D v)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(Vector3D.IsNaN(v));

            return new(p.X + v.X,
                       p.Y + v.Y,
                       p.Z + v.Z);
        }
        public static Point3D operator +(Point3D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point3D(+p.X, +p.Y, +p.Z);
        }

        //overrides -
        public static Vector3D operator -(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            return new Vector3D(p.X - p1.X,
                                p.Y - p1.Y,
                                p.Z - p1.Z);
        }
        public static Point3D operator -(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(!double.IsNaN(d));

            return new Point3D(p.X - d,
                               p.Y - d,
                               p.Z - d);
        }
        public static Point3D operator -(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(!double.IsNaN(d));

            return new Point3D(p.X - d,
                               p.Y - d,
                               p.Z - d);
        }
        public static Point3D operator -(Point3D p, Vector3D v)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(Vector3D.IsNaN(v));

            return new(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }
        public static Point3D operator -(Point3D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point3D(-p.X, -p.Y, -p.Z);
        }

        //overrides *
        public static Point3D operator *(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            return new Point3D(p.X * p1.X,
                               p.Y * p1.Y,
                               p.Z * p1.Z);
        }
        public static Point3D operator *(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));

            return new Point3D(p.X * d,
                               p.Y * d,
                               p.Z * d);
        }
        public static Point3D operator *(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point3D(p.X * d,
                               p.Y * d,
                               p.Z * d);
        }
        public static Point3D operator *(Point3D p, Vector3D v)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(Vector3D.IsNaN(v));

            return new(p.X * v.X,
                       p.Y * v.Y,
                       p.Z * v.Z);
        }

        //overrides /
        public static Point3D operator /(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));
            Debug.Assert(p1.X != 0 || p1.Y != 0 || p1.Z != 0);

            return new Point3D(p.X / p1.X,
                               p.Y / p1.Y,
                               p.Z / p1.Z);
        }
        public static Point3D operator /(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(d != 0);

            double inv = 1 / d;

            return new Point3D(p.X * inv,
                               p.Y * inv,
                               p.Z * inv);
        }
        public static Point3D operator /(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(p.X != 0 || p.Y != 0 || p.Z != 0);

            return new Point3D(p.X / d,
                               p.Y / d,
                               p.Z / d);
        }
        public static Point3D operator /(Point3D p, Vector3D v)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(Vector3D.IsNaN(v));
            Debug.Assert(v.X != 0 || v.Y != 0 || v.Z != 0);

            return new(p.X / v.X,
                       p.Y / v.Y,
                       p.Z / v.Z);
        }

        //overrides >
        public static bool operator >(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X > p1.X && p.Y > p1.Y && p.Z > p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X > d && p.Y > d && p.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));

            if (d > p.X && d > p.Y && d > p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p !> p1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p !> d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));

            if (d !> p)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X == d && p.Y == d && p.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));

            if (d == p.X && d == p.Y && d == p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p == p1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));

            if (d == p)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X <= p1.X && p.Y <= p1.Y && p.Z <= p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X <= d && p.Y <= d && p.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));

            if (d <= p.X && d <= p.Y && d <= p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Point3D p, Point3D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p !<= p1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point3D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p !<= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Point3D p)
        {
            Debug.Assert(IsNaN(p));

            if (d !<= p)
            {
                return true;
            }
            return false;
        }

        //overrides []
        public double this[int i]
        {
            get
            {
                if (i == 0)
                {
                    Debug.Assert(!double.IsNaN(X));
                    return X;
                }
                if (i == 1)
                {
                    Debug.Assert(!double.IsNaN(Y));
                    return Y;
                }
                if (i == 2)
                {
                    Debug.Assert(!double.IsNaN(Z));
                    return Z;
                }

                throw new IndexOutOfRangeException();
            }
        }

        //overrides ToString
        public override string ToString()
        {
            return $"[{X}, {Y}, {Z}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is not Point3D other)
            {
                return false;
            }

            return this == other;
        }
        public override int GetHashCode() {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
