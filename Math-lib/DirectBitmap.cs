using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Math_lib
{

    public class DirectBitmap {

        const int BytesPerPixel = 3;

        public DirectBitmap(int width, int height) {
            Width  = width;
            Height = height;
            Bits   = new byte[Stride * height];
        }
        public DirectBitmap(int width, int height, byte[] b)
        {
            Width = width;
            Height = height;
            Bits = b;
        }

        public byte[] Bits { get; }

        public int Height { get; }
        public int Width  { get; }

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
                ProcessPixel(x    + 1, y);
                ProcessPixel(x    - 1, y);

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

}