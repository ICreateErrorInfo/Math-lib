using System.Numerics;

namespace Moarx.Math;

public static class Ellipse2D {

    public static Ellipse2D<T> Create<T>(Point2D<T> midPoint, T horizontalStretch, T verticalStretch) where T : struct, INumber<T> {
        return new(midPoint, horizontalStretch, verticalStretch);
    }

    public static Ellipse2D<T> Circle<T>(Point2D<T> midPoint, T radius) where T : struct, INumber<T> {
        return new(midPoint, radius);
    }

}

public readonly record struct Ellipse2D<T>
    where T : struct, INumber<T> {

    public Ellipse2D(Point2D<T> midPoint, T horizontalStretch, T verticalStretch) {
        MidPoint          = midPoint;
        HorizontalStretch = horizontalStretch;
        VerticalStretch   = verticalStretch;
    }

    public Ellipse2D(Point2D<T> midPoint, T radius) {
        MidPoint          = midPoint;
        HorizontalStretch = VerticalStretch = radius;
    }

    public Point2D<T> MidPoint          { get; }
    public T          HorizontalStretch { get; }
    public T          VerticalStretch   { get; }

}