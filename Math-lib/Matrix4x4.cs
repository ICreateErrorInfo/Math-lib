using System;

namespace Math_lib
{
    public class Matrix4x4 : Matrix
    {
        public Matrix4x4() : base(4,4)
        {

        }

        private Matrix4x4(double[,] m) : base(m)
        {
            if (m.GetLength(1) != 4 || m.GetLength(2) != 4) {
                throw new ArgumentException("Das Array hat nicht die Größe 4x4");
            }
        }

        public static Matrix4x4 ScaleMarix(Vector v)
        {
            return new(new[,]
            {
                {v.X,   0 ,  0 ,0 },
                {  0, v.Y ,  0 ,0 },
                {  0,   0 ,v.Z ,0 },
                {  0,   0 ,  0 ,1 }
            });
        }
    }
}
