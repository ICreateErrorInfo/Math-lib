using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public readonly struct Vector2
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }

        //Constructors
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Vector2(double i)
        {
            X = Y = i;
        }
        public Vector2(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
        }
        public Vector2(Vector3 v)
        {
            X = v.X;
            Y = v.Y;
        }
        public Vector2(Point2 p)
        {
            X = p.X;
            Y = p.Y;
        }
        public Vector2(Point3 p)
        {
            X = p.X;
            Y = p.Y;
        }

        //Methods
        public double GetLength()
        {
            return Math.Sqrt(GetLengthSqrt());
        }
        public double GetLengthSqrt()
        {
            return X * X + Y * Y;
        }
        public static double Dot(Vector2 v, Vector2 v1)
        {
            return v.X * v1.X
                 + v.Y * v1.Y;
        }
        public static Vector2 UnitVector(Vector2 v)
        {
            return v / v.GetLength();
        }
        public static Vector2 Abs(Vector2 v)
        {
            return new(Math.Abs(v.X),
                       Math.Abs(v.Y));
        }
        public static Vector2 Ceiling(Vector2 v)
        {
            return new(Math.Ceiling(v.X),
                       Math.Ceiling(v.Y));
        }
        public static Vector2 Floor(Vector2 v)
        {
            return new(Math.Floor(v.X),
                       Math.Floor(v.Y));
        }
        public static Vector2 Max(Vector2 v, Vector2 v1)
        {
            return new(Math.Max(v.X, v1.X),
                       Math.Max(v.Y, v1.Y));
        }
        public static Vector2 Min(Vector2 v, Vector2 v1)
        {
            return new(Math.Min(v.X, v1.X),
                       Math.Min(v.Y, v1.Y));
        }
        public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max)
        {
            return new(Math.Clamp(v.X, min.X, min.X),
                       Math.Clamp(v.Y, min.Y, min.Y));
        }
        public static Vector2 SquareRoot(Vector2 v)
        {
            return new(Math.Sqrt(v.X),
                       Math.Sqrt(v.Y));
        }

        //overrides +
        public static Vector2 operator +(Vector2 v, Vector2 v1)
        {
            return new(v.X + v1.X,
                       v.Y + v1.Y);
        }
        public static Vector2 operator +(Vector2 v, double v1)
        {
            return new(v.X + v1,
                       v.Y + v1);
        }
        public static Vector2 operator +(double v1, Vector2 v)
        {
            return new(v.X + v1,
                       v.Y + v1);
        }
        public static Vector2 operator +(Vector2 v)
        {
            return new(+v.X, +v.Y);
        }

        //overrides -
        public static Vector2 operator -(Vector2 v, Vector2 v1)
        {
            return new(v.X - v1.X,
                       v.Y - v1.Y);
        }
        public static Vector2 operator -(Vector2 v, double v1)
        {
            return new(v.X - v1,
                       v.Y - v1);
        }
        public static Vector2 operator -(double v1, Vector2 v)
        {
            return new(v.X - v1,
                       v.Y - v1);
        }
        public static Vector2 operator -(Vector2 v)
        {
            return new(-v.X, -v.Y);
        }

        //overrides *
        public static Vector2 operator *(Vector2 v, Vector2 v1)
        {
            return new(v.X * v1.X,
                       v.Y * v1.Y);
        }
        public static Vector2 operator *(Vector2 v, double v1)
        {
            return new(v.X * v1,
                       v.Y * v1);
        }
        public static Vector2 operator *(double v1, Vector2 v)
        {
            return new(v.X * v1,
                       v.Y * v1);
        }
        public static Vector2 operator *(Vector2 v, Point2 p)
        {
            return new(v.X * p.X,
                       v.Y * p.Y);
        }

        //overrides /
        public static Vector2 operator /(Vector2 v, Vector2 v1)
        {
            return new(v.X / v1.X,
                       v.Y / v1.Y);
        }
        public static Vector2 operator /(Vector2 v, double v1)
        {
            return new(v.X / v1,
                       v.Y / v1);
        }
        public static Vector2 operator /(double v1, Vector2 v)
        {
            return new(v.X / v1,
                       v.Y / v1);
        }
        public static Vector2 operator /(Vector2 v, Point2 p)
        {
            return new(v.X / p.X,
                       v.Y / p.Y);
        }

        //overrides >
        public static bool operator >(Vector2 v, Vector2 v1)
        {
            if (v.X > v1.X && v.Y > v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector2 v, double v1)
        {
            if (v.X > v1 && v.Y > v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double v, Vector2 v1)
        {
            if (v > v1.X && v > v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector2 v, Point2 p)
        {
            if (v.X > p.X && v.Y > p.Y)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Vector2 v, Vector2 v1)
        {
            if (v.X < v1.X && v.Y < v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector2 v, double v1)
        {
            if (v.X < v1 && v.Y < v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double v, Vector2 v1)
        {
            if (v < v1.X && v < v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector2 v, Point2 p)
        {
            if (v.X < p.X && v.Y < p.Y)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Vector2 v, Vector2 v1)
        {
            if (v.X.Equals(v1.X) && v.Y.Equals(v1.Y))
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Vector2 v, double v1)
        {
            if (v.X == v1 && v.Y == v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double v, Vector2 v1)
        {
            if (v == v1.X && v == v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Vector2 v, Point2 p)
        {
            if (v.X == p.X && v.Y == p.Y)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Vector2 v, Vector2 v1)
        {
            if (v.X == v1.X && v.Y == v1.Y)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector2 v, double v1)
        {
            if (v.X == v1 && v.Y == v1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double v, Vector2 v1)
        {
            if (v == v1.X && v == v1.Y)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector2 v, Point2 p)
        {
            if (v.X == p.X && v.Y == p.Y)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Vector2 v, Vector2 v1)
        {
            if (v.X <= v1.X && v.Y <= v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector2 v, double v1)
        {
            if (v.X <= v1 && v.Y <= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double v, Vector2 v1)
        {
            if (v <= v1.X && v <= v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector2 v, Point2 p)
        {
            if (v.X <= p.X && v.Y <= p.Y)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Vector2 v, Vector2 v1)
        {
            if (v.X >= v1.X && v.Y >= v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector2 v, double v1)
        {
            if (v.X >= v1 && v.Y >= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double v, Vector2 v1)
        {
            if (v >= v1.X && v >= v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector2 v, Point2 p)
        {
            if (v.X >= p.X && v.Y >= p.Y)
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
            if (obj is not Vector2 other)
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
