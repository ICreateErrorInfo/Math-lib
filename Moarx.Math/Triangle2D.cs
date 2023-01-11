namespace Moarx.Math;
public class Triangle2D {
    public readonly Point2D<double> Point1;
    public readonly Point2D<double> Point2;
    public readonly Point2D<double> Point3;

    public Triangle2D(Point2D<double> point1, Point2D<double> point2, Point2D<double> point3) {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }
}

