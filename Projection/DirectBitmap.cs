using System;
using System.Drawing;

namespace Projection {

    public class DirectBitmap {

        const int BytesPerPixel = 3;

        public DirectBitmap(int width, int height) {
            Width  = width;
            Height = height;
            Bits   = new byte[Stride * height];
        }

        public byte[] Bits { get; }

        public int Height { get; }
        public int Width  { get; }

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

}