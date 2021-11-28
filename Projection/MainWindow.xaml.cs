﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

using Math_lib;
using Microsoft.Win32;
using Projection.Effects;
using Projection.Primitives;

namespace Projection
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window {

        readonly DispatcherTimer   _timer;
        readonly Mesh              _mesh;
        readonly Effect            _effect;

        double    _angleY;
        double _angleX;
        Vector3D _trans = new(0, 0, 3);

        public MainWindow() {

            InitializeComponent();

            //var solidColor = new SolidColorEffect();
            //solidColor.SetColor(Color.Green);
            //_effect = solidColor;
            //_mesh = Cube.GetPlain();

            var solidGeoEffect = new SolidGeoEffect();
            solidGeoEffect.BindColors(new List<Color>() { Color.Red, Color.Green, Color.Blue, Color.Magenta, Color.Yellow, Color.Cyan });
            _effect = solidGeoEffect;
            _mesh = Cube.GetPlain();

            //Load Mesh
            ShowOpenFile();

            //Render
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100),
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
            Pipeline p = new Pipeline();
            _effect.BindTranslation(_trans);
            _effect.BindRotation(Matrix.RotateYMarix(_angleY) * Matrix.RotateXMarix(_angleX));

            p.Draw(_mesh, _effect);

            Image.Source = p.Bmp.ToImageSource();
        }       
    }
}