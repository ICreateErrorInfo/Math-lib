using System.Windows;
using Math_lib;

namespace RaytracingInOneWeek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Raytracer r = new Raytracer(1920, 1080);

            Screen.Source = r.Render().ToImageSource();
        }
    }
}
