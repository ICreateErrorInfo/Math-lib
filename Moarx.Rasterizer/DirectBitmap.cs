using System.Drawing;

namespace Moarx.Rasterizer;

public class DirectBitmap {

    public DirectBitmap(int width, int height):
        this(width: width, height: height, bytes: new byte[width * 3 * height], bytesPerPixel: 3) {
    }

    public DirectBitmap(int width, int height, byte[] bytes)
        : this(width: width, height: height, bytes: bytes, bytesPerPixel: 3) {
    }

    public DirectBitmap(int width, int height, byte[] bytes, int bytesPerPixel) {

        if (width <= 0) {
            throw new ArgumentOutOfRangeException(nameof(width), "width is less or 0");
        }

        if (height <= 0) {
            throw new ArgumentOutOfRangeException(nameof(height), "height is less or 0");
        }

        if (bytesPerPixel < 1) {
            throw new ArgumentOutOfRangeException(nameof(bytesPerPixel), "bytesPerPixel is less than 1");
        }

        if (bytes.Length != width * height * bytesPerPixel) {
            throw new ArgumentException("The byte array does not match with the width and hight");
        }

        Width         = width;
        Height        = height;
        Bits          = bytes;
        BytesPerPixel = bytesPerPixel;
    }

    public int    Width         { get; }
    public int    Height        { get; }
    public byte[] Bits          { get; }
    public int    BytesPerPixel { get; }

    public int Stride => Width * BytesPerPixel;

    public void Clear() {
        Array.Clear(Bits, 0, Bits.Length);
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