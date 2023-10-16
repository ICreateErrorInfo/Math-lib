using Moarx.Rasterizer;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SPH_FluidSim; 
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow: Window {
    Stopwatch sw = new Stopwatch();
    double deltaTime = 1;

    public MainWindow() {
        InitializeComponent();
        ParticleSystem system = new ParticleSystem();

        Start(system);
    }

    public async void Start(ParticleSystem system) {
        while(true) {
            sw.Restart();
            await Task.Run(() => system.Update(deltaTime));
            image.Source = ToImageSource(system.GetBitmap());
            sw.Stop();
            deltaTime = (double)sw.ElapsedMilliseconds/100;
        }
    }

    ImageSource ToImageSource(DirectBitmap bitmap) {

        var bs = BitmapSource.Create(
                pixelWidth: bitmap.Width,
                pixelHeight: bitmap.Height,
                dpiX: 96,
                dpiY: 96,
                pixelFormat: PixelFormats.Bgra32,
                palette: null,
                pixels: bitmap.GetBytes(),
                stride: bitmap.Stride);

        return bs;

    }
}
