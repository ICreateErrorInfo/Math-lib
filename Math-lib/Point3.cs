using System;

namespace Math_lib
{
    public readonly struct Point3
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }
        public double W { get; init; }


        //Constructors
        public Point3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
        }
        public Point3(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Point3(double i)
        {
            X = Y = Z = i;
            W = 1;
        }
        public Point3(Point3 p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
            W = p.W;
        }
        public Point3(Point2 v)
        {
            X = v.X;
            Y = v.Y;
            Z = 0;
            W = 1;
        }
        public Point3(Vector3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = v.W;
        }
        public Point3(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
            Z = 0;
            W = 1;
        }


        //Methods
        public static double Distance(Point3 p, Point3 p1)
        {
            return new Vector3(p - p1).GetLength();
        }
        public static double DistanceSqrt(Point3 p, Point3 p1)
        {
            return new Vector3(p - p1).GetLengthSqrt();
        }
        public static Point3 Lerp(double t, Point3 p, Point3 p1)
        {
            return (1 - t) * p + t * p1;
        }
        public static Point3 Min(Point3 p, Point3 p1)
        {
            return new Point3(Math.Min(p.X, p1.X),
                             Math.Min(p.Y, p1.Y),
                             Math.Min(p.Z, p1.Z));
        }
        public static Point3 Max(Point3 p, Point3 p1)
        {
            return new Point3(Math.Max(p.X, p1.X),
                             Math.Max(p.Y, p1.Y),
                             Math.Max(p.Z, p1.Z));
        }
        public static Point3 Floor(Point3 p)
        {
            return new Point3(Math.Floor(p.X),
                             Math.Floor(p.Y),
                             Math.Floor(p.Z));
        }
        public static Point3 Ceil(Point3 p)
        {
            return new Point3(Math.Ceiling(p.X),
                             Math.Ceiling(p.Y),
                             Math.Ceiling(p.Z));
        }
        public static Point3 Abs(Point3 p)
        {
            return new Point3(Math.Abs(p.X),
                             Math.Abs(p.Y),
                             Math.Abs(p.Z));
        }
        public static Point3 Permute(Point3 p, int x, int y, int z)
        {
            return new Point3(p[x], p[y], p[z]);
        }

        //overrides +
        public static Point3 operator +(Point3 p, Point3 p1)
        {
            return new Point3(p.X + p1.X,
                             p.Y + p1.Y,
                             p.Z + p1.Z);
        }
        public static Point3 operator +(Point3 p, double p1)
        {
            return new Point3(p.X + p1,
                             p.Y + p1,
                             p.Z + p1);
        }
        public static Point3 operator +(double d, Point3 p)
        {
            return new Point3(p.X + d,
                             p.Y + d,
                             p.Z + d);
        }
        public static Point3 operator +(Point3 p, Vector3 v)
        {
            return new Point3(p.X + v.X,
                             p.Y + v.Y,
                             p.Z + v.Z);
        }
        public static Point3 operator +(Vector3 v, Point3 p)
        {
            return new Point3(v.X + p.X,
                             v.Y + p.Y,
                             v.Z + p.Z);
        }
        public static Point3 operator +(Point3 p, Normal n)
        {
            return new Point3(p.X + n.X,
                             p.Y + n.Y,
                             p.Z + n.Z);
        }
        public static Point3 operator +(Point3 p)
        {
            return new Point3(+p.X, +p.Y, +p.Z);
        }

        //overrides -
        public static Vector3 operator -(Point3 p, Point3 p1)
        {
            return new Vector3(p.X - p1.X,
                              p.Y - p1.Y,
                              p.Z - p1.Z);
        }
        public static Point3 operator -(Point3 p, double d)
        {
            return new Point3(p.X - d,
                             p.Y - d,
                             p.Z - d);
        }
        public static Point3 operator -(double d, Point3 p)
        {
            return new Point3(p.X - d,
                             p.Y - d,
                             p.Z - d);
        }
        public static Point3 operator -(Point3 p, Vector3 v)
        {
            return new Point3(p.X - v.X,
                             p.Y - v.Y,
                             p.Z - v.Z);
        }
        public static Point3 operator -(Vector3 v, Point3 p)
        {
            return new Point3(v.X - p.X,
                             v.Y - p.Y,
                             v.Z - p.Z);
        }
        public static Point3 operator -(Point3 p, Normal n)
        {
            return new Point3(p.X - n.X,
                             p.Y - n.Y,
                             p.Z - n.Z);
        }
        public static Point3 operator -(Point3 p)
        {
            return new Point3(-p.X, -p.Y, -p.Z);
        }

        //overrides *
        public static Point3 operator *(Point3 p, Point3 p1)
        {
            return new Point3(p.X * p1.X,
                             p.Y * p1.Y,
                             p.Z * p1.Z);
        }
        public static Point3 operator *(Point3 p, double d)
        {
            return new Point3(p.X * d,
                             p.Y * d,
                             p.Z * d);
        }
        public static Point3 operator *(double d, Point3 p)
        {
            return new Point3(p.X * d,
                             p.Y * d,
                             p.Z * d);
        }
        public static Point3 operator *(Point3 p, Vector3 v)
        {
            return new Point3(p.X * v.X,
                             p.Y * v.Y,
                             p.Z * v.Z);
        }
        public static Point3 operator *(Point3 p, Normal n)
        {
            return new Point3(p.X * n.X,
                             p.Y * n.Y,
                             p.Z * n.Z);
        }

        //overrides /
        public static Point3 operator /(Point3 p, Point3 p1)
        {
            return new Point3(p.X / p1.X,
                             p.Y / p1.Y,
                             p.Z / p1.Z);
        }
        public static Point3 operator /(Point3 p, double d)
        {
            return new Point3(p.X / d,
                             p.Y / d,
                             p.Z / d);
        }
        public static Point3 operator /(double d, Point3 p)
        {
            return new Point3(p.X / d,
                             p.Y / d,
                             p.Z / d);
        }
        public static Point3 operator /(Point3 p, Vector3 v)
        {
            return new Point3(p.X / v.X,
                             p.Y / v.Y,
                             p.Z / v.Z);
        }
        public static Point3 operator /(Point3 p, Normal n)
        {
            return new Point3(p.X / n.X,
                             p.Y / n.Y,
                             p.Z / n.Z);
        }

        //overrides >
        public static bool operator >(Point3 p, Point3 p1)
        {
            if (p.X > p1.X && p.Y > p1.Y && p.Z > p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point3 p, double d)
        {
            if (p.X > d && p.Y > d && p.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Point3 p)
        {
            if (d > p.X && d > p.Y && d > p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point3 p, Vector3 v)
        {
            if (p.X > v.X && p.Y > v.Y && p.Z > v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point3 p, Normal n)
        {
            if (p.X > n.X && p.Y > n.Y && p.Z > n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Point3 p, Point3 p1)
        {
            if (p.X < p1.X && p.Y < p1.Y && p.Z < p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point3 p, double d)
        {
            if (p.X < d && p.Y < d && p.Z < d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Point3 p)
        {
            if (d < p.X && d < p.Y && d < p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point3 p, Vector3 v)
        {
            if (p.X < v.X && p.Y < v.Y && p.Z < v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point3 p, Normal n)
        {
            if (p.X < n.X && p.Y < n.Y && p.Z < n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Point3 p, Point3 p1)
        {
            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point3 p, double d)
        {
            if (p.X == d && p.Y == d && p.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Point3 p)
        {
            if (d == p.X && d == p.Y && d == p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point3 p, Vector3 p1)
        {
            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point3 p, Normal n)
        {
            if (p.X == n.X && p.Y == n.Y && p.Z == n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Point3 p, Point3 p1)
        {
            if (p.X == p1.X && p.Y == p1.Y && p.Z == p1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point3 p, double d)
        {
            if (p.X == d && p.Y == d && p.Z == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Point3 p)
        {
            if (d == p.X && d == p.Y && d == p.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point3 p, Vector3 v)
        {
            if (p.X == v.X && p.Y == v.Y && p.Z == v.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point3 p, Normal n)
        {
            if (p.X == n.X && p.Y == n.Y && p.Z == n.Z)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Point3 p, Point3 p1)
        {
            if (p.X <= p1.X && p.Y <= p1.Y && p.Z <= p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point3 p, double d)
        {
            if (p.X <= d && p.Y <= d && p.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Point3 p)
        {
            if (d <= p.X && d <= p.Y && d <= p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point3 p, Vector3 v)
        {
            if (p.X <= v.X && p.Y <= v.Y && p.Z <= v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point3 p, Normal n)
        {
            if (p.X <= n.X && p.Y <= n.Y && p.Z <= n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Point3 p, Point3 p1)
        {
            if (p.X >= p1.X && p.Y >= p1.Y && p.Z >= p1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point3 p, double d)
        {
            if (p.X >= d && p.Y >= d && p.Z >= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Point3 p)
        {
            if (d >= p.X && d >= p.Y && d >= p.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point3 p, Vector3 v)
        {
            if (p.X >= v.X && p.Y >= v.Y && p.Z >= v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point3 p, Normal n)
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
            if (obj is not Point3 other)
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
