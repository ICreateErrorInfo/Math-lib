using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math;
public readonly record struct Point2D<T>
    where T : INumber<T> {

    public T X { get; init; }
    public T Y { get; init; }

    public Point2D(T x, T y) {
        X = x; Y = y;
        CheckNaN();
    }

    private void CheckNaN() {
        if (T.IsNaN(X) | T.IsNaN(Y)) {
            Debug.Assert(false, "Point data has NaN");
        }
    }
    public Vector2D<T> ToVector() {
        CheckNaN();
        return new Vector2D<T>(X, Y);
    }

    //TODO punkt + punkt sinn?
    public static Point2D<T> operator +(Point2D<T> left, Point2D<T> right) => new() {
        X = left.X + right.X,
        Y = left.Y + right.Y
    };
    public static Point2D<T> operator +(Point2D<T> left, Vector2D<T> right) => new() {
        X = left.X + right.X,
        Y = left.Y + right.Y
    };

    public static Vector2D<T> operator -(Point2D<T> left, Point2D<T> right) => new() {
        X = left.X - right.X,
        Y = left.Y - right.Y
    };
    public static Point2D<T> operator -(Point2D<T> left, Vector2D<T> right) => new() {
        X = left.X - right.X,
        Y = left.Y - right.Y
    };
    public static Point2D<T> operator -(Point2D<T> point) => new() {
        X = -point.X,
        Y = -point.Y
    };

    //TODO punkt * punkt sinn?
    public static Point2D<T> operator *(Point2D<T> left, Point2D<T> right) => new() {
        X = left.X * right.X,
        Y = left.Y * right.Y
    };
    public static Point2D<T> operator *(Point2D<T> left, T scalar) => new() {
        X = left.X * scalar,
        Y = left.Y * scalar
    };
    public static Point2D<T> operator *(T scalar, Point2D<T> right) => new() {
        X = right.X * scalar,
        Y = right.Y * scalar
    };

    public static Point2D<T> operator /(Point2D<T> left, T scalar) {
        Debug.Assert(!T.IsNaN(scalar));

        T inv = T.CreateChecked(1) / scalar;

        Point2D<T> point = new() {
            X = left.X * inv,
            Y = left.Y * inv
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

            throw new IndexOutOfRangeException();
        }
    }
}

