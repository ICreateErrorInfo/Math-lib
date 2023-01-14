using Moarx.Math;
using System.Drawing;
using System.Security.AccessControl;

namespace Moarx.Rasterizer;

public class DirectGraphics {

    readonly DirectBitmap _bitmap;

    DirectGraphics(DirectBitmap bitmap) {
        _bitmap = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
    }

    public static DirectGraphics Create(DirectBitmap bitmap) => new(bitmap);

    public void FloodFill(int x, int y, Color newColor) {

        newColor = Color.FromArgb(newColor.ToArgb()); // get rid of named Color...

        var replaceColor = _bitmap.GetPixel(x, y);
        if (newColor == replaceColor) {
            return;
        }

        FloodFillmpl(x, y, newColor, replaceColor);

    }

    void FloodFillmpl(int x1, int y1, Color newColor, Color replaceColor) {

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

        Color? SafeGetPixel(int x, int y) {

            if (x < 0 || x >= _bitmap.Width ||
                y < 0 || y >= _bitmap.Height) {
                return null;
            }

            return _bitmap.GetPixel(x, y);
        }
    }

    public void DrawLine(Point2D<int> start, Point2D<int> end, Color color) {
        DrawLine(new(start, end), color);
    }

    //TODO bug
    public void DrawLine(Line2D<int> line, Color color) {
        Vector2D<int> slope = line.EndPoint - line.StartPoint;

        int dx = System.Math.Abs((int)slope.X);
        int dy = -System.Math.Abs((int)slope.Y);

        int sx = line.StartPoint.X < line.EndPoint.X ? 1 : -1;
        int sy = line.StartPoint.Y < line.EndPoint.Y ? 1 : -1;

        int error = dx + dy;

        int newX = (int)line.StartPoint.X;
        int newY = (int)line.StartPoint.Y;

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

    public void DrawEllipse(Ellipse2D<int> ellipse, Color color) {
        //Circle
        if (ellipse.VerticalStretch == ellipse.HorizontalStretch) {
            int radius = (int)ellipse.VerticalStretch;

            int f     = 1 - radius;
            int ddF_x = 0;
            int ddF_y = -2 * radius;
            int x     = 0;
            int y     = radius;

            _bitmap.SetPixel((int)ellipse.MidPoint.X, (int)ellipse.MidPoint.Y + radius, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X, (int)ellipse.MidPoint.Y - radius, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X                          + radius, (int)ellipse.MidPoint.Y, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X                          - radius, (int)ellipse.MidPoint.Y, color);

            while (x < y) {
                if (f >= 0) {
                    y     -= 1;
                    ddF_y += 2;
                    f     += ddF_y;
                }

                x     += 1;
                ddF_x += 2;
                f     += ddF_x + 1;

                _bitmap.SetPixel((int)ellipse.MidPoint.X + x, (int)ellipse.MidPoint.Y + y, color);
                _bitmap.SetPixel((int)ellipse.MidPoint.X - x, (int)ellipse.MidPoint.Y + y, color);
                _bitmap.SetPixel((int)ellipse.MidPoint.X + x, (int)ellipse.MidPoint.Y - y, color);
                _bitmap.SetPixel((int)ellipse.MidPoint.X - x, (int)ellipse.MidPoint.Y - y, color);
                _bitmap.SetPixel((int)ellipse.MidPoint.X + y, (int)ellipse.MidPoint.Y + x, color);
                _bitmap.SetPixel((int)ellipse.MidPoint.X - y, (int)ellipse.MidPoint.Y + x, color);
                _bitmap.SetPixel((int)ellipse.MidPoint.X + y, (int)ellipse.MidPoint.Y - x, color);
                _bitmap.SetPixel((int)ellipse.MidPoint.X - y, (int)ellipse.MidPoint.Y - x, color);
            }

            return;
        }

        //Ellipse
        int    dx    = 0,                                                     dy = (int)ellipse.VerticalStretch;
        double a2    = ellipse.HorizontalStretch * ellipse.HorizontalStretch, b2 = ellipse.VerticalStretch * ellipse.VerticalStretch;
        double error = b2 - (2 * ellipse.VerticalStretch - 1) * a2,           errorDoubled;

        while (dy >= 0) {
            _bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y - dy, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y - dy, color);

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
            _bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y + dy, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y - dy, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y - dy, color);

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
            _bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y, color);
            _bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y, color);
        }
    }

    public void DrawTriangle(Triangle2D<int> triangle, Color color) {
        DrawTriangle(triangle.Point1, triangle.Point2, triangle.Point3, color);
    }

    public void DrawTriangle(Point2D<int> point1, Point2D<int> point2, Point2D<int> point3, Color color) {
        DrawLine(point1, point2, color);
        DrawLine(point2, point3, color);
        DrawLine(point3, point1, color);
    }

    public void DrawTriangleFilled(Triangle2D<int> triangle, Color color) {

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

        Point2D<int> splitPoint= new((int)(top.X + ((float)(middle.Y - top.Y) / (float)(bottom.Y - top.Y)) * (bottom.X - top.X)), middle.Y);
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
    private void DrawBottomFlatTriangle(Point2D<int> top, Point2D<int> bottomLeft, Point2D<int> bottomRight, System.Drawing.Color color) {
        double inverseSlopeLeft  = (bottomLeft.X - top.X) / (bottomLeft.Y - top.Y);
        double inverseSlopeRight = (double)(bottomRight.X - top.X) / (bottomRight.Y - top.Y);

        double currentXPositionLeft  = top.X;
        double currentXPositionRight = top.X;

        for(int scanlineY = top.Y; scanlineY <= bottomLeft.Y; scanlineY++) {
            DrawLine(new Line2D<int>(new((int)System.Math.Floor(currentXPositionLeft), scanlineY), new((int)System.Math.Floor(currentXPositionRight), scanlineY)), color);
            currentXPositionLeft  += inverseSlopeLeft;
            currentXPositionRight += inverseSlopeRight;
        }
    }
    private void DrawTopFlatTriangle(Point2D<int> topRight, Point2D<int> topLeft, Point2D<int> bottom, System.Drawing.Color color) {
        double inverseSlopeLeft  = (bottom.X - topLeft.X) / (bottom.Y - topLeft.Y);
        double inverseSlopeRight = (double)(bottom.X - topRight.X) / (bottom.Y - topRight.Y);

        double currentXPositionLeft  = bottom.X;
        double currentXPositionRight = bottom.X;

        for (int scanlineY = bottom.Y; scanlineY > topRight.Y; scanlineY--) {
            DrawLine(new Line2D<int>(new((int)System.Math.Floor(currentXPositionLeft), scanlineY), new((int)System.Math.Floor(currentXPositionRight), scanlineY)), color);
            currentXPositionLeft  -= inverseSlopeLeft;
            currentXPositionRight -= inverseSlopeRight;
        }
    }

}