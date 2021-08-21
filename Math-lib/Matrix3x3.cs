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
        private Matrix3x3(double[,] m) : base(m)
        {
            if (m.GetLength(0) != 3 || m.GetLength(1) != 3)
            {
                throw new ArgumentException("Das Array hat nicht die Größe 4x4");
            }
        }

        //Methods
        public static Matrix3x3 Identity()
        {
            return new Matrix3x3(new double[,]
            {
                {1,0,0 },
                {0,1,0 },
                {0,0,1 }
            });
        }
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
        public static Point3D operator *(Matrix3x3 m, Point3D i)
        {
            double x = i.X * m[0, 0] + i.Y * m[1, 0] + i.Z * m[2, 0];
            double y = i.X * m[0, 1] + i.Y * m[1, 1] + i.Z * m[2, 1];
            double z = i.X * m[0, 2] + i.Y * m[1, 2] + i.Z * m[2, 2];

            return new Point3D(x, y, z);
        }
        public static Vector3D operator *(Matrix3x3 m, Vector3D i)
        {
            double x = i.X * m[0, 0] + i.Y * m[1, 0] + i.Z * m[2, 0];
            double y = i.X * m[0, 1] + i.Y * m[1, 1] + i.Z * m[2, 1];
            double z = i.X * m[0, 2] + i.Y * m[1, 2] + i.Z * m[2, 2];

            return new Vector3D(x, y, z);
        }
        public static Vertex operator *(Matrix3x3 m, Vertex i)
        {
            double x = i.pos.X * m[0, 0] + i.pos.Y * m[1, 0] + i.pos.Z * m[2, 0];
            double y = i.pos.X * m[0, 1] + i.pos.Y * m[1, 1] + i.pos.Z * m[2, 1];
            double z = i.pos.X * m[0, 2] + i.pos.Y * m[1, 2] + i.pos.Z * m[2, 2];

            return new Vertex(new Point3D(x, y, z));
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
