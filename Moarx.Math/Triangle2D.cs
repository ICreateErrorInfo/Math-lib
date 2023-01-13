namespace Moarx.Math;

public readonly record struct Triangle2D {

    public Triangle2D(Point2D<double> point1, Point2D<double> point2, Point2D<double> point3) {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }

    public Point2D<double> Point1 { get; }
    public Point2D<double> Point2 { get; }
    public Point2D<double> Point3 { get; }

}