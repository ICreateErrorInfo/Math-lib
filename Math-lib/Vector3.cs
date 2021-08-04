using System;

namespace Math_lib
{
    public readonly struct Vector3
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }
        public double W { get; init; }


        //Constructors
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
        }
        public Vector3(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector3(double i)
        {
            X = Y = Z = i;
            W = 1;
        }
        public Vector3(Vector3 v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = v.W;
        }
        public Vector3(Point3 p)
        {
            X = p.X;
            Y = p.Y;
            Z = p.Z;
            W = p.W;
        }
        public Vector3(Normal n)
        {
            X = n.X;
            Y = n.Y;
            Z = n.Z;
            W = 1;
        }
        public Vector3(Point2 p)
        {
            X = p.X;
            Y = p.Y;
            Z = 0;
            W = 1;
        }
        public Vector3(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
            Z = 0;
            W = 1;
        }


        //Mehods
        public double GetLength()
        {
            return Math.Sqrt(GetLengthSqrt());
        }
        public double GetLengthSqrt()
        {
            return X * X + Y * Y + Z * Z;
        }
        public static double Dot(Vector3 v, Vector3 v1)
        {
            return v.X * v1.X
                 + v.Y * v1.Y
                 + v.Z * v1.Z;
        }
        public static Vector3 Cross(Vector3 v, Vector3 v1)
        {
            return new(v.Y * v1.Z - v.Z * v1.Y,
                       v.Z * v1.X - v.X * v1.Z,
                       v.X * v1.Y - v.Y * v1.X);
        }
        public static Vector3 UnitVector(Vector3 v)
        {
            return v / v.GetLength();
        }
        public static Vector3 Abs(Vector3 v)
        {
            return new(Math.Abs(v.X),
                       Math.Abs(v.Y),
                       Math.Abs(v.Z));
        }
        public static Vector3 Ceiling(Vector3 v)
        {
            return new(Math.Ceiling(v.X),
                       Math.Ceiling(v.Y),
                       Math.Ceiling(v.Z));
        }
        public static Vector3 Floor(Vector3 v)
        {
            return new(Math.Floor(v.X),
                       Math.Floor(v.Y),
                       Math.Floor(v.Z));
        }
        public static Vector3 Max(Vector3 v, Vector3 v1)
        {
            return new(Math.Max(v.X, v1.X),
                       Math.Max(v.Y, v1.Y),
                       Math.Max(v.Z, v1.Z));
        }
        public static Vector3 Min(Vector3 v, Vector3 v1)
        {
            return new(Math.Min(v.X, v1.X),
                       Math.Min(v.Y, v1.Y),
                       Math.Min(v.Z, v1.Z));
        }
        public static Vector3 Clamp(Vector3 v, Vector3 min, Vector3 max)
        {
            return new(Math.Clamp(v.X, min.X, min.X),
                       Math.Clamp(v.Y, min.Y, min.Y),
                       Math.Clamp(v.Z, min.Z, min.Z));
        }
        public static Vector3 SquareRoot(Vector3 v)
        {
            return new(Math.Sqrt(v.X),
                       Math.Sqrt(v.Y),
                       Math.Sqrt(v.Z));
        }
        public static Vector3 Reflect(Vector3 d, Vector3 n)
        {
            return d - 2 * Dot(d, n) * n;
        }
        public static Vector3 Refract(Vector3 i, Vector3 n, double eta)
        {
            double cosi = Dot(-i, n);
            double cost2 = 1 - eta * eta * (1 - cosi * cosi);
            Vector3 t = eta * i + (eta * cosi - Math.Sqrt(Math.Abs(cost2)) * n);
            if (cost2 > 0)
            {
                return t * 1;
            }
            else
            {
                return new Vector3(0);
            }
        }


        //overrides +
        public static Vector3 operator +(Vector3 v, Vector3 v1)
        {
            return new(v.X + v1.X,
                       v.Y + v1.Y,
                       v.Z + v1.Z);
        }
        public static Vector3 operator +(Vector3 v, double v1)
        {
            return new(v.X + v1,
                       v.Y + v1,
                       v.Z + v1);
        }
        public static Vector3 operator +(double v1, Vector3 v)
        {
            return new(v.X + v1,
                       v.Y + v1,
                       v.Z + v1);
        }
        public static Vector3 operator +(Vector3 v, Normal n)
        {
            return new(v.X + n.X,
                       v.Y + n.Y,
                       v.Z + n.Z);
        }
        public static Vector3 operator +(Vector3 v)
        {
            return new(+v.X, +v.Y, +v.Z);
        }

        //overrides -
        public static Vector3 operator -(Vector3 v, Vector3 v1)
        {
            return new(v.X - v1.X,
                       v.Y - v1.Y,
                       v.Z - v1.Z);
        }
        public static Vector3 operator -(Vector3 v, double v1)
        {
            return new(v.X - v1,
                       v.Y - v1,
                       v.Z - v1);
        }
        public static Vector3 operator -(double v1, Vector3 v)
        {
            return new(v.X - v1,
                       v.Y - v1,
                       v.Z - v1);
        }
        public static Vector3 operator -(Vector3 v, Normal n)
        {
            return new(v.X - n.X,
                       v.Y - n.Y,
                       v.Z - n.Z);
        }
        public static Vector3 operator -(Vector3 v)
        {
            return new(-v.X, -v.Y, -v.Z);
        }

        //overrides *
        public static Vector3 operator *(Vector3 v, Vector3 v1)
        {
            return new(v.X * v1.X,
                       v.Y * v1.Y,
                       v.Z * v1.Z);
        }
        public static Vector3 operator *(Vector3 v, double v1)
        {
            return new(v.X * v1,
                       v.Y * v1,
                       v.Z * v1);
        }
        public static Vector3 operator *(double v1, Vector3 v)
        {
            return new(v.X * v1,
                       v.Y * v1,
                       v.Z * v1);
        }
        public static Vector3 operator *(Vector3 v, Point3 p)
        {
            return new(v.X * p.X,
                       v.Y * p.Y,
                       v.Z * p.Z);
        }
        public static Vector3 operator *(Vector3 v, Normal n)
        {
            return new(v.X * n.X,
                       v.Y * n.Y,
                       v.Z * n.Z);
        }

        //overrides /
        public static Vector3 operator /(Vector3 v, Vector3 v1)
        {
            return new(v.X / v1.X,
                       v.Y / v1.Y,
                       v.Z / v1.Z);
        }
        public static Vector3 operator /(Vector3 v, double v1)
        {
            return new(v.X / v1,
                       v.Y / v1,
                       v.Z / v1);
        }
        public static Vector3 operator /(double v1, Vector3 v)
        {
            return new(v.X / v1,
                       v.Y / v1,
                       v.Z / v1);
        }
        public static Vector3 operator /(Vector3 v, Point3 p)
        {
            return new(v.X / p.X,
                       v.Y / p.Y,
                       v.Z / p.Z);
        }
        public static Vector3 operator /(Vector3 v, Normal n)
        {
            return new(v.X / n.X,
                       v.Y / n.Y,
                       v.Z / n.Z);
        }

        //overrides >
        public static bool operator >(Vector3 v, Vector3 v1)
        {
            if (v.X > v1.X && v.Y > v1.Y && v.Z > v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector3 v, double v1)
        {
            if (v.X > v1 && v.Y > v1 && v.Z > v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double v, Vector3 v1)
        {
            if (v > v1.X && v > v1.Y && v > v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector3 v, Point3 v1)
        {
            if (v.X > v1.X && v.Y > v1.Y && v.Z > v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(Vector3 v, Normal n)
        {
            if (v.X > n.X && v.Y > n.Y && v.Z > n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides <
        public static bool operator <(Vector3 v, Vector3 v1)
        {
            if (v.X < v1.X && v.Y < v1.Y && v.Z < v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector3 v, double v1)
        {
            if (v.X < v1 && v.Y < v1 && v.Z < v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(double v, Vector3 v1)
        {
            if (v < v1.X && v < v1.Y && v < v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector3 v, Point3 v1)
        {
            if (v.X < v1.X && v.Y < v1.Y && v.Z < v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <(Vector3 v, Normal n)
        {
            if (v.X < n.X && v.Y < n.Y && v.Z < n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides ==
        public static bool operator ==(Vector3 v, Vector3 v1)
        {
            if (v.X.Equals(v1.X) && v.Y.Equals(v1.Y) && v.Z.Equals(v1.Z))
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Vector3 v, double v1)
        {
            if (v.X == v1 && v.Y == v1 && v.Z == v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double v, Vector3 v1)
        {
            if (v == v1.X && v == v1.Y && v == v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Vector3 v, Point3 v1)
        {
            if (v.X == v1.X && v.Y == v1.Y && v.Z == v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(Vector3 v, Normal n)
        {
            if (v.X == n.X && v.Y == n.Y && v.Z == n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides !=
        public static bool operator !=(Vector3 v, Vector3 v1)
        {
            if (v.X == v1.X && v.Y == v1.Y && v.Z == v1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector3 v, double v1)
        {
            if (v.X == v1 && v.Y == v1 && v.Z == v1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double v, Vector3 v1)
        {
            if (v == v1.X && v == v1.Y && v == v1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector3 v, Point3 v1)
        {
            if (v.X == v1.X && v.Y == v1.Y && v.Z == v1.Z)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector3 v, Normal n)
        {
            if (v.X == n.X && v.Y == n.Y && v.Z == n.Z)
            {
                return false;
            }
            return true;
        }

        //overides <=
        public static bool operator <=(Vector3 v, Vector3 v1)
        {
            if (v.X <= v1.X && v.Y <= v1.Y && v.Z <= v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector3 v, double v1)
        {
            if (v.X <= v1 && v.Y <= v1 && v.Z <= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double v, Vector3 v1)
        {
            if (v <= v1.X && v <= v1.Y && v <= v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector3 v, Point3 v1)
        {
            if (v.X <= v1.X && v.Y <= v1.Y && v.Z <= v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(Vector3 v, Normal n)
        {
            if (v.X <= n.X && v.Y <= n.Y && v.Z <= n.Z)
            {
                return true;
            }
            return false;
        }

        //overrides >=
        public static bool operator >=(Vector3 v, Vector3 v1)
        {
            if (v.X >= v1.X && v.Y >= v1.Y && v.Z >= v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector3 v, double v1)
        {
            if (v.X >= v1 && v.Y >= v1 && v.Z >= v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double v, Vector3 v1)
        {
            if (v >= v1.X && v >= v1.Y && v >= v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector3 v, Point3 v1)
        {
            if (v.X >= v1.X && v.Y >= v1.Y && v.Z >= v1.Z)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector3 v, Normal n)
        {
            if (v.X >= n.X && v.Y >= n.Y && v.Z >= n.Z)
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
            if (obj is not Vector3 other)
            {
                return false;
            }

            return this == other;
        }

        public override int GetHashCode() {
            return HashCode.Combine(X, Y, Z);
        }
    }
}
