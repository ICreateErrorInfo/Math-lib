using System;
// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Math_lib
{
    public readonly struct Point3D
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }


        //Constructors
        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point3D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point3D(double i)
        {
            X = Y = Z = i;
        }
        public Point3D(Point3D p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }


        //Methods
        public static double Distance(Point3D p, Point3D p1)
        {
            return new Vector3D(p - p1).GetLength();
        }
        public static double DistanceSqrt(Point3D p, Point3D p1)
        {
            return new Vector3D(p - p1).GetLengthSqrt();
        }
        public static Point3D Lerp(double t, Point3D p, Point3D p1)
        {
            return (1 - t) * p + t * p1;
        }
        public static Point3D Min(Point3D p, Point3D p1)
        {
            return new Point3D(Math.Min(p.X, p1.X),
                             Math.Min(p.Y, p1.Y),
                             Math.Min(p.Z, p1.Z));
        }
        public static Point3D Max(Point3D p, Point3D p1)
        {
            return new Point3D(Math.Max(p.X, p1.X),
                             Math.Max(p.Y, p1.Y),
                             Math.Max(p.Z, p1.Z));
        }
        public static Point3D Floor(Point3D p)
        {
            return new Point3D(Math.Floor(p.X),
                             Math.Floor(p.Y),
                             Math.Floor(p.Z));
        }
        public static Point3D Ceil(Point3D p)
        {
            return new Point3D(Math.Ceiling(p.X),
                             Math.Ceiling(p.Y),
                             Math.Ceiling(p.Z));
        }
        public static Point3D Abs(Point3D p)
        {
            return new Point3D(Math.Abs(p.X),
                             Math.Abs(p.Y),
                             Math.Abs(p.Z));
        }
        public static Point3D Permute(Point3D p, int x, int y, int z)
        {
            return new Point3D(p[x], p[y], p[z]);
        }
        public Vector3D ToVec()
        {
            return new Vector3D(X, Y, Z);
        }

        //overrides +
        public static Point3D operator +(Point3D p, Point3D p1)
        {
            return new Point3D(p.X + p1.X,
                             p.Y + p1.Y,
                             p.Z + p1.Z);
        }
        public static Point3D operator +(Point3D p, double p1)
        {
            return new Point3D(p.X + p1,
                             p.Y + p1,
                             p.Z + p1);
        }
        public static Point3D operator +(double d, Point3D p)
        {
            return new Point3D(p.X + d,
                             p.Y + d,
                             p.Z + d);
        }
        public static Point3D operator +(Point3D p, Vector3D v)
        {
            return new(p.X + v.X, p.Y + v.Y, p.Z + v.Z);
        }
        public static Point3D operator +(Point3D p)
        {
            return new Point3D(+p.X, +p.Y, +p.Z);
        }

        //overrides -
        public static Vector3D operator -(Point3D p, Point3D p1)
        {
            return new Vector3D(p.X - p1.X,
                              p.Y - p1.Y,
                              p.Z - p1.Z);
        }
        public static Point3D operator -(Point3D p, double d)
        {
            return new Point3D(p.X - d,
                             p.Y - d,
                             p.Z - d);
        }
        public static Point3D operator -(double d, Point3D p)
        {
            return new Point3D(p.X - d,
                             p.Y - d,
                             p.Z - d);
        }
        public static Point3D operator -(Point3D p, Vector3D v)
        {
            return new(p.X - v.X, p.Y - v.Y, p.Z - v.Z);
        }
        public static Point3D operator -(Point3D p)
        {
            return new Point3D(-p.X, -p.Y, -p.Z);
        }

        //overrides *
        public static Point3D operator *(Point3D p, Point3D p1)
        {
            return new Point3D(p.X * p1.X,
                             p.Y * p1.Y,
                             p.Z * p1.Z);
        }
        public static Point3D operator *(Point3D p, double d)
        {
            return new Point3D(p.X * d,
                             p.Y * d,
                             p.Z * d);
        }
        public static Point3D operator *(double d, Point3D p)
        {
            return new Point3D(p.X * d,
                             p.Y * d,
                             p.Z * d);
        }
        public static Point3D operator *(Point3D p, Vector3D v)
        {
            return new(p.X * v.X, p.Y * v.Y, p.Z * v.Z);
        }

        //overrides /
        public static Point3D operator /(Point3D p, Point3D p1)
        {
            return new Point3D(p.X / p1.X,
                             p.Y / p1.Y,
                             p.Z / p1.Z);
        }
        public static Point3D operator /(Point3D p, double d)
        {
            return new Point3D(p.X / d,
                             p.Y / d,
                             p.Z / d);
        }
        public static Point3D operator /(double d, Point3D p)
        {
            return new Point3D(p.X / d,
                             p.Y / d,
                             p.Z / d);
        }
        public static Point3D operator /(Point3D p, Vector3D v)
        {
            return new(p.X / v.X, p.Y / v.Y, p.Z / v.Z);
        }

        //overrides >
        public static bool operator >(Point3D p, Point3D p1)
        {
            if (p.X > p1.X && p.Y > p1.Y && p.Z > p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point3D p, double d)
        {
            if (p.X > d && p.Y > d && p.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Point3D p)
        {
            if (d > p.X && d > p.Y && d > p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Point3D p, Point3D p1)
        {
            if (p.X < p1.X && p.Y < p1.Y && p.Z < p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point3D p, double d)
        {
            if (p.X < d && p.Y < d && p.Z < d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Point3D p)
        {
            if (d < p.X && d < p.Y && d < p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Point3D p, Point3D p1)
        {
            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point3D p, double d)
        {
            if (p.X == d && p.Y == d && p.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Point3D p)
        {
            if (d == p.X && d == p.Y && d == p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Point3D p, Point3D p1)
        {
            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point3D p, double d)
        {
            if (p.X == d && p.Y == d && p.Z == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Point3D p)
        {
            if (d == p.X && d == p.Y && d == p.Z)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Point3D p, Point3D p1)
        {
            if (p.X <= p1.X && p.Y <= p1.Y && p.Z <= p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point3D p, double d)
        {
            if (p.X <= d && p.Y <= d && p.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Point3D p)
        {
            if (d <= p.X && d <= p.Y && d <= p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Point3D p, Point3D p1)
        {
            if (p.X >= p1.X && p.Y >= p1.Y && p.Z >= p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point3D p, double d)
        {
            if (p.X >= d && p.Y >= d && p.Z >= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Point3D p)
        {
            if (d >= p.X && d >= p.Y && d >= p.Z)
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
                    return X;
                }
                if (i == 1)
                {
                    return Y;
                }
                if (i == 2)
                {
                    return Z;
                }

                return 0;
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
