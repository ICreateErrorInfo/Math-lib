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
            _matrix = new double[row, col];
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


        //Methods
        public void SetCells(Func<int, int, double> func)
        {
            for (int r = 0; r < _row; r++)
            {
                for (int c = 0; c < _col; c++)
                {
                    _matrix[r,c] = func(r, c);
                }
            }
        }
        public void Identity()
        {
            SetCells((row, col) =>
            {
                if(row == col)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });
        }
        public static Matrix ScaleMarix(Vector3D v)
        {
            return new(new[,]
            {
                {v.X,   0 ,  0 },
                {  0, v.Y ,  0 },
                {  0,   0 ,v.Z }
            });
        }
        public static Matrix RotateXMarix(double a)
        {
            return new(new[,]
            {
                {  1,                          0,                         0  },
                {  0, Math.Round(Math.Cos(a),3) , Math.Round(-Math.Sin(a),3) },
                {  0, Math.Round(Math.Sin(a),3) , Math.Round( Math.Cos(a),3) },
            });
        }
        public static Matrix RotateYMarix(double a)
        {
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a), 3) , 0 , Math.Round(Math.Sin(a), 3) },
                {                            0, 1 ,                         0  },
                { Math.Round(-Math.Sin(a), 3) , 0 , Math.Round(Math.Cos(a), 3) },
            });
        }
        public static Matrix RotateZMarix(double a)
        {
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a),3), Math.Round(-Math.Sin(a),3), 0 },
                {  Math.Round(Math.Sin(a),3), Math.Round(Math.Cos(a),3) , 0 },
                {                          0,                          0, 1 },
            });
        }
        public static Matrix Projection(int width, int height, int fov, double zNear, double zFar)
        {
            double aspectRatio = (double)height / width;
            double fovRad = 1 / Math.Tan(fov * 0.5 / 180 * Math.PI);

            return new Matrix(new[,]
            {
                {Math.Round(fovRad * aspectRatio, 3),  0                    ,  0                                               , 0},
                {  0                               ,  Math.Round(fovRad,3) ,  0                                               , 0},
                {  0                               ,  0                    , Math.Round(-((zFar + zNear) / (zFar - zNear)), 3), Math.Round(-((2*zFar*zNear) / (zFar - zNear)), 3)},
                {  0                               ,  0                    , -1                                               , 0}
            });                                                                        
        }
        public static Matrix PointAt(Point3D pos, Vector3D target, Vector3D up)
        {
            Vector3D newForward = target - pos;
            newForward = Vector3D.Normalize(newForward);

            Vector3D a = newForward * Vector3D.Dot(up, newForward);
            Vector3D newUp = up - a;
            newUp = Vector3D.Normalize(newUp);

            Vector3D newRight = Vector3D.Cross(newUp, newForward);

            return new Matrix(new[,]
            {
                {newRight.X   ,newRight.Y   , newRight.Z   , 0},
                {newUp.X      ,newUp.Y      , newUp.Z      , 0},
                {newForward.X ,newForward.Y , newForward.Z , 0},
                {pos.X        ,pos.Y        , pos.Z        , 1}
            });

        }
        public static Matrix LookAt(Point3D pos, Vector3D target, Vector3D up)
        {
            var zaxis = Vector3D.Normalize((target - pos));
            var xaxis = Vector3D.Normalize((Vector3D.Cross(up, zaxis)));
            var yaxis = Vector3D.Cross(zaxis, xaxis);

            double ta = -Vector3D.Dot(xaxis, (Vector3D)pos);
            double tb = -Vector3D.Dot(yaxis, (Vector3D)pos);
            double tc = -Vector3D.Dot(zaxis, (Vector3D)pos);

            return new Matrix(new[,]
            {
                { xaxis.X, yaxis.X, zaxis.X, 0},
                { xaxis.Y, yaxis.Y, zaxis.Y, 0},
                { xaxis.Z, yaxis.Z, zaxis.Z, 0},
                { ta     , tb     , tc     , 1}
            });
        }
        public static Matrix Translation(double x, double y, double z)
        {
            return new Matrix(new[,]
            {
                { 1,0,0,0 },
                { 0,1,0,0 },
                { 0,0,1,0 },
                { x,y,z,1 }
            });
        }
        public static Matrix Transpose4x4(Matrix m)
        {
            if(m._row == 4 && m._col == 4)
            {
                Matrix mOut = new Matrix(4, 4);

                mOut[0, 0] = m[0, 0];
                mOut[0, 1] = m[0, 1];
                mOut[0, 2] = m[0, 2];
                mOut[0, 3] = m[0, 3];
                mOut[1, 0] = m[1, 0];
                mOut[1, 1] = m[1, 1];
                mOut[1, 2] = m[1, 2];
                mOut[1, 3] = m[1, 3];
                mOut[2, 0] = m[2, 0];
                mOut[2, 1] = m[2, 1];
                mOut[2, 2] = m[2, 2];
                mOut[2, 3] = m[2, 3];
                mOut[3, 0] = m[3, 0];
                mOut[3, 1] = m[3, 1];
                mOut[3, 2] = m[3, 2];
                mOut[3, 3] = m[3, 3];

                return mOut;
            }
            else
            {
                throw new ArgumentException("The Matrix is not 4x4");
            }
        }
        public static Matrix Inverse4x4(Matrix m)
        {
            if (m._row == 4 && m._col == 4)
            {
                int[] indxc = new int[4];
                int[] indxr = new int[4];


                int[] ipiv = { 0, 0, 0, 0 };

                double[,] minv = new double[4, 4];

                for (int i = 0; i < 4; i++)
                {
                    int irow = 0, icol = 0;
                    double big = 0;
                    // Choose pivot
                    for (int j = 0; j < 4; j++)
                    {
                        if (ipiv[j] != 1)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                if (ipiv[k] == 0)
                                {
                                    if (Math.Abs(minv[j, k]) >= big)
                                    {
                                        big = Math.Abs(minv[j, k]);
                                        irow = j;
                                        icol = k;
                                    }
                                }
                                else if (ipiv[k] > 1)
                                    throw new Exception("Singular matrix in MatrixInvert");
                            }
                        }
                    }
                    ++ipiv[icol];
                    // Swap rows _irow_ and _icol_ for pivot
                    if (irow != icol)
                    {
                        for (int k = 0; k < 4; ++k) Mathe.Swap(ref minv[irow, k], ref minv[icol, k]);
                    }
                    indxr[i] = irow;
                    indxc[i] = icol;
                    if (minv[icol, icol] == 0) throw new Exception("Singular matrix in MatrixInvert");

                    // Set $m[icol][icol]$ to one by scaling row _icol_ appropriately
                    double pivinv = 1 / minv[icol, icol];
                    minv[icol, icol] = 1;
                    for (int j = 0; j < 4; j++) minv[icol, j] *= pivinv;

                    // Subtract this row from others to zero out their columns
                    for (int j = 0; j < 4; j++)
                    {
                        if (j != icol)
                        {
                            double save = minv[j, icol];
                            minv[j, icol] = 0;
                            for (int k = 0; k < 4; k++) minv[j, k] -= minv[icol, k] * save;
                        }
                    }
                }
                // Swap columns to reflect permutation
                for (int j = 3; j >= 0; j--)
                {
                    if (indxr[j] != indxc[j])
                    {
                        for (int k = 0; k < 4; k++)
                            Mathe.Swap(ref minv[k, indxr[j]], ref minv[k, indxc[j]]);
                    }
                }
                return new Matrix(minv);
            }
            else
            {
                throw new ArgumentException("The Matrix is not 4x4");
            }
        }


        //overrides
        public static Point3D operator *(Matrix m, Point3D p)
        {
            if (m._row == 4 && m._col == 4)
            {
                Matrix mOut = new Matrix(4, 1);

                mOut.SetCells((row, col) =>
                {
                    return p.X * m[row, 0] + p.Y * m[row, 1] + p.Z * m[row, 2] + m[row, 3];
                });

                return new Point3D(mOut[0, 0] / mOut[3, 0], mOut[1, 0] / mOut[3, 0], mOut[2, 0] / mOut[3, 0]);
            }
            else if(m._row == 3 && m._col == 3)
            {
                Matrix mOut = new Matrix(3, 1);

                mOut.SetCells((row, col) =>
                {
                    return p.X * m[row, 0] + p.Y * m[row, 1] + p.Z * m[row, 2];
                });

                return new Point3D(mOut[0, 0], mOut[1, 0], mOut[2, 0]);
            }
            else
            {
                throw new ArgumentException("The Matrix is not 4x4 or 3x3");
            }
        }
        public static Point3D operator *(Matrix m, Vector3D p)
        {
            if (m._row == 4 && m._col == 4)
            {
                Matrix mOut = new Matrix(4, 1);

                mOut.SetCells((row, col) =>
                {
                    return p.X * m[row, 0] + p.Y * m[row, 1] + p.Z * m[row, 2] + m[row, 3];
                });

                return new Point3D(mOut[0, 0] / mOut[3, 0], mOut[1, 0] / mOut[3, 0], mOut[2, 0] / mOut[3, 0]);
            }
            else if (m._row == 3 && m._col == 3)
            {
                Matrix mOut = new Matrix(3, 1);

                mOut.SetCells((row, col) =>
                {
                    return p.X * m[row, 0] + p.Y * m[row, 1] + p.Z * m[row, 2];
                });

                return new Point3D(mOut[0, 0], mOut[1, 0], mOut[2, 0]);
            }
            else
            {
                throw new ArgumentException("The Matrix is not 4x4 or 3x3");
            }
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix matrix = new Matrix(m1._row, m2._col);

            matrix.SetCells((row, col) =>
            {
                double ergebnis = 0;
                for (int i = 0; i < m1._col; i++)
                {
                    ergebnis += m1[row, i] * m2[i, col];
                }
                return ergebnis;
            });

            return matrix;
        }

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
