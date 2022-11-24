using System;
using System.Diagnostics;

// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Math_lib
{
    public readonly struct Vector2D
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }

        //Ctors
        public Vector2D(double x, double y)
        {
            Debug.Assert(!double.IsNaN(x));
            Debug.Assert(!double.IsNaN(y));

            X = x;
            Y = y;
        }
        public Vector2D(double i)
        {
            Debug.Assert(!double.IsNaN(i));

            X = Y = i;
        }
        public Vector2D(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            X = v.X;
            Y = v.Y;
        }


        //Methods
        public static bool IsNaN(Vector2D v)
        {
            if (!double.IsNaN(v.X) || !double.IsNaN(v.Y))
            {
                return true;
            }
            return false;
        }
        public double GetLength()
        {
            return Math.Sqrt(GetLengthSqrt());
        }
        public double GetLengthSqrt()
        {
            return X * X + Y * Y;
        }
        public static Vector2D Normalize(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            return v / v.GetLength();
        }
        public static double Dot(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return v.X * v1.X
                 + v.Y * v1.Y;
        }
        public static Vector2D Abs(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Abs(v.X),
                       Math.Abs(v.Y));
        }
        public static Vector2D Ceiling(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Ceiling(v.X),
                       Math.Ceiling(v.Y));
        }
        public static Vector2D Floor(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Floor(v.X),
                       Math.Floor(v.Y));
        }
        public static Vector2D Max(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(Math.Max(v.X, v1.X),
                       Math.Max(v.Y, v1.Y));
        }
        public static Vector2D Min(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(Math.Min(v.X, v1.X),
                       Math.Min(v.Y, v1.Y));
        }
        public static Vector2D Clamp(Vector2D v, Vector2D min, Vector2D max)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(min));
            Debug.Assert(IsNaN(max));

            return new(Math.Clamp(v.X, min.X, max.X),
                       Math.Clamp(v.Y, min.Y, max.Y));
        }
        public static int MaxDimension(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            if(v.X > v.Y)
            {
                return 0;
            }else
            {
                return 1;
            }
        }
        public static Vector2D Permute(Vector2D p, int x, int y)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(!double.IsNaN(x));
            Debug.Assert(!double.IsNaN(y));

            return new Vector2D(p[x], p[y]);
        }
        public static Vector2D SquareRoot(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Sqrt(v.X),
                       Math.Sqrt(v.Y));
        }
        public static explicit operator Point2D(Vector2D a)
        {
            Debug.Assert(IsNaN(a));

            return new Point2D() { X = a.X, Y = a.Y };
        }

        //overrides +
        public static Vector2D operator +(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(v.X + v1.X,
                       v.Y + v1.Y);
        }
        public static Vector2D operator +(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            return new(+v.X, +v.Y);
        }

        //overrides -
        public static Vector2D operator -(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(v.X - v1.X,
                       v.Y - v1.Y);
        }
        public static Vector2D operator -(Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            return new(-v.X, -v.Y);
        }

        //overrides *
        public static Vector2D operator *(Vector2D v, Vector2D v1) {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(v.X * v1.X,
                       v.Y * v1.Y);
        }
        public static Vector2D operator *(Vector2D v, double d)
        {
            Debug.Assert(IsNaN(v));

            return new(v.X * d,
                       v.Y * d);
        }
        public static Vector2D operator *(double d, Vector2D v) {
            return v * d;
        }

        //overrides /
        public static Vector2D operator /(Vector2D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(d != 0);

            double inv = (double)1 / d;

            return new(v.X * inv,
                       v.Y * inv);
        }


        //overrides ==
        public static bool operator ==(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X.Equals(v1.X) && v.Y.Equals(v1.Y))
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Vector2D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (v.X == d && v.Y == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Vector2D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (d == v.X && d == v.Y)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v == v1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector2D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (v == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Vector2D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (d == v)
            {
                return false;
            }
            return true;
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

                throw new IndexOutOfRangeException();
            }
        }

        //overrides ToString
        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is not Vector2D other)
            {
                return false;
            }

            return this == other;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
