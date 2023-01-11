using Math_lib;
using Moarx.Rasterizer;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace RasterizerTest {

    public partial class MainWindow: Window {
        //TODO implement new Rasterizer
        public MainWindow() {
            InitializeComponent();

            Moarx.Math.DirectBitmap bmp = new Moarx.Math.DirectBitmap(500, 500);

            MoarxGraphics graphics = new MoarxGraphics(bmp);
            //graphics.DrawLine(new Moarx.Math.Line2D(new(0, 0), new(10, 5)), System.Drawing.Color.White);
            graphics.DrawEllipse(new Moarx.Math.Ellipse2D(new(250, 350), 100, 100), System.Drawing.Color.White);

            Display.Source = ToImageSource(graphics.Bitmap);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            base.OnRenderSizeChanged(sizeInfo);
            //r.UpdateSize((int)sizeInfo.NewSize.Width, (int)sizeInfo.NewSize.Height);

            //r.DrawLine(p1: new(0,0), p2: new(2, 0), c: System.Drawing.Color.Red, thickness: 1);

            //r.DrawCircle(p1: new(0,0), radius: 1, c: System.Drawing.Color.White, fill: true);

            //r.DrawCircle(new(0,0), 1, System.Drawing.Color.White, true);
            //r.DrawLine(new(-1,-1), new(1,1), System.Drawing.Color.White);

            //r.DrawRectangle(new(-1, 1), new(1, -1), System.Drawing.Color.White, true);
            //r.DrawLine(new(-1, 1), new(1, -1), System.Drawing.Color.White, 1);

            //r.DrawLine(new(0, 0), new(80, 8), System.Drawing.Color.White, 0.1);

            //r.DrawPoint(new(0.5,0.5), System.Drawing.Color.White);

            //r.DrawEllipse(new(0,0), 19, 2, System.Drawing.Color.White, true);

            //r.DrawQuadBezier(new(0,0), new(0, 2), new(2,2), System.Drawing.Color.White);

            //r.DrawPoint(new(0.5,0.5), System.Drawing.Color.White);
            //r.DrawTriangle(new(0,0), new(1,0), new(2,-2), System.Drawing.Color.AliceBlue);

            var i = Vector3D.Normalize(new Vector3D(3, 4, 5)).GetLength();

            //Display.Source = r.GetSource();
        }

        public static ImageSource ToImageSource(Moarx.Math.DirectBitmap bitmap) {

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
