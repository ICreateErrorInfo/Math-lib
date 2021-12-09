using System;
using System.Collections.Generic;
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
            for(int i = 0; i < _layers.Length; i++)
            {
                neuronsList.Add(new double[_layers[i]]);
            }
            _neurons = neuronsList.ToArray();
        }
        private void InitBiases()
        {
            Random random = new Random();

            List<double[]> biasList = new List<double[]>();
            for(int i = 0; i < _layers.Length; i++)
            {
                double[] bias = new double[_layers[i]];
                for(int j = 0; j < _layers[i]; j++)
                {
                    bias[j] = random.NextDouble()-0.5;
                }
                biasList.Add(bias);
            }
            _biases = biasList.ToArray();
        }
        private void InitWeights()
        {
            Random random = new Random();

            List<double[][]> weightsList = new List<double[][]>();
            for(int i = 1; i < _layers.Length; i++)
            {
                List<double[]> layerWeightsList = new List<double[]>();
                int neuronsInPreviousLayer = _layers[i - 1];
                for(int j = 0; j < _neurons[i].Length; j++)
                {
                    double[] neuronWeights = new double[neuronsInPreviousLayer];
                    for(int k = 0; k < neuronsInPreviousLayer; k++)
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
            return 1 / (1+Math.Exp(-x));
        }
        public double SigmoidPrime(double x)
        {
            return x * (1 - x);
        }

        public double[] FeedForward(double[] inputs)
        {
            for(int i = 0; i < inputs.Length; i++)
            {
                _neurons[0][i] = inputs[i];
            }

            for(int i = 1; i < _layers.Length; i++)
            {
                for(int j = 0; j < _neurons[i].Length; j++)
                {
                    double value = 0;
                    for(int k = 0; k < _neurons[i-1].Length; k++)
                    {
                        value += _weights[i - 1][j][k] * _neurons[i - 1][k];
                    }
                    _neurons[i][j] = Sigmoid(value + _biases[i][j]);
                }
            }

            return _neurons[_neurons.Length - 1];
        }
        public double[] GetOutput()
        {
            return _neurons[_neurons.Length - 1];
        }
    }
}
