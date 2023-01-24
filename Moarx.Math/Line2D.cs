using System.Numerics;

namespace Moarx.Math;

public static class Line2D {

    public static Line2D<T> Create<T>(Point2D<T> startPoint, Point2D<T> endPoint) where T : struct, INumber<T> {
        return new(startPoint, endPoint);
    }

}

public readonly record struct Line2D<T>
    where T : struct, INumber<T> {

    public Line2D(Point2D<T> startPoint, Point2D<T> endPoint) {
        StartPoint = startPoint;
        EndPoint   = endPoint;
    }

    public Point2D<T> StartPoint { get; }
    public Point2D<T> EndPoint   { get; }

    public Bounds2D<T> GetBoundingBox() {
        return new(StartPoint,
                   new(EndPoint.X, EndPoint.Y));
    }

    public Line2D<T> Transform(Vector2D<T> vector) {
        return new(StartPoint + vector, EndPoint + vector);
    }
}