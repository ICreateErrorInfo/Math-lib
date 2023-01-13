namespace Moarx.Math;

public readonly record struct Line2D {

    public Line2D(Point2D<double> startPoint, Point2D<double> endPoint) {
        StartPoint = startPoint;
        EndPoint   = endPoint;
    }

    public Point2D<double> StartPoint { get; }
    public Point2D<double> EndPoint   { get; }

}