using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Math_lib;
using Math_lib.VertexAttributes;
using Microsoft.Win32;
using Projection.Effects;
using Projection.Primitives;

namespace Projection
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        private readonly DispatcherTimer   _timer;
        private readonly Mesh              _mesh;
        private readonly Effect            _effect;
        private readonly Pipeline          _p;

        private double    _angleY;
        private double _angleX;
        private Vector3D _trans = new(0, 0, 3);
        private double _time;
        private double _maxTime = 0;
        private double _minTime = double.MaxValue;

        public MainWindow() {

            InitializeComponent();
            _p = new Pipeline();

            var solidcolor = new SolidColorEffect();
            solidcolor.SetColor(Color.Green);
            _effect = solidcolor;
            _mesh = Cube.GetPlain();

            var solidGeoEffect = new SolidGeoEffect();
            solidGeoEffect.BindColors(new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Yellow, Color.Cyan });
            _effect = solidGeoEffect;
            //_mesh = Cube.GetPlain();

            //var textureEffect = new TextureEffect();
            //var exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntfasterryAssembly()?.Location) ?? "";
            //string textureFile = Path.Combine(exeDir, "Images", @"office_skin.jpg");
            //textureEffect.BindTexture(textureFile);
            //_effect = textureEffect;
            //_mesh = Cube.GetSkinned();

            //var waveeffect = new WaveTextureEffect();
            //var exedir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly()?.Location) ?? "";
            //string texturefile = Path.Combine(exedir, "images", @"sauron-bhole-100x100.png");
            //waveeffect.BindTexture(texturefile);
            //_effect = waveeffect;
            //_mesh = Plane.GetSkinned(20);

            //var VertexFlatEffect = new VertexFlatEffect();
            //_effect = VertexFlatEffect;
            //_mesh = Plane.GetNormals(50);

            //var GeometryFlatEffect = new GeometryFlatEffect();
            //_effect = GeometryFlatEffect;
            //_mesh = Sphere.GetPlain();

            //Load Mesh
            ShowOpenFile();
            _mesh = Importer.mesh;

            Vertecies.Text = "Vertecies: " + _mesh.vertices.Count;

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
            if(e.Key == Key.Back)
            {
                _minTime = double.MaxValue;
                _maxTime = 0;
            }
            if (e.Key == Key.P)
            {
                _timer.IsEnabled ^= true;
                e.Handled = true;
            }

            if (e.Key == Key.Left)
            {
                _angleY -= 0.1;
            }
            if (e.Key == Key.Right)
            {
                _angleY += 0.1;
            }

            if (e.Key == Key.Up)
            {
                _angleX -= .1;
            }
            if (e.Key == Key.Down)
            {
                _angleX += .1;
            }
            if(e.Key == Key.Space)
            {
                _trans -= new Vector3D(0, .1, 0);
            }
            if (e.Key == Key.LeftCtrl)
            {
                _trans += new Vector3D(0, .1, 0);
            }
            if (e.Key == Key.D)
            {
                _trans -= new Vector3D(.1, 0, 0);
            }
            if (e.Key == Key.A)
            {
                _trans += new Vector3D(.1, 0, 0);
            }
        }

        private void OnRenderSzene(object sender, EventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            if(_effect.GetType() == typeof(WaveTextureEffect))
            {
                var we = (WaveTextureEffect)_effect;
                we.SetTime(_time);
            }

            _effect.BindTranslation(_trans);
            _effect.BindRotation(Matrix.RotateYMarix(_angleY) * Matrix.RotateXMarix(_angleX));

            _p.Draw(_mesh, _effect);

            Image.Source = _p.Bmp.ToImageSource();
            _time += .05;

            stopwatch.Stop();
            if(stopwatch.ElapsedMilliseconds > _maxTime && _time > 0.5)
            {
                _maxTime = stopwatch.ElapsedMilliseconds;
                TimeMax.Text = "MaxTime: " + stopwatch.ElapsedMilliseconds.ToString();
            }

            if(stopwatch.ElapsedMilliseconds < _minTime)
            {
                _minTime = stopwatch.ElapsedMilliseconds;
                TimeMin.Text = "MinTime: " + stopwatch.ElapsedMilliseconds.ToString();
            }

            Time.Text = stopwatch.ElapsedMilliseconds.ToString();
        }       
    }
}