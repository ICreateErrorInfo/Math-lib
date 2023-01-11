using Moarx.Math;
using System.Drawing;

namespace Moarx.Rasterizer;
public class MoarxGraphics
{
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
}
