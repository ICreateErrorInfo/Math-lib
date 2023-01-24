using System.Numerics;

namespace Moarx.Math;

public static class Triangle2D {

    public static Triangle2D<T> Create<T>(Point2D<T> point1, Point2D<T> point2, Point2D<T> point3)
        where T : struct, INumber<T> => new(point1, point2, point3);

}

public readonly record struct Triangle2D<T>
    where T : struct, INumber<T> {

    public Triangle2D(Point2D<T> point1, Point2D<T> point2, Point2D<T> point3) {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }

    public Point2D<T> Point1 { get;  }
    public Point2D<T> Point2 { get;  }
    public Point2D<T> Point3 { get;  }


    public Bounds2D<T> GetBoundingBox() {
        return new(new Point2D<T>(T.Min(Point1.X, T.Min(Point2.X, Point3.X)), T.Min(Point1.Y, T.Min(Point2.Y, Point3.Y))),
                   new Point2D<T>(T.Max(Point1.X, T.Max(Point2.X, Point3.X)), T.Max(Point1.Y, T.Max(Point2.Y, Point3.Y))));
    }

    public Triangle2D<T> Transform(Vector2D<T> vector) {
        return new(Point1 + vector, Point2 + vector, Point3 + vector);
    }
}