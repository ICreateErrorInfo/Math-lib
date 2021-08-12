using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Math_lib;
using Microsoft.Win32;

namespace Projection {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        private readonly DispatcherTimer _timer;
        private int angleY = 0;
        private int angleX = 0;
        Mesh mesh;
        TextureEffect effect;

        public MainWindow() {

            InitializeComponent();


            //SolidColorEffect effect = new SolidColorEffect();
            //effect.SetColor(Color.White);
            mesh = Mesh.GetCube();
            effect = new TextureEffect();
            effect.BindTexture(@"C:\Users\Moritz\source\repos\Math-lib\Projection\Images\sauron-bhole-100x100.png");

            //Load Mesh
            ShowOpenFile();

            //Render
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
                Importer.Obj(ofn.FileName);
            }
        }
        protected override void OnKeyDown(KeyEventArgs e) 
        {
            if (e.Key == Key.P)
            {
                _timer.IsEnabled ^= true;
                e.Handled = true;
            }

            if (e.Key == Key.A)
            {
                angleY += 1;
            }
            if (e.Key == Key.D)
            {
                angleY -= 1;
            }

            if (e.Key == Key.W)
            {
                angleX += 1;
            }
            if (e.Key == Key.S)
            {
                angleX -= 1;
            }
        }

        private void OnRenderSzene(object sender, EventArgs e) {

            Pipeline p = new Pipeline();
            p.BindTranslation(new(0,0,3));
            p.BindRotation(Matrix4x4.RotateYMarix(angleY) * Matrix4x4.RotateXMarix(angleX));

            p.Draw(mesh, effect);

            Image.Source = p.Bmp.ToImageSource();
        }       
    }
}