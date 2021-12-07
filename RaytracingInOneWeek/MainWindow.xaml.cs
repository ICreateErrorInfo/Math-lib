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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RenderScene();
        }

        private async void RenderScene()
        {

            Stopwatch sw = Stopwatch.StartNew();

            var progress = new Progress<ProgressData>(OnProgress);

            var imageData = await Task.Run(() => RenderImageData(progress));

            var bmp = ToBitmap(imageData);

            image.Source = BitmapToImageSource(bmp);

            ProgressBar.Visibility = Visibility.Collapsed;

            sw.Stop();
            Time.Text = (sw.ElapsedMilliseconds / 1000.0).ToString() + "s";
        }
        private ImageData RenderImageData(IProgress<ProgressData> progress)
        {
            int samplesPerPixel;
            int max_depth = 50;

            //World
            var world = new hittable_list();

            Point3D lookfrom = new Point3D();
            Point3D lookat = new Point3D();
            var vfov = 40;
            double aperture = 0;
            Vector3D background = new Vector3D(0, 0, 0);

            int imageWidth = 400;
            int imageHeight = 200;

            switch (1)
            {
                case 1:
                    var checker = new checker_texture(new Vector3D(.2, .3, .1), new Vector3D(.9, .9, .9));

                    var material = new Metal(new Vector3D(.7, .7, .7), 0.7);
                    var material1 = new Metal(new Vector3D(1, 0.32, 0.36), 0);
                    var material2 = new Metal(new Vector3D(0.90, 0.76, 0.46), 0);
                    var material3 = new Metal(new Vector3D(0.65, 0.77, 0.97), 0);
                    var material4 = new Metal(new Vector3D(0.90, 0.90, 0.90), 0);

                    var center2 = new Point3D(0, 0, -20) + new Vector3D(0, Mathe.random(0, .5, 1000), 0);

                    world.Add(new sphere(new Point3D(0.0, -10004, -20), 10000, new lambertian(checker)));
                    world.Add(new moving_sphere(new Point3D(0, 0, -20), center2, 0, 1, 4, material1));
                    world.Add(new sphere(new Point3D(5, -1, -15), 2, material2));
                    world.Add(new sphere(new Point3D(5, 0, -25), 3, material3));
                    world.Add(new sphere(new Point3D(-5.5, 0, -15), 3, material4));

                    lookfrom = new Point3D(0, 0, 0);
                    lookat = new Point3D(0, 0, -1);
                    vfov = 50;
                    aperture = 0.1;
                    background = new Vector3D(.7, .8, 1);
                    samplesPerPixel = 100;
                    break;

                case 2:
                    world = two_spheres();
                    lookfrom = new Point3D(13, 2, 3);
                    lookat = new Point3D(0, 0, 0);
                    samplesPerPixel = 100;
                    vfov = 20;
                    break;
                case 3:
                    world = two_perlin_spheres();
                    background = new Vector3D(.7, .8, 1);
                    lookfrom = new Point3D(13, 2, 3);
                    lookat = new Point3D(0, 0, 0);
                    samplesPerPixel = 1000;
                    vfov = 20;
                    break;
                case 4:
                    world = earth();
                    background = new Vector3D(.7, .8, 1);
                    lookfrom = new Point3D(13, 2, 3);
                    lookat = new Point3D(0, 0, 0);
                    samplesPerPixel = 1;
                    vfov = 20;
                    break;
                case 5:
                    world = simple_light();
                    samplesPerPixel = 100;
                    background = new Vector3D(0, 0, 0);
                    lookfrom = new Point3D(26, 3, 6);
                    lookat = new Point3D(0, 2, 0);
                    vfov = 20;
                    break;
                case 6:
                    world = cornell_box();
                    imageWidth = 300;
                    imageHeight = 300;
                    samplesPerPixel = 400;
                    background = new Vector3D(0, 0, 0);
                    lookfrom = new Point3D(278, 278, -800);
                    lookat = new Point3D(278, 278, 0);
                    vfov = 40;
                    break;
            }

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
                        pixelColor += ray_color(r, background, world, max_depth);
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

            ProgressBar.Maximum = d.TotalCount;
            ProgressBar.Value = d.Current;
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
        hittable_list two_spheres()
        {
            hittable_list objects = new hittable_list();

            var checker = new checker_texture(new Vector3D(0.2, 0.3, 0.1), new Vector3D(0.9, 0.9, 0.9));

            objects.Add(new sphere(new Point3D(0, -10, 0), 10, new lambertian(checker)));
            objects.Add(new sphere(new Point3D(0, 10, 0), 10, new lambertian(checker)));

            return objects;
        }
        hittable_list two_perlin_spheres()
        {
            hittable_list objects = new hittable_list();

            var pertext = new noise_texture(4);

            objects.Add(new sphere(new Point3D(0, -1000, 0), 1000, new lambertian(pertext)));
            objects.Add(new sphere(new Point3D(0, 2, 0), 2, new lambertian(pertext)));

            return objects;
        }
        hittable_list earth()
        {
            var earthTexture = new image_texture("C:/Users/Moritz/source/repos/Raytracer/Resources/earthmap.jpg");

            var earthSurface = new lambertian(earthTexture);
            var globe = new sphere(new Point3D(0, 0, 0), 2, earthSurface);
            var ret = new hittable_list();
            ret.Add(globe);

            return ret;
        }
        hittable_list simple_light()
        {
            hittable_list objekts = new hittable_list();

            var pertext = new noise_texture(4);
            objekts.Add(new sphere(new Point3D(0, -1000, 0), 1000, new lambertian(pertext)));
            objekts.Add(new sphere(new Point3D(0, 2, 0), 2, new lambertian(pertext)));

            var difflight = new diffuse_light(new Vector3D(4, 4, 4));
            objekts.Add(new xy_rect(3, 5, 1, 3, -2, difflight));

            return objekts;
        }
        hittable_list cornell_box()
        {
            hittable_list objects = new hittable_list();

            var red = new lambertian(new Vector3D(.65, .05, .05));
            var white = new lambertian(new Vector3D(.73, .73, .73));
            var green = new lambertian(new Vector3D(.12, .45, .15));
            var light = new diffuse_light(new Vector3D(15, 15, 15));

            box box1 = new box(new Point3D(130, 0, 65), new Point3D(295, 165, 230), white);
            box box2 = new box(new Point3D(265, 0, 295), new Point3D(430, 330, 460), white);

            objects.Add(box1.sides);
            objects.Add(box2.sides);

            objects.Add(new yz_rect(0, 555, 0, 555, 555, green));
            objects.Add(new yz_rect(0, 555, 0, 555, 0, red));
            objects.Add(new xz_rect(0, 555, 0, 555, 0, white));
            objects.Add(new xz_rect(0, 555, 0, 555, 555, white));
            objects.Add(new xy_rect(0, 555, 0, 555, 555, white));

            objects.Add(new xz_rect(213, 343, 227, 332, 554, light));

            return objects;
        }
    }
}
