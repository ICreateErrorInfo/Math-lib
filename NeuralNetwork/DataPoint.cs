using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class DataPoint
    {
        public double[] inputs;
        public double[] expectedOutputs;

        public DataPoint(double[] inputs, double[] expectedOutputs)
        {
            this.inputs = inputs;
            this.expectedOutputs = expectedOutputs;
        }
    }
}
