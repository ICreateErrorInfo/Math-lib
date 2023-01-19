namespace Moarx.Rasterizer {

    public readonly record struct DirectColor {

        public const int BlueChannel  = 0;
        public const int GreenChannel = 1;
        public const int RedChannel   = 2;
        public const int AlphaChannel = 3;

        public byte R { get; init; }
        public byte G { get; init; }
        public byte B { get; init; }
        public byte A { get; init; }

        public byte this[int channel] {
            get {

                return channel switch {
                    BlueChannel  => B,
                    GreenChannel => G,
                    RedChannel   => R,
                    AlphaChannel => A,
                    _            => throw new ArgumentOutOfRangeException(nameof(channel), channel, "valid range is [0,3]")
                };
            }
        }

        public byte GetMaxColorChannelValue() {
            return System.Math.Max(B, System.Math.Max(G, R));
        }

        public byte GetAverageColorChannelValue() {
            return (byte)((B + G + R) / 3);
        }

        public static DirectColor Blend(DirectColor a, DirectColor b) {

            // Eine Farbe mit Alpha 255 gewinnt eh immer.
            if (a.A == 255) {
                return a;
            }

            var aa = a.A / 255.0;

            if (b.A == 255) {
                // Farbe b hat keinen alpha Kanal
                var nAb = 1 - aa;
                return FromRgb(
                    red: (byte)(aa   * a.R + nAb * b.R),
                    green: (byte)(aa * a.G + nAb * b.G),
                    blue: (byte)(aa  * a.B + nAb * b.B)
                );
            } else {
                // Farbe a und b haben einen Alphawert < 255
                // Porter-Duff-Algorithmus 
                var ab    = b.A / 255.0;
                var ac    = aa + (1 - aa) * ab;
                var invAc = 1        / ac;
                var nAb   = (1 - aa) * ab;

                return FromRgba(
                    red: (byte)(invAc   * (aa * a.R + nAb * b.R)),
                    green: (byte)(invAc * (aa * a.G + nAb * b.G)),
                    blue: (byte)(invAc  * (aa * a.B + nAb * b.B)),
                    alpha: (byte)(255   * ac)
                );
            }
        }

        public static DirectColor FromRgba(byte red, byte green, byte blue, byte alpha) {
            return new() {
                R = red,
                G = green,
                B = blue,
                A = alpha
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
            return color with { A = alpha };
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