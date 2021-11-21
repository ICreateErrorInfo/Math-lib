using System;

namespace Math_lib
{
    // ReSharper disable once InconsistentNaming
    public class Matrix3x3 : Matrix
    {
        //Ctors
        public Matrix3x3() : base(3, 3)
        {

        }
        public Matrix3x3(double[,] m) : base(m)
        {
            if (m.GetLength(0) != 3 || m.GetLength(1) != 3)
            {
                throw new ArgumentException("Das Array hat nicht die Größe 3x3");
            }
        }


        //Methods
        public static Matrix3x3 ScaleMarix(Vector3D v)
        {
            return new(new[,]
            {
                {v.X,   0 ,  0 },
                {  0, v.Y ,  0 },
                {  0,   0 ,v.Z }
            });
        }
        public static Matrix3x3 RotateXMarix(double a)
        {
            return new(new[,]
            {
                {  1,                          0,                         0  },
                {  0, Math.Round(Math.Cos(a),3) , Math.Round(-Math.Sin(a),3) },
                {  0, Math.Round(Math.Sin(a),3) , Math.Round( Math.Cos(a),3) },
            });
        }
        public static Matrix3x3 RotateYMarix(double a)
        {
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a), 3) , 0 , Math.Round(Math.Sin(a), 3) },
                {                            0, 1 ,                         0  },
                { Math.Round(-Math.Sin(a), 3) , 0 , Math.Round(Math.Cos(a), 3) }, 
            });
        }
        public static Matrix3x3 RotateZMarix(double a)
        {
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a),3), Math.Round(-Math.Sin(a),3), 0 },
                {  Math.Round(Math.Sin(a),3), Math.Round(Math.Cos(a),3) , 0 },
                {                          0,                          0, 1 },
            });
        }      


        //overrides *
        public static Point3D operator *(Matrix3x3 m, Point3D p)
        {
            double x = p.X * m[0, 0] + p.Y * m[0, 1] + p.Z * m[0, 2];
            double y = p.X * m[1, 0] + p.Y * m[1, 1] + p.Z * m[1, 2];
            double z = p.X * m[2, 0] + p.Y * m[2, 1] + p.Z * m[2, 2];

            return new Point3D(x, y, z);
        }
        public static Vector3D operator *(Matrix3x3 m, Vector3D v)
        {
            double x = v.X * m[0, 0] + v.Y * m[0, 1] + v.Z * m[0, 2];
            double y = v.X * m[1, 0] + v.Y * m[1, 1] + v.Z * m[1, 2];
            double z = v.X * m[2, 0] + v.Y * m[2, 1] + v.Z * m[2, 2];

            return new Vector3D(x, y, z);
        }
        public static Matrix3x3 operator *(Matrix3x3 m1, Matrix3x3 m2)
        {
            Matrix3x3 matrix = new Matrix3x3();
            for(int c = 0; c < 3; c++)
            {
                for(int r = 0; r < 3; r++)
                {
                    matrix[r, c] = m1[r, 0] * m2[0, c] + m1[r, 1] * m2[1, c] + m1[r, 2] * m2[2, c];
                }
            }
            return matrix;
        }
    }
}
