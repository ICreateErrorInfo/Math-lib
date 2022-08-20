using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Layer
    {
        int _numNodesIn, _numNodesOut;
        public double[,] costGradientW;
        public double[]  costGradientB;
        public double[] inputs;
        public double[] weightedInputs;
        public double[] activations;
        public double[,] weights;
        public double[] biases;

        public Layer(int numNodesIn, int numNodesOut)
        {
            _numNodesIn = numNodesIn;
            _numNodesOut = numNodesOut;

            inputs = new double[numNodesIn];

            weightedInputs = new double[numNodesOut];
            activations = new double[numNodesOut];

            weights = new double[numNodesIn, numNodesOut];
            costGradientW = new double[numNodesIn, numNodesOut];

            biases = new double[numNodesOut];
            costGradientB = new double[numNodesOut];

            InitializeRandomWeights();
        }

        public void InitializeRandomWeights()
        {
            Random rng = new Random();

            for (int nodeIn = 0; nodeIn < _numNodesIn; nodeIn++)
            {
                for (int nodeOut = 0; nodeOut < _numNodesOut; nodeOut++)
                {
                    double randomValue = rng.NextDouble() * 2 - 1;
                    weights[nodeIn, nodeOut] = randomValue / Math.Sqrt(_numNodesIn);
                }
            }
        }

        public double[] CalculateOutputs(double[] inputs)
        {
            double[] activations = new double[_numNodesOut];

            for(int nodeOut = 0; nodeOut < _numNodesOut; nodeOut++)
            {
                double weightedInput = biases[nodeOut];
                for(int nodeIn = 0; nodeIn < _numNodesIn; nodeIn++)
                {
                    weightedInput += inputs[nodeIn] * weights[nodeIn, nodeOut];
                }
                weightedInputs[nodeOut] = weightedInput;
                activations[nodeOut] = ActivationFunction(weightedInput);
            }

            this.activations = activations;
            this.inputs = inputs;
            return activations;
        }

        double ActivationFunction(double weightedInput)
        {
            return 1 / (1 + Math.Exp(-weightedInput));
        }
        double ActivationFunctionDerivative(double weightedInput)
        {
            double activation = ActivationFunction(weightedInput);
            return activation * (1 - activation);
        }

        public void ApplyGradients(double learnRate)
        {
            for(int nodeOut = 0; nodeOut < _numNodesOut; nodeOut++)
            {
                biases[nodeOut] -= costGradientB[nodeOut] * learnRate;
                for (int nodeIn = 0; nodeIn < _numNodesIn; nodeIn++)
                {
                    weights[nodeIn, nodeOut] -= costGradientW[nodeIn, nodeOut] * learnRate;
                }
            }
        }
        public void ClearGradients()
        {
            costGradientB = new double[_numNodesOut];
            costGradientW = new double[_numNodesIn, _numNodesOut];
        }

        public double NodeCost(double outputActivation, double expectedOutput)
        {
            double error = outputActivation - expectedOutput;
            return error * error;
        }
        double NodeCostDerivative(double outputActivation, double expectedOutput)
        {
            return 2 * (outputActivation - expectedOutput);
        }

        public double[] CalculateOutputLayerNodeValues(double[] expectedOutputs)
        {
            double[] nodeValues = new double[expectedOutputs.Length];

            for(int i = 0; i < nodeValues.Length; i++)
            {
                double costDerivative = NodeCostDerivative(activations[i], expectedOutputs[i]);
                double activationDerivative = ActivationFunctionDerivative(weightedInputs[i]);
                nodeValues[i] = activationDerivative * costDerivative;
            }

            return nodeValues;
        }
        public void UpdateGradients(double[] nodeValues)
        {
            for(int nodeOut = 0; nodeOut < _numNodesOut; nodeOut++)
            {
                for(int nodeIn = 0; nodeIn < _numNodesIn; nodeIn++)
                {
                    double derivativeCostWrtWeight = inputs[nodeIn] * nodeValues[nodeOut];
                    costGradientW[nodeIn, nodeOut] += derivativeCostWrtWeight;
                }

                double derivativeCostWrtBias = 1 * nodeValues[nodeOut];
                costGradientB[nodeOut] += derivativeCostWrtBias;
            }
        }
        public double[] CalculateHiddenLayerNodeValues(Layer oldLayer, double[] oldNodeValues)
        {
            double[] newNodeValues = new double[_numNodesOut];

            for(int newNodeIndex = 0; newNodeIndex < newNodeValues.Length; newNodeIndex++)
            {
                double newNodeValue = 0;
                for(int oldNodeIndex = 0; oldNodeIndex < oldNodeValues.Length; oldNodeIndex++)
                {
                    double weightedInputDerivative = oldLayer.weights[newNodeIndex, oldNodeIndex];
                    newNodeValue += weightedInputDerivative * oldNodeValues[oldNodeIndex];
                }
                newNodeValue *= ActivationFunctionDerivative(weightedInputs[newNodeIndex]);
                newNodeValues[newNodeIndex] = newNodeValue;
            }

            return newNodeValues;
        }
    }
}
