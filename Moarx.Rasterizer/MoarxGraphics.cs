using Moarx.Math;
using System.Drawing;

namespace Moarx.Rasterizer;
public class MoarxGraphics {
    public readonly DirectBitmap Bitmap;

    public MoarxGraphics(DirectBitmap bitmap) {
        Bitmap = bitmap;
    }

    public void DrawLine(Line2D line, Color color) {
        Vector2D<double> slope = line.EndPoint - line.StartPoint;

        int dx =  System.Math.Abs((int)slope.X);
        int dy = -System.Math.Abs((int)slope.Y);

        int sx = line.StartPoint.X < line.EndPoint.X ? 1 : -1;
        int sy = line.StartPoint.Y < line.EndPoint.Y ? 1 : -1;

        int error = dx + dy;
        int errorDoubled;

        int newX = (int)line.StartPoint.X;
        int newY = (int)line.StartPoint.Y;

        while (true) {
            Bitmap.SetPixel(newX, newY, color);

            if (newX == line.EndPoint.X && newY == line.EndPoint.Y)
                break;

            errorDoubled = 2 * error;
            if (errorDoubled > dy) { error += dy; newX += sx; }
            if (errorDoubled < dx) { error += dx; newY += sy; }
        }
    }
    public void DrawEllipse(Ellipse2D ellipse, Color color) {
        //Circle
        if(ellipse.VerticalStretch == ellipse.HorizontalStretch) {
            int radius = (int)ellipse.VerticalStretch;

            int f = 1 - radius;
            int ddF_x = 0;
            int ddF_y = -2 * radius;
            int x = 0;
            int y = radius;

            Bitmap.SetPixel((int)ellipse.MidPoint.X, (int)ellipse.MidPoint.Y + radius, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X, (int)ellipse.MidPoint.Y - radius, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X + radius, (int)ellipse.MidPoint.Y, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X - radius, (int)ellipse.MidPoint.Y, color);

            while (x < y) {
                if (f >= 0) {
                    y -= 1;
                    ddF_y += 2;
                    f += ddF_y;
                }
                x += 1;
                ddF_x += 2;
                f += ddF_x + 1;

                Bitmap.SetPixel((int)ellipse.MidPoint.X + x, (int)ellipse.MidPoint.Y + y, color);
                Bitmap.SetPixel((int)ellipse.MidPoint.X - x, (int)ellipse.MidPoint.Y + y, color);
                Bitmap.SetPixel((int)ellipse.MidPoint.X + x, (int)ellipse.MidPoint.Y - y, color);
                Bitmap.SetPixel((int)ellipse.MidPoint.X - x, (int)ellipse.MidPoint.Y - y, color);
                Bitmap.SetPixel((int)ellipse.MidPoint.X + y, (int)ellipse.MidPoint.Y + x, color);
                Bitmap.SetPixel((int)ellipse.MidPoint.X - y, (int)ellipse.MidPoint.Y + x, color);
                Bitmap.SetPixel((int)ellipse.MidPoint.X + y, (int)ellipse.MidPoint.Y - x, color);
                Bitmap.SetPixel((int)ellipse.MidPoint.X - y, (int)ellipse.MidPoint.Y - x, color);
            }
            return;
        }

        //Ellipse
        int dx = 0, dy = (int)ellipse.VerticalStretch;
        double a2 = ellipse.HorizontalStretch*ellipse.HorizontalStretch, b2 = ellipse.VerticalStretch*ellipse.VerticalStretch;
        double error = b2-(2*ellipse.VerticalStretch-1)*a2, errorDoubled;

        while(dy >= 0) {
            Bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y + dy, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y + dy, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y - dy, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y - dy, color);

            errorDoubled = 2 * error;
            if (errorDoubled < (2 * dx + 1) * b2) { ++dx; error += (2 * dx + 1) * b2; }
            if (errorDoubled > -(2 * dy - 1) * a2) { --dy; error -= (2 * dy - 1) * a2; }
        }

        do {
            Bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y + dy, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y + dy, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y - dy, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y - dy, color);

            errorDoubled = 2 * error;
            if (errorDoubled < (2 * dx + 1) * b2) { ++dx; error += (2 * dx + 1) * b2; }
            if (errorDoubled > -(2 * dy - 1) * a2) { --dy; error -= (2 * dy - 1) * a2; }
        }
        while (dy >= 0);

        while (dx++ < ellipse.HorizontalStretch)
        {
            Bitmap.SetPixel((int)ellipse.MidPoint.X + dx, (int)ellipse.MidPoint.Y, color);
            Bitmap.SetPixel((int)ellipse.MidPoint.X - dx, (int)ellipse.MidPoint.Y, color);
        }
    }
}
