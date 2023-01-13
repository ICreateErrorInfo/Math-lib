namespace Moarx.Math;

public readonly record struct Rectangle2D {

    public Rectangle2D(Point2D<double> corner1, Point2D<double> corner2) {
        double smallX = System.Math.Min(corner1.X, corner2.X);
        double smallY = System.Math.Min(corner1.Y, corner2.Y);

        double largeX = System.Math.Max(corner1.X, corner2.X);
        double largeY = System.Math.Max(corner1.Y, corner2.Y);

        PointBottomLeft  = new Point2D<double>(smallX, smallY);
        PointBottomRight = new Point2D<double>(largeX, smallY);
        PointUpperRight  = new Point2D<double>(largeX, largeY);
        PointUpperLeft   = new Point2D<double>(smallX, largeY);
    }

    public Point2D<double> PointBottomLeft  { get; }
    public Point2D<double> PointBottomRight { get; }
    public Point2D<double> PointUpperRight  { get; }
    public Point2D<double> PointUpperLeft   { get; }

}