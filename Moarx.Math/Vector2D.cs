using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math;
public readonly record struct Vector2D<T>
    where T : INumber<T> {

    public T X { get; init; }
    public T Y { get; init; }

    public Vector2D(T x, T y) {
        X = x; Y = y;
        CheckNaN();
    }

    private void CheckNaN() {
        if (T.IsNaN(X) | T.IsNaN(Y)) {
            //TODO ob assert oder nicht
            Debug.Assert(false, "Vector data has NaN");
        }
    }
    public T GetLengthSquared() => this * this;
    public Point2D<T> ToPoint() {
        CheckNaN();
        return new Point2D<T>(X, Y);
    }

    public static Vector2D<T> operator +(Vector2D<T> left, Vector2D<T> right) => new() {
        X = left.X + right.X,
        Y = left.Y + right.Y,
    };

    public static Vector2D<T> operator -(Vector2D<T> left, Vector2D<T> right) => new() {
        X = left.X - right.X,
        Y = left.Y - right.Y
    };
    public static Vector2D<T> operator -(Vector2D<T> vector) => new() {
        X = -vector.X,
        Y = -vector.Y
    };

    public static T operator *(Vector2D<T> left, Vector2D<T> right) {
        return (left.X * right.X) + (left.Y * right.Y);
    }
    public static Vector2D<T> operator *(Vector2D<T> left, T scalar) => new() {
        X = left.X * scalar,
        Y = left.Y * scalar,
    };
    public static Vector2D<T> operator *(T scalar, Vector2D<T> right) => new() {
        X = right.X * scalar,
        Y = right.Y * scalar,
    };

    public static Vector2D<T> operator /(Vector2D<T> left, T scalar) {
        Debug.Assert(!T.IsNaN(scalar));

        Vector2D<T> vector = new() {
            X = left.X / scalar,
            Y = left.Y / scalar
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

            throw new IndexOutOfRangeException();
        }
    }
}

