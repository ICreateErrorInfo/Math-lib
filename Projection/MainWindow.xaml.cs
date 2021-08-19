using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        readonly DispatcherTimer   _timer;
        readonly Mesh              _mesh;
        readonly WaveTextureEffect _we;
        readonly Effect            _effect;

        double    _angleY;
        double _angleX;
        double _time;

        public MainWindow() {

            InitializeComponent();

            //var solidColor = new SolidColorEffect();
            //solidColor.SetColor(Color.White);
            //_effect = solidColor;
            //_mesh = Cube.GetPlain();

            //var textureEffect = new TextureEffect();
            //var exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? "";
            //string textureFile = Path.Combine(exeDir, "Images", @"sauron-bhole-100x100.png");
            //textureEffect.BindTexture(textureFile);
            //_effect = textureEffect;
            //_mesh = Cube.GetPlain();

            //var waveeffect = new wavetextureeffect();
            //var exedir = path.getdirectoryname(system.reflection.assembly.getentryassembly()?.location) ?? "";
            //string texturefile = path.combine(exedir, "images", @"sauron-bhole-100x100.png");
            //waveeffect.bindtexture(texturefile);
            //_we = waveeffect;
            //_effect = waveeffect;
            //_mesh = Plane.GetSkinned(20);

            //var solidGeoEffect = new SolidGeometryEffect();
            //solidGeoEffect.BindColors(new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Yellow, Color.Cyan });
            //_effect = solidGeoEffect;
            //_mesh = Cube.GetPlain();

            //var VertexFlatEffect = new VertexFlatEffect();
            //_effect = VertexFlatEffect;
            //_mesh = Cube.GetIndependentFacesNormals();

            var GeometryFlatEffect = new GeometryFlatEffect();
            _effect = GeometryFlatEffect;
            _mesh = Sphere.GetPlain();

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
                _angleY += 0.1;
            }
            if (e.Key == Key.D)
            {
                _angleY -= 0.1;
            }

            if (e.Key == Key.W)
            {
                _angleX += .1;
            }
            if (e.Key == Key.S)
            {
                _angleX -= .1;
            }
        }

        private void OnRenderSzene(object sender, EventArgs e) {

            _we?.SetTime(_time);

            Pipeline p = new Pipeline();
            _effect.BindTranslation(new(0,0,2));
            _effect.BindRotation(Matrix4x4.RotateYMarix(_angleY) * Matrix4x4.RotateXMarix(_angleX));

            p.Draw(_mesh, _effect);

            Image.Source = p.Bmp.ToImageSource();
            _time += .05;
        }       
    }
}