namespace Moarx.Math;
public class Line2D {
    public readonly Point2D<double> StartPoint;
    public readonly Point2D<double> EndPoint;

    public Line2D(Point2D<double> startPoint, Point2D<double> endPoint) {
        StartPoint = startPoint;
        EndPoint = endPoint;
    }
}

