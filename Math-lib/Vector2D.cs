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

        //Constructors
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;

        }
        public Vector2D(double i)
        {

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
            if (double.IsNaN(v.X) || double.IsNaN(v.Y))
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
            return X * X + Y * Y;
        }
        public static Vector2D Normalize(Vector2D v)
        {
            return v / v.GetLength();
        }
        public static double Dot(Vector2D v, Vector2D v1)
        {
            return v.X * v1.X
                 + v.Y * v1.Y;
        }
        public static double AbsDot(Vector2D v, Vector2D v1)
        {
            return Math.Abs(Dot(v,v1));
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
        public static Vector2D operator +(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));

            return new(v.X + v1,
                       v.Y + v1);
        }
        public static Vector2D operator +(double v1, Vector2D v)
        {
            Debug.Assert(IsNaN(v));

            return new(v.X + v1,
                       v.Y + v1);
        }
        public static Vector2D operator +(Vector2D v, Point2D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point2D.IsNaN(p));

            return new(v.X + p.X, v.Y + p.Y);
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
        public static Vector2D operator -(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            return new(v.X - v1,
                       v.Y - v1);
        }
        public static Vector2D operator -(double v1, Vector2D v)
        {
            Debug.Assert(IsNaN(v));
            return new(v.X - v1,
                       v.Y - v1);
        }
        public static Vector2D operator -(Vector2D v, Point2D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point2D.IsNaN(p));

            return new(v.X - p.X, v.Y - p.Y);
        }
        public static Vector2D operator -(Vector2D v)
        {
            Debug.Assert(IsNaN(v));
            return new(-v.X, -v.Y);
        }

        //overrides *
        public static Vector2D operator *(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new(v.X * v1.X,
                       v.Y * v1.Y);
        }
        public static Vector2D operator *(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            return new(v.X * v1,
                       v.Y * v1);
        }
        public static Vector2D operator *(double v1, Vector2D v)
        {
            Debug.Assert(IsNaN(v));
            return new(v.X * v1,
                       v.Y * v1);
        }
        public static Vector2D operator *(Vector2D v, Point2D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point2D.IsNaN(p));

            return new(v.X * p.X, v.Y * p.Y);
        }

        //overrides /
        public static Vector2D operator /(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));
            Debug.Assert(v1.X == 0 || v1.Y == 0);

            return new(v.X / v1.X,
                       v.Y / v1.Y);
        }
        public static Vector2D operator /(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(v1 == 0);

            double inv = (double)1 / v1;

            return new(v.X * inv,
                       v.Y * inv);
        }
        public static Vector2D operator /(double v1, Vector2D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(v.X == 0 || v.Y == 0);

            return new(v1 / v.X,
                       v1 / v.Y);
        }
        public static Vector2D operator /(Vector2D v, Point2D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point2D.IsNaN(p));
            Debug.Assert(p.X == 0 || p.Y == 0);

            return new(v.X / p.X, v.Y / p.Y);
        }

        //overrides >
        public static bool operator >(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X > v1.X && v.Y > v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X > v1 && v.Y > v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v > v1.X && v > v1.Y)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X < v1.X && v.Y < v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X < v1 && v.Y < v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v < v1.X && v < v1.Y)
            {
                return true;
            }
            return false;
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
        public static bool operator ==(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X == v1 && v.Y == v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v == v1.X && v == v1.Y)
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

            if (v.X == v1.X && v.Y == v1.Y)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X == v1 && v.Y == v1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v == v1.X && v == v1.Y)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X <= v1.X && v.Y <= v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X <= v1 && v.Y <= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v <= v1.X && v <= v1.Y)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Vector2D v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            if (v.X >= v1.X && v.Y >= v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector2D v, double v1)
        {
            Debug.Assert(IsNaN(v));
            if (v.X >= v1 && v.Y >= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double v, Vector2D v1)
        {
            Debug.Assert(IsNaN(v1));
            if (v >= v1.X && v >= v1.Y)
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
