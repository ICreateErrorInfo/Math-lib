using System.Windows;
using Math_lib;

namespace RasterizerTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  
            
            Rasterizer r = new Rasterizer(1920, 1080);

            //r.DrawLine(new(r.width - 1,0), new(0, r.height - 1), System.Drawing.Color.Red, 10);
            //r.DrawLine(new(0,0), new(r.width - 1, r.height - 1), System.Drawing.Color.FromArgb(226, 7, 255), 10);
            //r.DrawLine(new(0,r.height/2), new(r.width - 1, r.height/2), System.Drawing.Color.Cyan, 10);
            //r.DrawLine(new(r.width/2,0), new(r.width/2, r.height - 1), System.Drawing.Color.Green, 10);

            r.DrawCircle(new(r.width/2, r.height/2), 30, System.Drawing.Color.White, true);

            r.DrawCircle(new(0,0), 30, System.Drawing.Color.White, true); // Bug

            Display.Source = r.GetSource();
        }
    }
}
