using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace NeuralNetwork
{
    public static class MnistReader
    {
        private const string TrainImages = @"C:\Users\Moritz\source\repos\Math-lib\NeuralNetwork\Database\train-images.idx3-ubyte";
        private const string TrainLabels = @"C:\Users\Moritz\source\repos\Math-lib\NeuralNetwork\Database\train-labels.idx1-ubyte";
        private const string TestImages = @"C:\Users\Moritz\source\repos\Math-lib\NeuralNetwork\Database\t10k-images.idx3-ubyte";
        private const string TestLabels = @"C:\Users\Moritz\source\repos\Math-lib\NeuralNetwork\Database\t10k-labels.idx1-ubyte";

        public static IEnumerable<DataPoint> ReadTrainingData()
        {
            foreach (var item in Read(TrainImages, TrainLabels))
            {
                yield return item;
            }
        }

        public static IEnumerable<DataPoint> ReadTestData()
        {
            foreach (var item in Read(TestImages, TestLabels))
            {
                yield return item;
            }
        }

        private static IEnumerable<DataPoint> Read(string imagesPath, string labelsPath)
        {
            using BinaryReader labels = new BinaryReader(new FileStream(labelsPath, FileMode.Open));
            using BinaryReader images = new BinaryReader(new FileStream(imagesPath, FileMode.Open));

            int magicNumber = images.ReadBigInt32();
            int numberOfImages = images.ReadBigInt32();
            int width = images.ReadBigInt32();
            int height = images.ReadBigInt32();

            int magicLabel = labels.ReadBigInt32();
            int numberOfLabels = labels.ReadBigInt32();

            for (int i = 0; i < numberOfImages; i++)
            {
                var bytes = images.ReadBytes(width * height);
                var arr = new float[height, width];

                arr.ForEach((j, k) => arr[j, k] = bytes[j * height + k]);

                int label = labels.ReadByte();

                double[] expected = new double[10];
                expected[label] = 1;

                yield return new DataPoint()
                {
                    inputs = ConvertToOneD(arr),
                    expectedOutputs = expected,
                    label = label
                };
            }
        }
        public static double[] ConvertToOneD(float[,] input)
        {
            double[] my1DArray = new double[28 * 28];

            for (int y = 0; y < input.GetLength(1); y++)
            {
                for (int x = 0; input.GetLength(0) > x; x++)
                {
                    my1DArray[y * input.GetLength(0) + x] = input[x, y] / 255;
                }
            }
            return my1DArray;
        }
    }
    public static class Extensions
    {
        public static int ReadBigInt32(this BinaryReader br)
        {
            var bytes = br.ReadBytes(sizeof(Int32));
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static void ForEach<T>(this T[,] source, Action<int, int> action)
        {
            for (int w = 0; w < source.GetLength(0); w++)
            {
                for (int h = 0; h < source.GetLength(1); h++)
                {
                    action(w, h);
                }
            }
        }
    }
}
