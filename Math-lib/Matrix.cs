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

            Identity();
        }
        public Matrix(double[,] m)
        {
            _matrix = m;
            _row = m.GetLength(0);
            _col = m.GetLength(1);
        }
        public Matrix(double[] m)
        {
            _matrix = new double[1, m.Length];

            for(int i = 0; i < m.Length; i++)
            {
                _matrix[0, i] = m[i];
            }

            _row = m.Length;
            _col = 1;
        }
        public Matrix(double t00, double t01, double t02, double t03, double t10,
                         double t11, double t12, double t13, double t20, double t21,
                         double t22, double t23, double t30, double t31, double t32,
                         double t33)
        {
            double[,] m = new double[4, 4];

            m[0, 0] = t00;
            m[0, 1] = t01;
            m[0, 2] = t02;
            m[0, 3] = t03;
            m[1, 0] = t10;
            m[1, 1] = t11;
            m[1, 2] = t12;
            m[1, 3] = t13;
            m[2, 0] = t20;
            m[2, 1] = t21;
            m[2, 2] = t22;
            m[2, 3] = t23;
            m[3, 0] = t30;
            m[3, 1] = t31;
            m[3, 2] = t32;
            m[3, 3] = t33;

            _matrix = m;
            _row = 4;
            _col = 4;
        }


        //Methods
        public void Identity()
        {
            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    _matrix[i, j] = 0;
                }
            }

            for (int i = 0; i < Math.Min(_row, _col); i++)
            {
                _matrix[i, i] = 1;
            }
        }

        //overrides
        public double this[int row, int column]
        {
            get => _matrix[row, column];
            set => _matrix[row, column] = value;
        }
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
