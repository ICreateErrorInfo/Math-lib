using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math;
public readonly record struct Point2D<T>
    where T :struct, INumber<T>{

    readonly T _x;
    readonly T _y;

    public Point2D(T x, T y) {
        X =  x ;
        Y =  y ;
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
    
    public static Point2D<T> Minimum(Point2D<T> point1, Point2D<T> point2) => new() {
        X = T.Min(point1.X, point2.X),
        Y = T.Min(point1.Y, point2.Y)
    };
    public static Point2D<T> Maximum(Point2D<T> point1, Point2D<T> point2) => new() {
        X = T.Max(point1.X, point2.X),
        Y = T.Max(point1.Y, point2.Y)
    };
    public Vector2D<T> ToVector() {
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

