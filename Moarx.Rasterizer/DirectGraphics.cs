using Moarx.Math;

namespace Moarx.Rasterizer;

public class DirectGraphics {

    readonly DirectBitmap _bitmap;

    DirectGraphics(DirectBitmap bitmap) {
        _bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
    }

    public static DirectGraphics Create(DirectBitmap bitmap) => new(bitmap);

    public void FloodFill(int x, int y, DirectColor newColor) {


        var replaceColor = _bitmap.GetPixel(x, y);
        if (newColor == replaceColor) {
            return;
        }

        FloodFillmpl(x, y, newColor, replaceColor);

    }
    void FloodFillmpl(int x1, int y1, DirectColor newColor, DirectColor replaceColor) {

        Stack<(int X, int Y)> stack = new();

        ProcessPixel(x1, y1);

        while (stack.Any()) {

            var (x, y) = stack.Pop();

            ProcessPixel(x, y + 1);
            ProcessPixel(x, y - 1);
            ProcessPixel(x    + 1, y);
            ProcessPixel(x    - 1, y);

        }

        void ProcessPixel(int x, int y) {

            var currentColor = SafeGetPixel(x, y);

            if (currentColor == replaceColor) {
                stack.Push((x, y));
                _bitmap.SetPixel(x, y, newColor);
            }
        }

        DirectColor? SafeGetPixel(int x, int y) {

            if (x < 0 || x >= _bitmap.Width ||
                y < 0 || y >= _bitmap.Height) {
                return null;
            }

            return _bitmap.GetPixel(x, y);
        }
    }


    public void DrawLine(Point2D<int> start, Point2D<int> end, DirectColor color) {
        DrawLine(new(start, end), color);
    }
    public void DrawLine(Line2D<int> line, DirectColor color) {
        Vector2D<int> slope = line.EndPoint - line.StartPoint;

        int dx = System.Math.Abs(slope.X);
        int dy = -System.Math.Abs(slope.Y);

        int sx = line.StartPoint.X < line.EndPoint.X ? 1 : -1;
        int sy = line.StartPoint.Y < line.EndPoint.Y ? 1 : -1;

        int error = dx + dy;

        int newX = line.StartPoint.X;
        int newY = line.StartPoint.Y;

        while (true) {
            _bitmap.SetPixel(newX, newY, color);

            if (newX == line.EndPoint.X && newY == line.EndPoint.Y)
                break;

            var errorDoubled = 2 * error;
            if (errorDoubled > dy) {
                error += dy;
                newX  += sx;
            }

            if (errorDoubled < dx) {
                error += dx;
                newY  += sy;
            }
        }
    }

    public void DrawAntiAliasedLine(Point2D<int> start, Point2D<int> end, DirectColor color) {
        DrawAntiAliasedLine(new(start, end), color);
    }
    public void DrawAntiAliasedLine(Line2D<int> line, DirectColor color) {
        bool steep = System.Math.Abs(line.EndPoint.Y - line.StartPoint.Y) > System.Math.Abs(line.EndPoint.X - line.StartPoint.X);

        int x0 = line.StartPoint.X, y0 = line.StartPoint.Y;
        int x1 = line.EndPoint.X, y1 = line.EndPoint.Y;

        if (steep) {
            (x0, y0) = (y0, x0);
            (x1, y1) = (y1, x1);
        }

        if(x0 > x1) {
            (x0, x1) = (x1, x0);
            (y0, y1) = (y1, y0);
        }

        Vector2D<int> slope = new Point2D<int>(x0, y0) -  new Point2D<int>(x1, y1);

        double gradient = slope.X switch {
            0 => 1,
            _ => (double)slope.Y / slope.X
        };

        double xend = (int)(x0+0.5);
        double yend = y0 + gradient * (xend - x0);
        double xgap = rfpart(x0 + 1);
        double xpxl1 = xend;
        double ypxl1 = (int)yend;

        if (steep) {
            _bitmap.BlendPixel((int)ypxl1,     (int)xpxl1, GetColor(color, rfpart(yend) * xgap));
            _bitmap.BlendPixel((int)ypxl1 + 1, (int)xpxl1, GetColor(color, fpart(yend)  * xgap));
        } else {
            _bitmap.BlendPixel((int)xpxl1, (int)ypxl1  ,  GetColor(color, rfpart(yend) * xgap));
            _bitmap.BlendPixel((int)xpxl1, (int)ypxl1 +1, GetColor(color, fpart(yend)  * xgap));
        }
        double intery = yend + gradient;

        xend = (int)(x1 + 0.5);
        yend = y1+ gradient*(xend- x1);
        xgap = fpart(x1 + 1);
        double xpxl2 = xend;
        double ypxl2 = (int)yend;

        if (steep) {
            _bitmap.BlendPixel((int)ypxl2,     (int)xpxl2, GetColor(color, rfpart(yend) * xgap));
            _bitmap.BlendPixel((int)ypxl2 + 1, (int)xpxl2, GetColor(color, fpart(yend)  * xgap));
        } else {
            _bitmap.BlendPixel((int)xpxl2, (int)ypxl2,     GetColor(color, rfpart(yend) * xgap));
            _bitmap.BlendPixel((int)xpxl2, (int)ypxl2 + 1, GetColor(color, fpart(yend)  * xgap));
        }

        if (steep) {
            for (double x = xpxl1 + 1; x <= xpxl2 - 1; x++){
                _bitmap.BlendPixel((int)intery,     (int)x, GetColor(color, rfpart(intery)));
                _bitmap.BlendPixel((int)intery + 1, (int)x, GetColor(color, fpart(intery)));
                intery = intery + gradient;
            }
        } else {
            for (double x = xpxl1 + 1; x <= xpxl2 - 1; x++) {
                _bitmap.BlendPixel((int)x, (int)intery,     GetColor(color, rfpart(intery)));
                _bitmap.BlendPixel((int)x, (int)intery + 1, GetColor(color, fpart(intery)));
                intery = intery + gradient;
            }
        }
    }

    public void DrawEllipse(Ellipse2D<int> ellipse, DirectColor color) {
        //Circle
        if (ellipse.VerticalStretch == ellipse.HorizontalStretch) {
            int radius = ellipse.VerticalStretch;

            int f     = 1 - radius;
            int ddF_x = 0;
            int ddF_y = -2 * radius;
            int x     = 0;
            int y     = radius;

            _bitmap.SetPixel(ellipse.MidPoint.X, ellipse.MidPoint.Y + radius, color);
            _bitmap.SetPixel(ellipse.MidPoint.X, ellipse.MidPoint.Y - radius, color);
            _bitmap.SetPixel(ellipse.MidPoint.X                     + radius, ellipse.MidPoint.Y, color);
            _bitmap.SetPixel(ellipse.MidPoint.X                     - radius, ellipse.MidPoint.Y, color);

            while (x < y) {
                if (f >= 0) {
                    y     -= 1;
                    ddF_y += 2;
                    f     += ddF_y;
                }

                x     += 1;
                ddF_x += 2;
                f     += ddF_x + 1;

                _bitmap.SetPixel(ellipse.MidPoint.X + x, ellipse.MidPoint.Y + y, color);
                _bitmap.SetPixel(ellipse.MidPoint.X - x, ellipse.MidPoint.Y + y, color);
                _bitmap.SetPixel(ellipse.MidPoint.X + x, ellipse.MidPoint.Y - y, color);
                _bitmap.SetPixel(ellipse.MidPoint.X - x, ellipse.MidPoint.Y - y, color);
                _bitmap.SetPixel(ellipse.MidPoint.X + y, ellipse.MidPoint.Y + x, color);
                _bitmap.SetPixel(ellipse.MidPoint.X - y, ellipse.MidPoint.Y + x, color);
                _bitmap.SetPixel(ellipse.MidPoint.X + y, ellipse.MidPoint.Y - x, color);
                _bitmap.SetPixel(ellipse.MidPoint.X - y, ellipse.MidPoint.Y - x, color);
            }

            return;
        }

        //Ellipse
        int    dx    = 0,                                                     dy = ellipse.VerticalStretch;
        double a2    = ellipse.HorizontalStretch * ellipse.HorizontalStretch, b2 = ellipse.VerticalStretch * ellipse.VerticalStretch;
        double error = b2 - (2 * ellipse.VerticalStretch - 1) * a2,           errorDoubled;

        while (dy >= 0) {
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y - dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y - dy, color);

            errorDoubled = 2 * error;
            if (errorDoubled < (2 * dx + 1) * b2) {
                ++dx;
                error += (2 * dx + 1) * b2;
            }

            if (errorDoubled > -(2 * dy - 1) * a2) {
                --dy;
                error -= (2 * dy - 1) * a2;
            }
        }

        do {
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y - dy, color);
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y - dy, color);

            errorDoubled = 2 * error;
            if (errorDoubled < (2 * dx + 1) * b2) {
                ++dx;
                error += (2 * dx + 1) * b2;
            }

            if (errorDoubled > -(2 * dy - 1) * a2) {
                --dy;
                error -= (2 * dy - 1) * a2;
            }
        } while (dy >= 0);

        while (dx++ < ellipse.HorizontalStretch) {
            _bitmap.SetPixel(ellipse.MidPoint.X + dx, ellipse.MidPoint.Y, color);
            _bitmap.SetPixel(ellipse.MidPoint.X - dx, ellipse.MidPoint.Y, color);
        }
    }
    public void DrawAntiAliasedEllipse(Ellipse2D<int> ellipse, DirectColor color) {
        double radiusX = ellipse.HorizontalStretch;
        double radiusY = ellipse.VerticalStretch;
        double radiusXSquared = radiusX * radiusX;
        double radiusYSquared = radiusY * radiusY;

        double maxTransparency = 255;

        double quater = System.Math.Round(radiusXSquared / System.Math.Sqrt(radiusXSquared + radiusYSquared));
        for(int x = 0; x <= quater; x++) {
            double y = radiusY * System.Math.Sqrt(1 - x * x / radiusXSquared);
            double error = y - System.Math.Floor(y);

            double transparency = System.Math.Round(error * maxTransparency);
            int alpha = (int)transparency;
            int alpha2 = (int)(maxTransparency - transparency);

            SetPixel4(ellipse.MidPoint, x, (int)System.Math.Floor(y), DirectColor.FromArgb((byte)alpha, color));
            SetPixel4(ellipse.MidPoint, x, (int)System.Math.Floor(y) - 1, DirectColor.FromArgb((byte)alpha2, color));
        }

        quater = System.Math.Round(radiusYSquared / System.Math.Sqrt(radiusXSquared + radiusYSquared));
        for (int y = 0; y <= quater; y++) {
            double x = radiusX * System.Math.Sqrt(1 - y * y / radiusYSquared);
            double error = x - System.Math.Floor(x);

            double transparency = System.Math.Round(error * maxTransparency);
            int alpha = (int)transparency;
            int alpha2 = (int)(maxTransparency - transparency);

            SetPixel4(ellipse.MidPoint, (int)System.Math.Floor(x), y, DirectColor.FromArgb((byte)alpha, color));
            SetPixel4(ellipse.MidPoint, (int)System.Math.Floor(x) - 1, y, DirectColor.FromArgb((byte)alpha2, color));
        }
    }

    public void DrawTriangle(Triangle2D<int> triangle, DirectColor color) {
        DrawTriangle(triangle.Point1, triangle.Point2, triangle.Point3, color);
    }
    public void DrawAntiAliasedTriangle(Triangle2D<int> triangle, DirectColor color) {
        DrawAntiAliasedLine(new(triangle.Point1, triangle.Point2), color);
        DrawAntiAliasedLine(new(triangle.Point2, triangle.Point3), color);
        DrawAntiAliasedLine(new(triangle.Point3, triangle.Point1), color);
    }
    public void DrawAntiAliasedTriangleFilled(Triangle2D<int> triangle, DirectColor color) {
        DrawAntiAliasedLine(new(triangle.Point1, triangle.Point2), color);
        DrawAntiAliasedLine(new(triangle.Point2, triangle.Point3), color);
        DrawAntiAliasedLine(new(triangle.Point3, triangle.Point1), color);

        Point2D<int> top = triangle.Point1, middle = triangle.Point2, bottom = triangle.Point3;

        //Sort points by Y
        if (middle.Y < top.Y)
            (top, middle) = (middle, top);
        if (middle.Y > bottom.Y)
            (bottom, middle) = (middle, bottom);
        if (middle.Y < top.Y)
            (top, middle) = (middle, top);


        if (top.Y == middle.Y) //top flat
        {
            Point2D<int> right = top, left = middle;
            if (right.X < left.X)
                (left, right) = (right, left);

            DrawTopFlatAnitAliasedTriangle(right, left, bottom, color);
            return;
        }

        if (bottom.Y == middle.Y) //top bottom
        {
            Point2D<int> right = bottom, left = middle;
            if (right.X < left.X)
                (left, right) = (right, left);

            DrawBottomFlatAnitAliasedTriangle(top, left, right, color);
            return;
        }

        Point2D<int> splitPoint = new((int)(top.X + ((middle.Y - top.Y) / (float)(bottom.Y - top.Y)) * (bottom.X - top.X)), middle.Y);
        if (splitPoint.X < middle.X) {
            //split point is left
            DrawBottomFlatAnitAliasedTriangle(top, splitPoint, middle, color);
            DrawTopFlatAnitAliasedTriangle(middle, splitPoint, bottom, color);
        } else {
            //split point is right
            DrawBottomFlatAnitAliasedTriangle(top, middle, splitPoint, color);
            DrawTopFlatAnitAliasedTriangle(splitPoint, middle, bottom, color);
        }
    }
    public void DrawSSAATriangleFilled(Triangle2D<int> triangle, int samples, DirectColor color) {
        DirectBitmap supersampledBitmap = DirectBitmap.Create(_bitmap.Width * samples, _bitmap.Height * samples);

        Triangle2D<int> scaledTriangle = new Triangle2D<int>(triangle.Point1 * samples, triangle.Point2 * samples, triangle.Point3 * samples);

        DirectGraphics g = DirectGraphics.Create(supersampledBitmap);
        g.DrawTriangleFilled(scaledTriangle, color);

        DownSample(supersampledBitmap, samples);
    }

    public void DrawTriangle(Point2D<int> point1, Point2D<int> point2, Point2D<int> point3, DirectColor color) {
        DrawLine(point1, point2, color);
        DrawLine(point2, point3, color);
        DrawLine(point3, point1, color);
    }

    public void DrawTriangleFilled(Triangle2D<int> triangle, DirectColor color) {

        Point2D<int> top = triangle.Point1, middle = triangle.Point2, bottom = triangle.Point3;

        //Sort points by Y
        if (middle.Y < top.Y)    (top, middle)    = (middle, top);
        if (middle.Y > bottom.Y) (bottom, middle) = (middle, bottom);
        if (middle.Y < top.Y)    (top, middle)    = (middle, top);


        if (top.Y == middle.Y) //top flat
        {
            Point2D<int> right = top, left = middle;
            if (right.X < left.X) (left, right) = (right, left);

            DrawTopFlatTriangle(right, left, bottom, color);
            return;
        }

        if (bottom.Y == middle.Y) //top bottom
        {
            Point2D<int> right = bottom, left = middle;
            if (right.X < left.X) (left, right) = (right, left);

            DrawBottomFlatTriangle(top, left, right, color);
            return;
        }

        Point2D<int> splitPoint = new((int)(top.X + ((middle.Y - top.Y) / (float)(bottom.Y - top.Y)) * (bottom.X - top.X)), middle.Y);
        if(splitPoint.X < middle.X) {
            //split point is left
            DrawBottomFlatTriangle(top, splitPoint, middle, color);
            DrawTopFlatTriangle(middle, splitPoint, bottom, color);
        } else {
            //split point is right
            DrawBottomFlatTriangle(top, middle, splitPoint, color);
            DrawTopFlatTriangle(splitPoint, middle, bottom, color);
        }

    }
    private void DrawBottomFlatTriangle(Point2D<int> top, Point2D<int> bottomLeft, Point2D<int> bottomRight, DirectColor color) {
        double inverseSlopeLeft  = (double)(bottomLeft.X - top.X) / (bottomLeft.Y - top.Y);
        double inverseSlopeRight = (double)(bottomRight.X - top.X) / (bottomRight.Y - top.Y);

        double currentXPositionLeft  = top.X;
        double currentXPositionRight = top.X;

        for(int scanlineY = top.Y; scanlineY <= bottomLeft.Y; scanlineY++) {
            //TODO loop
            DrawLine(new Line2D<int>(new((int)System.Math.Floor(currentXPositionLeft), scanlineY), new((int)System.Math.Floor(currentXPositionRight), scanlineY)), color);
            currentXPositionLeft  += inverseSlopeLeft;
            currentXPositionRight += inverseSlopeRight;
        }
    }
    private void DrawTopFlatTriangle(Point2D<int> topRight, Point2D<int> topLeft, Point2D<int> bottom, DirectColor color) {
        double inverseSlopeLeft  = (double)(bottom.X - topLeft.X) / (bottom.Y - topLeft.Y);
        double inverseSlopeRight = (double)(bottom.X - topRight.X) / (bottom.Y - topRight.Y);

        double currentXPositionLeft  = bottom.X;
        double currentXPositionRight = bottom.X;

        for (int scanlineY = bottom.Y; scanlineY > topRight.Y; scanlineY--) {
            DrawLine(new Line2D<int>(new((int)System.Math.Floor(currentXPositionLeft), scanlineY), new((int)System.Math.Floor(currentXPositionRight), scanlineY)), color);
            currentXPositionLeft  -= inverseSlopeLeft;
            currentXPositionRight -= inverseSlopeRight;
        }
    }

    private void DrawBottomFlatAnitAliasedTriangle(Point2D<int> top, Point2D<int> bottomLeft, Point2D<int> bottomRight, DirectColor color) {
        double inverseSlopeLeft  = (double)(bottomLeft.X - top.X) / (bottomLeft.Y - top.Y);
        double inverseSlopeRight = (double)(bottomRight.X - top.X) / (bottomRight.Y - top.Y);

        double currentXPositionLeft  = top.X;
        double currentXPositionRight = top.X;

        for (int scanlineY = top.Y; scanlineY <= bottomLeft.Y; scanlineY++) {
            //TODO loop
            //TODO not optimal
            if(currentXPositionLeft != currentXPositionRight) {
                DrawLine(new Line2D<int>(new((int)System.Math.Floor(currentXPositionLeft + 1), scanlineY), new((int)System.Math.Floor(currentXPositionRight - 1), scanlineY)), color);
            }
            currentXPositionLeft += inverseSlopeLeft;
            currentXPositionRight += inverseSlopeRight;
        }
    }
    private void DrawTopFlatAnitAliasedTriangle(Point2D<int> topRight, Point2D<int> topLeft, Point2D<int> bottom, DirectColor color) {
        double inverseSlopeLeft  = (double)(bottom.X - topLeft.X) / (bottom.Y - topLeft.Y);
        double inverseSlopeRight = (double)(bottom.X - topRight.X) / (bottom.Y - topRight.Y);

        double currentXPositionLeft  = bottom.X;
        double currentXPositionRight = bottom.X;

        for (int scanlineY = bottom.Y; scanlineY > topRight.Y; scanlineY--) {

            if (currentXPositionLeft != currentXPositionRight) {
                DrawLine(new Line2D<int>(new((int)System.Math.Floor(currentXPositionLeft) + 1, scanlineY), new((int)System.Math.Floor(currentXPositionRight) - 1, scanlineY)), color);
            }
            currentXPositionLeft -= inverseSlopeLeft;
            currentXPositionRight -= inverseSlopeRight;
        }
    }

    //Anit aliased line methods
    private double fpart(double x) {
        if (x < 0)
            return 1 - (x - System.Math.Floor(x));

        return x - System.Math.Floor(x);
    }
    private double rfpart(double x) {
        return 1 - fpart(x);
    }
    private DirectColor GetColor(DirectColor color, double brightness) {
        return DirectColor.FromArgb((byte)(brightness * 255), color);
    }

    //SSAA
    private void DownSample(DirectBitmap sampledBitmap, int samples) {
        for (int x = 0; x < _bitmap.Width; x++) {
            for (int y = 0; y < _bitmap.Height; y++) {
                Vector3D<int> sampledColor = new Vector3D<int>();
                for (int i = 0; i < samples; i++) {
                    for (int j = 0; j < samples; j++) {

                        Vector3D<int> newColor = new Vector3D<int>((int)(sampledBitmap.GetPixel(x * samples + i, y * samples + j).R),
                                                                   (int)(sampledBitmap.GetPixel(x * samples + i, y * samples + j).G),
                                                                   (int)(sampledBitmap.GetPixel(x * samples + i, y * samples + j).B));

                        if (newColor.X == 0 && newColor.Y == 0 && newColor.Z == 0) {
                            newColor = new(_bitmap.GetPixel(x, y).R, _bitmap.GetPixel(x, y).G, _bitmap.GetPixel(x, y).B);
                        }

                        sampledColor += newColor;
                    }
                }

                _bitmap.SetPixel(x, y, DirectColor.FromArgb(255,
                                                            (byte)(sampledColor.X / (samples * samples)),
                                                            (byte)(sampledColor.Y / (samples * samples)),
                                                            (byte)(sampledColor.Z / (samples * samples))));
            }
        }
    } 


    //Anti aliased ellipse methods
    private void SetPixel4(Point2D<int> center, int deltaX, int deltaY, DirectColor color) {
        _bitmap.BlendPixel(center.X + deltaX, center.Y + deltaY, color);
        _bitmap.BlendPixel(center.X - deltaX, center.Y + deltaY, color);
        _bitmap.BlendPixel(center.X + deltaX, center.Y - deltaY, color);
        _bitmap.BlendPixel(center.X - deltaX, center.Y - deltaY, color);
    }
}