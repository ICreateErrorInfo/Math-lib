namespace Moarx.Math;

public readonly record struct Ellipse2D {

    public Ellipse2D(Point2D<double> midPoint, double horizontalStretch, double verticalStretch) {
        MidPoint          = midPoint;
        HorizontalStretch = horizontalStretch;
        VerticalStretch   = verticalStretch;
    }

    public Ellipse2D(Point2D<double> midPoint, double radius) {
        MidPoint          = midPoint;
        HorizontalStretch = VerticalStretch = radius;
    }

    public Point2D<double> MidPoint          { get; }
    public double          HorizontalStretch { get; }
    public double          VerticalStretch   { get; }

}