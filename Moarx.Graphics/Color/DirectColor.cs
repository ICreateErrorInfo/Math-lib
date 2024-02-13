namespace Moarx.Graphics.Color {

    [Serializable]
    public readonly record struct DirectColor {

        public const int BlueChannel  = 0;
        public const int GreenChannel = 1;
        public const int RedChannel   = 2;
        public const int AlphaChannel = 3;

        public const int MinChannelValue = 0;
        public const int MaxChannelValue = 255;

        public byte R { get; init; }
        public byte G { get; init; }
        public byte B { get; init; }
        public byte A { get; init; }

        public byte this[int channel] {
            get {

                return channel switch {
                    BlueChannel => B,
                    GreenChannel => G,
                    RedChannel => R,
                    AlphaChannel => A,
                    _ => throw new ArgumentOutOfRangeException(nameof(channel), channel, $"valid range is [{MinChannelValue}, {MaxChannelValue}]")
                };
            }
        }

        public byte GetMaxColorChannelValue() {
            return System.Math.Max(B, System.Math.Max(G, R));
        }

        public byte GetAverageColorChannelValue() {
            return (byte)((B + G + R) / 3);
        }

        public DirectColor Blend(DirectColor b) => Blend(this, b);

        public static DirectColor Blend(DirectColor a, DirectColor b) {

            // Eine Farbe mit Alpha 255 gewinnt immer.
            if (a.A == MaxChannelValue) {
                return a;
            }

            var alphaA = a.A / (double)MaxChannelValue;

            if (b.A == MaxChannelValue) {
                // Farbe b hat keinen alpha Kanal
                var newAlphaB = 1 - alphaA;
                return FromRgb(
                    red: (byte)(alphaA * a.R + newAlphaB * b.R),
                    green: (byte)(alphaA * a.G + newAlphaB * b.G),
                    blue: (byte)(alphaA * a.B + newAlphaB * b.B)
                );
            } else {
                // Farbe a und b haben einen Alphawert < 255
                // Porter-Duff-Algorithmus 
                var alphaB    = b.A / (double)MaxChannelValue;
                var newAlphaB = (1 - alphaA) * alphaB;

                var alphaC    = alphaA + (1 - alphaA) * alphaB;
                var invAlphaC = 1 / alphaC;

                return FromRgba(
                    red: (byte)(invAlphaC * (alphaA * a.R + newAlphaB * b.R)),
                    green: (byte)(invAlphaC * (alphaA * a.G + newAlphaB * b.G)),
                    blue: (byte)(invAlphaC * (alphaA * a.B + newAlphaB * b.B)),
                    alpha: (byte)(MaxChannelValue * alphaC)
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
                A = MaxChannelValue
            };
        }

        public static DirectColor FromRgb(RGB rgb) {
            return new() {
                R = (byte)(rgb.R * MaxChannelValue),
                G = (byte)(rgb.G * MaxChannelValue),
                B = (byte)(rgb.B * MaxChannelValue),
                A = MaxChannelValue
            };
        }

    }

}