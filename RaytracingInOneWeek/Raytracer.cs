﻿using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Math_lib;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Math_lib.Spectrum;
using System.Windows.Media;

namespace Raytracing
{
    public class Raytracer
    {
        private System.Windows.Controls.Image _image;
        private System.Windows.Controls.ProgressBar _progressBar;
        private System.Windows.Controls.TextBlock _time;

        public Raytracer(System.Windows.Controls.Image image,
                         System.Windows.Controls.ProgressBar progressBar,
                         System.Windows.Controls.TextBlock time)
        {
            _image = image;
            _progressBar = progressBar;
            _time = time;
        }

        public static void Init() {
            SampledSpectrum.Init();
        }
        public async void RenderScene(Scene scene)
        {
            Stopwatch sw = Stopwatch.StartNew();

            Init();

            var progress = new Progress<ProgressData>(OnProgress);

            var imageData = await Task.Run(() => RenderImageData(progress, scene));

            var bmp = ToBitmap(imageData);

            _image.Source = ToImageSource(bmp);

            _progressBar.Visibility = Visibility.Collapsed;

            sw.Stop();
            _time.Text = (sw.ElapsedMilliseconds / 1000.0).ToString() + "s";
        }
        private ImageData RenderImageData(IProgress<ProgressData> progress, Scene scene)
        {
            int samplesPerPixel = scene.SamplesPerPixel;
            int maxDepth = scene.MaxDepth;
            var world = scene.Accel;

            Point3D lookfrom = scene.Lookfrom;
            Point3D lookat = scene.Lookat;
            var vfov = scene.VFov;
            double aperture = scene.Aperture;
            SampledSpectrum background = scene.Background;

            int imageWidth = scene.ImageWidth;
            int imageHeight = scene.ImageHeight;

            double aspectRatio = imageWidth / (double)imageHeight;

            //Camera
            Vector3D vup = scene.VUp;
            var distToFocus = scene.FocusDistance;

            Camera cam = new Camera(lookfrom, lookat, vup, vfov, aspectRatio, aperture, distToFocus, 0, 1);

            SampledSpectrum[,] vArr = new SampledSpectrum[imageHeight, imageWidth];

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
                    SampledSpectrum pixelColor = SampledSpectrum.FromRGB(new double[] {0, 0, 0 }, SampledSpectrum.SpectrumType.Reflectance);
                    for (int s = 0; s < samplesPerPixel; s++)
                    {
                        var u = (i + ((double)RandX[s] / byte.MaxValue)) / (imageWidth - 1);
                        var v = (j + ((double)RandX[s] / byte.MaxValue)) / (imageHeight - 1);
                        Ray r = cam.get_ray(u, v);

                        pixelColor += ray_color(r, background, world, maxDepth);
                    }

                    vArr[j, i] = pixelColor;
                }
            }
            );
            return new ImageData(
                data: vArr,
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

            public ImageData(SampledSpectrum[,] data, int width, int height, int samplesPerPixel)
            {
                Data = data;
                Width = width;
                Height = height;
                SamplesPerPixel = samplesPerPixel;
            }

            public SampledSpectrum[,] Data { get; }
            public int Width { get; }
            public int Height { get; }
            public int SamplesPerPixel { get; }

        }

        private static DirectBitmap ToBitmap(ImageData imageData)
        {
            DirectBitmap bmp = new DirectBitmap(imageData.Width, imageData.Height);

            for (int j = 0; j < imageData.Height; j++)
            {
                for (int i = 0; i < imageData.Width; i++)
                {
                    bmp.SetPixel(i, (j - (imageData.Height - 1)) * -1, toColor(imageData.Data[j, i], imageData.SamplesPerPixel));
                }
            }

            return bmp;
        }
        public static SampledSpectrum ray_color(Ray r, SampledSpectrum background, Primitive world, int depth)
        {
            SurfaceInteraction isect = new SurfaceInteraction();

            if (depth <= 0)
            {
                return SampledSpectrum.FromRGB(new double[] { 0, 0, 0 }, SampledSpectrum.SpectrumType.Reflectance);
            }

            if (!world.Intersect(r, out isect))
            {
                return background;
            }
            Ray scattered = new Ray();
            SampledSpectrum attenuation = new SampledSpectrum();
            SampledSpectrum emitted = isect.Primitive.GetMaterial().Emitted(isect.U, isect.V, isect.P);

            if (!isect.Primitive.GetMaterial().Scatter(r, ref isect, out attenuation, out scattered))
            {
                return emitted;
            }

            return emitted + attenuation * ray_color(scattered, background, world, depth - 1);
        }
        public static System.Drawing.Color toColor(SampledSpectrum pixel_color, int samples_per_pixel)
        {
            var scale = (double)1 / (double)samples_per_pixel;

            for (int i = 0; i < pixel_color.NSamples; i++) {
                pixel_color.c[i] = Math.Sqrt(scale * pixel_color.c[i]);
            }

            double[] rgb = pixel_color.ToRGB();

            var r = rgb[0];
            var g = rgb[1];
            var b = rgb[2];

            return System.Drawing.Color.FromArgb(Convert.ToInt32(255 * Math.Clamp(r, 0, 0.999)),
                                  Convert.ToInt32(255 * Math.Clamp(g, 0, 0.999)),
                                  Convert.ToInt32(255 * Math.Clamp(b, 0, 0.999)));
        }
        public static ImageSource ToImageSource(DirectBitmap bitmap) {

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
