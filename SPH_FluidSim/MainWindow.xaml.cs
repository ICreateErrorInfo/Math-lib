using Moarx.Rasterizer;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SPH_FluidSim; 
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow: Window {
    public MainWindow() {
        InitializeComponent();
        ParticleSystem system = new ParticleSystem();
        system.Update();
        image.Source = ToImageSource(system.GetBitmap());
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
