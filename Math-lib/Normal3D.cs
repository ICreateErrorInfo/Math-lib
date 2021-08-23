using System;
using System.Diagnostics;
// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Math_lib
{
    public readonly struct Normal3D
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }


        //Ctors
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
            Debug.Assert(IsNaN(n));
            X = n.X;
            Y = n.Y;
            Z = n.Z;
        }


        //Methods
        public static bool IsNaN(Normal3D v)
        {
            if (double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z))
            {
                return false;
                throw new ArgumentOutOfRangeException("NaN", "Number cant be NaN");
            }
            return true;
        }
        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        public double GetLengthSqrt()
        {
            return X * X + Y * Y + Z * Z;
        }
        public static double Dot(Normal3D n1, Vector3D v2)
        {
            return n1.X * v2.X + n1.Y * v2.Y + n1.Z * v2.Z;
        }
        public static double Dot(Vector3D v1, Normal3D n2)
        {
            return v1.X * n2.X + v1.Y * n2.Y + v1.Z * n2.Z;
        }
        public static double Dot(Normal3D n1, Normal3D n2)
        {
            return n1.X * n2.X + n1.Y * n2.Y + n1.Z * n2.Z;
        }
        public static double AbsDot(Normal3D n1, Vector3D v2)
        {
            return Math.Abs(Dot(n1, v2));
        }
        public static double AbsDot(Vector3D v1, Normal3D n2)
        {
            return Math.Abs(Dot(v1, n2));
        }
        public static double AbsDot(Normal3D n1, Normal3D n2)
        {
            return Math.Abs(Dot(n1, n2));
        }
        public static Vector3D Cross(Vector3D v, Normal3D v1)
        {
            return new((v.Y * v1.Z) - (v.Z * v1.Y),
                       (v.Z * v1.X) - (v.X * v1.Z),
                       (v.X * v1.Y) - (v.Y * v1.X));
        }
        public static Vector3D Cross(Normal3D v, Vector3D v1)
        {
            return new((v.Y * v1.Z) - (v.Z * v1.Y),
                       (v.Z * v1.X) - (v.X * v1.Z),
                       (v.X * v1.Y) - (v.Y * v1.X));
        }
        public static Normal3D Faceforward(Normal3D n, Vector3D v)
        {
            return (Dot(n, v) < 0) ? -n : n;
        }
        public static Normal3D Faceforward(Normal3D n, Normal3D n1)
        {
            return (Dot(n, n1) < 0) ? -n : n;
        }
        public static Vector3D Faceforward(Vector3D v, Vector3D v1)
        {
            return (Vector3D.Dot(v, v1) < 0) ? -v : v;
        }
        public static Vector3D Faceforward(Vector3D v, Normal3D n1)
        {
            return (Dot(v, n1) < 0) ? -v : v;
        }
        public static Normal3D Normalize(Normal3D n)
        {
            return n / n.GetLength();
        }
        public static explicit operator Normal3D(Vector3D a)
        {
            Debug.Assert(Vector3D.IsNaN(a));
            return new Normal3D() { X = a.X, Y = a.Y, Z = a.Z };
        }
        public static explicit operator Vector3D(Normal3D a)
        {
            Debug.Assert(IsNaN(a));
            return new Vector3D() { X = a.X, Y = a.Y, Z = a.Z };
        }


        //overrides +
        public static Normal3D operator +(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            return new(n.X + n1.X,
                       n.Y + n1.Y,
                       n.Z + n1.Z);
        }
        public static Normal3D operator +(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            return new(n.X + d,
                       n.Y + d,
                       n.Z + d);
        }
        public static Normal3D operator +(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return new(n.X + d,
                       n.Y + d,
                       n.Z + d);
        }
        public static Normal3D operator +(Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return new(+n.X, +n.Y, +n.Z);
        }

        //overrides -
        public static Normal3D operator -(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            return new(n.X - n1.X,
                       n.Y - n1.Y,
                       n.Z - n1.Z);
        }
        public static Normal3D operator -(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            return new(n.X - d,
                       n.Y - d,
                       n.Z - d);
        }
        public static Normal3D operator -(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return new(n.X - d,
                       n.Y - d,
                       n.Z - d);
        }
        public static Normal3D operator -(Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return new(-n.X, -n.Y, -n.Z);
        }

        //overrides *
        public static Normal3D operator *(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            return new(n.X * n1.X,
                       n.Y * n1.Y,
                       n.Z * n1.Z);
        }
        public static Normal3D operator *(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            return new(n.X * d,
                       n.Y * d,
                       n.Z * d);
        }
        public static Normal3D operator *(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return new(n.X * d,
                       n.Y * d,
                       n.Z * d);
        }

        //overrides /
        public static Normal3D operator /(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            return new(n.X / n1.X,
                       n.Y / n1.Y,
                       n.Z / n1.Z);
        }
        public static Normal3D operator /(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            double inv = (double)1 / d;

            return new(n.X * inv,
                       n.Y * inv,
                       n.Z * inv);
        }
        public static Normal3D operator /(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return new(n.X / d,
                       n.Y / d,
                       n.Z / d);
        }

        //overrides >
        public static bool operator >(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            if (n.X > n1.X && n.Y > n1.Y && n.Z > n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            if (n.X > d || n.Y > d || n.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            if (d > n.X && d > n.Y && d > n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            if (n.X < n1.X && n.Y < n1.Y && n.Z < n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            if (n.X < d && n.Y < d && n.Z < d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            if (d < n.X && d < n.Y && d < n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            if (n.X == n1.X && n.Y == n1.Y && n.Z == n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            if (n.X == d && n.Y == d && n.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            if (d == n.X && d == n.Y && d == n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            if (n.X == n1.X && n.Y == n1.Y && n.Z == n1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            if (n.X == d && n.Y == d && n.Z == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            if (d == n.X && d == n.Y && d == n.Z)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            if (n.X <= n1.X && n.Y <= n1.Y && n.Z <= n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            if (n.X <= d && n.Y <= d && n.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            if (d <= n.X && d <= n.Y && d <= n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            if (n.X >= n1.X && n.Y >= n1.Y && n.Z >= n1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));

            if (n.X >= d && n.Y >= d && n.Z >= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));

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
