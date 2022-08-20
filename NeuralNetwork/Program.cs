using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace NeuralNetwork
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<List<DataPoint>> Batches = new List<List<DataPoint>>();
            NeuralNetwork network = new NeuralNetwork(new[] { 784, 16, 16, 10 });

            int miniBatchSize = 1;
            List<DataPoint> miniBatch = new List<DataPoint>();

            int counter = 0;
            foreach (var image in MnistReader.ReadTrainingData())
            {
                double[] expected = new double[10];
                expected[image.Label] = 1;

                miniBatch.Add(new DataPoint(image.Data, expected));

                counter++;
                if (counter == miniBatchSize)
                {
                    Batches.Add(miniBatch);
                    miniBatch = new List<DataPoint>();
                    counter = 0;
                }
            }

            double learningRate = 0.1;
            int evolutions = 5;

            for (int j = 0; j < evolutions; j++)
            {
                for (int i = 0; i < Batches.Count; i++)
                {
                    network.Learn(Batches[i].ToArray(), learningRate);
                }
                Console.WriteLine(j);
            }

            counter = 0;
            foreach (var image in MnistReader.ReadTestData())
            {
                Console.WriteLine();
                double[] output = network.CalculateOutputs(image.Data);

                foreach (double value in output)
                {
                    Console.WriteLine(Math.Round(value, 4));
                }
                Console.WriteLine();
                Console.WriteLine(FindIndexOfHighestValue(output));
                Console.WriteLine(image.Label);

                if(counter == 3)
                {
                    break;
                }

                counter++;
            }
        }

        public static int FindIndexOfHighestValue(double[] input)
        {
            double maxValue = input.Max();
            int maxIndex = input.ToList().IndexOf(maxValue);

            return maxIndex;
        }

    }
}
