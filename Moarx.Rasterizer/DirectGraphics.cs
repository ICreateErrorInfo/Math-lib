using Moarx.Math;
using System.Drawing;

namespace Moarx.Rasterizer;

public class DirectGraphics {

    readonly DirectBitmap _bitmap;

    public DirectGraphics(DirectBitmap bitmap) {
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

    public void DrawLine(Point2D<double> start, Point2D<double> end, Color color) {
        DrawLine(new(start, end), color);
    }

    public void DrawLine(Line2D<double> line, Color color) {
        Vector2D<double> slope = line.EndPoint - line.StartPoint;

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

    public void DrawEllipse(Ellipse2D<double> ellipse, Color color) {
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

    public void DrawTriangle(Triangle2D<double> triangle, Color color) {
        DrawTriangle(triangle.Point1, triangle.Point2, triangle.Point3, color);
    }

    public void DrawTriangle(Point2D<double> point1, Point2D<double> point2, Point2D<double> point3, Color color) {
        DrawLine(point1, point2, color);
        DrawLine(point2, point3, color);
        DrawLine(point3, point1, color);
    }

    public void DrawTriangleFilled(Triangle2D<double> triangle, Color color) {

    }

}