using System.Windows;
using Math_lib;

namespace RasterizerTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        public Rasterizer r;
        public MainWindow()
        {
            InitializeComponent();
            r = new Rasterizer(width: 1,
                               height: 1,
                               scale: 8,
                               cooMi: true);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) 
        {
            base.OnRenderSizeChanged(sizeInfo);
            r.UpdateScale((int)sizeInfo.NewSize.Width, (int)sizeInfo.NewSize.Height);

            //r.DrawLine(p1: new(0,0), p2: new(2, 0), c: System.Drawing.Color.Red, thickness: 1);

            //r.DrawCircle(p1: new(0,0), radius: 1, c: System.Drawing.Color.White, fill: true);

            //r.DrawCircle(new(0,0), 1, System.Drawing.Color.White, true);
            //r.DrawLine(new(-1,-1), new(1,1), System.Drawing.Color.White);

            //r.DrawRectangle(new(-1, 1), new(1, -1), System.Drawing.Color.White, true);
            //r.DrawLine(new(-1, 1), new(1, -1), System.Drawing.Color.White, 1);

            //r.DrawLine(new(0, 0), new(80, 8), System.Drawing.Color.White);

            //r.DrawPoint(new(0.5,0.5), System.Drawing.Color.White); //Todo

            //r.DrawEllipse(new(0,0), 19, 2, System.Drawing.Color.White, true);

            r.DrawQuadBezier(new(0,0), new(0, 2), new(2,2), System.Drawing.Color.White);

            Display.Source = r.GetSource();
        }

      
    }
}
