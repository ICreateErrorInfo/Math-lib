using Microsoft.Win32;
using Moarx.Graphics.Color;
using Moarx.Graphics.Spectrum;
using Moarx.Math;
using Raytracing.Camera;
using Raytracing.Materials;
using Raytracing.Primitives;
using Raytracing.Shapes;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Raytracing;
public partial class MainWindow : Window
{
    RGBColorSpace colorSpace;
    Raytracer raytracer;

    record SceneDefinition(string Name, Func<int, int, int, int, Scene> Factory, int Width, int Height, int Spp, int MaxDepth, bool RequiresMeshFile = false);

    List<SceneDefinition> sceneDefinitions;
    string selectedMeshPath;

    public MainWindow()
    {
        InitializeComponent();

        raytracer = new Raytracer(image, ProgressBar, Time);
        raytracer.Init();

        colorSpace = RGBColorSpace.sRGB;

        sceneDefinitions = new List<SceneDefinition>
        {
            new("First Scene",        FirstScene,        400,  200,  100,  50),
            new("Test Sphere",        TestSphere,        400,  200,  100,  10),
            new("Test Triangle",      TestTriangle,      400,  200,  100,  50),
            new("Test Cone",          TestCone,          400,  200,  100,  50),
            new("Test Disk",          TestDisk,          400,  200,  100,  50),
            new("Test Cylinder",      TestCylinder,      400,  200,  100,  50),
            new("Two Spheres",        TwoSpheres,        400,  200,  100,  50),
            new("Two Perlin Spheres", TwoPerlinSpheres,  400,  200,  100,  50),
            new("Simple Light",       SimpleLight,       400,  200,  1000, 50),
            new("Cornell Box",        CornellBox,        300,  300,  100,  50),
            new("Earth",              Earth,             1920, 1080, 10,   50),
            new("Import Mesh...",     TestImporter,      400,  200,  100,  50, RequiresMeshFile: true),
        };

        SceneComboBox.ItemsSource = sceneDefinitions;
        SceneComboBox.SelectedIndex = 0;
    }

    void SceneComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (SceneComboBox.SelectedItem is not SceneDefinition selected) {
            return;
        }

        WidthTextBox.Text = selected.Width.ToString();
        HeightTextBox.Text = selected.Height.ToString();
        SamplesTextBox.Text = selected.Spp.ToString();
        MaxDepthTextBox.Text = selected.MaxDepth.ToString();

        UpdateSettingsVisibility();
    }

    void UpdateSettingsVisibility()
    {
        if (SceneComboBox.SelectedItem is not SceneDefinition selected) {
            return;
        }

        MeshPanel.Visibility = selected.RequiresMeshFile ? Visibility.Visible : Visibility.Collapsed;

        bool showSettings = !selected.RequiresMeshFile || !string.IsNullOrEmpty(selectedMeshPath);
        SettingsPanel.Visibility = showSettings ? Visibility.Visible : Visibility.Collapsed;
    }

    void BrowseMeshButton_Click(object sender, RoutedEventArgs e)
    {
        string path = ShowOpenFile();
        if (string.IsNullOrEmpty(path)) {
            return;
        }

        selectedMeshPath = path;
        MeshFilePathTextBox.Text = path;

        UpdateSettingsVisibility();
    }

    async void RenderButton_Click(object sender, RoutedEventArgs e)
    {
        if (SceneComboBox.SelectedItem is not SceneDefinition selected) {
            return;
        }

        if (!int.TryParse(WidthTextBox.Text, out int width) || width <= 0 ||
            !int.TryParse(HeightTextBox.Text, out int height) || height <= 0 ||
            !int.TryParse(SamplesTextBox.Text, out int spp) || spp <= 0 ||
            !int.TryParse(MaxDepthTextBox.Text, out int maxDepth) || maxDepth <= 0) {
            MessageBox.Show("Please enter valid positive numbers for width, height, samples and max depth.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        RenderButton.IsEnabled = false;
        SceneComboBox.IsEnabled = false;

        try {
            Scene scene = selected.Factory(width, height, spp, maxDepth);
            await raytracer.RenderScene(scene, colorSpace);
        }
        catch (Exception ex) {
            MessageBox.Show($"Rendering failed: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally {
            RenderButton.IsEnabled = true;
            SceneComboBox.IsEnabled = true;
        }
    }

    Scene TestSphere(int width = 400, int height = 200, int spp = 100, int maxDepth = 10) {
        var material = new Metal(new RGBAlbedoSpectrum(colorSpace, new(.65, .7, .46)), 0.7, colorSpace);
        Sphere s = new Sphere(new(0, 0.1, 0), 0.1, -0.1, 0.1, 360);
        Sphere s1 = new Sphere(new(0.2, 0, 0), 0.1, -0.08, 0.08, 360);
        PrimitiveList h = new();
        h.Add(new GeometricPrimitive(s, material));
        h.Add(new GeometricPrimitive(s1, material));

        Scene scene = new Scene(h, spp, maxDepth, CreateCamera(width, height, new(0,0,1), new(0,0,0), 20),  new RGBIlluminantSpectrum(colorSpace, new( .7, .8, 1 )), width, height);

        return scene;
    }
    Scene TestImporter(int width = 400, int height = 200, int spp = 100, int maxDepth = 50) {
        TriangleMesh mesh = TriangleMesh.Import(selectedMeshPath, colorSpace);
        PrimitiveList h = new();

        for (int i = 0; i < mesh.NTriangles; i++) {
            h.Add(new GeometricPrimitive(new Triangle(Transform.Translate(new(0)), Transform.Translate(new(0)), mesh, i), mesh.Material));
        }

        Scene scene = new Scene(h, spp, maxDepth, CreateCamera(width, height, new(0,0,-15), new(0,1,0), 20), new RGBIlluminantSpectrum(colorSpace, new( .7, .8, 1 )), width, height);
        return scene;
    }
    Scene TestTriangle(int width = 400, int height = 200, int spp = 100, int maxDepth = 50) {
        var material = new Metal( new RGBAlbedoSpectrum(colorSpace, new(.65, .7, .46)), 0.7, colorSpace);

        int nTri = 1;
        List<int> indices = new List<int>() { 0, 1, 2 };
        int nVert = 3;
        List<Point3D<double>> Points = new List<Point3D<double>>() { new(-2, 0, 0), new(2, 0, 0), new(0, 2, 0) };

        Transform objToWorld = Transform.Translate(new Vector3D<double>(0));
        Transform worldToObj = Transform.Translate(new Vector3D<double>(0));

        TriangleMesh mesh = new TriangleMesh(objToWorld, nTri, indices, nVert, Points, material);

        Triangle tri = new Triangle(objToWorld, worldToObj, mesh, 0);

        PrimitiveList h = new(new GeometricPrimitive(tri, mesh.Material));

        Scene scene = new Scene(h, spp, maxDepth, CreateCamera(width, height, new(0,1,-10), new(0,1,0),20), new RGBIlluminantSpectrum(colorSpace, new( .7, .8, 1 )), width, height);

        return scene;
    }
    Scene TestCone(int width = 400, int height = 200, int spp = 100, int maxDepth = 50) {
        var material = new Metal(new RGBAlbedoSpectrum(colorSpace, new(1, 0.32, 0.36 )), 0, colorSpace);
        var material2 = new Metal(new RGBAlbedoSpectrum(colorSpace, new( 0.90, 0.76, 0.46 )), 1, colorSpace);
        Cone c = new Cone(new(0, -10, -1.5), 1, 1, 360);
        Sphere s = new Sphere(new(2, -10, 0), 1, -2, 2, 360);
        PrimitiveList h = new(new GeometricPrimitive(c, material));
        h.Add(new GeometricPrimitive(s, material2));

        Scene scene = new Scene(h, spp, maxDepth, CreateCamera(width, height, new(0,0,0), new(0,-1,0.001), 20), new RGBIlluminantSpectrum(colorSpace, new( .7, .8, 1 )), width, height);

        return scene;
    }
    Scene TestDisk(int width = 400, int height = 200, int spp = 100, int maxDepth = 50) {
        var material = new Metal(new RGBAlbedoSpectrum(colorSpace, new( .65, .7, .46 )), 0.7, colorSpace);
        Disk d = new Disk(new(0, 0, 0), 0, 0.1, 0.05, 180);
        PrimitiveList h = new(new GeometricPrimitive( d, material));

        Scene scene = new Scene(h, spp, maxDepth, CreateCamera(width, height, new(0,0,1), new(0,0,0), 20), new RGBIlluminantSpectrum(colorSpace, new( .7, .8, 1 )), width, height);

        return scene;
    }
    Scene TestCylinder(int width = 400, int height = 200, int spp = 100, int maxDepth = 50) {
        var material = new Metal(new RGBAlbedoSpectrum(colorSpace, new( .65, .7, .46 )), 0.7, colorSpace);
        Cylinder s = new Cylinder(new(0.1, 0, 0), 0.1, -0.1, 0.1, 360);
        PrimitiveList h = new(new GeometricPrimitive(s,material));

        Scene scene = new Scene(h, spp, maxDepth, CreateCamera(width, height, new(0,1,0), new(0,0,0.001), 20), new RGBIlluminantSpectrum(colorSpace, new( .7, .8, 1 )), width, height);

        return scene;
    }
    Scene FirstScene(int width = 400, int height = 200, int spp = 100, int maxDepth = 50) {
        PrimitiveList world = new PrimitiveList();

        var checker = new CheckerTexture(new RGBAlbedoSpectrum(colorSpace, new(.2, .3, .1)), new RGBAlbedoSpectrum(colorSpace, new(.9, .9, .9)), colorSpace);

        var material  = new Metal(new RGBAlbedoSpectrum(colorSpace, new(.7, .7, .7 )), 0.7, colorSpace);
        var material1 = new Metal(new RGBAlbedoSpectrum(colorSpace, new(1, 0.32, 0.36 )), 0, colorSpace);
        var material2 = new Metal(new RGBAlbedoSpectrum(colorSpace, new(0.90, 0.76, 0.46 )), 0,colorSpace);
        var material3 = new Metal(new RGBAlbedoSpectrum(colorSpace, new(0.65, 0.77, 0.97 )), 0, colorSpace);
        var material4 = new Metal(new RGBAlbedoSpectrum(colorSpace, new(0.90, 0.90, 0.90 )), 0,colorSpace);

        var center2 = new Point3D<double>(0, 0, -20) + new Vector3D<double>(0, 0.5, 0);

        world.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0.0, -10004, -20), 10000), new Lambertian(checker)));
        //world.Add(new GeometricPrimitive(new MovingSphere(new Point3D(0, 0, -20), center2, 0, 1, 4), material1));
        world.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(5, -1, -15), 2), material2));
        world.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(5, 0, -25), 3), material3));
        world.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(-5.5, 0, -15), 3), material4));

        Scene scene = new Scene(world, spp, maxDepth, CreateCamera(width, height, new(0,0,0), new(0,0,-1), 50, 15), new RGBIlluminantSpectrum(colorSpace, new( .8039, .8863, 1 )), width, height);

        return scene;
    }
    Scene TwoSpheres(int width = 400, int height = 200, int spp = 100, int maxDepth = 50) {
        PrimitiveList objects = new PrimitiveList();

        var checker = new CheckerTexture(new RGBAlbedoSpectrum(colorSpace, new(0.2,0.3,0.1)), new RGBAlbedoSpectrum(colorSpace, new(0.9,0.9,0.9)), colorSpace);

        objects.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0, -10, 0), 10), new Lambertian(checker)));
        objects.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0, 10.1, 0), 10), new Lambertian(checker)));

        Scene scene = new Scene(objs: objects,
                                spp: spp,
                                maxD: maxDepth,
                                CreateCamera(width, height, new(13,2,3), new(0,0,0), 20, 10),
                                background: new RGBIlluminantSpectrum(colorSpace, new(1,1,1)),
                                imageWidth: width,
                                imageHeight: height);

        return scene;
    }
    Scene TwoPerlinSpheres(int width = 400, int height = 200, int spp = 100, int maxDepth = 50) {
        PrimitiveList objects = new PrimitiveList();

        var pertext = new NoiseTexture(4, colorSpace);

        objects.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0, -1000, 0), 1000), new Lambertian(pertext)));
        objects.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0, 2.1, 0), 2), new Lambertian(pertext)));

        Scene scene = new Scene(objs: objects,
                                spp: spp,
                                maxD: maxDepth,
                                CreateCamera(width, height, new(13,2,3),new(0,0,0),20),
                                background: new RGBIlluminantSpectrum(colorSpace, new( .7, .8, 1 )),
                                imageWidth: width,
                                imageHeight: height);

        return scene;
    }
    Scene Earth(int width = 1920, int height = 1080, int spp = 10, int maxDepth = 50) {
        var earthTexture = new ImageTexture( "", colorSpace);

        var earthSurface = new Lambertian(earthTexture);
        var globe = new GeometricPrimitive(new Sphere(new Point3D<double>(0, 0, 0), 2), earthSurface);
        var ret = new PrimitiveList();
        ret.Add(globe);

        Scene scene = new Scene(objs: ret,
                                spp: spp,
                                maxD: maxDepth,
                                CreateCamera(width, height, new(13,2,3), new(0,0,0), 20),
                                background: new RGBIlluminantSpectrum(colorSpace, new( .7, .8, 1 )),
                                imageWidth: width,
                                imageHeight: height) ;

        return scene;
    }
    Scene SimpleLight(int width = 400, int height = 200, int spp = 1000, int maxDepth = 50) {
        PrimitiveList objekts = new PrimitiveList();

        var pertext = new NoiseTexture(4, colorSpace);
        objekts.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0, -1000, 0), 1000), new Lambertian(pertext)));
        objekts.Add(new GeometricPrimitive(new Sphere(new Point3D<double>(0, 2.1, 0), 2), new Lambertian(pertext)));

        var difflight = new DiffuseLight(new RGBIlluminantSpectrum(colorSpace, new(10,10,10)), colorSpace);
        objekts.Add(new GeometricPrimitive(new XYRect(3, 5, 1, 3, -2), difflight));

        Scene scene = new Scene(objs: objekts,
                                spp: spp,
                                maxD: maxDepth,
                                CreateCamera(width, height, new(26,3,6),new(0,2,0),20),
                                background: new RGBAlbedoSpectrum(colorSpace, new(0,0,0)),
                                imageWidth: width,
                                imageHeight: height);

        return scene;
    }
    Scene CornellBox(int width = 300, int height = 300, int spp = 100, int maxDepth = 50) {

        PrimitiveList objects = new PrimitiveList();

        var red =   new Lambertian  (new RGBAlbedoSpectrum(colorSpace, new( .65, .05, .05 )), colorSpace);
        var white = new Lambertian  (new RGBAlbedoSpectrum(colorSpace, new( .73, .73, .73 )), colorSpace);
        var green = new Lambertian  (new RGBAlbedoSpectrum(colorSpace, new( .12, .45, .15 )), colorSpace);
        var light = new DiffuseLight(new RGBIlluminantSpectrum(colorSpace, new( 20, 20, 20 )), colorSpace);

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
                                spp: spp,
                                maxD: maxDepth,
                                CreateCamera(width, height, new(278, 278, -800), new(278,278,0), 40),
                                background: new RGBIlluminantSpectrum(colorSpace, new(0,0,0)),
                                imageWidth: width,
                                imageHeight: height);

        return scene;
    }

    private ICamera CreateCamera(int width, int height, Point3D<double> origin, Point3D<double> lookAt, int fov, double focusDistance = 0) {
        double aspectRatio = width / (double)height;

        Bounds2D<double> screen;

        if(aspectRatio > 1) {
            screen = new Bounds2D<double>(
            new Point2D<double>(-aspectRatio, -1),
            new Point2D<double>(aspectRatio, 1)
            );
        } else {
            screen = new Bounds2D<double>(
            new(-1, -1 / aspectRatio),
            new(1, 1 / aspectRatio)
            );
        }

        double lensRadius = 0;

        if (focusDistance != 0) {
            lensRadius = 0.1;
        }

        CameraTransform cameraToWorld = new CameraTransform(Transform.LookAt(origin, lookAt, new(0, -1, 0)).Inverse());

        return new PerspectiveCamera(cameraToWorld, 0, 1, width, height, screen, lensRadius, focusDistance, fov, lookAt);
        //return new OrthographicCamera(cameraToWorld, 0, 1, width, height, screen, lensRadius, focusDistance, lookAt);
    }

    private string ShowOpenFile()
    {
        var ofn = new OpenFileDialog
        {
            Filter = "Object files (*.obj)|*.obj",
        };
        return ofn.ShowDialog() == true ? ofn.FileName : null;
    }
}
