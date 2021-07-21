using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public class Matrix
    {
        //Felder
        readonly double[,] matrix;
        readonly int row;
        readonly int col;

        //Constructors
        public Matrix(int row, int col)
        {
            matrix = new double[col, row];
            this.row = row;
            this.col = col;
        }
        public Matrix(Matrix m)
        {
            matrix = m.matrix;
            row = m.row;
            col = m.col;
        }
        public Matrix(double[,] m)
        {
            matrix = m;
            row = m.GetLength(0);
            col = m.GetLength(1);
        }
        public Matrix(double[] m)
        {
            matrix = new double[0, m.Length];
            for(int i = 0; i < m.Length; i++)
            {
                matrix[0, i] = m[i];
            }
        }

        //methods
        public static Matrix IdentityMatrix(int i)
        {
            if(i <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(i), "Muss größer als 0 sein");
            }

            Matrix m = new Matrix(i, i);
            
            for(int j = 0; j < m.row; j++)
            {
                for(int k = 0; k < m.col; k++)
                {
                    if(j == k)
                    {
                        m.matrix[j, k] = 1;
                    }
                    else
                    {
                        m.matrix[j, k] = 0;
                    }
                }
            }

            return m;
        }


        //overrides *
        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.row, b.col);

            for (int i = 0; i < a.row; i++)
            {
                for (int j = 0; j < b.col; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < a.col; k++)
                    {
                        sum += a.matrix[i, k] * b.matrix[k, j];
                    }
                    c.matrix[i, j] = sum;
                }
            }

            return c;
        }

        //overrides +
        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.row, b.col);

            for (int i = 0; i < a.row; i++)
            {
                for (int j = 0; j < b.col; j++)
                {
                    c.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
                }
            }

            return c;
        }

        //overrides -
        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix c = new Matrix(a.row, b.col);

            for (int i = 0; i < a.row; i++)
            {
                for (int j = 0; j < b.col; j++)
                {
                    c.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
                }
            }

            return c;
        }

        //overides toString
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < row; i++)
            {
                output = $"{output}[{matrix[i, 0]}";

                for (int j = 1; j < col; j++)
                {
                    output = $"{output}|{matrix[i, j]}";
                }
                output = $"{output}] \r\n";
            }
            return output;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (obj is not Matrix other)
            {
                return false;
            }

            return this == other;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
