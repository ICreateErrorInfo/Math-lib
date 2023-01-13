using Moarx.Math;
using System.Drawing;

namespace Moarx.Rasterizer {

    public abstract partial class DirectBitmap {

        public class SlicedDirectBitmap: DirectBitmap {

            private readonly DirectBitmap _source;

            private readonly Rectangle2D<int> _sliceRectangle;

            public SlicedDirectBitmap(DirectBitmap source, Rectangle2D<int> sliceRectangle) {

                _source = source ?? throw new ArgumentNullException(nameof(source));

                if (sliceRectangle.Left < 0) {
                    throw new ArgumentOutOfRangeException();
                }

                if (sliceRectangle.Right > _source.Width) {
                    throw new ArgumentOutOfRangeException();
                }

                if (sliceRectangle.Top < 0) {
                    throw new ArgumentOutOfRangeException();
                }

                if (sliceRectangle.Bottom > _source.Height) {
                    throw new ArgumentOutOfRangeException();
                }

                _sliceRectangle = sliceRectangle;

            }

            public override int Width => _sliceRectangle.Width;

            public override int Height => _sliceRectangle.Height;

            public override int BytesPerPixel => _source.BytesPerPixel;

            public override int Stride => Width * BytesPerPixel;

            public override void Clear() {

                for (var y = 0; y < Height; ++y) {

                    for (var x = 0; x < Width; ++x) {
                        SetPixel(x, y, Color.Black);
                    }

                }
            }

            public override void SetPixel(int x, int y, Color color) {
                var xSource = TranslateXToSource(x);
                var ySource = TranslateYToSource(y);

                if (!_sliceRectangle.PointInRect(xSource, ySource)) {
                    return;
                }

                _source.SetPixel(xSource, ySource, color);
            }

            public override Color GetPixel(int x, int y) {
                var xSource = TranslateXToSource(x);
                var ySource = TranslateYToSource(y);

                if (!_sliceRectangle.PointInRect(xSource, ySource)) {
                    return Color.Black;
                }

                return _source.GetPixel(xSource, ySource);
            }

            public override byte[] GetBytes() {

                var size     = Stride * Height;
                var dstBytes = new byte[size];

                var srcBytes = _source.GetBytes();
                for (var line = 0; line < Height; ++line) {

                    var srcIndex = line * _source.Stride + _sliceRectangle.Left;
                    var dstIndex = line * Stride;
                    Array.Copy(sourceArray: srcBytes,
                               sourceIndex: srcIndex,
                               destinationArray: dstBytes,
                               destinationIndex: dstIndex,
                               length: Stride);
                }

                return dstBytes;
            }

            private int TranslateXToSource(int value) => value + _sliceRectangle.Left;

            private int TranslateYToSource(int value) => value + _sliceRectangle.Top;

        }

    }

}