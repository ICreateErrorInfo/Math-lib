using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using NeuralNetwork;
using System.Drawing;

namespace Neural_Network_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NeuralNetwork.NeuralNetwork neural = new NeuralNetwork.NeuralNetwork(new int[] { 784, 16, 16, 10 });

        public MainWindow()
        {
            InitializeComponent();
        }

        public static int _currentImageNumber = 0;

        private void VohärigesBild(object sender, RoutedEventArgs e)
        {
            _currentImageNumber -= 1;
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _currentImageNumber += 1;
        }

        
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }


        private void TrainAi(object sender, RoutedEventArgs e)
        {
            double learningRate = 0.1;
            int Break = Convert.ToInt32(Samples.Text);
            int evolution = 1;

            for (int i = 0; i < evolution; i++)
            {
                foreach (var dataPoint in MnistReader.ReadTrainingData())
                {
                    DataPoint[] dataPoints = new DataPoint[] { dataPoint };
                    neural.Learn(dataPoints, learningRate);

                    if (Break <= 0)
                    {
                        break;
                    }
                    Break--;
                }
                Break = Convert.ToInt32(Samples.Text);
            }
        }
    }
}
