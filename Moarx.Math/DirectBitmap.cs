using System.Drawing;

namespace Moarx.Math;

public class DirectBitmap {

    int BytesPerPixel = 3;
    private int _height;
    private int _width;

    public DirectBitmap(int width, int height) {
        Width = width;
        Height = height;
        Bits = new byte[Stride * height];
    }
    public DirectBitmap(int width, int height, byte[] b) {
        if(b.Length != width * height * 3) {
            throw new ArgumentException("The byte array does not match with the width and hight");
        }

        Width = width;
        Height = height;
        Bits = b;
    }
    public DirectBitmap(int width, int height, byte[] b, int bytesPerPixel) {
        Width = width;
        Height = height;
        Bits = b;
        BytesPerPixel = bytesPerPixel;
    }

    public byte[] Bits { get; }

    public int Height {
        get => _height;
        set {
            if (double.IsNaN(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Height is NaN");
            }
            if (value <= 0) {
                throw new ArgumentOutOfRangeException(nameof(value), "value is less or 0");
            }
            _height = value;
        }
    }
    public int Width {
        get => _width;
        set {
            if (double.IsNaN(value)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Width is NaN");
            }
            if (value <= 0) {
                throw new ArgumentOutOfRangeException(nameof(value), "value is less or 0");
            }
            _width = value;
        }
    }

    public int Stride => Width * BytesPerPixel;

    public void Clear() {
        Array.Clear(Bits, 0, Bits.Length);
    }
    public void FloodFill(int x, int y, Color newColor) {

        newColor = Color.FromArgb(newColor.ToArgb()); // get rid of named Color...

        var replaceColor = GetPixel(x, y);
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
            ProcessPixel(x + 1, y);
            ProcessPixel(x - 1, y);

        }

        void ProcessPixel(int x, int y) {

            var currentColor = SafeGetPixel(x, y);

            if (currentColor == replaceColor) {
                stack.Push((x, y));
                SetPixel(x, y, newColor);
            }
        }

        Color? SafeGetPixel(int x, int y) {

            if (x < 0 || x >= Width ||
                y < 0 || y >= Height) {
                return null;
            }

            return GetPixel(x, y);
        }
    }
    public void SetPixel(int x, int y, Color color) {

        int index = GetIndex(x, y);

        Bits[index + 0] = color.B;
        Bits[index + 1] = color.G;
        Bits[index + 2] = color.R;

    }

    public Color this[int x, int y] {
        get => GetPixel(x, y);
        set => SetPixel(x, y, value);
    }
    public Color GetPixel(int x, int y) {

        int index = GetIndex(x, y);

        int b = Bits[index + 0];
        int g = Bits[index + 1];
        int r = Bits[index + 2];

        return Color.FromArgb(alpha: 255, red: r, green: g, blue: b);
    }

    int GetIndex(int x, int y) => (x + y * Width) * BytesPerPixel;

}

