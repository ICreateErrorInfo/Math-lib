using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moarx.Math.Tests {
    public static class BaseTestData {

        public static IEnumerable<object[]> AdditionData3D = new List<object[]>
        {
            new object[] { new double[] { 15, 15, 15 }, new double[] { 10, 5, 1 }, new double[] { 5, 10, 14 } },
            new object[] { new double[] {  0, -1, 15 }, new double[] { -10, -1, 1 }, new double[] { 10, 0, 14 } }
        };

        public static IEnumerable<object[]> SubtractionData3D = new List<object[]>
        {
            new object[] { new double[] { 0, 0, 0 }, new double[] { 15, 5, -10 }, new double[] { 15, 5, -10 } },
            new object[] { new double[] { -11, -1, 15 }, new double[] { -10, -1, 1 }, new double[] { 1, 0, -14 } }
        };

        public static IEnumerable<object[]> DivisionData3D = new List<object[]>
        {
            new object[] { new double[] { 0, 0, 0 }, new double[] { 0, 0, 0 }, new double[] { 1 }  },
            new object[] { new double[] { 3, 8, 0.25 }, new double[] { 12, 32, 1 }, new double[] { 4 } }
        };
    }
}
