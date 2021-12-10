using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNet
    {
        public int[] _layers;
        public double[][] _neurons;
        public double[][] _biases;
        public double[][][] _weights;

        public NeuralNet(int[] layers)
        {
            _layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                _layers[i] = layers[i];
            }
            InitNeurons();
            InitBiases();
            InitWeights();
        }

        private void InitNeurons()
        {
            List<double[]> neuronsList = new List<double[]>();
            for (int i = 0; i < _layers.Length; i++)
            {
                neuronsList.Add(new double[_layers[i]]);
            }
            _neurons = neuronsList.ToArray();
        }
        private void InitBiases()
        {
            Random random = new Random();

            List<double[]> biasList = new List<double[]>();
            for (int i = 0; i < _layers.Length; i++)
            {
                double[] bias = new double[_layers[i]];
                for (int j = 0; j < _layers[i]; j++)
                {
                    bias[j] = random.NextDouble() - 0.5;
                }
                biasList.Add(bias);
            }
            _biases = biasList.ToArray();
        }
        private void InitWeights()
        {
            Random random = new Random();

            List<double[][]> weightsList = new List<double[][]>();
            for (int i = 1; i < _layers.Length; i++)
            {
                List<double[]> layerWeightsList = new List<double[]>();
                int neuronsInPreviousLayer = _layers[i - 1];
                for (int j = 0; j < _neurons[i].Length; j++)
                {
                    double[] neuronWeights = new double[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                    {
                        neuronWeights[k] = random.NextDouble() - 0.5;
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            _weights = weightsList.ToArray();
        }
        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
        public double SigmoidPrime(double x)
        {
            return x * (1 - x);
        }

        public double[] FeedForward(double[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                _neurons[0][i] = inputs[i];
            }

            for (int i = 1; i < _layers.Length; i++)
            {
                for (int j = 0; j < _neurons[i].Length; j++)
                {
                    double value = 0;
                    for (int k = 0; k < _neurons[i - 1].Length; k++)
                    {
                        value += _weights[i - 1][j][k] * _neurons[i - 1][k];
                    }
                    _neurons[i][j] = Sigmoid(value + _biases[i][j]);
                }
            }

            return _neurons[_neurons.Length - 1];
        }
        public void Train(double[] input, double[] expected, double learningRate)
        {
            int L = _layers.Length - 1;

            FeedForward(input);
            double[] output = GetOutput();

            double[][] error = new double[L + 1][];

            List<double> err = new List<double>();
            for (int i = 0; i < output.Length; i++)
            {
                err.Add((output[i] - expected[i]) * SigmoidPrime(output[i]));
            }
            error[L] = err.ToArray();


            for (int i = 0; i < L; i++)
            {
                int l = L - i;
                List<double> err2 = new List<double>();
                for (int k = 0; k < _neurons[l - 1].Length; k++)
                {
                    double sumW = 0;
                    for (int u = 0; u < _neurons[l].Length; u++)
                    {
                        sumW += _weights[l - 1][u][k] * error[l][u];
                    }
                    err2.Add(sumW * SigmoidPrime(_neurons[l - 1][k]));
                }
                error[l - 1] = err2.ToArray();
            }

            for (int i = 1; i <= L; i++)
            {
                for (int k = 0; k < _neurons[i].Length; k++)
                {
                    _biases[i][k] -= error[i][k] * learningRate;
                    for (int u = 0; u < _neurons[i - 1].Length; u++)
                    {
                        _weights[i - 1][k][u] -= (error[i][k] * _neurons[i - 1][u]) * learningRate;
                    }
                }
            }
        }
        public void SaveToFile(string path)
        {
            List<string> lines = new List<string>();

            string layer = "l: ";
            foreach(var neurons in _layers)
            {
                layer += neurons + " ";
            }
            lines.Add(layer);

            for(int l = 0; l < _biases.Length; l++)
            {
                for(int n = 0; n < _biases[l].Length; n++)
                {
                    lines.Add("b: " + _biases[l][n]);
                }
            }

            foreach(double[][] l in _weights)
            {
                foreach(double[] neu in l)
                {
                    foreach(double preNeu in neu)
                    {
                        lines.Add("w: " + preNeu);
                    }
                }
            }

            string[] output = lines.ToArray();

            File.WriteAllLines(path, output);
        }
        public void LoadFromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);

            List<int> layers = new List<int>();

            List<double> biasPerLayer = new List<double>();
            List<double[]> biases = new List<double[]>();

            List<double> weightsPreLayer = new List<double>();
            List<double[]> weightsLayer = new List<double[]>();
            List<double[][]> weights = new List<double[][]>();

            int neuronCounter = 0;
            int layerCounter = 0;
            int preNeuronCounter = 0;
            foreach (var line in lines)
            {
                string[] splitted = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if(splitted[0] == "l:")
                {
                    for(int i = 1; i < splitted.Length; i++)
                    {
                        layers.Add(Convert.ToInt32(splitted[i]));
                    }
                }else if(splitted[0] == "b:")
                {
                    if(neuronCounter == layers[layerCounter])
                    {
                        neuronCounter = 0;
                        layerCounter++;
                        biases.Add(biasPerLayer.ToArray());
                        biasPerLayer.Clear();
                    }
                    biasPerLayer.Add(Convert.ToDouble(splitted[1]));
                    neuronCounter++;
                }else if(splitted[0] == "w:")
                {
                    if(weightsPreLayer.Count == 0)
                    {
                        neuronCounter = 0;
                        layerCounter = 1;
                    } // Reset

                    if (preNeuronCounter == layers[layerCounter - 1])
                    {
                        neuronCounter++;
                        weightsLayer.Add(weightsPreLayer.ToArray());
                        if (neuronCounter == layers[layerCounter])
                        {
                            neuronCounter = 0;
                            layerCounter++;
                            weights.Add(weightsLayer.ToArray());
                            weightsLayer.Clear();
                        }
                        weightsPreLayer.Clear();
                        preNeuronCounter = 0;
                    }

                    preNeuronCounter++;
                    weightsPreLayer.Add(Convert.ToDouble(splitted[1]));
                }
            }

            if(biasPerLayer.Count > 0)
            {
                biases.Add(biasPerLayer.ToArray());
            }
            if(weightsPreLayer.Count > 0)
            {
                weightsLayer.Add(weightsPreLayer.ToArray());
                weights.Add(weightsLayer.ToArray());
            }

            _layers = layers.ToArray();
            _biases = biases.ToArray();
            _weights = weights.ToArray();
        }
        public double[] GetOutput()
        {
            return _neurons[_neurons.Length - 1];
        }
        public int GetBiggestNumber()
        {
            int zahl = 0;
            double biggest = 0;
            int biggestNumber = 0;
            foreach (var element in GetOutput())
            {
                Console.WriteLine(zahl + ": " + Math.Round(element, 2).ToString());

                if (element > biggest)
                {
                    biggest = element;
                    biggestNumber = zahl;
                }
                zahl++;
            }

            return biggestNumber;
        }
    }
}
