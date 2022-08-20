using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNetwork
    {
        Layer[] _layers;

        public NeuralNetwork(params int[] layerSizes)
        {
            _layers = new Layer[layerSizes.Length - 1];
            for(int i = 0; i < _layers.Length; i++)
            {
                _layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
            }
        }

        public double[] CalculateOutputs(double[] inputs)
        {
            foreach (Layer layer in _layers)
            {
                inputs = layer.CalculateOutputs(inputs);
            }
            return inputs;
        }

        public void Learn(DataPoint[] trainingBatch, double learnRate)
        {
            foreach(DataPoint dataPoint in trainingBatch)
            {
                UpdateAllGradients(dataPoint);
            }

            ApplyAllGradients(learnRate / trainingBatch.Length);

            ClearAllGradients();
        }
        void ApplyAllGradients(double learnRate)
        {
            foreach (Layer layer in _layers)
            {
                layer.ApplyGradients(learnRate);
            }
        }
        void ClearAllGradients()
        {
            foreach(Layer layer in _layers)
            {
                layer.ClearGradients();
            }
        }
        void UpdateAllGradients(DataPoint dataPoint)
        {
            CalculateOutputs(dataPoint.inputs);

            Layer outputlayer = _layers[_layers.Length - 1];
            double[] nodevalues = outputlayer.CalculateOutputLayerNodeValues(dataPoint.expectedOutputs);
            outputlayer.UpdateGradients(nodevalues);

            for(int hiddenLayerIndex = _layers.Length - 2; hiddenLayerIndex >= 0; hiddenLayerIndex--)
            {
                Layer hiddenLayer = _layers[hiddenLayerIndex];
                nodevalues = hiddenLayer.CalculateHiddenLayerNodeValues(_layers[hiddenLayerIndex + 1], nodevalues);
                hiddenLayer.UpdateGradients(nodevalues);
            }
        }
        public double Cost(DataPoint dataPoint)
        {
            double[] outputs = CalculateOutputs(dataPoint.inputs);
            Layer outputLayer = _layers[_layers.Length - 1];
            double cost = 0;

            for(int nodeOut = 0; nodeOut < outputs.Length; nodeOut++)
            {
                cost += outputLayer.NodeCost(outputs[nodeOut], dataPoint.expectedOutputs[nodeOut]);
            }

            return cost;
        }
        public double Cost(DataPoint[] data)
        {
            double totalCost = 0;

            foreach(DataPoint dataPoint in data)
            {
                totalCost += Cost(dataPoint);
            }

            return totalCost / data.Length;
        }
    }
}
