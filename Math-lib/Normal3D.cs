using System;
// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Math_lib
{
    public readonly struct Normal3D
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }


        //Constructors
        public Normal3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Normal3D(double i)
        {
            X = Y = Z = i;
        }
        public Normal3D(Normal3D n)
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
        public static Normal3D Normalize(Normal3D n)
        {
            return n / n.GetLength();
        }


        //overrides +
        public static Normal3D operator +(Normal3D n, Normal3D n1)
        {
            return new(n.X + n1.X,
                       n.Y + n1.Y,
                       n.Z + n1.Z);
        }
        public static Normal3D operator +(Normal3D n, double d)
        {
            return new(n.X + d,
                       n.Y + d,
                       n.Z + d);
        }
        public static Normal3D operator +(double d, Normal3D n)
        {
            return new(n.X + d,
                       n.Y + d,
                       n.Z + d);
        }
        public static Normal3D operator +(Normal3D n)
        {
            return new(+n.X, +n.Y, +n.Z);
        }

        //overrides -
        public static Normal3D operator -(Normal3D n, Normal3D n1)
        {
            return new(n.X - n1.X,
                       n.Y - n1.Y,
                       n.Z - n1.Z);
        }
        public static Normal3D operator -(Normal3D n, double d)
        {
            return new(n.X - d,
                       n.Y - d,
                       n.Z - d);
        }
        public static Normal3D operator -(double d, Normal3D n)
        {
            return new(n.X - d,
                       n.Y - d,
                       n.Z - d);
        }
        public static Normal3D operator -(Normal3D n)
        {
            return new(-n.X, -n.Y, -n.Z);
        }

        //overrides *
        public static Normal3D operator *(Normal3D n, Normal3D n1)
        {
            return new(n.X * n1.X,
                       n.Y * n1.Y,
                       n.Z * n1.Z);
        }
        public static Normal3D operator *(Normal3D n, double d)
        {
            return new(n.X * d,
                       n.Y * d,
                       n.Z * d);
        }
        public static Normal3D operator *(double d, Normal3D n)
        {
            return new(n.X * d,
                       n.Y * d,
                       n.Z * d);
        }

        //overrides /
        public static Normal3D operator /(Normal3D n, Normal3D n1)
        {
            return new(n.X / n1.X,
                       n.Y / n1.Y,
                       n.Z / n1.Z);
        }
        public static Normal3D operator /(Normal3D n, double d)
        {
            return new(n.X / d,
                       n.Y / d,
                       n.Z / d);
        }
        public static Normal3D operator /(double d, Normal3D n)
        {
            return new(n.X / d,
                       n.Y / d,
                       n.Z / d);
        }

        //overrides >
        public static bool operator >(Normal3D n, Normal3D n1)
        {
            if (n.X > n1.X && n.Y > n1.Y && n.Z > n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Normal3D n, double d)
        {
            if (n.X > d || n.Y > d || n.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Normal3D n)
        {
            if (d > n.X && d > n.Y && d > n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Normal3D n, Normal3D n1)
        {
            if (n.X < n1.X && n.Y < n1.Y && n.Z < n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Normal3D n, double d)
        {
            if (n.X < d && n.Y < d && n.Z < d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Normal3D n)
        {
            if (d < n.X && d < n.Y && d < n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Normal3D n, Normal3D n1)
        {
            if (n.X == n1.X && n.Y == n1.Y && n.Z == n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Normal3D n, double d)
        {
            if (n.X == d && n.Y == d && n.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Normal3D n)
        {
            if (d == n.X && d == n.Y && d == n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Normal3D n, Normal3D n1)
        {
            if (n.X == n1.X && n.Y == n1.Y && n.Z == n1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Normal3D n, double d)
        {
            if (n.X == d && n.Y == d && n.Z == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Normal3D n)
        {
            if (d == n.X && d == n.Y && d == n.Z)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Normal3D n, Normal3D n1)
        {
            if (n.X <= n1.X && n.Y <= n1.Y && n.Z <= n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Normal3D n, double d)
        {
            if (n.X <= d && n.Y <= d && n.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Normal3D n)
        {
            if (d <= n.X && d <= n.Y && d <= n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Normal3D n, Normal3D n1)
        {
            if (n.X >= n1.X && n.Y >= n1.Y && n.Z >= n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Normal3D n, double d)
        {
            if (n.X >= d && n.Y >= d && n.Z >= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Normal3D n)
        {
            if (d >= n.X && d >= n.Y && d >= n.Z)
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
            if(obj is not Normal3D other)
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
