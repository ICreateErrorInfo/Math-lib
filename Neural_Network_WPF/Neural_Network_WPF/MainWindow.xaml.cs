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
            RenderImage(_currentImageNumber);
        }

        public static int _currentImageNumber = 0;

        private void VohärigesBild(object sender, RoutedEventArgs e)
        {
            _currentImageNumber -= 1;
            RenderImage(_currentImageNumber);
        }
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _currentImageNumber += 1;
            RenderImage(_currentImageNumber);
        }

        private void RenderImage(int imageNumber)
        {
            int number = 0;

            foreach (var dataPoint in MnistReader.ReadTestData())
            {
                if (number == imageNumber)
                {
                    Bitmap Bmp = new Bitmap(28, 28);
                    for (int y = 0; y < 28; y++)
                    {
                        for (int x = 0; x < 28; x++)
                        {
                            Color col = Color.FromArgb(255, Convert.ToInt32(dataPoint.inputs[(x + y * 28)] * 255), Convert.ToInt32(dataPoint.inputs[(x + y * 28)] * 255), Convert.ToInt32(dataPoint.inputs[(x + y * 28)] * 255));
                            Bmp.SetPixel(x, y, col);
                        }
                    }

                    Bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
                    imageShow.Source = BitmapToImageSource(Bmp);
                    label.Content = "Correct: " + dataPoint.label;

                    label2.Content = "Ai: " + Program.FindIndexOfHighestValue(neural.CalculateOutputs(dataPoint.inputs));

                    break;
                }
                number++;


            }
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
