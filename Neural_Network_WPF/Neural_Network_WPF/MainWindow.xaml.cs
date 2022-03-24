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
        NeuralNet neural = new NeuralNet(new int[] { 784,16, 16, 16, 10 });

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

            foreach (var image in MnistReader.ReadTestData())
            {
                if (number == imageNumber)
                {
                    Bitmap Bmp = new Bitmap(28, 28);
                    for (int y = 0; y < 28; y++)
                    {
                        for (int x = 0; x < 28; x++)
                        {
                            Color col = Color.FromArgb(255, Convert.ToInt32(image.Data[y, x]), Convert.ToInt32(image.Data[y, x]), Convert.ToInt32(image.Data[y, x]));
                            Bmp.SetPixel(y, x, col);
                        }
                    }

                    Bmp.RotateFlip(RotateFlipType.Rotate90FlipX);
                    imageShow.Source = BitmapToImageSource(Bmp);
                    label.Content = "Correct: " + image.Label;

                    double[] imageIn1DimArray = new double[image.Data.Length];
                    imageIn1DimArray = Program.ConvertToOneD(image.Data);

                    neural.FeedForward(imageIn1DimArray);
                    label2.Content = "Ai: " + neural.GetBiggestNumber();

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
            double learningRate = 0.5;
            int Break = Convert.ToInt32(Samples.Text);

            int breakCounter = 0;
            foreach (var image in MnistReader.ReadTrainingData())
            {
                double[] imageIn1DimArray = new double[image.Data.Length];
                imageIn1DimArray = Program.ConvertToOneD(image.Data);

                double[] expected = new double[10];

                for (int i = 0; i < 10; i++)
                {
                    if (i != image.Label)
                    {
                        expected[i] = 0;
                    }
                    else
                    {
                        expected[i] = 1;
                    }
                }

                neural.Train(imageIn1DimArray, expected, learningRate);

                if (breakCounter == Break)
                {
                    break;
                }
                breakCounter++;
            }
        }

        private void ButtonLoad(object sender, RoutedEventArgs e)
        {
            neural.LoadFromFile(@"C:\Users\Moritz\source\repos\Math-lib\Neural_Network_WPF\Neural_Network_WPF\Saved\NeuralNetwork.txt");
        }

        private void ButtonSave(object sender, RoutedEventArgs e)
        {
            neural.SaveToFile(@"C:\Users\Moritz\source\repos\Math-lib\Neural_Network_WPF\Neural_Network_WPF\Saved\NeuralNetwork.txt");
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            neural = new NeuralNet(new int[] { 784, 16, 16, 10 });
        }
    }
}
