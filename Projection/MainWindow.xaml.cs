﻿using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Math_lib;

using Point = Math_lib.Point;

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

            const int screenWidth  = 1024;
            const int screenHeight = 1024;

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
            RenderScene(_meshCube, _angle++, _rasterizer, Color.White);

            Image.Source = _rasterizer.Bmp.ToImageSource();
        }

        private static void RenderScene(Mesh meshCube, int angle, Rasterizer r, Color color) {

            var screenWidth  = r.Width;
            var screenHeight = r.Height;
            //Draw
            foreach (Triangle tri in meshCube.Triangles) {

                var triRotated = new Triangle();

                triRotated.Points[0] = Matrix4x4.RotateZMarix(angle) * tri.Points[0];
                triRotated.Points[1] = Matrix4x4.RotateZMarix(angle) * tri.Points[1];
                triRotated.Points[2] = Matrix4x4.RotateZMarix(angle) * tri.Points[2];

                triRotated.Points[0] = Matrix4x4.RotateYMarix(angle) * triRotated.Points[0];
                triRotated.Points[1] = Matrix4x4.RotateYMarix(angle) * triRotated.Points[1];
                triRotated.Points[2] = Matrix4x4.RotateYMarix(angle) * triRotated.Points[2];

                var triTranslated = new Triangle();

                triTranslated.Points[0] = new Point(triRotated.Points[0].X, triRotated.Points[0].Y, triRotated.Points[0].Z + 3);
                triTranslated.Points[1] = new Point(triRotated.Points[1].X, triRotated.Points[1].Y, triRotated.Points[1].Z + 3);
                triTranslated.Points[2] = new Point(triRotated.Points[2].X, triRotated.Points[2].Y, triRotated.Points[2].Z + 3);

                Point p  = Matrix4x4.Projection(screenWidth, screenHeight, 150, 0.1, 1000) * triTranslated.Points[0];
                Point p1 = Matrix4x4.Projection(screenWidth, screenHeight, 150, 0.1, 1000) * triTranslated.Points[1];
                Point p2 = Matrix4x4.Projection(screenWidth, screenHeight, 150, 0.1, 1000) * triTranslated.Points[2];

                var triProjected = new Triangle(p, p1, p2);

                triProjected.Points[0] += new Point(1, 1, 0);
                triProjected.Points[1] += new Point(1, 1, 0);
                triProjected.Points[2] += new Point(1, 1, 0);

                triProjected.Points[0] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);
                triProjected.Points[1] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);
                triProjected.Points[2] *= new Point(0.5 * screenWidth, 0.5 * screenHeight, 1);

                r.DrawTriangle(triProjected, color);
            }

        }

    }

}