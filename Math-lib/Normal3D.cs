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
            Debug.Assert(!double.IsNaN(x));
            Debug.Assert(!double.IsNaN(y));
            Debug.Assert(!double.IsNaN(z));

            X = x;
            Y = y;
            Z = z;
        }
        public Normal3D(double i)
        {
            Debug.Assert(!double.IsNaN(i));

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
        public static bool IsNaN(Normal3D n)
        {
            if (!double.IsNaN(n.X) || !double.IsNaN(n.Y) || !double.IsNaN(n.Z))
            {
                return false;
                throw new ArgumentOutOfRangeException("NaN", "Number cant be NaN");
            }
            return true;
        }
        public double GetLength()
        {
            Debug.Assert(IsNaN(this));

            return Math.Sqrt(GetLengthSqrt());
        }
        public double GetLengthSqrt()
        {
            Debug.Assert(IsNaN(this));

            return X * X + Y * Y + Z * Z;
        }
        public static double Dot(Normal3D n, Vector3D v)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(Vector3D.IsNaN(v));

            return n.X * v.X
                 + n.Y * v.Y
                 + n.Z * v.Z;
        }
        public static double Dot(Vector3D v, Normal3D n)
        {
            Debug.Assert(Vector3D.IsNaN(v));
            Debug.Assert(IsNaN(n));

            return v.X * n.X
                 + v.Y * n.Y
                 + v.Z * n.Z;
        }
        public static double Dot(Normal3D n1, Normal3D n2)
        {
            Debug.Assert(IsNaN(n1));
            Debug.Assert(IsNaN(n2));

            return n1.X * n2.X
                 + n1.Y * n2.Y
                 + n1.Z * n2.Z;
        }
        public static double AbsDot(Normal3D n, Vector3D v)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(Vector3D.IsNaN(v));

            return Math.Abs(Dot(n, v));
        }
        public static double AbsDot(Vector3D v, Normal3D n)
        {
            Debug.Assert(Vector3D.IsNaN(v));
            Debug.Assert(IsNaN(n));

            return Math.Abs(Dot(v, n));
        }
        public static double AbsDot(Normal3D n1, Normal3D n2)
        {
            Debug.Assert(IsNaN(n1));
            Debug.Assert(IsNaN(n2));

            return Math.Abs(Dot(n1, n2));
        }
        public static Vector3D Cross(Vector3D v, Normal3D n)
        {
            Debug.Assert(Vector3D.IsNaN(v));
            Debug.Assert(IsNaN(n));

            return new((v.Y * n.Z) - (v.Z * n.Y),
                       (v.Z * n.X) - (v.X * n.Z),
                       (v.X * n.Y) - (v.Y * n.X));
        }
        public static Vector3D Cross(Normal3D n, Vector3D v)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(Vector3D.IsNaN(v));

            return new((n.Y * v.Z) - (n.Z * v.Y),
                       (n.Z * v.X) - (n.X * v.Z),
                       (n.X * v.Y) - (n.Y * v.X));
        }
        public static Normal3D Faceforward(Normal3D n, Vector3D v)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(Vector3D.IsNaN(v));

            return (Dot(n, v) < 0) ? -n : n;
        }
        public static Normal3D Faceforward(Normal3D n, Normal3D n1)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(IsNaN(n1));

            return (Dot(n, n1) < 0) ? -n : n;
        }
        public static Vector3D Faceforward(Vector3D v, Vector3D v1)
        {
            Debug.Assert(Vector3D.IsNaN(v));
            Debug.Assert(Vector3D.IsNaN(v1));

            return (Vector3D.Dot(v, v1) < 0) ? -v : v;
        }
        public static Vector3D Faceforward(Vector3D v, Normal3D n1)
        {
            Debug.Assert(Vector3D.IsNaN(v));
            Debug.Assert(IsNaN(n1));

            return (Dot(v, n1) < 0) ? -v : v;
        }
        public static Normal3D Normalize(Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return n / n.GetLength();
        }
        public static explicit operator Normal3D(Vector3D v)
        {
            Debug.Assert(Vector3D.IsNaN(v));

            return new Normal3D() { X = v.X,
                                    Y = v.Y,
                                    Z = v.Z };
        }
        public static explicit operator Vector3D(Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return new Vector3D() { X = n.X,
                                    Y = n.Y,
                                    Z = n.Z };
        }
        public static explicit operator Point3D(Normal3D n)
        {
            Debug.Assert(IsNaN(n));

            return new Point3D() { X = n.X,
                                   Y = n.Y,
                                   Z = n.Z };
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
            Debug.Assert(!double.IsNaN(d));

            return new(n.X + d,
                       n.Y + d,
                       n.Z + d);
        }
        public static Normal3D operator +(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

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
            Debug.Assert(!double.IsNaN(d));

            return new(n.X - d,
                       n.Y - d,
                       n.Z - d);
        }
        public static Normal3D operator -(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

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
            Debug.Assert(!double.IsNaN(d));

            return new(n.X * d,
                       n.Y * d,
                       n.Z * d);
        }
        public static Normal3D operator *(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

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
            Debug.Assert(!double.IsNaN(d));

            double inv = (double)1 / d;

            return new(n.X * inv,
                       n.Y * inv,
                       n.Z * inv);
        }
        public static Normal3D operator /(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

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
            Debug.Assert(!double.IsNaN(d));

            if (n.X > d || n.Y > d || n.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

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

            if (n !> n1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

            if (n !> d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

            if (d !> n)
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
            Debug.Assert(!double.IsNaN(d));

            if (n.X == d && n.Y == d && n.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

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

            if (n == n1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

            if (n == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

            if (d == n)
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
            Debug.Assert(!double.IsNaN(d));

            if (n.X <= d && n.Y <= d && n.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

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

            if (n !<= n1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Normal3D n, double d)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

            if (n !<= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Normal3D n)
        {
            Debug.Assert(IsNaN(n));
            Debug.Assert(!double.IsNaN(d));

            if (d !<= n)
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
