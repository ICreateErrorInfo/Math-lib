using System;

namespace Math_lib
{
    public class Matrix
    {
        //Felder
        readonly double[,] _matrix;
        readonly int _row;
        readonly int _col;

        //Constructors
        public Matrix(int row, int col)
        {
            _matrix = new double[col, row];
            _row = row;
            _col = col;
        }
        public Matrix(Matrix m)
        {
            _matrix = m._matrix;
            _row = m._row;
            _col = m._col;
        }
        public Matrix(double[,] m)
        {
            _matrix = m;
            _row = m.GetLength(0);
            _col = m.GetLength(1);
        }

        public Matrix(double[] m)
        {
            _matrix = new double[0, m.Length];
            for(int i = 0; i < m.Length; i++)
            {
                _matrix[0, i] = m[i];
            }

            _row = m.Length;
            _col = 1;
        }

        public double this[int row, int column]
        {
            get => _matrix[row, column];
            set => _matrix[row, column] = value;
        }

        //methods
        public static Matrix IdentityMatrix(int i)
        {
            if(i <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i), "Muss größer als 0 sein");
            }

            Matrix m = new Matrix(i, i);
            
            for(int j = 0; j < m._row; j++)
            {
                for(int k = 0; k < m._col; k++)
                {
                    if(j == k)
                    {
                        m[j, k] = 1;
                    }
                    else
                    {
                        m[j, k] = 0;
                    }
                }
            }

            return m;
        }


        //overrides *
        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a._row, b._col);

            for (int i = 0; i < a._row; i++)
            {
                for (int j = 0; j < b._col; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < a._col; k++)
                    {
                        sum += a._matrix[i, k] * b._matrix[k, j];
                    }
                    c._matrix[i, j] = sum;
                }
            }

            return c;
        }

        //overrides +
        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a._row, b._col);

            for (int i = 0; i < a._row; i++)
            {
                for (int j = 0; j < b._col; j++)
                {
                    c._matrix[i, j] = a._matrix[i, j] + b._matrix[i, j];
                }
            }

            return c;
        }

        //overrides -
        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a._row, b._col);

            for (int i = 0; i < a._row; i++)
            {
                for (int j = 0; j < b._col; j++)
                {
                    c._matrix[i, j] = a._matrix[i, j] - b._matrix[i, j];
                }
            }

            return c;
        }

        //overides toString
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < _row; i++)
            {
                output = $"{output}[{_matrix[i, 0]}";

                for (int j = 1; j < _col; j++)
                {
                    output = $"{output}|{_matrix[i, j]}";
                }
                output = $"{output}] \r\n";
            }
            return output;
        }
    }
}
