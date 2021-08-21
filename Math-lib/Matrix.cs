using System;

namespace Math_lib
{
    public class Matrix
    {
        //Felder
        readonly double[,] _matrix;
        readonly int _row;
        readonly int _col;


        //Ctors
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


        //overrides
        public double this[int row, int column]
        {
            get => _matrix[row, column];
            set => _matrix[row, column] = value;
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
