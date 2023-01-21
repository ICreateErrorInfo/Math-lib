﻿using Moarx.Rasterizer;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Moarx.Math;

namespace RasterizerTest {

    public partial class MainWindow: Window {

        //TODO implement new Rasterizer
        public MainWindow() {
            InitializeComponent();

            int width         = 513;
            int height        = 513;
            int bytesPerPixel = 4;

            byte[] data = new byte[width * height * bytesPerPixel];

            var bmp = DirectBitmap.Create(width: width, height: height, data, bytesPerPixel);
            bmp.Clear(DirectColors.Gray);

            var graphics = DirectGraphics.Create(bmp);

            //graphics.DrawAntiAliasedLine(new(new(1, 1), new(2, 198)), DirectColors.Black);
            graphics.DrawAntiAliasedEllipse(new(new(250, 250), 100, 100), DirectColors.White);
            //graphics.DrawTriangle(new(0, 0), new(250, 499), new(499, 250), DirectColors.White);

            //var slice = Rectangle2D.Create(
            //    x: bmp.Width     / 2, y: bmp.Height      / 2,
            //    width: bmp.Width / 2, height: bmp.Height / 2);

            //var bmpSlice  = bmp.Slice(slice);
            //var grfxSlice = DirectGraphics.Create(bmpSlice);

            //grfxSlice.FloodFill(x: 0, y: 0, newColor: DirectColors.Red);

            //graphics.DrawTriangleFilled(new(new(10, 10), new(100, 200), new(300, 300)), DirectColors.Black);
            //graphics.DrawAntiAliasedTriangleFilled(new(new(10, 10), new(100, 200), new(300, 300)), DirectColors.Black);
            //graphics.DrawTriangle(new(new(10, 10), new(100, 200), new(300, 300)), DirectColors.Black);

            Display.Source = ToImageSource(bmp);
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

            //var i = Vector3D.Normalize(new Vector3D(3, 4, 5)).GetLength();

            //Display.Source = r.GetSource();
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
