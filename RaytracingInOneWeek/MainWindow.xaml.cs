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

namespace Raytracing
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Raytracer r = new Raytracer(image, ProgressBar, Time);
            r.RenderScene(Earth());
        }

        Scene FirstScene()
        {
            hittable_list world = new hittable_list();

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

            Scene scene = new Scene(world, 100, 50, new Point3D(0, 0, 0), new Point3D(0, 0, -1), 50, 0.1, new Vector3D(.7, .8, 1));

            return scene;
        }
        Scene TwoSpheres()
        {
            hittable_list objects = new hittable_list();

            var checker = new checker_texture(new Vector3D(0.2, 0.3, 0.1), new Vector3D(0.9, 0.9, 0.9));

            objects.Add(new sphere(new Point3D(0, -10, 0), 10, new lambertian(checker)));
            objects.Add(new sphere(new Point3D(0, 10, 0), 10, new lambertian(checker)));

            Scene scene = new Scene(objs: objects,
                                    spp: 100,
                                    maxD: 50,
                                    lookfrom: new Point3D(13, 2, 3),
                                    lookat: new Point3D(0, 0, 0),
                                    vfov: 20,
                                    aperture: 0.1,
                                    background: new Vector3D(0, 0, 0));

            return scene;
        }
        Scene TwoPerlinSpheres()
        {
            hittable_list objects = new hittable_list();

            var pertext = new noise_texture(4);

            objects.Add(new sphere(new Point3D(0, -1000, 0), 1000, new lambertian(pertext)));
            objects.Add(new sphere(new Point3D(0, 2, 0), 2, new lambertian(pertext)));

            Scene scene = new Scene(objs: objects,
                                    spp: 1000,
                                    maxD: 50,
                                    lookfrom: new Point3D(13, 2, 3),
                                    lookat: new Point3D(0, 0, 0),
                                    vfov: 20,
                                    aperture: 0.1,
                                    background: new Vector3D(.7, .8, 1));

            return scene;
        }
        Scene Earth()
        {
            var earthTexture = new image_texture("C:/Users/Moritz/source/repos/Raytracer/Resources/earthmap.jpg");

            var earthSurface = new lambertian(earthTexture);
            var globe = new sphere(new Point3D(0, 0, 0), 2, earthSurface);
            var ret = new hittable_list();
            ret.Add(globe);

            Scene scene = new Scene(objs: ret,
                                    spp: 1,
                                    maxD: 50,
                                    lookfrom: new Point3D(13, 2, 3),
                                    lookat: new Point3D(0, 0, 0),
                                    vfov: 20,
                                    aperture: 0.1,
                                    background: new Vector3D(.7, .8, 1),
                                    1920,
                                    1080);

            return scene;
        }
        Scene SimpleLight()
        {
            hittable_list objekts = new hittable_list();

            var pertext = new noise_texture(4);
            objekts.Add(new sphere(new Point3D(0, -1000, 0), 1000, new lambertian(pertext)));
            objekts.Add(new sphere(new Point3D(0, 2, 0), 2, new lambertian(pertext)));

            var difflight = new diffuse_light(new Vector3D(4, 4, 4));
            objekts.Add(new xy_rect(3, 5, 1, 3, -2, difflight));

            Scene scene = new Scene(objs: objekts,
                                    spp: 100,
                                    maxD: 50,
                                    lookfrom: new Point3D(26, 3, 6),
                                    lookat: new Point3D(0, 2, 0),
                                    vfov: 20,
                                    aperture: 0.1,
                                    background: new Vector3D(0, 0, 0));

            return scene;
        }
        Scene CornellBox()
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

            Scene scene = new Scene(objs: objects,
                                    spp: 400,
                                    maxD: 50,
                                    lookfrom: new Point3D(278, 278, -800),
                                    lookat: new Point3D(278, 278, 0),
                                    vfov: 40,
                                    aperture: 0.1,
                                    background: new Vector3D(0, 0, 0),
                                    imageWidth: 300,
                                    imageHeight: 300);

            return scene;
        }
    }
}
