using Math_lib;
using Microsoft.Win32;
using Raytracing.Materials;
using Raytracing.Shapes;
using Raytracing.Spectrum;
using System.Collections.Generic;
using System.Windows;

namespace Raytracing {
    public partial class MainWindow : Window
    {
        SpectrumFactory _factory = new RGBSpectrumFactory();

        public MainWindow()
        {
            InitializeComponent();

            Raytracer r = new Raytracer(_factory, image, ProgressBar, Time);

            r.Init();
            r.RenderScene(FirstScene());
        }

        Scene TestSphere()
        {
            var material = new Metal(_factory, _factory.CreateFromRGB(new double[] {.65, .7, .46 }, SpectrumMaterialType.Reflectance), 0.7);
            Sphere s = new Sphere(new(0, 0.1, 0), 0.1, -0.1, 0.1, 360);
            Sphere s1 = new Sphere(new(0.2, 0, 0), 0.1, -0.08, 0.08, 360);
            HittableList h = new();
            h.Add(new GeometricPrimitive(s, material));
            h.Add(new GeometricPrimitive(s1, material));

            Scene scene = new Scene(h, 100, 10, new Point3D(0, 0, 1), new Point3D(0, 0, 0), new(0, 1, 0), 20, 0.1, _factory.CreateFromRGB(new double[]{.7, .8, 1 }, SpectrumMaterialType.Reflectance));

            return scene;
        }
        Scene TestImporter()
        {
            TriangleMesh mesh = TriangleMesh.Import(_factory, ShowOpenFile());
            HittableList h = new();

            for (int i = 0; i < mesh.NTriangles; i++)
            {
                h.Add(new GeometricPrimitive(new Triangle(Transform.Translate(new(0)), Transform.Translate(new(0)), mesh, i), mesh.Material));
            }

            Scene scene = new Scene(h, 100, 50, new Point3D(0, 0, -15), new Point3D(0, 1, 0), new(1, -15, 1), 20, 0.1, _factory.CreateFromRGB(new double[] { .7, .8, 1 }, SpectrumMaterialType.Reflectance));
            return scene;
        }
        Scene TestTriangle()
        {
            var material = new Metal(_factory, _factory.CreateFromRGB(new double[] {.65, .7, .46 }, SpectrumMaterialType.Reflectance), 0.7);

            int nTri = 1;
            List<int> indices = new List<int>() { 0, 1, 2 };
            int nVert = 3;
            List<Point3D> Points = new List<Point3D>() { new(-2, 0, 0), new(2, 0, 0), new(0, 2, 0) };

            Transform objToWorld = Transform.Translate(new Vector3D(0));
            Transform worldToObj = Transform.Translate(new Vector3D(0));

            TriangleMesh mesh = new TriangleMesh(objToWorld, nTri, indices, nVert, Points, material);

            Triangle tri = new Triangle(objToWorld, worldToObj, mesh, 0);

            HittableList h = new(new GeometricPrimitive(tri, mesh.Material));

            Scene scene = new Scene(h, 100, 50, new Point3D(0, 1, -10), new Point3D(0, 1, 0), new(0, 1, 0), 20, 0.1, _factory.CreateFromRGB(new double[] { .7, .8, 1 }, SpectrumMaterialType.Reflectance));

            return scene;
        }
        Scene TestCone()
        {
            var material = new Metal(_factory, _factory.CreateFromRGB(new double[] {1, 0.32, 0.36 }, SpectrumMaterialType.Reflectance), 0);
            var material2 = new Metal(_factory, _factory.CreateFromRGB(new double[] { 0.90, 0.76, 0.46 }, SpectrumMaterialType.Reflectance), 1);
            Cone c = new Cone(new(0, -10, -1.5), 1, 1, 360);
            Sphere s = new Sphere(new(2, -10, 0), 1, -2, 2, 360);
            HittableList h = new(new GeometricPrimitive(c, material));
            h.Add(new GeometricPrimitive(s, material2));

            Scene scene = new Scene(h, 100, 50, new Point3D(0, 0, 0), new Point3D(0, -1, 0), new(0, 0, 1), 20, 0.1, _factory.CreateFromRGB(new double[] { .7, .8, 1 }, SpectrumMaterialType.Reflectance), 18);

            return scene;
        }
        Scene TestDisk()
        {
            var material = new Metal(_factory, _factory.CreateFromRGB(new double[] { .65, .7, .46 }, SpectrumMaterialType.Reflectance), 0.7);
            Disk d = new Disk(new(0, 0, 0), 0, 0.1, 0.05, 180);
            HittableList h = new(new GeometricPrimitive( d, material));

            Scene scene = new Scene(h, 100, 50, new Point3D(0, 0, 1), new Point3D(0, 0, 0), new(0, 1, 0), 20, 0.1, _factory.CreateFromRGB(new double[] { .7, .8, 1 }, SpectrumMaterialType.Reflectance));

            return scene;
        }
        Scene TestCylinder()
        {
            var material = new Metal(_factory, _factory.CreateFromRGB(new double[] { .65, .7, .46 }, SpectrumMaterialType.Reflectance), 0.7);
            Cylinder s = new Cylinder(new(0.1, 0, 0), 0.1, -0.1, 0.1, 360);
            HittableList h = new(new GeometricPrimitive(s,material));

            Scene scene = new Scene(h, 100, 50, new Point3D(0, 1, 0), new Point3D(0, 0, 0), new(0, 0, 1), 20, 0.1, _factory.CreateFromRGB(new double[] { .7, .8, 1 }, SpectrumMaterialType.Reflectance));

            return scene;
        }
        Scene FirstScene()
        {
            HittableList world = new HittableList();

            var checker = new CheckerTexture(_factory, _factory.CreateFromRGB(new double[] {.2, .3, .1 }, SpectrumMaterialType.Reflectance), _factory.CreateFromRGB(new double[] {.9, .9, .9 }, SpectrumMaterialType.Reflectance));

            var material  = new Metal(_factory, _factory.CreateFromRGB(new double[] {.7, .7, .7 }, SpectrumMaterialType.Reflectance), 0.7);
            var material1 = new Metal(_factory, _factory.CreateFromRGB(new double[] {1, 0.32, 0.36 }, SpectrumMaterialType.Reflectance), 0);
            var material2 = new Metal(_factory, _factory.CreateFromRGB(new double[] {0.90, 0.76, 0.46 }, SpectrumMaterialType.Reflectance), 0);
            var material3 = new Metal(_factory, _factory.CreateFromRGB(new double[] {0.65, 0.77, 0.97 }, SpectrumMaterialType.Reflectance), 0);
            var material4 = new Metal(_factory, _factory.CreateFromRGB(new double[] {0.90, 0.90, 0.90 }, SpectrumMaterialType.Reflectance), 0);

            var center2 = new Point3D(0, 0, -20) + new Vector3D(0, 0.5, 0);

            world.Add(new GeometricPrimitive(new Sphere(new Point3D(0.0, -10004, -20), 10000), new Lambertian(checker)));
            //world.Add(new GeometricPrimitive(new MovingSphere(new Point3D(0, 0, -20), center2, 0, 1, 4), material1));
            world.Add(new GeometricPrimitive(new Sphere(new Point3D(5, -1, -15), 2), material2));
            world.Add(new GeometricPrimitive(new Sphere(new Point3D(5, 0, -25), 3), material3));
            world.Add(new GeometricPrimitive(new Sphere(new Point3D(-5.5, 0, -15), 3), material4));

            Scene scene = new Scene(world, 100, 50, new Point3D(0, 0, 0), new Point3D(0, 0, -1), new(0, 1, 0), 50, 0.1, _factory.CreateFromRGB(new double[] { .7, .8, 1 }, SpectrumMaterialType.Reflectance), 20);

            return scene;
        }
        Scene TwoSpheres()
        {
            HittableList objects = new HittableList();

            var checker = new CheckerTexture(_factory, _factory.CreateFromRGB(new double[] {0.2, 0.3, 0.1 }, SpectrumMaterialType.Reflectance), _factory.CreateFromRGB(new double[] {0.9, 0.9, 0.9 }, SpectrumMaterialType.Reflectance));

            objects.Add(new GeometricPrimitive(new Sphere(new Point3D(0, -10, 0), 10), new Lambertian(checker)));
            objects.Add(new GeometricPrimitive(new Sphere(new Point3D(0, 10, 0), 10), new Lambertian(checker)));

            Scene scene = new Scene(objs: objects,
                                    spp: 100,
                                    maxD: 50,
                                    lookfrom: new Point3D(13, 2, 3),
                                    lookat: new Point3D(0, 0, 0),
                                    new(0, 1, 0),
                                    vFov: 20,
                                    aperture: 0.1,
                                    background: _factory.CreateFromRGB(new double[]{1, 1, 1 }, SpectrumMaterialType.Reflectance),
                                    focusDistance: 10);

            return scene;
        }
        Scene TwoPerlinSpheres()
        {
            HittableList objects = new HittableList();

            var pertext = new NoiseTexture(_factory, 4);

            objects.Add(new GeometricPrimitive(new Sphere(new Point3D(0, -1000, 0), 1000), new Lambertian(pertext)));
            objects.Add(new GeometricPrimitive(new Sphere(new Point3D(0, 2, 0), 2), new Lambertian(pertext)));

            Scene scene = new Scene(objs: objects,
                                    spp: 100,
                                    maxD: 50,
                                    lookfrom: new Point3D(13, 2, 3),
                                    lookat: new Point3D(0, 0, 0),
                                    new(0, 1, 0),
                                    vFov: 20,
                                    aperture: 0.1,
                                    background: _factory.CreateFromRGB(new double[] { .7, .8, 1 }, SpectrumMaterialType.Reflectance));

            return scene;
        }
        Scene Earth()
        {
            var earthTexture = new ImageTexture(_factory, "");

            var earthSurface = new Lambertian(earthTexture);
            var globe = new GeometricPrimitive(new Sphere(new Point3D(0, 0, 0), 2), earthSurface);
            var ret = new HittableList();
            ret.Add(globe);

            Scene scene = new Scene(objs: ret,
                                    spp: 10,
                                    maxD: 50,
                                    lookfrom: new Point3D(13, 2, 3),
                                    lookat: new Point3D(0, 0, 0),
                                    new(0, 1, 0),
                                    vFov: 20,
                                    aperture: 0.1,
                                    background: _factory.CreateFromRGB(new double[] { .7, .8, 1 }, SpectrumMaterialType.Reflectance),
                                    focusDistance: 0,
                                    imageWidth: 1920,
                                    imageHeight: 1080);

            return scene;
        }
        Scene SimpleLight()
        {
            HittableList objekts = new HittableList();

            var pertext = new NoiseTexture(_factory, 4);
            objekts.Add(new GeometricPrimitive(new Sphere(new Point3D(0, -1000, 0), 1000), new Lambertian(pertext)));
            objekts.Add(new GeometricPrimitive(new Sphere(new Point3D(0, 2, 0), 2), new Lambertian(pertext)));

            var difflight = new DiffuseLight(_factory, _factory.CreateFromRGB(new double[] { 4, 4, 4 }, SpectrumMaterialType.Reflectance));
            objekts.Add(new GeometricPrimitive(new XYRect(3, 5, 1, 3, -2), difflight));

            Scene scene = new Scene(objs: objekts,
                                    spp: 400,
                                    maxD: 50,
                                    lookfrom: new Point3D(26, 3, 6),
                                    lookat: new Point3D(0, 2, 0),
                                    new(0, 1, 0),
                                    vFov: 20,
                                    aperture: 0.1,
                                    background: _factory.CreateFromRGB(new double[]{0, 0, 0 }, SpectrumMaterialType.Reflectance));

            return scene;
        }
        Scene CornellBox()
        {
            HittableList objects = new HittableList();

            var red =   new Lambertian(_factory, _factory.CreateFromRGB(new double[] { .65, .05, .05 }, SpectrumMaterialType.Reflectance));
            var white = new Lambertian(_factory, _factory.CreateFromRGB(new double[] { .73, .73, .73 }, SpectrumMaterialType.Reflectance));
            var green = new Lambertian(_factory, _factory.CreateFromRGB(new double[] { .12, .45, .15 }, SpectrumMaterialType.Reflectance));
            var light = new DiffuseLight(_factory, _factory.CreateFromRGB(new double[] { 15, 15, 15 }, SpectrumMaterialType.Reflectance));

            //Box box1 = new Box(new Point3D(130, 0, 65), new Point3D(295, 165, 230), white);
            //Box box2 = new Box(new Point3D(265, 0, 295), new Point3D(430, 330, 460), white);

            //objects.Add(box1.Sides);
            //objects.Add(box2.Sides);

            objects.Add(new GeometricPrimitive(new YZRect(0, 555, 0, 555, 555), green));
            objects.Add(new GeometricPrimitive(new YZRect(0, 555, 0, 555, 0), red));
            objects.Add(new GeometricPrimitive(new XZRect(0, 555, 0, 555, 0), white));
            objects.Add(new GeometricPrimitive(new XZRect(0, 555, 0, 555, 555), white));
            objects.Add(new GeometricPrimitive(new XYRect(0, 555, 0, 555, 555), white));

            objects.Add(new GeometricPrimitive(new XZRect(213, 343, 227, 332, 554), light));

            Scene scene = new Scene(objs: objects,
                                    spp: 100,
                                    maxD: 50,
                                    lookfrom: new Point3D(278, 278, -800),
                                    lookat: new Point3D(278, 278, 0),
                                    new(0, 1, 0),
                                    vFov: 40,
                                    aperture: 0.1,
                                    background: _factory.CreateFromRGB(new double[] { 0, 0, 0 }, SpectrumMaterialType.Reflectance),
                                    imageWidth: 300,
                                    imageHeight: 300);

            return scene;
        }

        private string ShowOpenFile()
        {
            var ofn = new OpenFileDialog
            {
                Filter = "Object files (*.obj)|*.obj",
            };
            if (ofn.ShowDialog() == true)
            {
                return ofn.FileName;
            }
            throw new System.Exception();
        }
    }
}
