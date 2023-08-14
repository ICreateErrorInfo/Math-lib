using System;
using System.Numerics;
using System.Windows.Media;

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
            Vector3D newForward = target - pos.ToVector();
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
        public static Matrix LookAt(Point3D pos, Point3D target, Vector3D up)
        {
            var zaxis = Vector3D.Normalize(target - pos);
            var xaxis = Vector3D.Normalize(Vector3D.Cross(up, zaxis));
            var yaxis = Vector3D.Cross(zaxis, xaxis);

            double ta = -Vector3D.Dot(xaxis, pos.ToVector());
            double tb = -Vector3D.Dot(yaxis, pos.ToVector());
            double tc = -Vector3D.Dot(zaxis, pos.ToVector());

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
            Matrix4x4 matrix = new Matrix4x4((float)m[0,0], (float)m[0,1], (float)m[0,2], (float)m[0,3],
                                             (float)m[1,0], (float)m[1,1], (float)m[1,2], (float)m[1,3],
                                             (float)m[2,0], (float)m[2,1], (float)m[2,2], (float)m[2,3],
                                             (float)m[3,0], (float)m[3,1], (float)m[3,2], (float)m[3,3]);

            Matrix4x4 inverse = new Matrix4x4();
            Matrix inverseMatrix = new Matrix(4,4);

            if(Matrix4x4.Invert(matrix, out inverse)) {
                for(int i = 0; i < 4; i++) {
                    for(int j = 0; j < 4; j++) {
                        inverseMatrix[i, j] = inverse[i, j];
                    }
                }

                return inverseMatrix;
            }

            throw new Exception();
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
        public static Vector3D operator *(Matrix m, Vector3D p)
        {
            if (m._row == 4 && m._col == 4)
            {
                Matrix mOut = new Matrix(4, 1);

                mOut.SetCells((row, col) =>
                {
                    return p.X * m[row, 0] + p.Y * m[row, 1] + p.Z * m[row, 2] + m[row, 3];
                });

                return new Vector3D(mOut[0, 0] / mOut[3, 0], mOut[1, 0] / mOut[3, 0], mOut[2, 0] / mOut[3, 0]);
            }
            else if (m._row == 3 && m._col == 3)
            {
                Matrix mOut = new Matrix(3, 1);

                mOut.SetCells((row, col) =>
                {
                    return p.X * m[row, 0] + p.Y * m[row, 1] + p.Z * m[row, 2];
                });

                return new Vector3D(mOut[0, 0], mOut[1, 0], mOut[2, 0]);
            }
            else
            {
                throw new ArgumentException("The Matrix is not 4x4 or 3x3");
            }
        }
        public static Normal3D operator *(Matrix m, Normal3D n)
        {
            if (m._row == 4 && m._col == 4)
            {
                Matrix mOut = new Matrix(4, 1);

                mOut.SetCells((row, col) =>
                {
                    return n.X * m[row, 0] + n.Y * m[row, 1] + n.Z * m[row, 2] + m[row, 3];
                });

                return new Normal3D(mOut[0, 0] / mOut[3, 0], mOut[1, 0] / mOut[3, 0], mOut[2, 0] / mOut[3, 0]);
            }
            else if (m._row == 3 && m._col == 3)
            {
                Matrix mOut = new Matrix(3, 1);

                mOut.SetCells((row, col) =>
                {
                    return n.X * m[row, 0] + n.Y * m[row, 1] + n.Z * m[row, 2];
                });

                return new Normal3D(mOut[0, 0], mOut[1, 0], mOut[2, 0]);
            }
            else
            {
                throw new ArgumentException("The Matrix is not 4x4 or 3x3");
            }
        }
        public static Ray operator *(Matrix m, Ray r)
        {
            Ray r1 = new Ray(m * r.O, r.D, r.TMax, r.Time);

            return r1;
        }
        public static Bounds3D operator *(Matrix m, Bounds3D b)
        {
            return new Bounds3D(m * b.pMin, m * b.pMax);
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
