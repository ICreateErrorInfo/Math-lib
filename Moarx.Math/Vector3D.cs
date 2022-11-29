using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math;

public readonly record struct Vector3D<T>
    where T : INumber<T> {

    public T X { get; init; }
    public T Y { get; init; }
    public T Z { get; init; }

    public Vector3D(T x, T y, T z) {
        X = x;
        Y = y;
        Z = z;

        CheckNaN();
    }

    private void CheckNaN() {
        if(T.IsNaN(X) | T.IsNaN(Y) | T.IsNaN(Z)) {
            throw new Exception("Vector data has NaN");
        }
    }
    public Vector3D<T> CrossProduct(Vector3D<T> vector2){
        Vector3D<T> vector = new() {
            X = (Y * vector2.Z) - (Z * vector2.Y),
            Y = (Z * vector2.X) - (X * vector2.Z),
            Z = (X * vector2.Y) - (Y * vector2.X)
        };

        return vector;
    }
    public T GetLengthSquared() => this * this;
    public static Vector3D<T> Minimum(Vector3D<T> vector1, Vector3D<T> vector2) => new() {
        X = T.Min(vector1.X, vector2.X),
        Y = T.Min(vector1.Y, vector2.Y),
        Z = T.Min(vector1.Z, vector2.Z)
    };
    public static Vector3D<T> Maximum(Vector3D<T> vector1, Vector3D<T> vector2) => new() {
        X = T.Max(vector1.X, vector2.X),
        Y = T.Max(vector1.Y, vector2.Y),
        Z = T.Max(vector1.Z, vector2.Z)
    };
    public Point3D<T> ToPoint() {
        CheckNaN();
        return new Point3D<T>(X, Y, Z);
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

    public static T operator *(Vector3D<T> left, Vector3D<T> right) {
        return (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
    }
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

    public static Vector3D<T> operator /(Vector3D<T> left, T scalar) {
        Debug.Assert(!T.IsNaN(scalar));

        T inv = T.CreateChecked(1) / scalar;

        Vector3D<T> vector = new() {
            X = left.X * inv,
            Y = left.Y * inv,
            Z = left.Z * inv
        };

        return vector;
    }

    public T this[int i] {
        get {
            CheckNaN();

            if (i == 0) {
                return X;
            }
            if (i == 1) {
                return Y;
            }
            if (i == 2) {
                return Z;
            }

            throw new IndexOutOfRangeException();
        }
    }
}