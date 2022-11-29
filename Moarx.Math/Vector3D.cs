using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math;

public readonly record struct Vector3D<T>
    where T : struct, INumber<T> {

    readonly T _x;
    readonly T _y;
    readonly T _z;

    public Vector3D(T x, T y, T z) {
        X = x;
        Y = y;
        Z = z;
    }
    public Vector3D(T i) {
        X = i;
        Y = i;
        Z = i;
    }

    public static readonly Point3D<T> Empty = new();

    public T X {
        get => _x;
        init {
            if (T.IsNaN(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Vector data has NaN");
            }
            _x = value;
        }
    }
    public T Y {
        get => _y;
        init {
            if (T.IsNaN(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Vector data has NaN");
            }
            _y = value;
        }
    }
    public T Z {
        get => _z;
        init {
            if (T.IsNaN(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Vector data has NaN");
            }
            _z = value;
        }
    }


    public static Vector3D<T> CrossProduct(Vector3D<T> vector1, Vector3D<T> vector2) {
        Vector3D<T> vector = new() {
            X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y),
            Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z),
            Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X)
        };

        return vector;
    }
    public T GetLengthSquared() => this * this;
    public Point3D<T> ToPoint() {
        return new Point3D<T>(X, Y, Z);
    }


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
    public static Vector3D<T> operator *(Vector3D<T> left, T scalar) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");

        return new() {
            X = left.X * scalar,
            Y = left.Y * scalar,
            Z = left.Z * scalar
        };
    }
    public static Vector3D<T> operator *(T scalar, Vector3D<T> right) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");

        return new() {

            X = right.X * scalar,
            Y = right.Y * scalar,
            Z = right.Z * scalar
        };
    }

    public static Vector3D<T> operator /(Vector3D<T> left, T scalar) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");
        if (scalar == T.CreateChecked(0))
            throw new DivideByZeroException(nameof(scalar));

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