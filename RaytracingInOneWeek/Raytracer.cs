﻿using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Raytracing.Spectrum;
using Raytracing.Primitives;
using Raytracing.Camera;
using Raytracing.Mathmatic;
using Moarx.Math;
using Moarx.Rasterizer;
using Raytracing.Color;

namespace Raytracing {
    public class Raytracer
    {
        private System.Windows.Controls.Image _image;
        private System.Windows.Controls.ProgressBar _progressBar;
        private System.Windows.Controls.TextBlock _time;
        public static RGBColorSpace ColorSpace;

        public Raytracer(System.Windows.Controls.Image image,
                         System.Windows.Controls.ProgressBar progressBar,
                         System.Windows.Controls.TextBlock time)
        {
            _image = image;
            _progressBar = progressBar;
            _time = time;
        }

        public void Init() {
            SampledSpectrumConstants.Init();
            RGBToSpectrumTable.Init();
            RGBColorSpace.Init();
            ColorSpace = RGBColorSpace.sRGB;
        }
        public async void RenderScene(Scene scene)
        {
            Stopwatch timer = Stopwatch.StartNew();

            var progress = new Progress<ProgressData>(OnProgress);

            var imageData = await Task.Run(() => RenderImageData(progress, scene));

            var bitmap = ToBitmap(imageData);

            _image.Source = ToImageSource(bitmap);

            _progressBar.Visibility = Visibility.Collapsed;

            timer.Stop();
            _time.Text = (timer.ElapsedMilliseconds / 1000.0).ToString() + "s";
        }
        ImageData RenderImageData(IProgress<ProgressData> progress, Scene scene)
        {
            int samplesPerPixel = scene.SamplesPerPixel;
            int maxDepth = scene.MaxDepth;
            var worldBVHTree = scene.Accel;

            ISpectrum background = scene.Background;

            int imageWidth = scene.ImageWidth;
            int imageHeight = scene.ImageHeight;

            RGB[,] pixelArray = new RGB[imageHeight, imageWidth];

            var totalCount = imageHeight;
            var current = 0;
            progress.Report(new ProgressData(totalCount, current));

            //random numbers
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] RandX = new byte[samplesPerPixel];
            byte[] RandY = new byte[samplesPerPixel];
            crypto.GetBytes(RandX);
            crypto.GetBytes(RandY);

            Parallel.For(0, imageHeight, j => {
                //for (int j = 0; j < imageHeight; j++) {
                Interlocked.Increment(ref current);
                progress.Report(new ProgressData(totalCount, current));
                for (int i = 0; i < imageWidth; i++)
                {
                    RGB pixelColor = new RGB(0,0,0);
                    for (int s = 0; s < samplesPerPixel; s++) {
                        var u = (i + 0.5 - ((double)RandX[s] / byte.MaxValue));
                        var v = (j + 0.5 - ((double)RandY[s] / byte.MaxValue));
                        CameraSample sample = new CameraSample() { pointOnFilm = new Point2D<double>(u, v)};
                        Ray r = scene.Camera.GenerateRay(sample).generatedRay;
                        SampledWavelengths lambda = SampledWavelengths.SampleUnifrom(((double)RandX[s] / byte.MaxValue));
                        pixelColor += GetRayColor(r, background, worldBVHTree, maxDepth, lambda).ToRGB(lambda, ColorSpace);
                    }
                    pixelArray[j, i] = pixelColor / samplesPerPixel;
                }
            }
            );
            return new ImageData(
                data: pixelArray,
                width: imageWidth,
                height: imageHeight,
                samplesPerPixel: samplesPerPixel);
        }
        void OnProgress(ProgressData d)
        {

            _progressBar.Maximum = d.TotalCount;
            _progressBar.Value = d.Current;
        }
        readonly struct ProgressData
        {

            public ProgressData(int totalCount, int current)
            {
                TotalCount = totalCount;
                Current = current;
            }

            public int TotalCount { get; }
            public int Current { get; }

        }
        readonly struct ImageData
        {

            public ImageData(RGB[,] data, int width, int height, int samplesPerPixel)
            {
                Data = data;
                Width = width;
                Height = height;
                SamplesPerPixel = samplesPerPixel;
            }

            public RGB[,] Data { get; }
            public int Width { get; }
            public int Height { get; }
            public int SamplesPerPixel { get; }

        }

        DirectBitmap ToBitmap(ImageData imageData)
        {
            DirectBitmap bmp = DirectBitmap.Create(imageData.Width, imageData.Height);

            for (int j = 0; j < imageData.Height; j++)
            {
                for (int i = 0; i < imageData.Width; i++)
                {
                    var color = ToColor(imageData.Data[j, i], imageData.SamplesPerPixel);
                    bmp.SetPixel(i, (j - (imageData.Height - 1)) * -1, DirectColor.FromRgb(color.R, color.G, color.B));
                }
            }

            return bmp;
        }

        RGB ToSensorRGB(SampledSpectrum L, SampledWavelengths lambda, double imagingRatio) {
            L = SampledSpectrum.SafeDiv(L, lambda.PDF());
            return imagingRatio * new RGB((SampledSpectrumConstants.XNew.Sample(lambda) * L).Average(),
                                          (SampledSpectrumConstants.YNew.Sample(lambda) * L).Average(),
                                          (SampledSpectrumConstants.ZNew.Sample(lambda) * L).Average());
        }

        SampledSpectrum GetRayColor(Ray ray, ISpectrum background, Primitive world, int depth, SampledWavelengths lambda)
        {
            SurfaceInteraction interaction = new SurfaceInteraction();

            if (depth <= 0)
            {
                return new RGBAlbedoSpectrum(ColorSpace, new(0,0,0)).Sample(lambda);
            }

            interaction = world.Intersect(ray, interaction);
            if (!interaction.HasIntersection)
            {
                if(depth == 50) {

                }
                return background.Sample(lambda);
            }
            ISpectrum emitted = interaction.Primitive.GetMaterial().Emitted(interaction.UCoordinate, interaction.VCoordinate, interaction.P);

            interaction = interaction.Primitive.GetMaterial().Scatter(ray, interaction);

            if (!interaction.HasScattered)
            {
                return 20 * emitted.Sample(lambda);
            }

            return emitted.Sample(lambda) + interaction.Attenuation.Sample(lambda) * GetRayColor(interaction.ScatteredRay, background, world, depth - 1, lambda);
        }
        System.Drawing.Color ToColor(RGB pixel_color, int samples_per_pixel)
        {
            var r = pixel_color[0];
            var g = pixel_color[1];
            var b = pixel_color[2];

            return System.Drawing.Color.FromArgb(Convert.ToInt32(255 * Math.Clamp(r, 0, 0.999)),
                                                 Convert.ToInt32(255 * Math.Clamp(g, 0, 0.999)),
                                                 Convert.ToInt32(255 * Math.Clamp(b, 0, 0.999)));
        }
        static ImageSource ToImageSource(DirectBitmap bitmap) {

            var bs = BitmapSource.Create(
                pixelWidth: bitmap.Width,
                pixelHeight: bitmap.Height,
                dpiX: 96,
                dpiY: 96,
                pixelFormat: PixelFormats.Bgra32,
                palette: null,
                pixels: bitmap.GetBytes(),
                stride: bitmap.Stride);

            return bs;

        }
    }
}
