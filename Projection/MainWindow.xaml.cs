using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Math_lib;
using Microsoft.Win32;
using Point3 = Math_lib.Point3;
using Vector3 = Math_lib.Vector3;

namespace Projection {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly DispatcherTimer _timer;
        private  int             _angle;
        private readonly Rasterizer      _rasterizer;
        private Mesh mesh = new();


        public MainWindow() {

            InitializeComponent();

            //Init
            const int screenWidth = 1920;
            const int screenHeight = 1080;

            //Load Mesh
            ShowOpenFile();

            //Init Rasterizer
            _rasterizer = new ScannLineRasterizer(screenWidth, screenHeight);
            //_rasterizer = new BresenhamRasterizer(screenWidth, screenHeight);

            //Render
            Render();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(40),
                IsEnabled = true
            };

            _timer.Tick += OnRenderSzene;

        }

        private void ShowOpenFile()
        {
            var ofn = new OpenFileDialog
            {
                Filter = "Object files (*.obj)|*.obj",
            };
            if (ofn.ShowDialog() == true)
            {
                mesh = Importer.Obj(ofn.FileName).CreateMesh();
            }
        }
        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.Key == Key.P)
            {
                _timer.IsEnabled ^= true;
                e.Handled = true;
            }
        }

        private void OnRenderSzene(object sender, EventArgs e) {
            Render();
        }
        private void Render() {

            _rasterizer.Clear();
            RenderScene(mesh, _angle++, _rasterizer);

            Image.Source = _rasterizer.Bmp.ToImageSource();
        }
        private static void RenderScene(Mesh meshCube, int angle, Rasterizer r) {

            //Init
            var screenWidth  = r.Width;
            var screenHeight = r.Height;

            Point3 cameraP = new Point3(0, 0, 0);

            Matrix4x4 rotateZ = Matrix4x4.RotateZMarix(angle);
            Matrix4x4 rotateY = Matrix4x4.RotateYMarix(angle);
            Matrix4x4 rotateX = Matrix4x4.RotateXMarix(angle);

            Matrix4x4 projection = Matrix4x4.Projection(screenWidth, screenHeight, 100, 1, 1000);
            Matrix4x4 translation = Matrix4x4.Translation(0, 0, 3);

            Matrix4x4 worldMatrix = new Matrix4x4();
            worldMatrix = Matrix4x4.Identity();
            worldMatrix = worldMatrix * translation;
            worldMatrix = worldMatrix * rotateZ;
            worldMatrix = worldMatrix * rotateX;
            worldMatrix = worldMatrix * rotateY;

            //Draw
            foreach (Triangle3 tri in meshCube.Triangles) {

                //Transform triangel
                Triangle3 triTranformed = new Triangle3();
                triTranformed = worldMatrix * tri;

                //calc Normals
                Vector3 line1 = triTranformed.Points[1] - triTranformed.Points[0];
                Vector3 line2 = triTranformed.Points[2] - triTranformed.Points[0];

                Vector3 normal = Vector3.UnitVector(Vector3.Cross(line1, line2));

                //check visability
                if (Vector3.Dot(normal, triTranformed.Points[0] - cameraP) < 0)
                {
                    //Illumination
                    Vector3 lightDirection = new Vector3(0, 0, -1);
                    lightDirection = Vector3.UnitVector(lightDirection);

                    double dp = Vector3.Dot(normal, lightDirection);
                    var grayValue = Convert.ToByte(Math.Abs(dp * Byte.MaxValue));
                    var col = Color.FromArgb(255, grayValue, grayValue, grayValue);

                    //project
                    Triangle3 triProjected = projection * triTranformed;

                    triProjected.Points[0] /= triProjected.Points[0].W;
                    triProjected.Points[1] /= triProjected.Points[1].W;
                    triProjected.Points[2] /= triProjected.Points[2].W;

                    //Convert from Triangle3 to Triangle2
                    Triangle2 triProjectedConv = new(triProjected);

                    //Scaling into screenspace
                    triProjectedConv += new Point2(1, 1);
                    triProjectedConv *= new Point2(0.5 * screenWidth, 0.5 * screenHeight);

                    //Drawing
                    r.DrawTriangle(triProjectedConv, col, true);
                }       
            }
        }
    }
}