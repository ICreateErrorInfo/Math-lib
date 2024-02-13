﻿using Moarx.Graphics.Color;
using Moarx.Math;

namespace Moarx.Graphics;

public abstract partial class DirectBitmap {

    public static DirectBitmapSource Create(int width, int height) {
        return new DirectBitmapSource(width, height);
    }

    public static DirectBitmapSource Create(int width, int height, int bytesPerPixel) {
        return new DirectBitmapSource(width, height, bytesPerPixel);
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

    public void Clear(DirectColor color) {
        // TODO Optmieren...
        for (var y = 0; y < Height; y++) {
            for (var x = 0; x < Width; x++) {
                SetPixel(x, y, color);
            }
        }
    }

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