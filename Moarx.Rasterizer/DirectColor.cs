namespace Moarx.Rasterizer {

    public readonly record struct DirectColor {

        public byte R { get; init; }
        public byte G { get; init; }
        public byte B { get; init; }
        public byte A { get; init; }

        public static DirectColor FromRgba(byte r, byte g, byte b, byte a) {
            return new() {
                R = r,
                G = g,
                B = b,
                A = a
            };
        }

        public static DirectColor FromArgb(byte alpha, byte red, byte green, byte blue) {
            return new() {
                R = red,
                G = green,
                B = blue,
                A = alpha
            };
        }

        public static DirectColor FromArgb(byte alpha, DirectColor color) {
            return color with {
                A = alpha
            };
        }

        public static DirectColor FromRgb(byte red, byte green, byte blue) {
            return new() {
                R = red,
                G = green,
                B = blue,
                A = 255
            };
        }

    }

}