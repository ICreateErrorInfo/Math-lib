using System.Numerics;

namespace Moarx.Math;

public readonly record struct Point2D<T>
    where T : struct, INumber<T> {

    readonly T _x;
    readonly T _y;

    public Point2D(T x, T y) {
        X = x;
        Y = y;
    }

    public Point2D(T i) {
        X = i;
        Y = i;
    }

    public static readonly Point2D<T> Empty = new();

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

    public Vector2D<T> ToVector() {
        return new Vector2D<T>(X, Y);
    }


    public static explicit operator Point2D<int>(Point2D<T> v) {
        return new Point2D<int>(Convert.ToInt32(v.X), Convert.ToInt32(v.Y));
    }
    public static explicit operator Point2D<double>(Point2D<T> v) {
        return new Point2D<double>(Convert.ToDouble(v.X), Convert.ToDouble(v.Y));
    }

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

    public static Point2D<T> operator *(Point2D<T> left, T scalar) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");

        return new() {
            X = left.X * scalar,
            Y = left.Y * scalar
        };
    }

    public static Point2D<T> operator *(T scalar, Point2D<T> right) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");

        return new() {
            X = right.X * scalar,
            Y = right.Y * scalar
        };
    }

    public static Point2D<T> operator /(Point2D<T> left, T scalar) {
        if (T.IsNaN(scalar))
            throw new ArgumentOutOfRangeException(nameof(scalar), "scalar is NaN");
        if (scalar == T.CreateChecked(0))
            throw new DivideByZeroException(nameof(scalar));

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

    public override string ToString() {
        return $"[{X}, {Y}]";
    }

}