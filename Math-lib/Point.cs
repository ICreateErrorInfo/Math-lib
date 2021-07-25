using System;

namespace Math_lib
{
    public readonly struct Point
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }


        //Constructors
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Point(double i)
        {
            X = Y = Z = i;
        }
        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
        }
        public Point(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }


        //Methods
        public static double Distance(Point p, Point p1)
        {
            return new Vector(p - p1).GetLength();
        }
        public static double DistanceSqrt(Point p, Point p1)
        {
            return new Vector(p - p1).GetLengthSqrt();
        }
        public static Point Lerp(double t, Point p, Point p1)
        {
            return (1 - t) * p + t * p1;
        }
        public static Point Min(Point p, Point p1)
        {
            return new Point(Math.Min(p.X, p1.X),
                             Math.Min(p.Y, p1.Y),
                             Math.Min(p.Z, p1.Z));
        }
        public static Point Max(Point p, Point p1)
        {
            return new Point(Math.Max(p.X, p1.X),
                             Math.Max(p.Y, p1.Y),
                             Math.Max(p.Z, p1.Z));
        }
        public static Point Floor(Point p)
        {
            return new Point(Math.Floor(p.X),
                             Math.Floor(p.Y),
                             Math.Floor(p.Z));
        }
        public static Point Ceil(Point p)
        {
            return new Point(Math.Ceiling(p.X),
                             Math.Ceiling(p.Y),
                             Math.Ceiling(p.Z));
        }
        public static Point Abs(Point p)
        {
            return new Point(Math.Abs(p.X),
                             Math.Abs(p.Y),
                             Math.Abs(p.Z));
        }
        public static Point Permute(Point p, int x, int y, int z)
        {
            return new Point(p[x], p[y], p[z]);
        }


        //overrides +
        public static Point operator +(Point p, Point p1)
        {
            return new Point(p.X + p1.X,
                             p.Y + p1.Y,
                             p.Z + p1.Z);
        }
        public static Point operator +(Point p, double p1)
        {
            return new Point(p.X + p1,
                             p.Y + p1,
                             p.Z + p1);
        }
        public static Point operator +(double d, Point p)
        {
            return new Point(p.X + d,
                             p.Y + d,
                             p.Z + d);
        }
        public static Point operator +(Point p, Vector v)
        {
            return new Point(p.X + v.X,
                             p.Y + v.Y,
                             p.Z + v.Z);
        }
        public static Point operator +(Vector v, Point p)
        {
            return new Point(v.X + p.X,
                             v.Y + p.Y,
                             v.Z + p.Z);
        }
        public static Point operator +(Point p, Normal n)
        {
            return new Point(p.X + n.X,
                             p.Y + n.Y,
                             p.Z + n.Z);
        }
        public static Point operator +(Point p)
        {
            return new Point(+p.X, +p.Y, +p.Z);
        }

        //overrides -
        public static Point operator -(Point p, Point p1)
        {
            return new Point(p.X - p1.X,
                             p.Y - p1.Y,
                             p.Z - p1.Z);
        }
        public static Point operator -(Point p, double d)
        {
            return new Point(p.X - d,
                             p.Y - d,
                             p.Z - d);
        }
        public static Point operator -(double d, Point p)
        {
            return new Point(p.X - d,
                             p.Y - d,
                             p.Z - d);
        }
        public static Point operator -(Point p, Vector v)
        {
            return new Point(p.X - v.X,
                             p.Y - v.Y,
                             p.Z - v.Z);
        }
        public static Point operator -(Vector v, Point p)
        {
            return new Point(v.X - p.X,
                             v.Y - p.Y,
                             v.Z - p.Z);
        }
        public static Point operator -(Point p, Normal n)
        {
            return new Point(p.X - n.X,
                             p.Y - n.Y,
                             p.Z - n.Z);
        }
        public static Point operator -(Point p)
        {
            return new Point(-p.X, -p.Y, -p.Z);
        }

        //overrides *
        public static Point operator *(Point p, Point p1)
        {
            return new Point(p.X * p1.X,
                             p.Y * p1.Y,
                             p.Z * p1.Z);
        }
        public static Point operator *(Point p, double d)
        {
            return new Point(p.X * d,
                             p.Y * d,
                             p.Z * d);
        }
        public static Point operator *(double d, Point p)
        {
            return new Point(p.X * d,
                             p.Y * d,
                             p.Z * d);
        }
        public static Point operator *(Point p, Vector v)
        {
            return new Point(p.X * v.X,
                             p.Y * v.Y,
                             p.Z * v.Z);
        }
        public static Point operator *(Point p, Normal n)
        {
            return new Point(p.X * n.X,
                             p.Y * n.Y,
                             p.Z * n.Z);
        }

        //overrides /
        public static Point operator /(Point p, Point p1)
        {
            return new Point(p.X / p1.X,
                             p.Y / p1.Y,
                             p.Z / p1.Z);
        }
        public static Point operator /(Point p, double d)
        {
            return new Point(p.X / d,
                             p.Y / d,
                             p.Z / d);
        }
        public static Point operator /(double d, Point p)
        {
            return new Point(p.X / d,
                             p.Y / d,
                             p.Z / d);
        }
        public static Point operator /(Point p, Vector v)
        {
            return new Point(p.X / v.X,
                             p.Y / v.Y,
                             p.Z / v.Z);
        }
        public static Point operator /(Point p, Normal n)
        {
            return new Point(p.X / n.X,
                             p.Y / n.Y,
                             p.Z / n.Z);
        }

        //overrides >
        public static bool operator >(Point p, Point p1)
        {
            if (p.X > p1.X && p.Y > p1.Y && p.Z > p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point p, double d)
        {
            if (p.X > d && p.Y > d && p.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Point p)
        {
            if (d > p.X && d > p.Y && d > p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point p, Vector v)
        {
            if (p.X > v.X && p.Y > v.Y && p.Z > v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point p, Normal n)
        {
            if (p.X > n.X && p.Y > n.Y && p.Z > n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Point p, Point p1)
        {
            if (p.X < p1.X && p.Y < p1.Y && p.Z < p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point p, double d)
        {
            if (p.X < d && p.Y < d && p.Z < d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Point p)
        {
            if (d < p.X && d < p.Y && d < p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point p, Vector v)
        {
            if (p.X < v.X && p.Y < v.Y && p.Z < v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point p, Normal n)
        {
            if (p.X < n.X && p.Y < n.Y && p.Z < n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Point p, Point p1)
        {
            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point p, double d)
        {
            if (p.X == d && p.Y == d && p.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Point p)
        {
            if (d == p.X && d == p.Y && d == p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point p, Vector p1)
        {
            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point p, Normal n)
        {
            if (p.X == n.X && p.Y == n.Y && p.Z == n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Point p, Point p1)
        {
            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point p, double d)
        {
            if (p.X == d && p.Y == d && p.Z == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Point p)
        {
            if (d == p.X && d == p.Y && d == p.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point p, Vector v)
        {
            if (p.X == v.X && p.Y == v.Y && p.Z == v.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point p, Normal n)
        {
            if (p.X == n.X && p.Y == n.Y && p.Z == n.Z)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Point p, Point p1)
        {
            if (p.X <= p1.X && p.Y <= p1.Y && p.Z <= p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point p, double d)
        {
            if (p.X <= d && p.Y <= d && p.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Point p)
        {
            if (d <= p.X && d <= p.Y && d <= p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point p, Vector v)
        {
            if (p.X <= v.X && p.Y <= v.Y && p.Z <= v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point p, Normal n)
        {
            if (p.X <= n.X && p.Y <= n.Y && p.Z <= n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Point p, Point p1)
        {
            if (p.X >= p1.X && p.Y >= p1.Y && p.Z >= p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point p, double d)
        {
            if (p.X >= d && p.Y >= d && p.Z >= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Point p)
        {
            if (d >= p.X && d >= p.Y && d >= p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point p, Vector v)
        {
            if (p.X >= v.X && p.Y >= v.Y && p.Z >= v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point p, Normal n)
        {
            if (p.X >= n.X && p.Y >= n.Y && p.Z >= n.Z)
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
            if (obj is not Point other)
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
