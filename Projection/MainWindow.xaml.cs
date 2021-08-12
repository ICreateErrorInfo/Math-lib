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
        Effect _effect;
        WaveTextureEffect we;
        double timer = 0;

        public MainWindow() {

            InitializeComponent();

            mesh = Plane.GetSkinned(20);

            //var solidColor = new SolidColorEffect();
            //solidColor.SetColor(Color.White);
            //_effect = solidColor;


            //var textureEffect = new TextureEffect();
            //textureEffect.BindTexture(@"C:\Users\Moritz\source\repos\Math-lib\Projection\Images\sauron-bhole-100x100.png");
            //_effect = textureEffect;

            var waveEffect = new WaveTextureEffect();
            waveEffect.BindTexture(@"C:\Users\Moritz\source\repos\Math-lib\Projection\Images\sauron-bhole-100x100.png");
            we = waveEffect;

            //Load Mesh
            ShowOpenFile();

            //Render
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1),
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
                Importer.Obj(ofn.FileName, true);
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

            we.SetTime(timer);
            _effect = we;

            Pipeline p = new Pipeline();
            _effect.BindTranslation(new(0,0,2));
            _effect.BindRotation(Matrix4x4.RotateYMarix(angleY) * Matrix4x4.RotateXMarix(angleX));

            p.Draw(mesh, _effect);

            Image.Source = p.Bmp.ToImageSource();
            timer += .05;
        }       
    }
}