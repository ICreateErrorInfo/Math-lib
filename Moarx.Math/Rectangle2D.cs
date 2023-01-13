using System.Numerics;

namespace Moarx.Math;

public static class Rectangle2D {

    public static Rectangle2D<T> Create<T>(T x, T y, T width, T height) where T : struct, INumber<T> {
        return new Rectangle2D<T>(x: x, y: y, width: width, height: height);
    }

    public static Rectangle2D<T> Create<T>(Point2D<T> corner1, Point2D<T> corner2) where T : struct, INumber<T> {
        return new Rectangle2D<T>(corner1, corner2);
    }

}

public readonly record struct Rectangle2D<T>
    where T : struct, INumber<T> {

    public Rectangle2D(T x, T y, T width, T height) {

        T smallX = x;
        T smallY = y;

        T largeX = x + width;
        T largeY = y + height;

        TopLeft  = new Point2D<T>(smallX, smallY);
        TopRight = new Point2D<T>(largeX, smallY);

        BottomLeft  = new Point2D<T>(smallX, largeY);
        BottomRight = new Point2D<T>(largeX, largeY);
        
    }

    public Rectangle2D(Point2D<T> corner1, Point2D<T> corner2) {
        T smallX = T.Min(corner1.X, corner2.X);
        T smallY = T.Min(corner1.Y, corner2.Y);

        T largeX = T.Max(corner1.X, corner2.X);
        T largeY = T.Max(corner1.Y, corner2.Y);

        TopLeft  = new Point2D<T>(smallX, smallY);
        TopRight = new Point2D<T>(largeX, smallY);
        BottomLeft = new Point2D<T>(smallX, largeY);
        BottomRight = new Point2D<T>(largeX, largeY);
       
    }

    public T Left   => TopLeft.X;
    public T Top    => TopLeft.Y;
    public T Right  => BottomRight.X;
    public T Bottom => BottomRight.Y;

    public T Width  => Right  - Left;
    public T Height => Bottom - Top;

    public Point2D<T> BottomLeft  { get; }
    public Point2D<T> BottomRight { get; }
    public Point2D<T> TopRight    { get; }
    public Point2D<T> TopLeft     { get; }

    public bool PointInRect(Point2D<T> point) => PointInRect(point.X, point.Y);
    public bool PointInRect(T x, T y) => x >= Left && x < Right && y >= Top && y < Bottom;

}