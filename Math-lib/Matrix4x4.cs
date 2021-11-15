using System;

namespace Math_lib
{
    public class Matrix4x4 : Matrix
    {
        //Ctors
        public Matrix4x4() : base(4, 4)
        {

        }
        public Matrix4x4(double[,] m) : base(m)
        {
            if (m.GetLength(0) != 4 || m.GetLength(1) != 4)
            {
                throw new ArgumentException("Das Array hat nicht die Größe 4x4");
            }
        }
        public Matrix4x4(double t00, double t01, double t02, double t03, double t10,
                         double t11, double t12, double t13, double t20, double t21,
                         double t22, double t23, double t30, double t31, double t32,
                         double t33) : base(4,4)
        {
            double[,] m = new double[4, 4];

            m[0,0] = t00;
            m[0,1] = t01;
            m[0,2] = t02;
            m[0,3] = t03;
            m[1,0] = t10;
            m[1,1] = t11;
            m[1,2] = t12;
            m[1,3] = t13;
            m[2,0] = t20;
            m[2,1] = t21;
            m[2,2] = t22;
            m[2,3] = t23;
            m[3,0] = t30;
            m[3,1] = t31;
            m[3,2] = t32;
            m[3,3] = t33;
        }


        //Methods
        public static Matrix4x4 Identity()
        {
            return new Matrix4x4(new double[,]
            {
                {1,0,0,0 },
                {0,1,0,0 },
                {0,0,1,0 },
                {0,0,0,1 }
            });
        }
        public static Matrix4x4 Projection(int width, int height, int fov, double zNear, double zFar)
        {
            double aspectRatio = (double)height / width;
            double fovRad = 1 / Math.Tan(fov * 0.5 / 180 * Math.PI);

            return new Matrix4x4(new[,]
            {
                {Math.Round(aspectRatio * fovRad,3),  0                   ,  0                                             , 0},
                {  0                               , Math.Round(fovRad,3) ,  0                                             , 0},
                {  0                               ,  0                   , Math.Round(zFar / (zFar - zNear),3)            , 1},
                {  0                               ,  0                   , Math.Round(-zNear * zFar / (zFar - zNear), 3)  , 0}
            });
        }
        public static Matrix4x4 PointAt(Point3D pos, Vector3D target, Vector3D up)
        {
            Vector3D newForward = target - pos;
            newForward = Vector3D.Normalize(newForward);

            Vector3D a = newForward * Vector3D.Dot(up, newForward);
            Vector3D newUp = up - a;
            newUp = Vector3D.Normalize(newUp);

            Vector3D newRight = Vector3D.Cross(newUp, newForward);

            return new Matrix4x4(new[,]
            {
                {newRight.X   ,newRight.Y   , newRight.Z   , 0},
                {newUp.X      ,newUp.Y      , newUp.Z      , 0},
                {newForward.X ,newForward.Y , newForward.Z , 0},
                {pos.X        ,pos.Y        , pos.Z        , 1}
            });

        }
        public static Matrix4x4 LookAt(Point3D pos, Vector3D target, Vector3D up)
        {
            var zaxis = Vector3D.Normalize((target - pos));
            var xaxis = Vector3D.Normalize((Vector3D.Cross(up, zaxis)));
            var yaxis = Vector3D.Cross(zaxis, xaxis);

            double ta = -Vector3D.Dot(xaxis, (Vector3D)pos);
            double tb = -Vector3D.Dot(yaxis, (Vector3D)pos);
            double tc = -Vector3D.Dot(zaxis, (Vector3D)pos);

            return new Matrix4x4(new[,]
            {
                { xaxis.X, yaxis.X, zaxis.X, 0},
                { xaxis.Y, yaxis.Y, zaxis.Y, 0},
                { xaxis.Z, yaxis.Z, zaxis.Z, 0},
                { ta     , tb     , tc     , 1}
            });
        }
        public static Matrix4x4 Translation(double x, double y, double z)
        {
            return new Matrix4x4(new[,]
            {
                { 1,0,0,0 },
                { 0,1,0,0 },
                { 0,0,1,0 },
                { x,y,z,1 }
            });
        }
        public static Matrix4x4 RotateXMarix(double a)
        {
            return new(new[,]
            {
                {  1,                          0,                         0 , 0 },
                {  0, Math.Round(Math.Cos(a),3) , Math.Round(-Math.Sin(a),3), 0 },
                {  0, Math.Round(Math.Sin(a),3) , Math.Round( Math.Cos(a),3), 0 },
                {  0,                          0,                         0 , 1 }              
            });
        }
        public static Matrix4x4 RotateYMarix(double a)
        {
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a), 3) , 0 , Math.Round(Math.Sin(a), 3) },
                {                            0, 1 ,                         0  },
                { Math.Round(-Math.Sin(a), 3) , 0 , Math.Round(Math.Cos(a), 3) },
                {                            0, 0 ,                         1  }
            });
        }
        public static Matrix4x4 RotateZMarix(double a)
        {
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a),3), Math.Round(-Math.Sin(a),3), 0, 0 },
                {  Math.Round(Math.Sin(a),3), Math.Round(Math.Cos(a),3) , 0, 0 },
                {                          0,                          0, 1, 0 },
                {                          0,                          0, 0, 1 }
            });
        }
        public static Matrix4x4 Transpose(Matrix4x4 m)
        {
            return new Matrix4x4(m[0,0], m[1,0], m[2,0], m[3,0], m[0,1],
                                 m[1,1], m[2,1], m[3,1], m[0,2], m[1,2],
                                 m[2,2], m[3,2], m[0,3], m[1,3], m[2,3],
                                 m[3,3]);
        }
        public static Matrix4x4 Inverse(Matrix4x4 m)
        {
            int[] indxc = new int[4];
            int[] indxr = new int[4];


            int[] ipiv = { 0, 0, 0, 0 };

            double[,] minv = new double[4,4];

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
                                if (Math.Abs(minv[j,k]) >= big)
                                {
                                    big = Math.Abs(minv[j,k]);
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
                    for (int k = 0; k < 4; ++k) Math1.Swap(ref minv[irow, k], ref minv[icol, k]);
                }
                indxr[i] = irow;
                indxc[i] = icol;
                if (minv[icol,icol] == 0) throw new Exception("Singular matrix in MatrixInvert");

                // Set $m[icol][icol]$ to one by scaling row _icol_ appropriately
                double pivinv = 1 / minv[icol,icol];
                minv[icol,icol] = 1;
                for (int j = 0; j < 4; j++) minv[icol,j] *= pivinv;

                // Subtract this row from others to zero out their columns
                for (int j = 0; j < 4; j++)
                {
                    if (j != icol)
                    {
                        double save = minv[j,icol];
                        minv[j,icol] = 0;
                        for (int k = 0; k < 4; k++) minv[j,k] -= minv[icol,k] * save;
                    }
                }
            }
            // Swap columns to reflect permutation
            for (int j = 3; j >= 0; j--)
            {
                if (indxr[j] != indxc[j])
                {
                    for (int k = 0; k < 4; k++)
                        Math1.Swap(ref minv[k,indxr[j]], ref minv[k,indxc[j]]);
                }
            }
            return new Matrix4x4(minv);
        }


        //overrides *
        public static Point3D operator *(Matrix4x4 m, Point3D i)
        {
            double x = i.X * m[0, 0] + i.Y * m[1, 0] + i.Z * m[2, 0] + m[3, 0];
            double y = i.X * m[0, 1] + i.Y * m[1, 1] + i.Z * m[2, 1] + m[3, 1];
            double z = i.X * m[0, 2] + i.Y * m[1, 2] + i.Z * m[2, 2] + m[3, 2];
            double w = i.X * m[0, 3] + i.Y * m[1, 3] + i.Z * m[2, 3] + m[3, 3];

            return new Point3D(x, y, z) / w;
        }
        public static Vector3D operator *(Matrix4x4 m, Vector3D i)
        {
            double x = i.X * m[0, 0] + i.Y * m[1, 0] + i.Z * m[2, 0] + m[3, 0];
            double y = i.X * m[0, 1] + i.Y * m[1, 1] + i.Z * m[2, 1] + m[3, 1];
            double z = i.X * m[0, 2] + i.Y * m[1, 2] + i.Z * m[2, 2] + m[3, 2];
            double w = i.X * m[0, 3] + i.Y * m[1, 3] + i.Z * m[2, 3] + m[3, 3];

            return new Vector3D(x, y, z) / w;
        }
        public static Matrix4x4 operator *(Matrix4x4 m1, Matrix4x4 m2)
        {
            Matrix4x4 matrix = new Matrix4x4();
            for (int c = 0; c < 4; c++)
            {
                for (int r = 0; r < 4; r++)
                {
                    matrix[r, c] = m1[r, 0] * m2[0, c] + m1[r, 1] * m2[1, c] + m1[r, 2] * m2[2, c] + m1[r, 3] * m2[3, c];
                }
            }
            return matrix;
        }

        //overrides ==
        public static bool operator ==(Matrix4x4 m, Matrix4x4 m1)
        {
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                    if (m[i,j] != m1[i,j]) return false;
            return true;
        }

        //overrides !=
        public static bool operator !=(Matrix4x4 m, Matrix4x4 m1)
        {
            return !(m == m1);
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

            throw new NotImplementedException();
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
