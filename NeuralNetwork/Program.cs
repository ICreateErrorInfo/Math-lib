using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NeuralNet neural = new NeuralNet(new int[] { 3, 4, 1 });

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

            double[] expectedOutput = new double[] { 0.61249 };
            int L = neural._layers.Length - 1;
            double learningRate = 0.5;

            for (int j = 0; j < 10; j++)
            {
                neural.FeedForward(new double[] { 1, 0, 1 });
                double[] output = neural.GetOutput();
                foreach (var outNeuron in output)
                {
                    Console.WriteLine(outNeuron);
                }
                Console.WriteLine();

                double[][] error = new double[L + 1][];

                List<double> err = new List<double>();
                for (int i = 0; i < output.Length; i++)
                {
                    err.Add((output[i] - expectedOutput[i]) * neural.SigmoidPrime(output[i]));
                }
                error[L] = err.ToArray();


                for (int i = 1; i < L; i++)
                {
                    int l = L - i;
                    List<double> err2 = new List<double>();
                    for (int k = 0; k < neural._neurons[l].Length; k++)
                    {
                        double sumW = 0;
                        for (int u = 0; u < neural._neurons[l + 1].Length; u++)
                        {
                            sumW += neural._weights[i - 1][k][u] * error[l + 1][u];
                        }
                        err2.Add(sumW * neural.SigmoidPrime(neural._neurons[l][k]));
                    }
                    error[l] = err2.ToArray();
                }

                for (int i = 1; i <= L; i++)
                {
                    for (int k = 0; k < neural._neurons[i].Length; k++)
                    {
                        neural._biases[i][k] -= error[i][k] * learningRate;
                        for (int u = 0; u < neural._neurons[i - 1].Length; u++)
                        {
                            neural._weights[i - 1][k][u] -= (error[i][k] * neural._neurons[i - 1][u]) * learningRate;
                        }
                    }
                }

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
}
