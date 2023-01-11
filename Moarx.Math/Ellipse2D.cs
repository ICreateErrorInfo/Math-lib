namespace Moarx.Math;
public class Ellipse2D {
    public readonly Point2D<double> MidPoint;
    public readonly double HorizontalStretch;
    public readonly double VerticalStretch;

    public Ellipse2D(Point2D<double> midPoint, double horizontalStretch, double verticalStretch) {
        MidPoint = midPoint;
        HorizontalStretch = horizontalStretch;
        VerticalStretch = verticalStretch;
    }

    public Ellipse2D(Point2D<double> midPoint, double radius) {
        MidPoint = midPoint;
        HorizontalStretch = VerticalStretch = radius;
    }
}

