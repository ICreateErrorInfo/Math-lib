using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Math_lib;
using Rasterizer_lib;

namespace SandSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly DispatcherTimer _timer;
        public Rasterizer r;
        public Sim s;
        public int scale = 100;

        public MainWindow()
        {
            InitializeComponent();
            s = new Sim(scale);
            r = new Rasterizer(width: 800,
                               height: 800,
                               scale: scale,
                               cooMi: false, false);

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(0),
                IsEnabled = true
            };

            _timer.Tick += OnRenderSzene;
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            //r.UpdateScale((int)sizeInfo.NewSize.Width, (int)sizeInfo.NewSize.Height);

            Display.Source = r.GetSource();
        }
        private void OnRenderSzene(object? sender, EventArgs e) {
          
            r.Clear();
            s.UpdateFrame();

            for (int y = s.array.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < s.array.GetLength(1); x++) 
                {
                    
                    var color = s.array[x, y].color;
                   
                    if (color == System.Drawing.Color.Black) 
                    {
                        continue;
                    }

                    r.DrawPoint(new Point2D(x + .5, -(y - (scale - 1)) + .5), color);

                }
            }

            Display.Source = r.GetSource();
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Particle p = new Particle();
                p.id = "sand";
                p.color = System.Drawing.Color.Yellow;

                s.array[scale / 2, scale - 1] = p;
            }
        }
    }
}
