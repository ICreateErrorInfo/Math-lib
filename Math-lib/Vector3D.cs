using System;
using System.Diagnostics;
// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Math_lib
{
    public readonly struct Vector3D
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }


        //Constructors
        public Vector3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3D(double i)
        {
            X = Y = Z = i;
        }
        public Vector3D(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }


        //Mehods
        public static bool IsNaN(Vector3D v)
        {
            if (double.IsNaN(v.X) || double.IsNaN(v.Y) || double.IsNaN(v.Z))
            {
                return false;
            }
            return true;
        }
        public double GetLength()
        {
            return Math.Sqrt(GetLengthSqrt());
        }
        public double GetLengthSqrt()
        {
            return X * X + Y * Y + Z * Z;
        }
        public static double Dot(Vector3D v, Vector3D v1)
        {
            return v.X * v1.X
                 + v.Y * v1.Y
                 + v.Z * v1.Z;
        }
        public static Vector3D Cross(Vector3D v, Vector3D v1)
        {
            return new(v.Y * v1.Z - v.Z * v1.Y,
                       v.Z * v1.X - v.X * v1.Z,
                       v.X * v1.Y - v.Y * v1.X);
        }
        public Vector3D UnitVector()
        {
            return this / this.GetLength();
        }
        public static Vector3D Abs(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Abs(v.X),
                       Math.Abs(v.Y),
                       Math.Abs(v.Z));
        }
        public static Vector3D Ceiling(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Ceiling(v.X),
                       Math.Ceiling(v.Y),
                       Math.Ceiling(v.Z));
        }
        public static Vector3D Floor(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Floor(v.X),
                       Math.Floor(v.Y),
                       Math.Floor(v.Z));
        }
        public static Vector3D Max(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(Math.Max(v.X, v1.X),
                       Math.Max(v.Y, v1.Y),
                       Math.Max(v.Z, v1.Z));
        }
        public static Vector3D Min(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(Math.Min(v.X, v1.X),
                       Math.Min(v.Y, v1.Y),
                       Math.Min(v.Z, v1.Z));
        }
        public static Vector3D Clamp(Vector3D v, Vector3D min, Vector3D max)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(min));
            Debug.Assert(IsNaN(max));

            return new(Math.Clamp(v.X, min.X, max.X),
                       Math.Clamp(v.Y, min.Y, max.Y),
                       Math.Clamp(v.Z, min.Z, max.Z));
        }
        public static Vector3D SquareRoot(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Sqrt(v.X),
                       Math.Sqrt(v.Y),
                       Math.Sqrt(v.Z));
        }
        public static Vector3D Reflect(Vector3D d, Vector3D n)
        {
            return d - 2 * Dot(d, n) * n;
        }
        public static Vector3D Refract(Vector3D i, Vector3D n, double eta)
        {
            double cosi = Dot(-i, n);
            double cost2 = 1 - eta * eta * (1 - cosi * cosi);
            Vector3D t = eta * i + (eta * cosi - Math.Sqrt(Math.Abs(cost2)) * n);
            if (cost2 > 0)
            {
                return t * 1;
            }
            else
            {
                return new Vector3D(0);
            }
        }
        public Vector3D Saturate()
        {
            double x = Math.Min(1, Math.Max(0, X));
            double y = Math.Min(1, Math.Max(0, Y));
            double z = Math.Min(1, Math.Max(0, Z));

            return new(x, y, z);
        }
        public Vector3D GetSaturated()
        {
            Vector3D temp = this;
            temp.Saturate();
            return temp;
        }
        public static explicit operator Point3D(Vector3D a)
        {
            Debug.Assert(IsNaN(a));
            return new Point3D() { X = a.X, Y = a.Y, Z = a.Z };
        }


        //overrides +
        public static Vector3D operator +(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(v.X + v1.X,
                       v.Y + v1.Y,
                       v.Z + v1.Z);
        }
        public static Vector3D operator +(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));

            return new(v.X + v1,
                       v.Y + v1,
                       v.Z + v1);
        }
        public static Vector3D operator +(double v1, Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return new(v.X + v1,
                       v.Y + v1,
                       v.Z + v1);
        }
        public static Vector3D operator +(Vector3D v, Point3D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point3D.IsNan(p));

            return new(v.X + p.X, v.Y + p.Y, v.Z + p.Z);
        }
        public static Vector3D operator +(Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            return new(+v.X, +v.Y, +v.Z);
        }

        //overrides -
        public static Vector3D operator -(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(v.X - v1.X,
                       v.Y - v1.Y,
                       v.Z - v1.Z);
        }
        public static Vector3D operator -(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            return new(v.X - v1,
                       v.Y - v1,
                       v.Z - v1);
        }
        public static Vector3D operator -(double v1, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            return new(v.X - v1,
                       v.Y - v1,
                       v.Z - v1);
        }
        public static Vector3D operator -(Vector3D v, Point3D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point3D.IsNan(p));

            return new(v.X - p.X, v.Y - p.Y, v.Z - p.Z);
        }
        public static Vector3D operator -(Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            return new(-v.X, -v.Y, -v.Z);
        }

        //overrides *
        public static Vector3D operator *(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(v.X * v1.X,
                       v.Y * v1.Y,
                       v.Z * v1.Z);
        }
        public static Vector3D operator *(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            return new(v.X * v1,
                       v.Y * v1,
                       v.Z * v1);
        }
        public static Vector3D operator *(double v1, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            return new(v.X * v1,
                       v.Y * v1,
                       v.Z * v1);
        }
        public static Vector3D operator *(Vector3D v, Point3D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point3D.IsNan(p));

            return new(v.X * p.X, v.Y * p.Y, v.Z * p.Z);
        }

        //overrides /
        public static Vector3D operator /(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(v.X / v1.X,
                       v.Y / v1.Y,
                       v.Z / v1.Z);
        }
        public static Vector3D operator /(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));

            return new(v.X / v1,
                       v.Y / v1,
                       v.Z / v1);
        }
        public static Vector3D operator /(double v1, Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return new(v1 / v.X,
                       v1 / v.Y,
                       v1 / v.Z);
        }
        public static Vector3D operator /(Vector3D v, Point3D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point3D.IsNan(p));

            return new(v.X / p.X, v.Y / p.Y, v.Z / p.Z);
        }

        //overrides >
        public static bool operator >(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X > v1.X && v.Y > v1.Y && v.Z > v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X > v1 && v.Y > v1 && v.Z > v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v > v1.X && v > v1.Y && v > v1.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X < v1.X && v.Y < v1.Y && v.Z < v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X < v1 && v.Y < v1 && v.Z < v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            if (d < v.X && d < v.Y && d < v.Z)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X.Equals(v1.X) && v.Y.Equals(v1.Y) && v.Z.Equals(v1.Z))
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X == v1 && v.Y == v1 && v.Z == v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            if (d == v.X && d == v.Y && d == v.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X == v1.X && v.Y == v1.Y && v.Z == v1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X == v1 && v.Y == v1 && v.Z == v1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            if (d == v.X && d == v.Y && d == v.Z)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X <= v1.X && v.Y <= v1.Y && v.Z <= v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X <= v1 && v.Y <= v1 && v.Z <= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v <= v1.X && v <= v1.Y && v <= v1.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X >= v1.X && v.Y >= v1.Y && v.Z >= v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector3D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X >= v1 && v.Y >= v1 && v.Z >= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v >= v1.X && v >= v1.Y && v >= v1.Z)
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
            if (obj is not Vector3D other)
            {
                return false;
            }

            return this == other;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
