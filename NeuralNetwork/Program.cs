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
            List<DataPoint> TestBatch = new List<DataPoint>();

            List<List<DataPoint>> Batches = new List<List<DataPoint>>();
            NeuralNetwork network = new NeuralNetwork(new[] { 784, 16, 16, 10 });

            int miniBatchSize = 1;
            List<DataPoint> miniBatch = new List<DataPoint>();

            int counter = 0;
            foreach (var dataPoint in MnistReader.ReadTrainingData())
            {
                miniBatch.Add(dataPoint);

                counter++;
                if (counter == miniBatchSize)
                {
                    Batches.Add(miniBatch);
                    miniBatch = new List<DataPoint>();
                    counter = 0;
                }
                TestBatch.Add(dataPoint);
            }

            double learningRate = 0.1;
            int evolutions = 6;

            for (int j = 0; j < evolutions; j++)
            {
                for (int i = 0; i < Batches.Count; i++)
                {
                    network.Learn(Batches[i].ToArray(), learningRate);
                }
                Console.WriteLine(j);
                Console.WriteLine(network.Cost(TestBatch.ToArray()));
            }

            counter = 0;
            foreach (var dataPoint in MnistReader.ReadTestData())
            {
                Console.WriteLine();
                double[] output = network.CalculateOutputs(dataPoint.inputs);

                foreach (double value in output)
                {
                    Console.WriteLine(Math.Round(value, 4));
                }
                Console.WriteLine();
                Console.WriteLine(FindIndexOfHighestValue(output));
                Console.WriteLine(dataPoint.label);

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
