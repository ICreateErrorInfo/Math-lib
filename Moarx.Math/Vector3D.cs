using System.Numerics;

namespace Moarx.Math; 

public readonly record struct Vector3D<T>(T X, T Y, T Z) 
    where T : 
    INumber<T>, 
    IRootFunctions<T>
{
    public Vector3D<T> CrossProduct(Vector3D<T> vector2) => new() {
        X = (Y * vector2.Z) - (Z * vector2.Y),
        Y = (Z * vector2.X) - (X * vector2.Z),
        Z = (X * vector2.Y) - (Y * vector2.X)
    };

    public Vector3D<T> ToNormalized() => this / GetLength();
    public T GetLength() => T.Sqrt(GetLengthSquared());
    public T GetLengthSquared() => this * this;
    public Vector3D<T> Abs() => new() 
    {
        X = T.Abs(X),
        Y = T.Abs(Y),
        Z = T.Abs(Z)
    };

    //public int MaxDimension() => (X > Y) ? ((X > Z) ? 0 : 2) : ((Y > Z) ? 1 : 2);
    public int MaxDimension() => (Z > Y) ? ((Z > X) ? 2 : 0) : ((Y > X) ? 1 : 0);

    public Vector3D<T> Permute(int x, int y, int z) => new(X: this[x], Y: this[y], Z: this[z]);

    public static Vector3D<T> GetReflectionVector(Vector3D<T> v, Vector3D<T> v1) => v - T.CreateChecked(2) * v * v1 * v1;
    
    public Vector3D<T> Saturate() => Clamp(min: T.CreateChecked(0), max: T.CreateChecked(1));

    public Vector3D<T> Clamp(T min, T max) {
        return new (
            X: T.Clamp(X, min: min, max: max), 
            Y: T.Clamp(Y, min: min, max: max), 
            Z: T.Clamp(Z, min: min, max: max));
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
            Vector3D<double> p = GetRandomVector(-1, 1);
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