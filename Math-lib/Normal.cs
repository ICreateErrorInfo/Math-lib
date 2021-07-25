using System;

namespace Math_lib
{
    public readonly struct Normal
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }


        //Constructors
        public Normal(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Normal(double i)
        {
            X = Y = Z = i;
        }
        public Normal(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
        public Normal(Point v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
        public Normal(Normal n)
        {
            X = n.X;
            Y = n.Y;
            Z = n.Z;
        }


        //Methods
        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        public double GetLengthSqrt()
        {
            return X * X + Y * Y + Z * Z;
        }
        public static Normal Normalize(Normal n)
        {
            return n / n.GetLength();
        }


        //overrides +
        public static Normal operator +(Normal n, Normal n1)
        {
            return new(n.X + n1.X,
                       n.Y + n1.Y,
                       n.Z + n1.Z);
        }
        public static Normal operator +(Normal n, double d)
        {
            return new(n.X + d,
                       n.Y + d,
                       n.Z + d);
        }
        public static Normal operator +(double d, Normal n)
        {
            return new(n.X + d,
                       n.Y + d,
                       n.Z + d);
        }
        public static Normal operator +(Normal n, Vector v)
        {
            return new(n.X + v.X,
                       n.Y + v.Y,
                       n.Z + v.Z);
        }
        public static Normal operator +(Normal n, Point p)
        {
            return new(n.X + p.X,
                       n.Y + p.Y,
                       n.Z + p.Z);
        }
        public static Normal operator +(Normal n)
        {
            return new(+n.X, +n.Y, +n.Z);
        }

        //overrides -
        public static Normal operator -(Normal n, Normal n1)
        {
            return new(n.X - n1.X,
                       n.Y - n1.Y,
                       n.Z - n1.Z);
        }
        public static Normal operator -(Normal n, double d)
        {
            return new(n.X - d,
                       n.Y - d,
                       n.Z - d);
        }
        public static Normal operator -(double d, Normal n)
        {
            return new(n.X - d,
                       n.Y - d,
                       n.Z - d);
        }
        public static Normal operator -(Normal n, Vector v)
        {
            return new(n.X - v.X,
                       n.Y - v.Y,
                       n.Z - v.Z);
        }
        public static Normal operator -(Normal n, Point p)
        {
            return new(n.X - p.X,
                       n.Y - p.Y,
                       n.Z - p.Z);
        }
        public static Normal operator -(Normal n)
        {
            return new(-n.X, -n.Y, -n.Z);
        }

        //overrides *
        public static Normal operator *(Normal n, Normal n1)
        {
            return new(n.X * n1.X,
                       n.Y * n1.Y,
                       n.Z * n1.Z);
        }
        public static Normal operator *(Normal n, double d)
        {
            return new(n.X * d,
                       n.Y * d,
                       n.Z * d);
        }
        public static Normal operator *(double d, Normal n)
        {
            return new(n.X * d,
                       n.Y * d,
                       n.Z * d);
        }
        public static Normal operator *(Normal n, Vector v)
        {
            return new(n.X * v.X,
                       n.Y * v.Y,
                       n.Z * v.Z);
        }
        public static Normal operator *(Normal n, Point p)
        {
            return new(n.X * p.X,
                       n.Y * p.Y,
                       n.Z * p.Z);
        }

        //overrides /
        public static Normal operator /(Normal n, Normal n1)
        {
            return new(n.X / n1.X,
                       n.Y / n1.Y,
                       n.Z / n1.Z);
        }
        public static Normal operator /(Normal n, double d)
        {
            return new(n.X / d,
                       n.Y / d,
                       n.Z / d);
        }
        public static Normal operator /(double d, Normal n)
        {
            return new(n.X / d,
                       n.Y / d,
                       n.Z / d);
        }
        public static Normal operator /(Normal n, Vector v)
        {
            return new(n.X / v.X,
                       n.Y / v.Y,
                       n.Z / v.Z);
        }
        public static Normal operator /(Normal n, Point p)
        {
            return new(n.X / p.X,
                       n.Y / p.Y,
                       n.Z / p.Z);
        }

        //overrides >
        public static bool operator >(Normal n, Normal n1)
        {
            if (n.X > n1.X && n.Y > n1.Y && n.Z > n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Normal n, double d)
        {
            if (n.X > d || n.Y > d || n.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Normal n)
        {
            if (d > n.X && d > n.Y && d > n.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Normal n, Vector v)
        {
            if (n.X > v.X && n.Y > v.Y && n.Z > v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Normal n, Point p)
        {
            if (n.X > p.X && n.Y > p.Y && n.Z > p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Normal n, Normal n1)
        {
            if (n.X < n1.X && n.Y < n1.Y && n.Z < n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Normal n, double d)
        {
            if (n.X < d && n.Y < d && n.Z < d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Normal n)
        {
            if (d < n.X && d < n.Y && d < n.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Normal n, Vector v)
        {
            if (n.X < v.X && n.Y < v.Y && n.Z < v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Normal n, Point p)
        {
            if (n.X < p.X && n.Y < p.Y && n.Z < p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Normal n, Normal n1)
        {
            if (n.X == n1.X && n.Y == n1.Y && n.Z == n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Normal n, double d)
        {
            if (n.X == d && n.Y == d && n.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Normal n)
        {
            if (d == n.X && d == n.Y && d == n.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Normal n, Vector v)
        {
            if (n.X == v.X && n.Y == v.Y && n.Z == v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Normal n, Point p)
        {
            if (n.X == p.X && n.Y == p.Y && n.Z == p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Normal n, Normal n1)
        {
            if (n.X == n1.X && n.Y == n1.Y && n.Z == n1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Normal n, double d)
        {
            if (n.X == d && n.Y == d && n.Z == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Normal n)
        {
            if (d == n.X && d == n.Y && d == n.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Normal n, Vector v)
        {
            if (n.X == v.X && n.Y == v.Y && n.Z == v.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Normal n, Point p)
        {
            if (n.X == p.X && n.Y == p.Y && n.Z == p.Z)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Normal n, Normal n1)
        {
            if (n.X <= n1.X && n.Y <= n1.Y && n.Z <= n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Normal n, double d)
        {
            if (n.X <= d && n.Y <= d && n.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Normal n)
        {
            if (d <= n.X && d <= n.Y && d <= n.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Normal n, Vector v)
        {
            if (n.X <= v.X && n.Y <= v.Y && n.Z <= v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Normal n, Point p)
        {
            if (n.X <= p.X && n.Y <= p.Y && n.Z <= p.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Normal n, Normal n1)
        {
            if (n.X >= n1.X && n.Y >= n1.Y && n.Z >= n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Normal n, double d)
        {
            if (n.X >= d && n.Y >= d && n.Z >= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Normal n)
        {
            if (d >= n.X && d >= n.Y && d >= n.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Normal n, Vector v)
        {
            if (n.X >= v.X && n.Y >= v.Y && n.Z >= v.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Normal n, Point p)
        {
            if (n.X >= p.X && n.Y >= p.Y && n.Z >= p.Z)
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
            if(obj is not Normal other)
            {
                return false;
            }

            return this == other;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X,Y,Z);
        }
    }
}
