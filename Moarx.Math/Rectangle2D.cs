using System.Numerics;

namespace Moarx.Math;

public readonly record struct Rectangle2D<T>
    where T : struct, INumber<T> {

    public Rectangle2D(Point2D<T> corner1, Point2D<T> corner2) {
        T smallX = T.Min(corner1.X, corner2.X);
        T smallY = T.Min(corner1.Y, corner2.Y);

        T largeX = T.Max(corner1.X, corner2.X);
        T largeY = T.Max(corner1.Y, corner2.Y);

        BottomLeft  = new Point2D<T>(smallX, smallY);
        BottomRight = new Point2D<T>(largeX, smallY);
        TopRight    = new Point2D<T>(largeX, largeY);
        TopLeft     = new Point2D<T>(smallX, largeY);
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