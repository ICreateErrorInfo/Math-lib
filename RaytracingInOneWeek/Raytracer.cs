using System;
using System.Diagnostics;
using System.Security.Cryptography;
using Math_lib;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RaytracingInOneWeek
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

        public async void RenderScene(Scene scene)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var progress = new Progress<ProgressData>(OnProgress);

            var imageData = await Task.Run(() => RenderImageData(progress, scene));

            var bmp = ToBitmap(imageData);

            _image.Source = BitmapToImageSource(bmp);

            _progressBar.Visibility = Visibility.Collapsed;

            sw.Stop();
            _time.Text = (sw.ElapsedMilliseconds / 1000.0).ToString() + "s";
        }
        private ImageData RenderImageData(IProgress<ProgressData> progress, Scene scene)
        {
            int samplesPerPixel = scene.samplesPerPixel;
            int maxDepth        = scene.maxDepth;
            var world           = scene.Objects;

            Point3D lookfrom    = scene.lookfrom;
            Point3D lookat      = scene.lookat;
            var vfov            = scene.vfov;
            double aperture     = scene.aperture;
            Vector3D background = scene.background;

            int imageWidth      = scene.imageWidth;
            int imageHeight     = scene.imageHeight;

            double aspectRatio = imageWidth / (double)imageHeight;

            //Camera
            Vector3D vup = new Vector3D(0, 1, 0);
            var dist_to_focus = 20;

            Camera cam = new Camera(lookfrom, lookat, vup, vfov, aspectRatio, aperture, dist_to_focus, 0, 1);

            Vector3D[,] vArr = new Vector3D[imageHeight, imageWidth];

            var totalCount = imageHeight;
            var current = 0;
            progress.Report(new ProgressData(totalCount, current));

            //random numbers
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            byte[] RandX = new byte[samplesPerPixel];
            byte[] RandY = new byte[samplesPerPixel];
            crypto.GetBytes(RandX);
            crypto.GetBytes(RandY);

            Parallel.For(0, imageHeight, j =>
            {
                Interlocked.Increment(ref current);
                progress.Report(new ProgressData(totalCount, current));
                for (int i = 0; i < imageWidth; i++)
                {
                    Vector3D pixelColor = new Vector3D(0, 0, 0);
                    for (int s = 0; s < samplesPerPixel; s++)
                    {
                        var u = (i + ((double)RandX[s] / byte.MaxValue)) / (imageWidth - 1);
                        var v = (j + ((double)RandX[s] / byte.MaxValue)) / (imageHeight - 1);
                        Ray r = cam.get_ray(u, v);
                        pixelColor += ray_color(r, background, world, maxDepth);
                    }

                    vArr[j, i] = pixelColor;
                }
            });
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

            public ImageData(Vector3D[,] data, int width, int height, int samplesPerPixel)
            {
                Data = data;
                Width = width;
                Height = height;
                SamplesPerPixel = samplesPerPixel;
            }

            public Vector3D[,] Data { get; }
            public int Width { get; }
            public int Height { get; }
            public int SamplesPerPixel { get; }

        }

        private static Bitmap ToBitmap(ImageData imageData)
        {

            Bitmap bmp = new Bitmap(imageData.Width, imageData.Height);

            for (int j = 0; j < imageData.Height; j++)
            {
                for (int i = 0; i < imageData.Width; i++)
                {
                    bmp.SetPixel(i, (j - (imageData.Height - 1)) * -1, toColor(imageData.Data[j, i], imageData.SamplesPerPixel));
                }
            }

            return bmp;
        }
        public static Vector3D ray_color(Ray r, Vector3D background, hittable world, int depth)
        {
            hit_record rec = new hit_record();

            if (depth <= 0)
            {
                return new Vector3D(0, 0, 0);
            }

            zwischenSpeicher zw = world.Hit(r, 0.0001, Mathe.infinity, rec);
            if (!zw.IsTrue)
            {
                return background;
            }
            Ray scattered = new Ray();
            Vector3D attenuation = new Vector3D();
            Vector3D emitted = zw.rec.mat_ptr.emitted(zw.rec.u, zw.rec.v, zw.rec.p);

            zw = zw.rec.mat_ptr.scatter(r, zw.rec, attenuation, scattered);
            if (!zw.IsTrue)
            {
                return emitted;
            }

            return emitted + zw.attenuation * ray_color(zw.scattered, background, world, depth - 1);
        }
        public static Color toColor(Vector3D pixel_color, int samples_per_pixel)
        {
            var r = pixel_color.X;
            var g = pixel_color.Y;
            var b = pixel_color.Z;

            var scale = (double)1 / (double)samples_per_pixel;

            r = Math.Sqrt(scale * r);
            g = Math.Sqrt(scale * g);
            b = Math.Sqrt(scale * b);

            return Color.FromArgb(Convert.ToInt32(255 * Mathe.clamp(r, 0, 0.999)),
                                  Convert.ToInt32(255 * Mathe.clamp(g, 0, 0.999)),
                                  Convert.ToInt32(255 * Mathe.clamp(b, 0, 0.999)));
        }
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
