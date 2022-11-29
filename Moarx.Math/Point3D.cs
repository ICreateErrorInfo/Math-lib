﻿using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math;

public readonly record struct Point3D<T>
    where T : INumber<T> {

    public T X { get; init; }
    public T Y { get; init; }
    public T Z { get; init; }

    public Point3D(T x, T y, T z) {
        X = x;
        Y = y;
        Z = z;

        CheckNaN();
    }

    private void CheckNaN() {
        if (T.IsNaN(X) | T.IsNaN(Y) | T.IsNaN(Z)) {
            Debug.Assert(false, "Point data has NaN");
        }
    }
    public static Point3D<T> Minimum(Point3D<T> point1, Point3D<T> point2) => new() {
        X = T.Min(point1.X, point2.X),
        Y = T.Min(point1.Y, point2.Y),
        Z = T.Min(point1.Z, point2.Z)
    };
    public static Point3D<T> Maximum(Point3D<T> point1, Point3D<T> point2) => new() {
        X = T.Max(point1.X, point2.X),
        Y = T.Max(point1.Y, point2.Y),
        Z = T.Max(point1.Z, point2.Z)
    };
    public static Point3D<T> Permute(Point3D<T> p, int x, int y, int z) => new() {
        X = p[x],
        Y = p[y],
        Z = p[z]
    };
    public Vector3D<T> ToVector() {
        CheckNaN();
        return new Vector3D<T>(X, Y, Z);
    }

    //TODO punkt + punkt sinn?
    public static Point3D<T> operator +(Point3D<T> left, Point3D<T> right) => new() {
        X = left.X + right.X,
        Y = left.Y + right.Y,
        Z = left.Z + right.Z
    };
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

    //TODO punkt * punkt sinn?
    public static Point3D<T> operator *(Point3D<T> left, Point3D<T> right) => new() {
        X = left.X * right.X,
        Y = left.Y * right.Y,
        Z = left.Z * right.Z
    };
    public static Point3D<T> operator *(Point3D<T> left, T scalar) => new() {
        X = left.X * scalar,
        Y = left.Y * scalar,
        Z = left.Z * scalar
    };
    public static Point3D<T> operator *(T scalar, Point3D<T> right) => new() {
        X = right.X * scalar,
        Y = right.Y * scalar,
        Z = right.Z * scalar
    };

    public static Point3D<T> operator /(Point3D<T> left, T scalar) {
        Debug.Assert(!T.IsNaN(scalar));

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
}