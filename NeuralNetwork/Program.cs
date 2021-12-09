using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NeuralNet neural = new NeuralNet(new int[] { 3, 4, 1 });

            double[] input = new double[] { 1, 2, 3 };
            double[] expectedOutput = new double[] { 0.3 };
            double learningRate = 0.5;

            for(int i = 0; i < 1000; i++)
            {
                neural.Learn(input, expectedOutput, learningRate);
            }

            neural.FeedForward(input);

            foreach(var element in neural.GetOutput())
            {
                Console.WriteLine(element);
            }

            //foreach (var image in MnistReader.ReadTrainingData())
            //{
            //    double[] imageIn1DimArray = new double[image.Data.Length];
            //    imageIn1DimArray = ConvertToOneD(image.Data);

            //    neural.FeedForward(imageIn1DimArray);

            //    double[] output = neural.GetOutput();

            //    Console.WriteLine();
            //    foreach (var percent in output)
            //    {
            //        Console.WriteLine(Math.Round(percent, 2).ToString());
            //    }
            //}

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
}
