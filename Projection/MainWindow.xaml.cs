using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Math_lib;

using Point = Math_lib.Point;
using Vector = Math_lib.Vector;

namespace Projection {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        readonly DispatcherTimer _timer;
        private  int             _angle;
        readonly Mesh            _meshCube = new();
        readonly Rasterizer      _rasterizer;

        public MainWindow() {

            //Cube
            _meshCube.Triangles.Add(new Triangle(new Point(0, 0, 0), new Point(0, 1, 0), new Point(1, 1, 0)));
            _meshCube.Triangles.Add(new Triangle(new Point(0, 0, 0), new Point(1, 1, 0), new Point(1, 0, 0)));

            _meshCube.Triangles.Add(new Triangle(new Point(1, 0, 0), new Point(1, 1, 0), new Point(1, 1, 1)));
            _meshCube.Triangles.Add(new Triangle(new Point(1, 0, 0), new Point(1, 1, 1), new Point(1, 0, 1)));

            _meshCube.Triangles.Add(new Triangle(new Point(1, 0, 1), new Point(1, 1, 1), new Point(0, 1, 1)));
            _meshCube.Triangles.Add(new Triangle(new Point(1, 0, 1), new Point(0, 1, 1), new Point(0, 0, 1)));

            _meshCube.Triangles.Add(new Triangle(new Point(0, 0, 1), new Point(0, 1, 1), new Point(0, 1, 0)));
            _meshCube.Triangles.Add(new Triangle(new Point(0, 0, 1), new Point(0, 1, 0), new Point(0, 0, 0)));

            _meshCube.Triangles.Add(new Triangle(new Point(0, 1, 0), new Point(0, 1, 1), new Point(1, 1, 1)));
            _meshCube.Triangles.Add(new Triangle(new Point(0, 1, 0), new Point(1, 1, 1), new Point(1, 1, 0)));

            _meshCube.Triangles.Add(new Triangle(new Point(1, 0, 1), new Point(0, 0, 1), new Point(0, 0, 0)));
            _meshCube.Triangles.Add(new Triangle(new Point(1, 0, 1), new Point(0, 0, 0), new Point(1, 0, 0)));

            InitializeComponent();

            const int screenWidth  = 1080;
            const int screenHeight = 1080;

            _rasterizer  = new BresenhamRasterizer(screenWidth, screenHeight);

            Render();
            _timer = new DispatcherTimer {
                Interval = TimeSpan.FromMilliseconds(40),
                IsEnabled = true
            };

            _timer.Tick += OnRenderSzene;

        }

        protected override void OnKeyDown(KeyEventArgs e) {
            _angle += 1;
            OnRenderSzene(null, null);
        }

        private void OnRenderSzene(object sender, EventArgs e) {
            Render();
        }

        private void Render() {

            _rasterizer.Clear();
            RenderScene(_meshCube, _angle++, _rasterizer);

            Image.Source = _rasterizer.Bmp.ToImageSource();
        }

        private static void RenderScene(Mesh meshCube, int angle, Rasterizer r) {

            var screenWidth  = r.Width;
            var screenHeight = r.Height;

            Point cameraP = new Point(0, 0, 0);

            Matrix4x4 rotateZ = Matrix4x4.RotateZMarix(angle);
            Matrix4x4 rotateY = Matrix4x4.RotateYMarix(angle);

            Matrix4x4 projection = Matrix4x4.Projection(screenWidth, screenHeight, 90, 0.1, 1000);

            //Draw
            foreach (Triangle tri in meshCube.Triangles) {

                //Rotation
                var triRotated = new Triangle();

                triRotated.Points[0] = rotateZ * tri.Points[0];
                triRotated.Points[1] = rotateZ * tri.Points[1];
                triRotated.Points[2] = rotateZ * tri.Points[2];

                triRotated.Points[0] = rotateY * triRotated.Points[0];
                triRotated.Points[1] = rotateY * triRotated.Points[1];
                triRotated.Points[2] = rotateY * triRotated.Points[2];

                //Transation
                var triTranslated = new Triangle();

                triTranslated.Points[0] = new Point(triRotated.Points[0].X, triRotated.Points[0].Y, triRotated.Points[0].Z + 3);
                triTranslated.Points[1] = new Point(triRotated.Points[1].X, triRotated.Points[1].Y, triRotated.Points[1].Z + 3);
                triTranslated.Points[2] = new Point(triRotated.Points[2].X, triRotated.Points[2].Y, triRotated.Points[2].Z + 3);

                //calc Normals
                Vector line1 = triTranslated.Points[1] - triTranslated.Points[0];
                Vector line2 = triTranslated.Points[2] - triTranslated.Points[0];

                Vector normal = Vector.UnitVector(Vector.Cross(line1, line2));

                //check visability
                if (Vector.Dot(normal, triTranslated.Points[0] - cameraP) < 0)
                {
                    //Illumination
                    Vector lightDirection = new Vector(0, 0, -1);
                    lightDirection = Vector.UnitVector(lightDirection);

                    double dp = Vector.Dot(normal, lightDirection);
                    var grayValue = Convert.ToByte(Math.Abs(dp * Byte.MaxValue));
                    var col = Color.FromArgb(255, grayValue, grayValue, grayValue);

                    //project
                    Point p  = projection * triTranslated.Points[0];
                    Point p1 = projection * triTranslated.Points[1];
                    Point p2 = projection * triTranslated.Points[2];

                    var triProjected = new Triangle(p, p1, p2);

                    //Scaled
                    triProjected.Points[0] += new Point(1, 1, 0);
                    triProjected.Points[1] += new Point(1, 1, 0);
                    triProjected.Points[2] += new Point(1, 1, 0);

                    triProjected.Points[0] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);
                    triProjected.Points[1] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);
                    triProjected.Points[2] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);

                    r.DrawTriangle(triProjected, col, true);
                }       
            }
        }
    }
}