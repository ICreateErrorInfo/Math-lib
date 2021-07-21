namespace Math_lib
{
    public class Matrix4x4 : Matrix
    {
        public Matrix4x4(double[,] m) : base(4,4)
        {

        }

        public static Matrix4x4 ScaleMarix(Vector v)
        {
            return new Matrix4x4(new double[,]
            {
                {v.X,   0 ,  0 ,0 },
                {  0, v.Y ,  0 ,0 },
                {  0,   0 ,v.Z ,0 },
                {  0,   0 ,  0 ,1 }
            });
        }
    }
}
