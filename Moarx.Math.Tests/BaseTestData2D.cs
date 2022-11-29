namespace Moarx.Math.Tests {
    public static class BaseTestData2D {

        public static IEnumerable<object[]> AdditionData = new List<object[]>
        {
            new object[] { new double[] { 15, 15 }, new double[] { 10, 5 }, new double[] { 5, 10 } },
            new object[] { new double[] {  0, -1 }, new double[] { -10, -1 }, new double[] { 10, 0 } }
        };
        public static IEnumerable<object[]> SubtractionData = new List<object[]>
        {
            new object[] { new double[] { 0, 0 }, new double[] { 15, 5 }, new double[] { 15, 5 } },
            new object[] { new double[] { -11, -1 }, new double[] { -10, -1 }, new double[] { 1, 0 } }
        };
        public static IEnumerable<object[]> DivisionData = new List<object[]>
        {
            new object[] { new double[] { 0, 0 }, new double[] { 0, 0 }, new double[] { 1 }  },
            new object[] { new double[] { 3, 8 }, new double[] { 12, 32 }, new double[] { 4 } }
        };
        public static IEnumerable<object[]> NegationData = new List<object[]>
        {
            new object[] { new double[] { -1, -67 }, new double[] { 1, 67 }},
            new object[] { new double[] { 3, -8 }, new double[] { -3, 8 }}
        };
        public static IEnumerable<object[]> MultiplicationData = new List<object[]>
        {
            new object[] { new double[] { 0, 0 }, new double[] { 0, 0 }, new double[] { 1 }  },
            new object[] { new double[] { 48, -40 }, new double[] { 12, -10 }, new double[] { 4 } }
        };
        public static IEnumerable<object[]> AccessOperatorData = new List<object[]>
        {
            new object[] { new double[] { 1 }, new double[] { 0, 1 }, new double[] { 1 }  },
            new object[] { new double[] { -10 }, new double[] { 48, -10 }, new double[] { 1} },
            new object[] { new double[] { 48 }, new double[] { 48, -0 }, new double[] { 0} }
        };
        public static IEnumerable<object[]> DotProductData = new List<object[]>
        {
            new object[] { new double[] { 125}, new double[] { 15, 5 }, new double[] { 5, 10 } },
            new object[] { new double[] { -10 }, new double[] { -10, -1 }, new double[] { 1, 0 } },
            new object[] { new double[] { 0 }, new double[] { -10, -1 }, new double[] { 0, 0 } }
        };


        public static IEnumerable<object[]> CastData = new List<object[]>
        {
            new object[] { new double[] { 0, 0 }, new double[] { 0, 0 } },
            new object[] { new double[] { 50, -10 }, new double[] { 50, -10 } }
        };
    }
}
