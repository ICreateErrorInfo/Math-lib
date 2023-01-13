using System.Numerics;

namespace Moarx.Math;

public readonly record struct Vector2D<T>
    where T : struct, INumber<T> {

    readonly T _x;
    readonly T _y;

    public Vector2D(T x, T y) {
        X = x;
        Y = y;
    }

    public Vector2D(T i) {
        X = i;
        Y = i;
    }

    public static readonly Point2D<T> Empty = new();

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

    public T GetLengthSquared() => this * this;

    public Point2D<T> ToPoint() {
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

    public static Vector2D<T> operator *(Vector2D<T> left, T scalar) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");

        return new() {
            X = left.X * scalar,
            Y = left.Y * scalar,
        };
    }

    public static Vector2D<T> operator *(T scalar, Vector2D<T> right) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");

        return new() {
            X = right.X * scalar,
            Y = right.Y * scalar,
        };
    }

    public static Vector2D<T> operator /(Vector2D<T> left, T scalar) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");
        if (scalar == T.CreateChecked(0))
            throw new DivideByZeroException(nameof(scalar));

        T inv = T.CreateChecked(1) / scalar;

        Vector2D<T> vector = new() {
            X = left.X * inv,
            Y = left.Y * inv
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

            throw new IndexOutOfRangeException();
        }
    }

    public override string ToString() {
        return $"[{X}, {Y}]";
    }

}