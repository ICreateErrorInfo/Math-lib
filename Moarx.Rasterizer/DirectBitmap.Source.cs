using System.Drawing;

namespace Moarx.Rasterizer {

    public abstract partial class DirectBitmap {

        public sealed class DirectBitmapSource: DirectBitmap {

            public DirectBitmapSource(int width, int height) :
                this(width: width, height: height, bytes: new byte[width * 3 * height], bytesPerPixel: 3) {
            }
            public DirectBitmapSource(int width, int height, int bytesPerPixel) :
                this(width: width, height: height, bytes: new byte[width * bytesPerPixel * height], bytesPerPixel: bytesPerPixel) {
            }

            public DirectBitmapSource(int width, int height, byte[] bytes)
                : this(width: width, height: height, bytes: bytes, bytesPerPixel: 3) {
            }

            public DirectBitmapSource(int width, int height, byte[] bytes, int bytesPerPixel) {

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

                Width = width;
                Height = height;
                Bits = bytes;
                //TODO support alpha channel in ToImageSource: bgr24 to bgra32
                BytesPerPixel = 3;
            }

            public byte[] Bits { get; }

            public override int Width { get; }
            public override int Height { get; }

            public override int BytesPerPixel { get; }

            public override int Stride => Width * BytesPerPixel;

            public override void Clear() {
                Array.Clear(Bits, 0, Bits.Length);
            }

            public override void SetPixel(int x, int y, Color color) {

                int index = GetIndex(x, y);
                double alphaA = (double)color.A / 255;

                if (BytesPerPixel == 4) {
                    double alphaB = (double)Bits[index + 3] / 255;
                    double newAlpha = alphaA + (1 - alphaA) * alphaB;

                    Bits[index + 0] = (byte)(1 / newAlpha * (alphaA * color.B + (1 - alphaA) * alphaB * Bits[index + 0]));
                    Bits[index + 1] = (byte)(1 / newAlpha * (alphaA * color.G + (1 - alphaA) * alphaB * Bits[index + 1]));
                    Bits[index + 2] = (byte)(1 / newAlpha * (alphaA * color.R + (1 - alphaA) * alphaB * Bits[index + 2]));
                    //Bits[index + 3] = (byte)(newAlpha * 255);
                    //TODO support alpha channel in ToImageSource: bgr24 to bgra32
                }

                Bits[index + 0] = (byte)(alphaA * color.B + (1 - alphaA) * Bits[index + 0]);
                Bits[index + 1] = (byte)(alphaA * color.G + (1 - alphaA) * Bits[index + 1]);
                Bits[index + 2] = (byte)(alphaA * color.R + (1 - alphaA) * Bits[index + 2]);
            }

            public override Color GetPixel(int x, int y) {

                int index = GetIndex(x, y);

                int b = Bits[index + 0];
                int g = Bits[index + 1];
                int r = Bits[index + 2];

                return Color.FromArgb(alpha: 255, red: r, green: g, blue: b);
            }

            public override byte[] GetBytes() => Bits;

            int GetIndex(int x, int y) => (x + y * Width) * BytesPerPixel;

        }

    }

}