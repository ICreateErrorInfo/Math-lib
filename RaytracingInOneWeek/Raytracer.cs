using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Math_lib;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Raytracing.Spectrum;

namespace Raytracing {
    public class Raytracer
    {
        private System.Windows.Controls.Image _image;
        private System.Windows.Controls.ProgressBar _progressBar;
        private System.Windows.Controls.TextBlock _time;
        private SpectrumFactory _factory;

        public Raytracer(SpectrumFactory spectrumFactory,
                         System.Windows.Controls.Image image,
                         System.Windows.Controls.ProgressBar progressBar,
                         System.Windows.Controls.TextBlock time)
        {
            _image = image;
            _progressBar = progressBar;
            _time = time;
            _factory = spectrumFactory;
        }

        public void Init() {
            SampledSpectrumConstants.Init();
        }
        public async void RenderScene(Scene scene)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var progress = new Progress<ProgressData>(OnProgress);

            var imageData = await Task.Run(() => RenderImageData(progress, scene));

            var bmp = ToBitmap(imageData);

            _image.Source = ToImageSource(bmp);

            _progressBar.Visibility = Visibility.Collapsed;

            sw.Stop();
            _time.Text = (sw.ElapsedMilliseconds / 1000.0).ToString() + "s";
        }
        ImageData RenderImageData(IProgress<ProgressData> progress, Scene scene)
        {
            int samplesPerPixel = scene.SamplesPerPixel;
            int maxDepth = scene.MaxDepth;
            var world = scene.Accel;

            Point3D lookfrom = scene.Lookfrom;
            Point3D lookat = scene.Lookat;
            var vfov = scene.VFov;
            double aperture = scene.Aperture;
            ISpectrum background = scene.Background;

            int imageWidth = scene.ImageWidth;
            int imageHeight = scene.ImageHeight;

            double aspectRatio = imageWidth / (double)imageHeight;

            //Camera
            Vector3D vup = scene.VUp;
            var distToFocus = scene.FocusDistance;

            Camera cam = new Camera(lookfrom, lookat, vup, vfov, aspectRatio, aperture, distToFocus, 0, 1);

            ISpectrum[,] vArr = new ISpectrum[imageHeight, imageWidth];

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
                    ISpectrum pixelColor = _factory.CreateFromRGB(new double[] {0, 0, 0 }, SpectrumMaterialType.Reflectance);
                    for (int s = 0; s < samplesPerPixel; s++)
                    {
                        var u = (i + ((double)RandX[s] / byte.MaxValue)) / (imageWidth - 1);
                        var v = (j + ((double)RandX[s] / byte.MaxValue)) / (imageHeight - 1);
                        Ray r = cam.get_ray(u, v);

                        pixelColor += GetRayColor(r, background, world, maxDepth);
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

            public ImageData(ISpectrum[,] data, int width, int height, int samplesPerPixel)
            {
                Data = data;
                Width = width;
                Height = height;
                SamplesPerPixel = samplesPerPixel;
            }

            public ISpectrum[,] Data { get; }
            public int Width { get; }
            public int Height { get; }
            public int SamplesPerPixel { get; }

        }

        DirectBitmap ToBitmap(ImageData imageData)
        {
            DirectBitmap bmp = new DirectBitmap(imageData.Width, imageData.Height);

            for (int j = 0; j < imageData.Height; j++)
            {
                for (int i = 0; i < imageData.Width; i++)
                {
                    bmp.SetPixel(i, (j - (imageData.Height - 1)) * -1, ToColor(imageData.Data[j, i], imageData.SamplesPerPixel));
                }
            }

            return bmp;
        }
        ISpectrum GetRayColor(Ray r, ISpectrum background, Primitive world, int depth)
        {
            SurfaceInteraction isect = new SurfaceInteraction();

            if (depth <= 0)
            {
                return _factory.CreateFromRGB(new double[] { 0, 0, 0 }, SpectrumMaterialType.Reflectance);
            }

            isect = world.Intersect(r, isect);
            if (!isect.HasIntersection)
            {
                return background;
            }
            ISpectrum emitted = isect.Primitive.GetMaterial().Emitted(isect.UCoordinate, isect.VCoordinate, isect.P);

            isect = isect.Primitive.GetMaterial().Scatter(r, isect);

            if (!isect.HasScattered)
            {
                return emitted;
            }

            return emitted + isect.Attenuation * GetRayColor(isect.ScatteredRay, background, world, depth - 1);
        }
        System.Drawing.Color ToColor(ISpectrum pixel_color, int samples_per_pixel)
        {
            var scale = (double)1 / (double)samples_per_pixel;
            
            for (int i = 0; i < pixel_color.NumberSamples; i++) {
                pixel_color.coefficients[i] = Math.Sqrt(scale * pixel_color.coefficients[i]);
            }

            double[] rgb = pixel_color.ToRGB();

            var r = rgb[0];
            var g = rgb[1];
            var b = rgb[2];

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
                pixelFormat: PixelFormats.Bgr24,
                palette: null,
                pixels: bitmap.Bits,
                stride: bitmap.Stride);

            return bs;

        }
    }
}
