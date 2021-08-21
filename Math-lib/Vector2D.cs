using System;
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
            X = v.X;
            Y = v.Y;
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
        public static double Dot(Vector2D v, Vector2D v1)
        {
            return v.X * v1.X
                 + v.Y * v1.Y;
        }
        public Vector2D UnitVector()
        {
            return this / this.GetLength();
        }
        public static Vector2D Abs(Vector2D v)
        {
            return new(Math.Abs(v.X),
                       Math.Abs(v.Y));
        }
        public static Vector2D Ceiling(Vector2D v)
        {
            return new(Math.Ceiling(v.X),
                       Math.Ceiling(v.Y));
        }
        public static Vector2D Floor(Vector2D v)
        {
            return new(Math.Floor(v.X),
                       Math.Floor(v.Y));
        }
        public static Vector2D Max(Vector2D v, Vector2D v1)
        {
            return new(Math.Max(v.X, v1.X),
                       Math.Max(v.Y, v1.Y));
        }
        public static Vector2D Min(Vector2D v, Vector2D v1)
        {
            return new(Math.Min(v.X, v1.X),
                       Math.Min(v.Y, v1.Y));
        }
        public static Vector2D Clamp(Vector2D v, Vector2D min, Vector2D max)
        {
            return new(Math.Clamp(v.X, min.X, min.X),
                       Math.Clamp(v.Y, min.Y, min.Y));
        }
        public static Vector2D SquareRoot(Vector2D v)
        {
            return new(Math.Sqrt(v.X),
                       Math.Sqrt(v.Y));
        }
        public Point2D ToPoint()
        {
            return new(X, Y);
        }

        //overrides +
        public static Vector2D operator +(Vector2D v, Vector2D v1)
        {
            return new(v.X + v1.X,
                       v.Y + v1.Y);
        }
        public static Vector2D operator +(Vector2D v, double v1)
        {
            return new(v.X + v1,
                       v.Y + v1);
        }
        public static Vector2D operator +(double v1, Vector2D v)
        {
            return new(v.X + v1,
                       v.Y + v1);
        }
        public static Vector2D operator +(Vector2D v, Point2D p)
        {
            return new(v.X + p.X, v.Y + p.Y);
        }
        public static Vector2D operator +(Vector2D v)
        {
            return new(+v.X, +v.Y);
        }

        //overrides -
        public static Vector2D operator -(Vector2D v, Vector2D v1)
        {
            return new(v.X - v1.X,
                       v.Y - v1.Y);
        }
        public static Vector2D operator -(Vector2D v, double v1)
        {
            return new(v.X - v1,
                       v.Y - v1);
        }
        public static Vector2D operator -(double v1, Vector2D v)
        {
            return new(v.X - v1,
                       v.Y - v1);
        }
        public static Vector2D operator -(Vector2D v, Point2D p)
        {
            return new(v.X - p.X, v.Y - p.Y);
        }
        public static Vector2D operator -(Vector2D v)
        {
            return new(-v.X, -v.Y);
        }

        //overrides *
        public static Vector2D operator *(Vector2D v, Vector2D v1)
        {
            return new(v.X * v1.X,
                       v.Y * v1.Y);
        }
        public static Vector2D operator *(Vector2D v, double v1)
        {
            return new(v.X * v1,
                       v.Y * v1);
        }
        public static Vector2D operator *(double v1, Vector2D v)
        {
            return new(v.X * v1,
                       v.Y * v1);
        }
        public static Vector2D operator *(Vector2D v, Point2D p)
        {
            return new(v.X * p.X, v.Y * p.Y);
        }

        //overrides /
        public static Vector2D operator /(Vector2D v, Vector2D v1)
        {
            return new(v.X / v1.X,
                       v.Y / v1.Y);
        }
        public static Vector2D operator /(Vector2D v, double v1)
        {
            return new(v.X / v1,
                       v.Y / v1);
        }
        public static Vector2D operator /(double v1, Vector2D v)
        {
            return new(v.X / v1,
                       v.Y / v1);
        }
        public static Vector2D operator /(Vector2D v, Point2D p)
        {
            return new(v.X / p.X, v.Y / p.Y);
        }

        //overrides >
        public static bool operator >(Vector2D v, Vector2D v1)
        {
            if (v.X > v1.X && v.Y > v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector2D v, double v1)
        {
            if (v.X > v1 && v.Y > v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double v, Vector2D v1)
        {
            if (v > v1.X && v > v1.Y)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Vector2D v, Vector2D v1)
        {
            if (v.X < v1.X && v.Y < v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector2D v, double v1)
        {
            if (v.X < v1 && v.Y < v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double v, Vector2D v1)
        {
            if (v < v1.X && v < v1.Y)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Vector2D v, Vector2D v1)
        {
            if (v.X.Equals(v1.X) && v.Y.Equals(v1.Y))
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Vector2D v, double v1)
        {
            if (v.X == v1 && v.Y == v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double v, Vector2D v1)
        {
            if (v == v1.X && v == v1.Y)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Vector2D v, Vector2D v1)
        {
            if (v.X == v1.X && v.Y == v1.Y)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector2D v, double v1)
        {
            if (v.X == v1 && v.Y == v1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double v, Vector2D v1)
        {
            if (v == v1.X && v == v1.Y)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Vector2D v, Vector2D v1)
        {
            if (v.X <= v1.X && v.Y <= v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector2D v, double v1)
        {
            if (v.X <= v1 && v.Y <= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double v, Vector2D v1)
        {
            if (v <= v1.X && v <= v1.Y)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Vector2D v, Vector2D v1)
        {
            if (v.X >= v1.X && v.Y >= v1.Y)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector2D v, double v1)
        {
            if (v.X >= v1 && v.Y >= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double v, Vector2D v1)
        {
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
