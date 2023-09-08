using System.Numerics;


namespace Moarx.Math;
public readonly struct SquareMatrix{

    public readonly double[,] _matrix;
    public readonly int _size;

    public SquareMatrix(int size) {
        _matrix = new double[size, size];

        for(int i = 0; i < size; i++) {
            for(int j = 0; j < size; j++) {
                _matrix[i, j] = (i == j ) ? 1 : 0;
            }
        }
        _size = size;
    }
    public SquareMatrix(double[,] matrix) {
        _matrix = matrix;
        _size = matrix.GetLength(0);
    }

    public static SquareMatrix GetZeroMatrix(int size) {
        double[,] dm = new double[size, size];
        SquareMatrix m = new SquareMatrix(dm);
        return m;
    }

    public bool IsIdentity() {
        for (int i = 0; i < _size; i++) {
            for (int j = 0; j < _size; j++) {
                if(i == j) {
                    if (_matrix[i,j] != 1) {
                        return false;
                    }
                } else if (_matrix[i,j] != 0){
                    return false;
                }
            }
        }
        return true;
    }

    double Determinant1x1() {
        return _matrix[0, 0];
    }
    double Determinant2x2() {
        return MathmaticMethods.DifferenceOfProducts(_matrix[0, 0], _matrix[1, 1], _matrix[0, 1], _matrix[1, 0]);
    }
    double Determinant3x3() {
        double minor12 = MathmaticMethods.DifferenceOfProducts(_matrix[1,1], _matrix[2,2], _matrix[1,2], _matrix[2,1]);
        double minor02 = MathmaticMethods.DifferenceOfProducts(_matrix[1,0], _matrix[2,2], _matrix[1,2], _matrix[2,0]);
        double minor01 = MathmaticMethods.DifferenceOfProducts(_matrix[1,0], _matrix[2,1], _matrix[1,1], _matrix[2,0]);
        return MathmaticMethods.FMA(_matrix[0, 2], minor01, MathmaticMethods.DifferenceOfProducts(_matrix[0, 0], minor12, _matrix[0, 1], minor02));
    }
    double Determinant4x4() {
        double s0 = MathmaticMethods.DifferenceOfProducts(_matrix[0,0], _matrix[1,1], _matrix[1,0], _matrix[0,1]);
        double s1 = MathmaticMethods.DifferenceOfProducts(_matrix[0,0], _matrix[1,2], _matrix[1,0], _matrix[0,2]);
        double s2 = MathmaticMethods.DifferenceOfProducts(_matrix[0,0], _matrix[1,3], _matrix[1,0], _matrix[0,3]);
                                                          
        double s3 = MathmaticMethods.DifferenceOfProducts(_matrix[0,1], _matrix[1,2], _matrix[1,1], _matrix[0,2]);
        double s4 = MathmaticMethods.DifferenceOfProducts(_matrix[0,1], _matrix[1,3], _matrix[1,1], _matrix[0,3]);
        double s5 = MathmaticMethods.DifferenceOfProducts(_matrix[0,2], _matrix[1,3], _matrix[1,2], _matrix[0,3]);
                                                          
        double c0 = MathmaticMethods.DifferenceOfProducts(_matrix[2,0], _matrix[3,1], _matrix[3,0], _matrix[2,1]);
        double c1 = MathmaticMethods.DifferenceOfProducts(_matrix[2,0], _matrix[3,2], _matrix[3,0], _matrix[2,2]);
        double c2 = MathmaticMethods.DifferenceOfProducts(_matrix[2,0], _matrix[3,3], _matrix[3,0], _matrix[2,3]);
                                                         
        double c3 = MathmaticMethods.DifferenceOfProducts(_matrix[2,1], _matrix[3,2], _matrix[3,1], _matrix[2,2]);
        double c4 = MathmaticMethods.DifferenceOfProducts(_matrix[2,1], _matrix[3,3], _matrix[3,1], _matrix[2,3]);
        double c5 = MathmaticMethods.DifferenceOfProducts(_matrix[2,2], _matrix[3,3], _matrix[3,2], _matrix[2,3]);

        return (MathmaticMethods.DifferenceOfProducts(s0, c5, s1, c4) + MathmaticMethods.DifferenceOfProducts(s2, c3, -s3, c2) +
                MathmaticMethods.DifferenceOfProducts(s5, c0, s4, c1));
    }
    double DeterminantNxN() {
        double[,] sub = new double[_size - 1,_size - 1];
        double det = 0;

        for (int i = 0; i < _size; ++i) {
            for (int j = 0; j < _size - 1; ++j)
                for (int k = 0; k < _size - 1; ++k)
                    sub[j,k] = _matrix[j + 1,k < i ? k : k + 1];

            double sign = (i % 2 == 0) ? 1.0 : -1.0;
            det += sign * _matrix[0,i] * new SquareMatrix(sub).Determinant();
        }

        return det;
    }
    public double Determinant() {
        switch(_size) {
            case 1: 
                return Determinant1x1();
            case 2:
                return Determinant2x2();
            case 3:
                return Determinant3x3();
            case 4:
                return Determinant4x4();
            default: 
                return DeterminantNxN();
        };
    }

    public SquareMatrix Transpose() {
        SquareMatrix m = new SquareMatrix(_size);
        for (int i = 0; i < _size; i++) {
            for (int j = 0; j < _size; j++) {
                m._matrix[i, j] = _matrix[j,i];
            }
        }
        return m;
    }

    SquareMatrix? Inverse3x3() {
        double det = Determinant3x3();
        if(det == 0) {
            return null;
        }
        double invDet = 1 / det;

        double[,] matrix = new double[3,3];

        matrix[0,0] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[1,1], _matrix[2,2], _matrix[1,2], _matrix[2,1]);
        matrix[1,0] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[1,2], _matrix[2,0], _matrix[1,0], _matrix[2,2]);
        matrix[2,0] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[1,0], _matrix[2,1], _matrix[1,1], _matrix[2,0]);
        matrix[0,1] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[0,2], _matrix[2,1], _matrix[0,1], _matrix[2,2]);
        matrix[1,1] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[0,0], _matrix[2,2], _matrix[0,2], _matrix[2,0]);
        matrix[2,1] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[0,1], _matrix[2,0], _matrix[0,0], _matrix[2,1]);
        matrix[0,2] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[0,1], _matrix[1,2], _matrix[0,2], _matrix[1,1]);
        matrix[1,2] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[0,2], _matrix[1,0], _matrix[0,0], _matrix[1,2]);
        matrix[2,2] = invDet * MathmaticMethods.DifferenceOfProducts(_matrix[0,0], _matrix[1,1], _matrix[0,1], _matrix[1,0]);

        SquareMatrix r = new SquareMatrix(matrix);

        return r;
    }
    SquareMatrix? Inverse4x4() {
        double s0 = MathmaticMethods.DifferenceOfProducts(_matrix[0,0], _matrix[1,1], _matrix[1,0], _matrix[0,1]);
        double s1 = MathmaticMethods.DifferenceOfProducts(_matrix[0,0], _matrix[1,2], _matrix[1,0], _matrix[0,2]);
        double s2 = MathmaticMethods.DifferenceOfProducts(_matrix[0,0], _matrix[1,3], _matrix[1,0], _matrix[0,3]);

        double s3 = MathmaticMethods.DifferenceOfProducts(_matrix[0,1], _matrix[1,2], _matrix[1,1], _matrix[0,2]);
        double s4 = MathmaticMethods.DifferenceOfProducts(_matrix[0,1], _matrix[1,3], _matrix[1,1], _matrix[0,3]);
        double s5 = MathmaticMethods.DifferenceOfProducts(_matrix[0,2], _matrix[1,3], _matrix[1,2], _matrix[0,3]);

        double c0 = MathmaticMethods.DifferenceOfProducts(_matrix[2,0], _matrix[3,1], _matrix[3,0], _matrix[2,1]);
        double c1 = MathmaticMethods.DifferenceOfProducts(_matrix[2,0], _matrix[3,2], _matrix[3,0], _matrix[2,2]);
        double c2 = MathmaticMethods.DifferenceOfProducts(_matrix[2,0], _matrix[3,3], _matrix[3,0], _matrix[2,3]);

        double c3 = MathmaticMethods.DifferenceOfProducts(_matrix[2,1], _matrix[3,2], _matrix[3,1], _matrix[2,2]);
        double c4 = MathmaticMethods.DifferenceOfProducts(_matrix[2,1], _matrix[3,3], _matrix[3,1], _matrix[2,3]);
        double c5 = MathmaticMethods.DifferenceOfProducts(_matrix[2,2], _matrix[3,3], _matrix[3,2], _matrix[2,3]);

        double determinant = MathmaticMethods.InnerProduct(s0, c5, -s1, c4, s2, c3, s3, c2, s5, c0, -s4, c1);
        if (determinant == 0)
            return null;
        double s = 1/determinant;

        double[,] inv = { {s * MathmaticMethods.InnerProduct( _matrix[1,1], c5, _matrix[1,3], c3, -_matrix[1,2], c4),
                           s * MathmaticMethods.InnerProduct(-_matrix[0,1], c5, _matrix[0,2], c4, -_matrix[0,3], c3),
                           s * MathmaticMethods.InnerProduct( _matrix[3,1], s5, _matrix[3,3], s3, -_matrix[3,2], s4),
                           s * MathmaticMethods.InnerProduct(-_matrix[2,1], s5, _matrix[2,2], s4, -_matrix[2,3], s3)},

                          {s * MathmaticMethods.InnerProduct(-_matrix[1,0], c5, _matrix[1,2], c2, -_matrix[1,3], c1),
                           s * MathmaticMethods.InnerProduct( _matrix[0,0], c5, _matrix[0,3], c1, -_matrix[0,2], c2),
                           s * MathmaticMethods.InnerProduct(-_matrix[3,0], s5, _matrix[3,2], s2, -_matrix[3,3], s1),
                           s * MathmaticMethods.InnerProduct( _matrix[2,0], s5, _matrix[2,3], s1, -_matrix[2,2], s2)},

                          {s * MathmaticMethods.InnerProduct( _matrix[1,0], c4, _matrix[1,3], c0, -_matrix[1,1], c2),
                           s * MathmaticMethods.InnerProduct(-_matrix[0,0], c4, _matrix[0,1], c2, -_matrix[0,3], c0),
                           s * MathmaticMethods.InnerProduct( _matrix[3,0], s4, _matrix[3,3], s0, -_matrix[3,1], s2),
                           s * MathmaticMethods.InnerProduct(-_matrix[2,0], s4, _matrix[2,1], s2, -_matrix[2,3], s0)},

                          {s * MathmaticMethods.InnerProduct(-_matrix[1,0], c3, _matrix[1,1], c1, -_matrix[1,2], c0),
                           s * MathmaticMethods.InnerProduct( _matrix[0,0], c3, _matrix[0,2], c0, -_matrix[0,1], c1),
                           s * MathmaticMethods.InnerProduct(-_matrix[3,0], s3, _matrix[3,1], s1, -_matrix[3,2], s0),
                           s * MathmaticMethods.InnerProduct( _matrix[2,0], s3, _matrix[2,2], s0, -_matrix[2,1], s1)}
        };

        return new SquareMatrix(inv);
    }
    SquareMatrix? InverseNxN() {
        int N = _size;

        int[] indxc = new int[N];
        int[] indxr = new int[N];
        int[] ipiv = new int[N];

        double[,] minv = new double[N,N] ;

        for (int i = 0; i < N; ++i)
            for (int j = 0; j < N; ++j)
                minv[i,j] = _matrix[i,j];
        for (int i = 0; i < N; i++) {
            int irow = 0, icol = 0;
            double big = 0;
            // Choose pivot
            for (int j = 0; j < N; j++) {
                if (ipiv[j] != 1) {
                    for (int k = 0; k < N; k++) {
                        if (ipiv[k] == 0) {
                            if (System.Math.Abs(minv[j,k]) >= big) {
                                big = System.Math.Abs(minv[j,k]);
                                irow = j;
                                icol = k;
                            }
                        } else if (ipiv[k] > 1)
                            return null;  // singular
                    }
                }
            }
            ++ipiv[icol];
            // Swap rows _irow_ and _icol_ for pivot
            if (irow != icol) {
                for (int k = 0; k < N; ++k)
                    MathmaticMethods.Swap(ref minv[irow,k], ref minv[icol,k]);
            }
            indxr[i] = irow;
            indxc[i] = icol;
            if (minv[icol,icol] == 0)
                return null;  // singular

            // Set $m[icol][icol]$ to one by scaling row _icol_ appropriately
            double pivinv = 1 / minv[icol,icol];
            minv[icol,icol] = 1;
            for (int j = 0; j < N; j++)
                minv[icol,j] *= pivinv;

            // Subtract this row from others to zero out their columns
            for (int j = 0; j < N; j++) {
                if (j != icol) {
                    double save = minv[j,icol];
                    minv[j,icol] = 0;
                    for (int k = 0; k < N; k++)
                        minv[j,k] = MathmaticMethods.FMA(-minv[icol,k], save, minv[j,k]);
                }
            }
        }
        // Swap columns to reflect permutation
        for (int j = N - 1; j >= 0; j--) {
            if (indxr[j] != indxc[j]) {
                for (int k = 0; k < N; k++)
                    MathmaticMethods.Swap(ref minv[k,indxr[j]], ref minv[k,indxc[j]]);
            }
        }

        return new SquareMatrix(minv);
    }
    public SquareMatrix? Inverse() {
        switch (_size) {
            case 3:
                return Inverse3x3();
            case 4:
                return Inverse4x4();
            default:
                return InverseNxN();
        }
    }

    public T[] Mul<T>(T[] v) where T : INumber<T> {
        T[] result = new T[_size];
        for (int i = 0; i < _size; ++i) {
            result[i] = T.CreateChecked(0);
            for (int j = 0; j < _size; ++j)
                result[i] += (T)Convert.ChangeType(_matrix[i, j], typeof(T)) * v[j];
        }
        return result;
    }
    SquareMatrix Mul3x3(SquareMatrix m2) {
        double[,] r = new double[3,3];
        for (int i = 0; i < 3; ++i)
            for (int j = 0; j < 3; ++j)
                r[i, j] = MathmaticMethods.InnerProduct(_matrix[i, 0], m2._matrix[0, j], _matrix[i, 1], m2._matrix[1, j], _matrix[i, 2], m2._matrix[2, j]);
        return new(r);
    }
    SquareMatrix Mul4x4(SquareMatrix m2) {
        double[,] r = new double[4,4];
        for (int i = 0; i < 4; ++i)
            for (int j = 0; j < 4; ++j)
                r[i,j] = MathmaticMethods.InnerProduct(_matrix[i,0], m2._matrix[0,j], _matrix[i,1], m2._matrix[1,j], _matrix[i,2],
                                                       m2._matrix[2,j], _matrix[i,3], m2._matrix[3,j]);
        return new(r);
    }
    SquareMatrix MulNxN(SquareMatrix m2) {
        double[,] r = new double[_size, _size];
        for (int i = 0; i < _size; ++i)
            for (int j = 0; j < _size; ++j) {
                r[i,j] = 0;
                for (int k = 0; k < _size; ++k)
                    r[i,j] = MathmaticMethods.FMA(_matrix[i,k], m2._matrix[k,j], r[i,j]);
            }
        return new(r);
    }

    public static SquareMatrix operator +(SquareMatrix m, SquareMatrix m2) {
        for(int i = 0; i < m._size; i++) {
            for(int j = 0; j < m._size; j++) {
                m._matrix[i, j] += m2._matrix[i, j];
            }
        }
        return m;
    }
    public static SquareMatrix operator *(SquareMatrix m, double s) {
        for (int i = 0; i < m._size; i++) {
            for (int j = 0; j < m._size; j++) {
                m._matrix[i, j] *= s;
            }
        }
        return m;
    }
    public static SquareMatrix operator *(double s, SquareMatrix m) {
        return m * s;
    }
    public static SquareMatrix operator *(SquareMatrix m, SquareMatrix m2) {
        switch (m._size) {
            case 3:
                return m.Mul3x3(m2);
            case 4:
                return m.Mul4x4(m2);
            default: 
                return m.MulNxN(m2);
        }
    }
    public static SquareMatrix operator /(SquareMatrix m, double s) {
        for (int i = 0; i < m._size; i++) {
            for (int j = 0; j < m._size; j++) {
                m._matrix[i, j] /= s;
            }
        }
        return m;
    }

    public static bool operator ==(SquareMatrix m1, SquareMatrix m2) {
        for (int i = 0; i < m1._size; ++i)
            for (int j = 0; j < m1._size; ++j)
                if (m1._matrix[i,j] != m2._matrix[i,j])
                    return false;
        return true;
    }
    public static bool operator !=(SquareMatrix m1, SquareMatrix m2) {
        for (int i = 0; i < m1._size; ++i)
            for (int j = 0; j < m1._size; ++j)
                if (m1._matrix[i, j] != m2._matrix[i, j])
                    return true;
        return false;
    }

    public double this[int i, int j] {
        get {
            return _matrix[i, j];
        }
    }
}
