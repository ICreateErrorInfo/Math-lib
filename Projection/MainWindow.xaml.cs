using System;
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
        Vector3D trans = new Vector3D(0,0,3);
        double _time;

        public MainWindow() {

            InitializeComponent();

            //var solidColor = new SolidColorEffect();
            //solidColor.SetColor(Color.White);
            //_effect = solidColor;
            //_mesh = Cube.GetPlain();

            //Todo TextureBug

            //var textureEffect = new TextureEffect();
            //var exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? "";
            //string textureFile = Path.Combine(exeDir, "Images", @"sauron-bhole-100x100.png");
            //textureEffect.BindTexture(textureFile);
            //_effect = textureEffect;
            //_mesh = Cube.GetPlain();

            //var waveeffect = new WaveTextureEffect();
            //var exedir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? "";
            //string texturefile = Path.Combine(exedir, "images", @"sauron-bhole-100x100.png");
            //waveeffect.BindTexture(texturefile);
            //_we = waveeffect;
            //_effect = waveeffect;
            //_mesh = Plane.GetSkinned(20);

            //var solidGeoEffect = new SolidGeometryEffect();
            //solidGeoEffect.BindColors(new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Yellow, Color.Cyan });
            //_effect = solidGeoEffect;
            //_mesh = Cube.GetPlain();

            var VertexFlatEffect = new VertexFlatEffect();
            _effect = VertexFlatEffect;
            _mesh = Cube.GetIndependentFacesNormals();

            //TODO clipping funktioniert nicht richtig

            //var GeometryFlatEffect = new GeometryFlatEffect();
            //_effect = GeometryFlatEffect;
            //_mesh = Sphere.GetPlain();

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
            if(e.Key == Key.Up)
            {
                trans += new Vector3D(0, .1, 0);
            }
            if (e.Key == Key.Down)
            {
                trans -= new Vector3D(0, .1, 0);
            }
            if (e.Key == Key.Right)
            {
                trans += new Vector3D(.1, 0, 0);
            }
            if (e.Key == Key.Left)
            {
                trans -= new Vector3D(.1, 0, 0);
            }
        }

        private void OnRenderSzene(object sender, EventArgs e) {

            _we?.SetTime(_time);

            Pipeline p = new Pipeline();
            _effect.BindTranslation(trans);
            _effect.BindRotation(Matrix3x3.RotateYMarix(_angleY) * Matrix3x3.RotateXMarix(_angleX));

            p.Draw(_mesh, _effect);

            Image.Source = p.Bmp.ToImageSource();
            _time += .05;
        }       
    }
}