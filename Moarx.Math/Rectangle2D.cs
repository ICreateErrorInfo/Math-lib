using System.Numerics;

namespace Moarx.Math;

public static class Rectangle2D {

    public static Rectangle2D<T> Create<T>(T x, T y, T width, T height) where T : struct, INumber<T> {
        return new Rectangle2D<T>(x: x, y: y, width: width, height: height);
    }

    public static Rectangle2D<T> Create<T>(Point2D<T> corner1, Point2D<T> corner2) where T : struct, INumber<T> {
        return new Rectangle2D<T>(corner1, corner2);
    }

    public static Rectangle2D<T> Create<T>(Point2D<T> topLeft, Point2D<T> bottomLeft, Point2D<T> bottomRight, Point2D<T> topRight) where T : struct, INumber<T> {
        return new Rectangle2D<T>(topLeft, bottomLeft, bottomRight, topRight);
    }

}

public readonly record struct Rectangle2D<T>
    where T : struct, INumber<T> {

    public Rectangle2D(T x, T y, T width, T height) {

        TopLeft     = new Point2D<T>(x        , y);
        TopRight    = new Point2D<T>(x + width, y);
        BottomLeft  = new Point2D<T>(x        , y + height);
        BottomRight = new Point2D<T>(x + width, y + height);

    }
    public Rectangle2D(Point2D<T> corner1, Point2D<T> corner2) {
        T smallX = T.Min(corner1.X, corner2.X);
        T smallY = T.Min(corner1.Y, corner2.Y);

        T largeX = T.Max(corner1.X, corner2.X);
        T largeY = T.Max(corner1.Y, corner2.Y);

        TopLeft     = new Point2D<T>(smallX, smallY);
        TopRight    = new Point2D<T>(largeX, smallY);
        BottomLeft  = new Point2D<T>(smallX, largeY);
        BottomRight = new Point2D<T>(largeX, largeY);

    }
    public Rectangle2D(Point2D<T> topLeft, Point2D<T> bottomLeft, Point2D<T> bottomRight, Point2D<T> topRight) {
        TopLeft = topLeft;
        TopRight = topRight;
        BottomLeft = bottomLeft;
        BottomRight = bottomRight;
    }


    public static readonly Rectangle2D<T> Empty = new();

    public bool IsEmpty => Height == T.Zero &&
                           Width  == T.Zero &&
                           X      == T.Zero &&
                           Y      == T.Zero;

    public T X      => TopLeft.X;
    public T Left   => X;
    public T Y      => TopLeft.Y;
    public T Top    => Y;
    public T Right  => BottomRight.X;
    public T Bottom => BottomRight.Y;

    public T Width  => Right  - Left;
    public T Height => Bottom - Top;

    public Point2D<T> BottomLeft  { get; }
    public Point2D<T> BottomRight { get; }
    public Point2D<T> TopRight    { get; }
    public Point2D<T> TopLeft     { get; }

    public static Rectangle2D<T> Intersect(Rectangle2D<T> a, Rectangle2D<T> b) {

        T x1 = T.Max(a.X, b.X);
        T x2 = T.Min(a.Right, b.Right);
        T y1 = T.Max(a.Y, b.Y);
        T y2 = T.Min(a.Bottom, b.Bottom);

        if (x2 >= x1 && y2 >= y1) {
            return Rectangle2D.Create(x1, y1, x2 - x1, y2 - y1);
        }

        return Empty;

    }

    public bool IntersectsWith(Rectangle2D<T> other) =>
        (other.X < Right)  && (X < other.Right) &&
        (other.Y < Bottom) && (Y < other.Bottom);

    public static Rectangle2D<T> Union(Rectangle2D<T> a, Rectangle2D<T> b) {
        T x1 = T.Min(a.X, b.X);
        T x2 = T.Max(a.Right, b.Right);
        T y1 = T.Min(a.Y, b.Y);
        T y2 = T.Max(a.Bottom, b.Bottom);

        return new Rectangle2D<T>(x: x1, y: y1, width: x2 - x1, height: y2 - y1);
    }

    public void Offset(Point2D<T> pos) => Offset(pos.X, pos.Y);

    public Rectangle2D<T> Offset(T x, T y) {
        return new(X + x, Y + y, Width, Height);

    }

    public bool Contains(Point2D<T> point) => Contains(point.X, point.Y);
    public bool Contains(T x, T y) => x >= Left && x < Right && y >= Top && y < Bottom;

    public bool Contains(Rectangle2D<T> rect) =>
        (X <= rect.X) && (rect.Right  <= Right) &&
        (Y <= rect.Y) && (rect.Bottom <= Right);

    public Rectangle2D<T> Inflate(T value) {
        return Inflate(value, value);
    }

    public Rectangle2D<T> Inflate(T width, T height) {
        return new Rectangle2D<T>(X - width, Y - height, width + Width + width, height + Height + height);
    }

    public override string ToString() => $"{{X={X},Y={Y},Width={Width},Height={Height}}}";

}