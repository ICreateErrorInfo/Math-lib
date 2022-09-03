using System;
using System.Diagnostics;
using System.Drawing;

// ReSharper disable CompareOfFloatsByEqualityOperator
namespace Math_lib
{
    public readonly struct Vector3D
    {
        //Properties
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }


        //Ctors
        public Vector3D(double x, double y, double z)
        {
            Debug.Assert(!double.IsNaN(x) || !double.IsNaN(y) || !double.IsNaN(z));

            X = x;
            Y = y;
            Z = z;
        }
        public Vector3D(double i)
        {
            Debug.Assert(!double.IsNaN(i));

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
            if (!double.IsNaN(v.X) || !double.IsNaN(v.Y) || !double.IsNaN(v.Z))
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
            Debug.Assert(!double.IsNaN(X));
            Debug.Assert(!double.IsNaN(Y));
            Debug.Assert(!double.IsNaN(Z));

            return X * X + Y * Y + Z * Z;
        }
        public static Vector3D Normalize(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return v / v.GetLength();
        }
        public static double Dot(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return v.X * v1.X
                 + v.Y * v1.Y
                 + v.Z * v1.Z;
        }
        public static Vector3D Cross(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return new((v.Y * v1.Z) - (v.Z * v1.Y),
                       (v.Z * v1.X) - (v.X * v1.Z),
                       (v.X * v1.Y) - (v.Y * v1.X));
        }
        public static Vector3D Abs(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Abs(v.X),
                       Math.Abs(v.Y),
                       Math.Abs(v.Z) );
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
        public static int MaxDimension(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return (v.X > v.Y) ? ((v.X > v.Z) ? 0 : 2) : ((v.Y > v.Z) ? 1 : 2);
        }
        public static Vector3D Permute(Vector3D p, int x, int y, int z)
        {
            Debug.Assert(IsNaN(p));
            Debug.Assert(!double.IsNaN(x));
            Debug.Assert(!double.IsNaN(y));
            Debug.Assert(!double.IsNaN(z));

            return new Vector3D(p[x], p[y], p[z]);
        }
        public static Vector3D SquareRoot(Vector3D v)
        {
            Debug.Assert(IsNaN(v));

            return new(Math.Sqrt(v.X),
                       Math.Sqrt(v.Y),
                       Math.Sqrt(v.Z));
        }
        public static Vector3D Reflect(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));

            return v - 2 * Dot(v, v1) * v1;
        }
        public static Vector3D Refract(Vector3D v, Vector3D v1, double eta)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));
            Debug.Assert(!double.IsNaN(eta));

            double cosi = Dot(-v, v1);
            double cost2 = 1 - eta * eta * (1 - cosi * cosi);
            Vector3D t = eta * v + (eta * cosi - Math.Sqrt(Math.Abs(cost2)) * v1);
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
        public Color ToColor(int samplesPerPixel = 1)
        {
            Debug.Assert(!double.IsNaN(samplesPerPixel));

            if (this <= 255)
            {
                byte max = byte.MaxValue;
                double r = X;
                double g = Y;
                double b = Z;

                double scale = (double)1 / samplesPerPixel;
                r *= scale;
                g *= scale;
                b *= scale;

                return Color.FromArgb(max, (int)(256 * Math.Clamp(r, 0, 0.999)), (int)(256 * Math.Clamp(g, 0, 0.999)), (int)(256 * Math.Clamp(b, 0, 0.999)));
            }
            throw new Exception("Number to Big");
        }
        public static Vector3D Random(int min, int max)
        {
            Debug.Assert(!double.IsNaN(min));
            Debug.Assert(!double.IsNaN(max));

            Random r = new Random();
            return new Vector3D(r.NextDouble() * (max - min) + min, r.NextDouble() * (max - min) + min, r.NextDouble() * (max - min) + min);
        }
        public static Vector3D RandomInUnitSphere()
        {
            while (true)
            {
                Vector3D p = Random(-1, 1);
                if (p.GetLengthSqrt() >= 1) continue;
                return p;
            }
        }
        public bool NearZero()
        {
            var s = 1e-8;
            return (Math.Abs(X) < s) && (Math.Abs(Y) < s) && (Math.Abs(Z) < s);
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
        public static Vector3D operator +(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            return new(v.X + d,
                       v.Y + d,
                       v.Z + d);
        }
        public static Vector3D operator +(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            return new(v.X + d,
                       v.Y + d,
                       v.Z + d);
        }
        public static Vector3D operator +(Vector3D v, Point3D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point3D.IsNaN(p));

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
        public static Vector3D operator -(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            return new(v.X - d,
                       v.Y - d,
                       v.Z - d);
        }
        public static Vector3D operator -(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            return new(d - v.X,
                       d - v.Y,
                       d - v.Z);
        }
        public static Vector3D operator -(Vector3D v, Point3D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point3D.IsNaN(p));

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
        public static Vector3D operator *(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            return new(v.X * d,
                       v.Y * d,
                       v.Z * d);
        }
        public static Vector3D operator *(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            return new(v.X * d,
                       v.Y * d,
                       v.Z * d);
        }
        public static Vector3D operator *(Vector3D v, Point3D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point3D.IsNaN(p));

            return new(v.X * p.X, v.Y * p.Y, v.Z * p.Z);
        }

        //overrides /
        public static Vector3D operator /(Vector3D v, Vector3D v1)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(IsNaN(v1));
            Debug.Assert(v1.X != 0 || v1.Y != 0 || v1.Z != 0);

            return new(v.X / v1.X,
                       v.Y / v1.Y,
                       v.Z / v1.Z);
        }
        public static Vector3D operator /(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));
            Debug.Assert(d != 0);

            double inv = (double)1 / d;

            return new(v.X * inv,
                       v.Y * inv,
                       v.Z * inv);
        }
        public static Vector3D operator /(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));
            Debug.Assert(v.X != 0 || v.Y != 0 || v.Z != 0);

            return new(d / v.X,
                       d / v.Y,
                       d / v.Z);
        }
        public static Vector3D operator /(Vector3D v, Point3D p)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(Point3D.IsNaN(p));
            Debug.Assert(p.X != 0 || p.Y != 0 || p.Z != 0);

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
        public static bool operator >(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (v.X > d && v.Y > d && v.Z > d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (d > v.X && d > v.Y && d > v.Z)
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

            if (v > v1)
            {
                return false;
            }
            return true;
        }
        public static bool operator <(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (v > d)
            {
                return false;
            }
            return true;
        }
        public static bool operator <(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (d > v)
            {
                return false;
            }
            return true;
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
        public static bool operator ==(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (v.X == d && v.Y == d && v.Z == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator ==(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

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

            if (v == v1)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (v == d)
            {
                return false;
            }
            return true;
        }
        public static bool operator !=(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (d == v)
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
        public static bool operator <=(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (v.X <= d && v.Y <= d && v.Z <= d)
            {
                return true;
            }
            return false;
        }
        public static bool operator <=(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (d <= v.X && d <= v.Y && d <= v.Z)
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

            if (v > v1 || v == v1)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(Vector3D v, double d)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (v > d || v == d)
            {
                return true;
            }
            return false;
        }
        public static bool operator >=(double d, Vector3D v)
        {
            Debug.Assert(IsNaN(v));
            Debug.Assert(!double.IsNaN(d));

            if (d > v || d == v)
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
