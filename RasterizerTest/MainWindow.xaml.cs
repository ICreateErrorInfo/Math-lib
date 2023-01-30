using Moarx.Rasterizer;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Moarx.Math;
using System.Windows.Threading;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Input;

namespace RasterizerTest {

    public partial class MainWindow: Window {

        private readonly DispatcherTimer   _timer;
        private DirectGraphics _graphics;
        private DirectBitmap _bitmap;
        private System.Windows.Point _mousePositionLeft;
        private System.Windows.Point _mousePositionRight;
        private double newHeight;

        public MainWindow() {
            InitializeComponent();

            int width         = 513;
            int height        = 513;
            int bytesPerPixel = 4;

            _bitmap = DirectBitmap.Create(width: width, height: height, bytesPerPixel);

            _graphics = DirectGraphics.Create(_bitmap);

            _timer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(1),
                IsEnabled = true
            };

            _timer.Tick += UpdateImage;
        }

        private void UpdateImage(object sender, EventArgs e) {
            _bitmap.Clear(DirectColors.Gray);
            Stopwatch stopwatch = Stopwatch.StartNew();

            //_graphics.DrawAntiAliasedLine(new(new(1, 1), new(200, 198)), DirectColors.Black);
            //_graphics.DrawEllipse(new(new(250, 250), 100, 100), DirectColors.White);
            //_graphics.DrawTriangle(new(0, 0), new(250, 499), new(499, 250), DirectColors.White);

            //var slice = Rectangle2D.Create(
            //    x: bmp.Width     / 2, y: bmp.Height      / 2,
            //    width: bmp.Width / 2, height: bmp.Height / 2);

            //var bmpSlice  = bmp.Slice(slice);
            //var grfxSlice = DirectGraphics.Create(bmpSlice);

            //grfxSlice.FloodFill(x: 0, y: 0, newColor: DirectColors.Red);

            DirectAttributes attributes = new DirectAttributes(DirectColors.Black, 10, DirectColors.Yellow);
            //_graphics.DrawTriangle(new(new(10, 10), new(100, 200), new(300, 300)), attributes);
            //_graphics.DrawEllipse(new(new(250, 250), 100, 100), attributes);
            //_graphics.DrawTriangleFilled(new(new(10, 10), new(100, 200), new(300, 300)), DirectColors.Black, DirectColors.Blue);
            //_graphics.DrawAntiAliasedTriangleFilled(new(new(10, 10), new(100, 200), new(300, 300)), DirectColors.Black);
            //_graphics.DrawTriangle(new(new(10, 10), new(100, 200), new(300, 300)), DirectColors.Black);
            //_graphics.DrawMSAATriangleFilled(new(new(10, 10), new(100, 200), new(300, 300)), 4, DirectColors.White);
            //_graphics.DrawLine(new(new(20, 20), new(200, 400)), attributes);
            QuadBezierCurve2D<int> bezierCurve2D = new();
            if (_mousePositionLeft == new System.Windows.Point(0, 0)) {
                bezierCurve2D = new QuadBezierCurve2D<int>(new(10, 500), new(200, 10), new(150, 100));
            } else {
                if (_mousePositionRight == new System.Windows.Point(0, 0)) {
                    bezierCurve2D = new QuadBezierCurve2D<int>(new(10, 500),
                                           new(200, 10),
                                           new((int)(_mousePositionLeft.X * newHeight), (int)(_mousePositionLeft.Y * newHeight)));
                } else {
                    bezierCurve2D = new QuadBezierCurve2D<int>(new(10, 500),
                                           new((int)(_mousePositionRight.X * newHeight), (int)(_mousePositionRight.Y * newHeight)),
                                           new((int)(_mousePositionLeft.X * newHeight), (int)(_mousePositionLeft.Y * newHeight)));
                }
            }


            _graphics.DrawQuadBezier(bezierCurve2D, attributes);
            _graphics.DrawEllipse(new(bezierCurve2D[0], 3), attributes);
            _graphics.DrawEllipse(new(bezierCurve2D[1], 3), attributes);
            _graphics.DrawEllipse(new(bezierCurve2D[2], 3), attributes);


            //_graphics.DrawSSAALine(new(new(20, 20), new(200, 400)), 2, 4, DirectColors.Tan);
            //_graphics.DrawRectangle(new(new(20, 20), new(150, 180)), attributes);

            Display.Source = ToImageSource(_bitmap);
            Time.Text = stopwatch.ElapsedMilliseconds.ToString() + "ms";
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            base.OnRenderSizeChanged(sizeInfo);
                newHeight = (_bitmap.Height) / (sizeInfo.NewSize.Height - 38);

        }
        protected override void OnMouseLeftButtonUp(System.Windows.Input.MouseButtonEventArgs e) {
            _mousePositionLeft = e.GetPosition(Display);
        }
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e) {
            _mousePositionRight = e.GetPosition(Display);
        }

        public static ImageSource ToImageSource(DirectBitmap bitmap) {
            var pixelFormat = PixelFormats.Bgr24;
            if (bitmap.BytesPerPixel == 4) {
                pixelFormat = PixelFormats.Bgra32;
            }

            var bs = BitmapSource.Create(
                pixelWidth: bitmap.Width,
                pixelHeight: bitmap.Height,
                dpiX: 96,
                dpiY: 96,
                pixelFormat: pixelFormat,
                palette: null,
                pixels: bitmap.GetBytes(),
                stride: bitmap.Stride);

            return bs;

        }
    }
}
