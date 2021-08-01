using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Projection {

    public static class DirectBitmapExtensions {

        public static ImageSource ToImageSource(this DirectBitmap bitmap) {

            var bs = BitmapSource.Create(
                pixelWidth: bitmap.Width,
                pixelHeight: bitmap.Height,
                dpiX: 96,
                dpiY: 96,
                pixelFormat: PixelFormats.Bgr24,
                palette: null,
                pixels: bitmap.Bits,
                stride: bitmap.Stride);

            return bs;

        }

    }

}