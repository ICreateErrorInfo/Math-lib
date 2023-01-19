using Moarx.Math;

namespace Moarx.Rasterizer;

public abstract partial class DirectBitmap {

    public static DirectBitmapSource Create(int width, int height) {
        return new DirectBitmapSource(width, height);
    }

    public static DirectBitmapSource Create(int width, int height, int bytesPerPixel) {
        return new DirectBitmapSource(width, height, bytesPerPixel);
    }

    public static DirectBitmapSource Create(int width, int height, byte[] bytes) {
        return new DirectBitmapSource(width, height, bytes);

    }

    public static DirectBitmapSource Create(int width, int height, byte[] bytes, int bytesPerPixel) {
        return new DirectBitmapSource(width, height, bytes, bytesPerPixel);
    }

    public abstract int Width  { get; }
    public abstract int Height { get; }

    public abstract int BytesPerPixel { get; }

    public DirectColor this[int x, int y] {
        get => GetPixel(x, y);
        set => SetPixel(x, y, value);
    }

    public abstract int Stride { get; }

    public abstract void Clear();
    public abstract void SetPixel(int x, int y, DirectColor color);

    public void BlendPixel(int x, int y, DirectColor color) {
        var dst = GetPixel(x, y);
        SetPixel(x, y, color.Blend(dst));
    }

    public abstract DirectColor GetPixel(int x, int y);

    public abstract byte[] GetBytes();

    public SlicedDirectBitmap Slice(Rectangle2D<int> sliceRectangle) => new(this, sliceRectangle);

}