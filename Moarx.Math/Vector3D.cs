using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math; 

public readonly record struct Vector3D<T>(T X, T Y, T Z) 
    where T : 
    IAdditionOperators<T, T, T>, 
    ISubtractionOperators<T, T, T>, 
    IMultiplyOperators<T, T, T>,
    INumber<T>, 
    IRootFunctions<T>
{
    public static Vector3D<T> CrossProduct(Vector3D<T> vector1, Vector3D<T> vector2) => new() {
        X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y),
        Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z),
        Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X)
    };
    public Vector3D<T> ToNormalized() => this / GetLength();
    public T GetLength() => T.Sqrt(GetLengthSquared());
    public T GetLengthSquared() => this * this;
    public static Vector3D<T> Abs(Vector3D<T> vector) => new Vector3D<T>
    {
        X = T.Abs(vector.X),
        Y = T.Abs(vector.Y),
        Z = T.Abs(vector.Z)
    };
    public static int MaxDimension(Vector3D<T> v) => (v.X > v.Y) ? ((v.X > v.Z) ? 0 : 2) : ((v.Y > v.Z) ? 1 : 2);
    public static Vector3D<T> Permute(Vector3D<T> p, int x, int y, int z)
    {
        return new Vector3D<T>(p[x], p[y], p[z]);
    }
    public static Vector3D<T> GetReflectionVector(Vector3D<T> v, Vector3D<T> v1) => v - T.CreateChecked(2) * v * v1 * v1;
    public Vector3D<T> Saturate()
    {
        T x = T.Min(T.CreateChecked(1), T.Max(T.CreateChecked(0), X));
        T y = T.Min(T.CreateChecked(1), T.Max(T.CreateChecked(0), Y));
        T z = T.Min(T.CreateChecked(1), T.Max(T.CreateChecked(0), Z));

        return new Vector3D<T>(x, y, z);
    }
    public static Vector3D<double> GetRandomVector(int min, int max)
    {
        Random r = new Random();
        return new Vector3D<double>(r.NextDouble() * (max - min) + min, r.NextDouble() * (max - min) + min, r.NextDouble() * (max - min) + min);
    }
    public static Vector3D<double> GetRandomVectorInUnitSphere()
    {
        while (true)
        {
            Vector3D<double> p = Random(-1, 1);
            if (p.GetLengthSquared() >= 1) continue;
            return p;
        }
    }

    //operator overload
    public static Vector3D<T> operator +(Vector3D<T> left, Vector3D<T> right) => new() {
        X = left.X + right.X,
        Y = left.Y + right.Y,
        Z = left.Z + right.Z
    };

    public static Vector3D<T> operator -(Vector3D<T> left, Vector3D<T> right) => new() {
        X = left.X - right.X,
        Y = left.Y - right.Y,
        Z = left.Z - right.Z
    };

    public static Vector3D<T> operator -(Vector3D<T> vector) => new() {
        X = -vector.X,
        Y = -vector.Y,
        Z = -vector.Z
    };

    public static T operator *(Vector3D<T> left, Vector3D<T> right) => (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
  
    public static Vector3D<T> operator *(Vector3D<T> left, T scalar) => new() {
        X = left.X * scalar,
        Y = left.Y * scalar,
        Z = left.Z * scalar
    };

    public static Vector3D<T> operator *(T scalar, Vector3D<T> right) => new() {
        X = right.X * scalar,
        Y = right.Y * scalar,
        Z = right.Z * scalar
    };

    public static Vector3D<T> operator /(Vector3D<T> left, T scalar) => new() {
        X = left.X / scalar,
        Y = left.Y / scalar,
        Z = left.Z / scalar
    };

    public T this[int i]
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
}