using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NeuralNet neural = new NeuralNet(new int[] { 784, 16, 16, 10 });
            double learningRate = 0.5;
            double Break = 10000;

            int breakCounter = 0;
            foreach (var image in MnistReader.ReadTrainingData())
            {
                double[] imageIn1DimArray = new double[image.Data.Length];
                imageIn1DimArray = ConvertToOneD(image.Data);

                double[] expected = new double[10];

                for(int i = 0; i< 10; i++)
                {
                    if(i != image.Label)
                    {
                        expected[i] = 0;
                    }
                    else
                    {
                        expected[i] = 1;
                    }
                }

                neural.Learn(imageIn1DimArray, expected, learningRate);

                //double[] output = neural.GetOutput();

                //Console.WriteLine();
                //int j = 0;
                //foreach (var percent in output)
                //{
                //    Console.WriteLine(j + ": " + Math.Round(percent, 2).ToString());
                //    j++;
                //}
                //Console.WriteLine("Right: " + image.Label);

                if(breakCounter == Break)
                {
                    break;
                }
                breakCounter++;
            }

            Console.WriteLine("Trained with: " + Break + " Samples");
            Console.WriteLine();

            int counter = 0;
            foreach (var image in MnistReader.ReadTestData())
            {
                double[] imageIn1DimArray = new double[image.Data.Length];
                imageIn1DimArray = ConvertToOneD(image.Data);

                neural.FeedForward(imageIn1DimArray);

                int zahl = 0;
                double biggest = 0;
                int biggestNumber = 0;
                foreach (var element in neural.GetOutput())
                {
                    Console.WriteLine(zahl + ": " + Math.Round(element, 2).ToString());

                    if (element > biggest)
                    {
                        biggest = element;
                        biggestNumber = zahl;
                    }
                    zahl++;
                }

                Console.WriteLine("Correct: " + image.Label);
                Console.WriteLine("Ai: " + biggestNumber);
                Console.WriteLine();

                if (counter == 10)
                {
                    break;
                }
                counter++;
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
