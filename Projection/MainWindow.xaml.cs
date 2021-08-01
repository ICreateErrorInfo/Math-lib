using System;
using System.Windows;
using System.Windows.Threading;

using Math_lib;

using Point = Math_lib.Point;

namespace Projection {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly DispatcherTimer _timer;
        private          int             _angle;
        readonly         Mesh            _meshCube = new();
        private readonly Rasterizer      _rasterizer;

        public MainWindow() {

            //Cube
            _meshCube.tris.Add(new Triangle(new Point(0, 0, 0), new Point(0, 1, 0), new Point(1, 1, 0)));
            _meshCube.tris.Add(new Triangle(new Point(0, 0, 0), new Point(1, 1, 0), new Point(1, 0, 0)));

            _meshCube.tris.Add(new Triangle(new Point(1, 0, 0), new Point(1, 1, 0), new Point(1, 1, 1)));
            _meshCube.tris.Add(new Triangle(new Point(1, 0, 0), new Point(1, 1, 1), new Point(1, 0, 1)));

            _meshCube.tris.Add(new Triangle(new Point(1, 0, 1), new Point(1, 1, 1), new Point(0, 1, 1)));
            _meshCube.tris.Add(new Triangle(new Point(1, 0, 1), new Point(0, 1, 1), new Point(0, 0, 1)));

            _meshCube.tris.Add(new Triangle(new Point(0, 0, 1), new Point(0, 1, 1), new Point(0, 1, 0)));
            _meshCube.tris.Add(new Triangle(new Point(0, 0, 1), new Point(0, 1, 0), new Point(0, 0, 0)));

            _meshCube.tris.Add(new Triangle(new Point(0, 1, 0), new Point(0, 1, 1), new Point(1, 1, 1)));
            _meshCube.tris.Add(new Triangle(new Point(0, 1, 0), new Point(1, 1, 1), new Point(1, 1, 0)));

            _meshCube.tris.Add(new Triangle(new Point(1, 0, 1), new Point(0, 0, 1), new Point(0, 0, 0)));
            _meshCube.tris.Add(new Triangle(new Point(1, 0, 1), new Point(0, 0, 0), new Point(1, 0, 0)));

            InitializeComponent();

            const int screenWidth  = 1024;
            const int screenHeight = 1024;

            _rasterizer = new Rasterizer(screenWidth, screenHeight);

            _timer = new DispatcherTimer {
                Interval  = TimeSpan.FromMilliseconds(40),
                IsEnabled = true
            };

            _timer.Tick += OnRenderSzene;

        }

        private void OnRenderSzene(object sender, EventArgs e) {

            _rasterizer.Clear();
            var bmp = RenderScene(_meshCube, _angle++, _rasterizer);
            Image.Source = bmp.ToImageSource();
        }

        private static DirectBitmap RenderScene(Mesh meshCube, int angle, Rasterizer r) {

            var screenWidth  = r.Width;
            var screenHeight = r.Height;
            //Draw
            foreach (Triangle tri in meshCube.tris) {

                var triRotated = new Triangle();

                triRotated.p[0] = Matrix4x4.RotateZMarix(angle) * tri.p[0];
                triRotated.p[1] = Matrix4x4.RotateZMarix(angle) * tri.p[1];
                triRotated.p[2] = Matrix4x4.RotateZMarix(angle) * tri.p[2];

                triRotated.p[0] = Matrix4x4.RotateYMarix(angle) * triRotated.p[0];
                triRotated.p[1] = Matrix4x4.RotateYMarix(angle) * triRotated.p[1];
                triRotated.p[2] = Matrix4x4.RotateYMarix(angle) * triRotated.p[2];

                var triTranslated = new Triangle();

                triTranslated.p[0] = new Point(triRotated.p[0].X, triRotated.p[0].Y, triRotated.p[0].Z + 3);
                triTranslated.p[1] = new Point(triRotated.p[1].X, triRotated.p[1].Y, triRotated.p[1].Z + 3);
                triTranslated.p[2] = new Point(triRotated.p[2].X, triRotated.p[2].Y, triRotated.p[2].Z + 3);

                Point p  = Matrix4x4.Projection(screenWidth, screenHeight, 90, 0.1, 1000) * triTranslated.p[0];
                Point p1 = Matrix4x4.Projection(screenWidth, screenHeight, 90, 0.1, 1000) * triTranslated.p[1];
                Point p2 = Matrix4x4.Projection(screenWidth, screenHeight, 90, 0.1, 1000) * triTranslated.p[2];

                var triProjected = new Triangle(p, p1, p2);

                triProjected.p[0] += new Point(1, 1, 0);
                triProjected.p[1] += new Point(1, 1, 0);
                triProjected.p[2] += new Point(1, 1, 0);

                triProjected.p[0] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);
                triProjected.p[1] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);
                triProjected.p[2] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);

                r.DrawTriangle(triProjected, System.Drawing.Color.White);
            }

            return r.Bmp;
        }

    }

}