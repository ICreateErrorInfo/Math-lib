using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
internal class SquareMatrixTests {

    [Test]
    public void TestCtor1() {
        SquareMatrix squareMatrix = new SquareMatrix(10);

        Assert.That(squareMatrix._size, Is.EqualTo(10));
        Assert.That(squareMatrix.IsIdentity, Is.True);
    }

    [Test]
    public void TestCtor2() {
        double[,] m = new double[9,9];

        SquareMatrix squareMatrix = new SquareMatrix(m);

        Assert.That(squareMatrix._size, Is.EqualTo(9));
    }

    [Test]
    public void TestIsIdentity() {
        double[,] m ={
            {1, 0, 0, 0 },
            {0, 1, 0, 0 },
            {0, 0, 1, 0 },
            {0, 0, 0, 1 },
        };

        SquareMatrix squareMatrix = new SquareMatrix(m);

        Assert.That(squareMatrix.IsIdentity, Is.True);

        double[,] m2 ={
            {1, 0, 0, 0 },
            {0, 1, 0, 0 },
            {0, 0, 1, 0 },
            {0, 0, 0, 1.1 },
        };

        SquareMatrix squareMatrix2 = new SquareMatrix(m2);

        Assert.That(squareMatrix2.IsIdentity, Is.False);
    }

    [Test]
    public void TestGetZeroMatrix() {
        var m = SquareMatrix.GetZeroMatrix(4);

        double[,] mexpect = new double[4,4];

        Assert.That(m._matrix, Is.EqualTo(mexpect));
    }

    [Test]
    public void TestDeterminant1x1() {
        var m = new SquareMatrix(new double[,]{
            { 12.65}
        });

        Assert.That(m.Determinant(), Is.EqualTo(12.65));
    }

    [Test]
    public void TestDeterminant2x2() {
        var m = new SquareMatrix(new double[,]{
            { 12, 7},
            {-2.1, 6.5 }
        });

        Assert.That(m.Determinant(), Is.EqualTo(92.7));
    }

    [Test]
    public void TestDeterminant3x3() {
        var m = new SquareMatrix(new double[,]{
            { 12, 4.1, 5.2},
            {0.56, -1.5, 17 },
            {-9, -1.8, 23 }
        });

        Assert.That(System.Math.Round(m.Determinant(), 2), Is.EqualTo(-802.35));
    }

    [Test]
    public void TestDeterminant4x4() {
        var m = new SquareMatrix(new double[,]{
            { 2, 1, 4, 5},
            {1, 7, 21, -12 },
            {6.5, -3.4, 1.1, 15.5 },
            {3.2, 2.2, -2.5, 1 }
        });

        Assert.That(System.Math.Round(m.Determinant(), 2), Is.EqualTo(-2713.3));
    }

    [Test]
    public void TestDeterminant5x5() {
        var m = new SquareMatrix(new double[,]{
            { 2, 1, 4, 5, 6},
            { 7, 8, 1, 3, 0 },
            { 9, 7, 1, 9, -1 },
            { -5, 1, 5, -7, 0.1 },
            { 0.7, 0.5, 1, 3, 1 }
        });

        Assert.That(System.Math.Round(m.Determinant(), 2), Is.EqualTo(-1474.41));
    }

    [Test]
    public void TestTranspose() {
        var m = new SquareMatrix(new double[,]{
            { 2, 1, 4, 5, 6},
            { 7, 8, 1, 3, 0 },
            { 9, 7, 1, 9, -1 },
            { -5, 1, 5, -7, 0.1 },
            { 0.7, 0.5, 1, 3, 1 }
        });

        var mExp = new SquareMatrix(new double[,]{
            { 2, 7, 9, -5, 0.7},
            { 1, 8, 7, 1, 0.5 },
            { 4, 1, 1, 5, 1 },
            { 5, 3, 9, -7, 3 },
            { 6, 0, -1, 0.1, 1 }
        });

        Assert.That(m.Transpose()._matrix, Is.EqualTo(mExp._matrix));
    }

    [Test]
    public void TestInverse3x3() {
        var m = new SquareMatrix(new double[,]{
            { 12, 4.1, 5.2},
            {0.56, -1.5, 17 },
            {-9, -1.8, 23 }
        });

        var mExp = new SquareMatrix(new double[,]{
            { 0.00486072, 0.129196, -0.0965913},
            {0.206743, -0.402318, 0.250624 },
            {0.0180819, 0.019069, 0.0252957 }
        });

        var mInv = m.Inverse().Value;

        var mul = m * mInv;

        var identity = new SquareMatrix(3);

        for (int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                Assert.That(System.Math.Round(mInv._matrix[i, j],5), Is.EqualTo(System.Math.Round(mExp[i, j], 5)));
                Assert.That(System.Math.Round(mul._matrix[i, j], 5), Is.EqualTo(identity[i, j]));
            }
        }
    }

    [Test]
    public void TestInverse4x4() {
        var m = new SquareMatrix(new double[,]{
            { 2, 1, 4, 5},
            {1, 7, 21, -12 },
            {6.5, -3.4, 1.1, 15.5 },
            {3.2, 2.2, -2.5, 1 }
        });

        var mExp = new SquareMatrix(new double[,]{
            { -0.366156, 0.0811742, 0.169941, 0.170788},
            {0.435702, -0.056481, -0.19316, 0.137692 },
            {0.0139903,0.0346442, 0.0271256, -0.0746692 },
            {0.24813, -0.0488888, -0.0510449, -0.0361184 }
        });

        var mInv = m.Inverse().Value;

        var mul = m * mInv;

        var identity = new SquareMatrix(4);

        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                Assert.That(System.Math.Round(mInv._matrix[i, j], 5), Is.EqualTo(System.Math.Round(mExp[i, j], 5)));
                Assert.That(System.Math.Round(mul._matrix[i, j], 5), Is.EqualTo(identity[i, j]));
            }
        }
    }

    [Test]
    public void TestInverse5x5() {
        var m = new SquareMatrix(new double[,]{
            { 2, 1, 4, 5, 6},
            { 7, 8, 1, 3, 0 },
            { 9, 7, 1, 9, -1 },
            { -5, 1, 5, -7, 0.1 },
            { 0.7, 0.5, 1, 3, 1 }
        });

        var mExp = new SquareMatrix(new double[,]{
            {0.282486, -0.297441, 0.390495, 0.0166168, -1.30608},
            {-0.224374, 0.412484, -0.362986, -0.0276043, 0.986022},
            {0.163842, -0.331339, 0.373546, 0.186108, -0.628116},
            {-0.115416, 0.0378524, -0.0677084, -0.0271973, 0.627505},
            {0.0968523, 0.219749, -0.262274, -0.102346, 0.166846}
        });

        var mInv = m.Inverse().Value;

        var mul = m * mInv;

        var identity = new SquareMatrix(5);

        for (int i = 0; i < 5; i++) {
            for (int j = 0; j < 5; j++) {
                Assert.That(System.Math.Round(mInv._matrix[i, j], 4), Is.EqualTo(System.Math.Round(mExp[i, j], 4)));
                Assert.That(System.Math.Round(mul._matrix[i, j], 4), Is.EqualTo(identity[i, j]));
            }
        }
    }

    [Test]
    public void TestPlusOp() {
        SquareMatrix m = new SquareMatrix(3);
        SquareMatrix m2 = new SquareMatrix(3);

        SquareMatrix mExp = new SquareMatrix(new double[,] {
            { 2, 0, 0},
            {0, 2, 0 },
            {0, 0, 2 }
        });

        Assert.That((m + m2)._matrix, Is.EqualTo(mExp._matrix));

    }

    [Test]
    public void TestScalarMulOp() {
        SquareMatrix m = new SquareMatrix(3);

        SquareMatrix mExp = new SquareMatrix(new double[,] {
            { 2, 0, 0},
            {0, 2, 0 },
            {0, 0, 2 }
        });

        Assert.That((m * 2)._matrix, Is.EqualTo(mExp._matrix));

    }

    [Test]
    public void TestScalarDivOp() {
        SquareMatrix m = new SquareMatrix(3);

        SquareMatrix mExp = new SquareMatrix(new double[,] {
            { 0.5, 0, 0},
            {0, 0.5, 0 },
            {0, 0, 0.5 }
        });

        Assert.That((m / 2)._matrix, Is.EqualTo(mExp._matrix));

    }
}
