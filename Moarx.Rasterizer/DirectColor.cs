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

    }

}