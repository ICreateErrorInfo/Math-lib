using System;
using System.Diagnostics;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Math_lib
{
    public readonly struct Point2D
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }

        //Constructors
        public Point2D(double x, double y)
        {
                X = x;
                Y = y;
        }
        public Point2D(double i)
        {
                X = Y = i;
        }
        public Point2D(Point2D p)
        {
            Debug.Assert(IsNaN(p));

            X = p.X;
            Y = p.Y;
        }

        //Methods
        public static bool IsNaN(Point2D v)
        {
            if (double.IsNaN(v.X) || double.IsNaN(v.Y))
            {
                return false;
                throw new ArgumentOutOfRangeException("NaN", "Number cant be NaN");
            }
            return true;
        }
        public static double Distance(Point2D p, Point2D p1)
        {
            return (p - p1).GetLength();
        }
        public static double DistanceSqrt(Point2D p, Point2D p1)
        {
            return (p - p1).GetLengthSqrt();
        }
        public static Point2D Lerp(double t, Point2D p, Point2D p1)
        {
            return (1 - t) * p + t * p1;
        }
        public static Point2D Min(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            return new Point2D(Math.Min(p.X, p1.X),
                             Math.Min(p.Y, p1.Y));
        }
        public static Point2D Max(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            return new Point2D(Math.Max(p.X, p1.X),
                              Math.Max(p.Y, p1.Y));
        }
        public static Point2D Floor(Point2D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(Math.Floor(p.X),
                              Math.Floor(p.Y));
        }
        public static Point2D Ceil(Point2D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(Math.Ceiling(p.X),
                              Math.Ceiling(p.Y));
        }
        public static Point2D Abs(Point2D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(Math.Abs(p.X),
                              Math.Abs(p.Y));
        }
        public static Point2D Permute(Point2D p, int x, int y)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(p[x], p[y]);
        }
        public static explicit operator Vector2D(Point2D a)
        {
            Debug.Assert(IsNaN(a));

            return new Vector2D() { X = a.X, Y = a.Y };
        }


        //overrides +
        public static Point2D operator +(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            return new Point2D(p.X + p1.X,
                              p.Y + p1.Y);
        }
        public static Point2D operator +(Point2D p, double p1)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(p.X + p1,
                              p.Y + p1);
        }
        public static Point2D operator +(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(p.X + d,
                              p.Y + d);
        }
        public static Point2D operator +(Point2D p, Vector2D v)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(Vector2D.IsNaN(v));

            return new(p.X + v.X, p.Y + v.Y);
        }
        public static Point2D operator +(Point2D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(+p.X, +p.Y);
        }

        //overrides -
        public static Vector2D operator -(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            return new Vector2D(p.X - p1.X,
                               p.Y - p1.Y);
        }
        public static Point2D operator -(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(p.X - d,
                              p.Y - d);
        }
        public static Point2D operator -(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(p.X - d,
                              p.Y - d);
        }
        public static Point2D operator -(Point2D p, Vector2D v)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(Vector2D.IsNaN(v));

            return new(p.X - v.X, p.Y - v.Y);
        }
        public static Point2D operator -(Point2D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(-p.X, -p.Y);
        }

        //overrides *
        public static Point2D operator *(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            return new Point2D(p.X * p1.X,
                              p.Y * p1.Y );
        }
        public static Point2D operator *(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(p.X * d,
                              p.Y * d);
        }
        public static Point2D operator *(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            return new Point2D(p.X * d,
                              p.Y * d);
        }
        public static Point2D operator *(Point2D p, Vector2D v)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(Vector2D.IsNaN(v));

            return new(p.X * v.X, p.Y * v.Y);
        }

        //overrides /
        public static Point2D operator /(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));
            Debug.Assert(p1.X != 0 || p1.Y != 0);

            return new Point2D(p.X / p1.X,
                              p.Y / p1.Y);
        }
        public static Point2D operator /(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(d != 0);

            double inv = (double)1 / d;

            return new Point2D(p.X * inv,
                               p.Y * inv);
        }
        public static Point2D operator /(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(p.X != 0 || p.Y != 0);

            return new Point2D(p.X / d,
                              p.Y / d);
        }
        public static Point2D operator /(Point2D p, Vector2D v)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(Vector2D.IsNaN(v));
            Debug.Assert(v.X != 0 || v.Y != 0);

            return new(p.X / v.X, p.Y / v.Y);
        }

        //overrides >
        public static bool operator >(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X > p1.X && p.Y > p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X > d && p.Y > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            if (d > p.X && d > p.Y)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X < p1.X && p.Y < p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X < d && p.Y < d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            if (d < p.X && d < p.Y)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X == p1.X && p.Y == p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X == d && p.Y == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            if (d == p.X && d == p.Y)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X == p1.X && p.Y == p1.Y)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X == d && p.Y == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            if (d == p.X && d == p.Y)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X <= p1.X && p.Y <= p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X <= d && p.Y <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            if (d <= p.X && d <= p.Y)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Point2D p, Point2D p1)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(IsNaN(p1));

            if (p.X >= p1.X && p.Y >= p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point2D p, double d)
        {
            Debug.Assert(IsNaN(p));

            if (p.X >= d && p.Y >= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Point2D p)
        {
            Debug.Assert(IsNaN(p));

            if (d >= p.X && d >= p.Y)
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
            if (obj is not Point2D other)
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
