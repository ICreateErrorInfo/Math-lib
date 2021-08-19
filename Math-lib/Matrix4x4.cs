using System;

namespace Math_lib
{
    // ReSharper disable once InconsistentNaming
    public class Matrix4x4 : Matrix
    {
        //Constructors
        public Matrix4x4() : base(4, 4)
        {

        }
        private Matrix4x4(double[,] m) : base(m)
        {
            if (m.GetLength(0) != 4 || m.GetLength(1) != 4)
            {
                throw new ArgumentException("Das Array hat nicht die Größe 4x4");
            }
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
        public static Matrix4x4 ScaleMarix(Vector3D v)
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
            Vector3D newForward = new(target - pos);
            newForward = Vector3D.UnitVector(newForward);

            Vector3D a = newForward * Vector3D.Dot(up, newForward);
            Vector3D newUp = up - a;
            newUp = Vector3D.UnitVector(newUp);

            Vector3D newRight = Vector3D.Cross(newUp, newForward);

            return new Matrix4x4(new[,]
            {
                {newRight.X   ,newRight.Y   , newRight.Z   , 0},
                {newUp.X      ,newUp.Y      , newUp.Z      , 0},
                {newForward.X ,newForward.Y , newForward.Z , 0},
                {pos.X        ,pos.Y        , pos.Z        , 1}
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
                {  1,                          0,                           0, 0 },
                {  0, Math.Round(Math.Cos(a),3) , Math.Round(-Math.Sin(a),3) , 0 },
                {  0, Math.Round(Math.Sin(a),3) , Math.Round( Math.Cos(a),3) , 0 },
                {  0,                          0,                           0, 1 }
            });
        }
        public static Matrix4x4 RotateYMarix(double a)
        {
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a), 3) , 0 , Math.Round(Math.Sin(a), 3), 0 },
                {                            0, 1 ,                          0, 0 },
                { Math.Round(-Math.Sin(a), 3) , 0 , Math.Round(Math.Cos(a), 3), 0 },
                {                            0, 0 ,                          0, 1 }
            });
        }
        public static Matrix4x4 RotateZMarix(double a)
        {
            return new(new[,]
            {
                {  Math.Round(Math.Cos(a),3), Math.Round(-Math.Sin(a),3), 0 , 0 },
                {  Math.Round(Math.Sin(a),3), Math.Round(Math.Cos(a),3) , 0 , 0 },
                {                          0,                          0, 1 , 0 },
                {                          0,                          0, 0 , 1 }
            });
        }
        public static Matrix4x4 LookAt(Point3D pos, Vector3D target, Vector3D up)
        {
            var zaxis = Vector3D.UnitVector(new(target - pos));
            var xaxis = Vector3D.UnitVector(Vector3D.Cross(up, zaxis));
            var yaxis = Vector3D.Cross(zaxis, xaxis);

            double ta = -Vector3D.Dot(xaxis, new(pos));
            double tb = -Vector3D.Dot(yaxis, new(pos));
            double tc = -Vector3D.Dot(zaxis, new(pos));

            return new Matrix4x4(new[,]
            {
                { xaxis.X, yaxis.X, zaxis.X, 0},
                { xaxis.Y, yaxis.Y, zaxis.Y, 0},
                { xaxis.Z, yaxis.Z, zaxis.Z, 0},
                { ta     , tb     , tc     , 1}
            });
        }
        public static double ToRad(double d)
        {
            return d * Math.PI / 180;
        }

        //overrides *
        public static Point3D operator *(Matrix4x4 m, Point3D i)
        {
            double x = i.X * m[0, 0] + i.Y * m[1, 0] + i.Z * m[2, 0] + i.W * m[3, 0];
            double y = i.X * m[0, 1] + i.Y * m[1, 1] + i.Z * m[2, 1] + i.W * m[3, 1];
            double z = i.X * m[0, 2] + i.Y * m[1, 2] + i.Z * m[2, 2] + i.W * m[3, 2];
            double w = i.X * m[0, 3] + i.Y * m[1, 3] + i.Z * m[2, 3] + i.W * m[3, 3];

            return new Point3D(x, y, z, w);
        }
        public static Vector3D operator *(Matrix4x4 m, Vector3D i)
        {
            double x = i.X * m[0, 0] + i.Y * m[1, 0] + i.Z * m[2, 0] + i.W * m[3, 0];
            double y = i.X * m[0, 1] + i.Y * m[1, 1] + i.Z * m[2, 1] + i.W * m[3, 1];
            double z = i.X * m[0, 2] + i.Y * m[1, 2] + i.Z * m[2, 2] + i.W * m[3, 2];
            double w = i.X * m[0, 3] + i.Y * m[1, 3] + i.Z * m[2, 3] + i.W * m[3, 3];

            return new Vector3D(x, y, z, w);
        }
        public static Vertex operator *(Matrix4x4 m, Vertex i)
        {
            double x = i.pos.X * m[0, 0] + i.pos.Y * m[1, 0] + i.pos.Z * m[2, 0] + i.pos.W * m[3, 0];
            double y = i.pos.X * m[0, 1] + i.pos.Y * m[1, 1] + i.pos.Z * m[2, 1] + i.pos.W * m[3, 1];
            double z = i.pos.X * m[0, 2] + i.pos.Y * m[1, 2] + i.pos.Z * m[2, 2] + i.pos.W * m[3, 2];
            double w = i.pos.X * m[0, 3] + i.pos.Y * m[1, 3] + i.pos.Z * m[2, 3] + i.pos.W * m[3, 3];

            return new Vertex(new Point3D(x, y, z, w));
        }
        public static Matrix4x4 operator *(Matrix4x4 m1, Matrix4x4 m2)
        {
            Matrix4x4 matrix = new Matrix4x4();
            for(int c = 0; c < 4; c++)
            {
                for(int r = 0; r < 4; r++)
                {
                    matrix[r, c] = m1[r, 0] * m2[0, c] + m1[r, 1] * m2[1, c] + m1[r, 2] * m2[2, c] + m1[r, 3] * m2[3, c];
                }
            }
            return matrix;
        }
    }
}
