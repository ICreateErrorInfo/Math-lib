using System;

namespace Math_lib
{
    public class Matrix4x4 : Matrix
    {
        //Constructors
        public Matrix4x4() : base(4,4)
        {

        }
        private Matrix4x4(double[,] m) : base(m)
        {
            if (m.GetLength(0) != 4 || m.GetLength(1) != 4) {
                throw new ArgumentException("Das Array hat nicht die Größe 4x4");
            }
        }

        //Methods
        public static Matrix4x4 ScaleMarix(Vector3 v)
        {
            return new(new[,]
            {
                {v.X,   0 ,  0 ,0 },
                {  0, v.Y ,  0 ,0 },
                {  0,   0 ,v.Z ,0 },
                {  0,   0 ,  0 ,1 }
            });
        }
        public static Matrix4x4 Projection(int width, int height, int fov, double zNear, double zFar)
        {
            double aspectRatio = (double)height / width;
            double fovRad      = (double)1      / Math.Tan(fov * 0.5 / 180 * Math.PI);

            return new Matrix4x4(new double[,]
            {
                {Math.Round(aspectRatio * fovRad,3),  0                   ,  0                                             , 0},
                {  0                               , Math.Round(fovRad,3) ,  0                                             , 0},
                {  0                               ,  0                   , Math.Round(zFar / (zFar - zNear),3)            , 1},
                {  0                               ,  0                   , Math.Round((-zFar * zNear) / (zFar - zNear), 3), 0}                    
            });
        }
        public static Matrix4x4 RotateXMarix(int ai) 
        {
            double a = ToRad(ai);
            return new(new[,]
            {
                {  1,                          0,                           0, 0 },
                {  0, Math.Round(Math.Cos(a),3) , Math.Round(-Math.Sin(a),3) , 0 },
                {  0, Math.Round(Math.Sin(a),3) , Math.Round( Math.Cos(a),3) , 0 },
                {  0,                          0,                           0, 1 }
            });
        }
        public static Matrix4x4 RotateYMarix(int ai)
        {
            double a = ToRad(ai);
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a), 3) , 0 , Math.Round(Math.Sin(a), 3), 0 },
                {                            0, 1 ,                          0, 0 },
                { Math.Round(-Math.Sin(a), 3) , 0 , Math.Round(Math.Cos(a), 3), 0 },
                {                            0, 0 ,                          0, 0 }
            });
        }
        public static Matrix4x4 RotateZMarix(int ai)
        {
            double a = ToRad(ai);
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a),3), Math.Round(-Math.Sin(a),3), 0 , 0 },
                {  Math.Round(Math.Sin(a),3), Math.Round(Math.Cos(a),3) , 0 , 0 },
                {                          0,                          0, 1 , 0 },
                {                          0,                          0, 0 , 1 }
            });
        }
        public static double ToRad(double d) 
        {
            return d * Math.PI / 180;
        }

        //overrides *
        public static Point3 operator *(Matrix4x4 m, Point3 i)
        {

            double x = i.X * m[0, 0] + i.Y * m[1, 0] + i.Z * m[2, 0] + m[3, 0];
            double y = i.X * m[0, 1] + i.Y * m[1, 1] + i.Z * m[2, 1] + m[3, 1];
            double z = i.X * m[0, 2] + i.Y * m[1, 2] + i.Z * m[2, 2] + m[3, 2];

            Point3 o = new Point3(x, y, z);

            double w = i.X * m[0, 3] + i.Y * m[1, 3] + i.Z * m[2, 3] + m[3, 3];

            if (w != 0)
            {
                Point3 wp = new Point3(w, w, w);

                o /= wp;
            }

            return o;
        }
        public static Triangle3 operator *(Matrix4x4 m, Triangle3 i)
        {
            return new Triangle3(m * i.Points[0],
                                 m * i.Points[1],
                                 m * i.Points[2]);
        }
    }
}
