namespace Moarx.Math;
public class Rectangle2D {
    public readonly Point2D<double> PointBottomLeft;
    public readonly Point2D<double> PointBottomRight;
    public readonly Point2D<double> PointUpperRight;
    public readonly Point2D<double> PointUpperLeft;

    public Rectangle2D(Point2D<double> Corner1, Point2D<double> Corner2) {
        double smallX = System.Math.Min(Corner1.X, Corner2.X);
        double smallY = System.Math.Min(Corner1.Y, Corner2.Y);

        double largeX = System.Math.Max(Corner1.X, Corner2.X);
        double largeY = System.Math.Max(Corner1.Y, Corner2.Y);

        PointBottomLeft = new Point2D<double>(smallX, smallY);
        PointBottomRight = new Point2D<double>(largeX, smallY);
        PointUpperRight = new Point2D<double>(largeX, largeY);
        PointUpperLeft = new Point2D<double>(smallX, largeY);
    }
}

