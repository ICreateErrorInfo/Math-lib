using System.Numerics;

namespace Moarx.Math;

public static class BezierCurve2D {
    public static BezierCurve2D<T> Create<T>(Point2D<T> startPoint, Point2D<T> point1, Point2D<T> point2, Point2D<T> endPoint) where T : struct, INumber<T> {
        return new BezierCurve2D<T>(startPoint, point1, point2, endPoint);
    }
}
public readonly record struct BezierCurve2D<T> 
    where T : struct, INumber<T> {

    public BezierCurve2D(Point2D<T> startPoint, Point2D<T> point1, Point2D<T> point2, Point2D<T> endPoint) {
        StartPoint = startPoint;
        Point1 = point1;
        Point2 = point2;
        EndPoint = endPoint;
    }

    public Point2D<T> StartPoint { get; }
    public Point2D<T> Point1 { get; }
    public Point2D<T> Point2 { get; }
    public Point2D<T> EndPoint { get; }

    public Point2D<T> this[int i] {
        get {
            if (i == 0) {
                return StartPoint;
            }

            if (i == 1) {
                return Point1;
            }

            if (i == 2) {
                return Point2;
            }

            if (i == 3){
                return EndPoint;
            }

            throw new IndexOutOfRangeException();
        }
    } 
}
