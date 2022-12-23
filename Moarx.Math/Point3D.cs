using System.Diagnostics;
using System.Numerics;
using System;

namespace Moarx.Math;

public readonly record struct Point3D<T>
    where T : struct, INumber<T> {

    readonly T _x;
    readonly T _y;
    readonly T _z;

    public Point3D(T x, T y, T z) {
        X = x;
        Y = y;
        Z = z;
    }
    public Point3D(T i) {
        X = i;
        Y = i;
        Z = i;
    }

    public static readonly Point3D<T> Empty = new();

    public T X {
        get => _x;
        init {
            if (T.IsNaN(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Point data has NaN");
            }
            _x = value;
        }
    }
    public T Y {
        get => _y;
        init {
            if (T.IsNaN(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Point data has NaN");
            }
            _y = value;
        }
    }
    public T Z {
        get => _z;
        init {
            if (T.IsNaN(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Point data has NaN");
            }
            _z = value;
        }
    }


    public Vector3D<T> ToVector() {
        return new Vector3D<T>(X, Y, Z);
    }
    public static Point3D<T> SmalestComponents(Point3D<T> p, Point3D<T> p1) {
        return new Point3D<T>(T.Min(p.X, p1.X),
                              T.Min(p.Y, p1.Y),
                              T.Min(p.Z, p1.Z));
    } //TODO Naming, UnitTests
    public static Point3D<T> GreatestComponents(Point3D<T> p, Point3D<T> p1) {
        return new Point3D<T>(T.Max(p.X, p1.X),
                              T.Max(p.Y, p1.Y),
                              T.Max(p.Z, p1.Z));
    } //TODO Naming, UnitTests


    public static Point3D<T> operator +(Point3D<T> left, Vector3D<T> right) => new() {
        X = left.X + right.X,
        Y = left.Y + right.Y,
        Z = left.Z + right.Z
    };

    public static Vector3D<T> operator -(Point3D<T> left, Point3D<T> right) => new() {
        X = left.X - right.X,
        Y = left.Y - right.Y,
        Z = left.Z - right.Z
    };
    public static Point3D<T> operator -(Point3D<T> left, Vector3D<T> right) => new() {
        X = left.X - right.X,
        Y = left.Y - right.Y,
        Z = left.Z - right.Z
    };
    public static Point3D<T> operator -(Point3D<T> point) => new() {
        X = -point.X,
        Y = -point.Y,
        Z = -point.Z
    };

    public static Point3D<T> operator *(Point3D<T> left, T scalar) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");

        return new() {
            X = left.X * scalar,
            Y = left.Y * scalar,
            Z = left.Z * scalar
        };
    }
    public static Point3D<T> operator *(T scalar, Point3D<T> right) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");

        return new() {
            X = right.X * scalar,
            Y = right.Y * scalar,
            Z = right.Z * scalar
        };
    }

    public static Point3D<T> operator /(Point3D<T> left, T scalar) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");
        if (scalar == T.CreateChecked(0))
            throw new DivideByZeroException(nameof(scalar));

        T inv = T.CreateChecked(1) / scalar;

        Point3D<T> point = new() {
            X = left.X * inv,
            Y = left.Y * inv,
            Z = left.Z * inv
        };

        return point;
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

    public override string ToString() {
        return $"[{X}, {Y}, {Z}]";
    }
}