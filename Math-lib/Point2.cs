using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public readonly struct Point2
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }

        //Constructors
        public Point2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point2(double i)
        {
            X = Y = i;
        }
        public Point2(Point2 p)
        {
            X = p.X;
            Y = p.Y;
        }
        public Point2(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
        }
        public Point2(Point3 p)
        {
            X = p.X;
            Y = p.Y;
        }
        public Point2(Vector3 v)
        {
            X = v.X;
            Y = v.Y;
        }

        //Methods
        public static double Distance(Point2 p, Point2 p1)
        {
            return new Vector2(p - p1).GetLength();
        }
        public static double DistanceSqrt(Point2 p, Point2 p1)
        {
            return new Vector2(p - p1).GetLengthSqrt();
        }
        public static Point2 Lerp(double t, Point2 p, Point2 p1)
        {
            return (1 - t) * p + t * p1;
        }
        public static Point2 Min(Point2 p, Point2 p1)
        {
            return new Point2(Math.Min(p.X, p1.X),
                             Math.Min(p.Y, p1.Y));
        }
        public static Point2 Max(Point2 p, Point2 p1)
        {
            return new Point2(Math.Max(p.X, p1.X),
                              Math.Max(p.Y, p1.Y));
        }
        public static Point2 Floor(Point2 p)
        {
            return new Point2(Math.Floor(p.X),
                              Math.Floor(p.Y));
        }
        public static Point2 Ceil(Point2 p)
        {
            return new Point2(Math.Ceiling(p.X),
                              Math.Ceiling(p.Y));
        }
        public static Point2 Abs(Point2 p)
        {
            return new Point2(Math.Abs(p.X),
                              Math.Abs(p.Y));
        }
        public static Point2 Permute(Point2 p, int x, int y, int z)
        {
            return new Point2(p[x], p[y]);
        }


        //overrides +
        public static Point2 operator +(Point2 p, Point2 p1)
        {
            return new Point2(p.X + p1.X,
                              p.Y + p1.Y);
        }
        public static Point2 operator +(Point2 p, double p1)
        {
            return new Point2(p.X + p1,
                              p.Y + p1);
        }
        public static Point2 operator +(double d, Point2 p)
        {
            return new Point2(p.X + d,
                              p.Y + d);
        }
        public static Point2 operator +(Point2 p, Vector2 v)
        {
            return new Point2(p.X + v.X,
                              p.Y + v.Y);
        }
        public static Point2 operator +(Vector2 v, Point2 p)
        {
            return new Point2(v.X + p.X,
                              v.Y + p.Y);
        }
        public static Point2 operator +(Point2 p)
        {
            return new Point2(+p.X, +p.Y);
        }

        //overrides -
        public static Vector2 operator -(Point2 p, Point2 p1)
        {
            return new Vector2(p.X - p1.X,
                               p.Y - p1.Y);
        }
        public static Point2 operator -(Point2 p, double d)
        {
            return new Point2(p.X - d,
                              p.Y - d);
        }
        public static Point2 operator -(double d, Point2 p)
        {
            return new Point2(p.X - d,
                              p.Y - d);
        }
        public static Point2 operator -(Point2 p, Vector2 v)
        {
            return new Point2(p.X - v.X,
                              p.Y - v.Y);
        }
        public static Point2 operator -(Vector3 v, Point2 p)
        {
            return new Point2(v.X - p.X,
                              v.Y - p.Y);
        }
        public static Point2 operator -(Point2 p)
        {
            return new Point2(-p.X, -p.Y);
        }

        //overrides *
        public static Point2 operator *(Point2 p, Point2 p1)
        {
            return new Point2(p.X * p1.X,
                              p.Y * p1.Y );
        }
        public static Point2 operator *(Point2 p, double d)
        {
            return new Point2(p.X * d,
                              p.Y * d);
        }
        public static Point2 operator *(double d, Point2 p)
        {
            return new Point2(p.X * d,
                              p.Y * d);
        }
        public static Point2 operator *(Point2 p, Vector2 v)
        {
            return new Point2(p.X * v.X,
                              p.Y * v.Y);
        }

        //overrides /
        public static Point2 operator /(Point2 p, Point2 p1)
        {
            return new Point2(p.X / p1.X,
                              p.Y / p1.Y);
        }
        public static Point2 operator /(Point2 p, double d)
        {
            return new Point2(p.X / d,
                              p.Y / d);
        }
        public static Point2 operator /(double d, Point2 p)
        {
            return new Point2(p.X / d,
                              p.Y / d);
        }
        public static Point2 operator /(Point2 p, Vector2 v)
        {
            return new Point2(p.X / v.X,
                              p.Y / v.Y);
        }

        //overrides >
        public static bool operator >(Point2 p, Point2 p1)
        {
            if (p.X > p1.X && p.Y > p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point2 p, double d)
        {
            if (p.X > d && p.Y > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Point2 p)
        {
            if (d > p.X && d > p.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Point2 p, Vector2 v)
        {
            if (p.X > v.X && p.Y > v.Y)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Point2 p, Point2 p1)
        {
            if (p.X < p1.X && p.Y < p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point2 p, double d)
        {
            if (p.X < d && p.Y < d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double d, Point2 p)
        {
            if (d < p.X && d < p.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Point2 p, Vector2 v)
        {
            if (p.X < v.X && p.Y < v.Y)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Point2 p, Point2 p1)
        {
            if (p.X == p1.X && p.Y == p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point2 p, double d)
        {
            if (p.X == d && p.Y == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Point2 p)
        {
            if (d == p.X && d == p.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Point2 p, Vector2 v)
        {
            if (p.X == v.X && p.Y == v.Y)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Point2 p, Point2 p1)
        {
            if (p.X == p1.X && p.Y == p1.Y)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point2 p, double d)
        {
            if (p.X == d && p.Y == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Point2 p)
        {
            if (d == p.X && d == p.Y)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Point2 p, Vector2 v)
        {
            if (p.X == v.X && p.Y == v.Y)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Point2 p, Point2 p1)
        {
            if (p.X <= p1.X && p.Y <= p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point2 p, double d)
        {
            if (p.X <= d && p.Y <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Point2 p)
        {
            if (d <= p.X && d <= p.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Point2 p, Vector2 v)
        {
            if (p.X <= v.X && p.Y <= v.Y )
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Point2 p, Point2 p1)
        {
            if (p.X >= p1.X && p.Y >= p1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point2 p, double d)
        {
            if (p.X >= d && p.Y >= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Point2 p)
        {
            if (d >= p.X && d >= p.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Point2 p, Vector2 v)
        {
            if (p.X >= v.X && p.Y >= v.Y)
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

                return 0;
            }
        }

        //overrides ToString
        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is not Point2 other)
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
