using System;
using System.Runtime.CompilerServices;

namespace Math_lib
{
    public class Transform
    {
        //Properties
        public Matrix m { get; private set; }
        public Matrix mInv { get; private set; }


        //Ctors
        public Transform()
        {
            m.Identity();
            mInv = Matrix.Inverse4x4(m);
        }
        public Transform(double[,] mat)
        {
            Matrix m1 = new Matrix(mat);
            mInv = Matrix.Inverse4x4(m1);
        }
        public Transform(Matrix m)
        {
            this.m = m;
            mInv = Matrix.Inverse4x4(m);
        }
        public Transform(Matrix m, Matrix mInv)
        {
            this.m = m;
            this.mInv = mInv;
        }


        //Mehtods
        public Transform Inverse()
        {
            return new Transform(mInv, m);
        }
        public Transform Transpose(Transform t)
        {
            return new Transform(Matrix.Transpose4x4(t.m), Matrix.Transpose4x4(t.mInv));
        }
        public bool IsIdentity()
        {
            return (m[0, 0] == 1 && m[0, 1] == 0 && m[0, 2] == 0 &&
                    m[0, 3] == 0 && m[1, 0] == 0 && m[1, 1] == 1 &&
                    m[1, 2] == 0 && m[1, 3] == 0 && m[2, 0] == 0 &&
                    m[2, 1] == 0 && m[2, 2] == 1 && m[2, 3] == 0 &&
                    m[3, 0] == 0 && m[3, 1] == 0 && m[3, 2] == 0 &&
                    m[3, 3] == 1);
        }
        public static Transform Translate(Vector3D delta)
        {
            Matrix m = new Matrix(new[,]
            {
                { 1, 0, 0, delta.X },
                { 0, 1, 0, delta.Y },
                { 0, 0, 1, delta.Z },
                { 0, 0, 0, 1       }
            });

            Matrix minv = new Matrix(new[,]
            {
                { 1, 0, 0, -delta.X },
                { 0, 1, 0, -delta.Y },
                { 0, 0, 1, -delta.Z },
                { 0, 0, 0,        1 }
            });

            return new Transform(m, minv);
        }
        public static Transform Scale(double x, double y, double z)
        {
            Matrix m = new Matrix(new[,]
               {{ x, 0, 0, 0},
                { 0, y, 0, 0},
                { 0, 0, z, 0},
                { 0, 0, 0, 1}});

            Matrix minv = new Matrix(new[,]
               {{ 1/x, 0,  0,  0},
                { 0,  1/y, 0,  0},
                { 0,   0, 1/z, 0},
                { 0,   0,  0,  1}});

            return new Transform(m, minv);
        }
        public static Transform RotateX(double a)
        {
            a = Mathe.ToRad(a);
            return new(new Matrix(new[,]
            {
                {  1,            0,           0 , 0 },
                {  0, Math.Cos(a) , -Math.Sin(a), 0 },
                {  0, Math.Sin(a) ,  Math.Cos(a), 0 },
                {  0,            0,            0, 1 }
            }), new Matrix(4,4));
        }
        public static Transform RotateY(double a)
        {
            a = Mathe.ToRad(a);
            return new(new Matrix(new[,]
            {
                {  Math.Cos(a) , 0 , Math.Sin(a), 0 },
                {             0, 1 ,          0 , 0 },
                { -Math.Sin(a) , 0 , Math.Cos(a), 0 },
                {             0, 0 ,           0, 1 }
            }), new Matrix(4, 4));
        }
        public static Transform RotateZ(double a)
        {
            a = Mathe.ToRad(a);
            return new(new Matrix(new[,]
            {
                {  Math.Cos(a), -Math.Sin(a), 0, 0 },
                {  Math.Sin(a), Math.Cos(a) , 0, 0 },
                {            0,            0, 1, 0 },
                {            0,            0, 0, 1 }
            }), new Matrix(4, 4));
        }
        public static Transform Rotate(double theta, Vector3D axis)
        {
            Vector3D a = Vector3D.Normalize(axis);
            double sinTheta = Math.Sin(Mathe.ToRad(theta));
            double cosTheta = Math.Cos(Mathe.ToRad(theta));
            Matrix m = new Matrix(4,4);

            m[0,0] = a.X * a.X + (1 - a.X * a.X) * cosTheta;
            m[0,1] = a.X * a.Y * (1 - cosTheta) - a.Z * sinTheta;
            m[0,2] = a.X * a.Z * (1 - cosTheta) + a.Y * sinTheta;
            m[0,3] = 0;

            m[1,0] = a.X * a.Y * (1 - cosTheta) + a.Z * sinTheta;
            m[1,1] = a.Y * a.Y + (1 - a.Y * a.Y) * cosTheta;
            m[1,2] = a.Y * a.Z * (1 - cosTheta) - a.X * sinTheta;
            m[1,3] = 0;
               
            m[2,0] = a.X * a.Z * (1 - cosTheta) - a.Y * sinTheta;
            m[2,1] = a.Y * a.Z * (1 - cosTheta) + a.X * sinTheta;
            m[2,2] = a.Z * a.Z + (1 - a.Z * a.Z) * cosTheta;
            m[2,3] = 0;

            return new Transform(m, Matrix.Transpose4x4(m));
        }
        public static Transform Orthographic(double zNear, double zFar) {
            return Scale(1, 1, 1 / (zFar - zNear)) *
                   Translate(new Vector3D(0, 0, -zNear));
        }
        public static Transform Perspective(double fov, double near, double far) {
            Matrix persp = new Matrix(new double[,] {
                {1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, far/(far-near), -((far*near) / (far - near))},
                {0, 0, 1,0 }
            });

            double invertTanAngle = (double)1 / Math.Tan(Mathe.ToRad(fov) / 2);

            return Scale(invertTanAngle, invertTanAngle, 1) * new Transform(persp);
        }
        public bool SwapsHandness()
        {
            double det = m[0, 0] * (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]) -
                         m[0, 1] * (m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0]) +
                         m[0, 2] * (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0]);
            return det < 0;
        }


        //override
        public static Vector3D operator *(Transform t, Vector3D v) {
            return t.m * v;
        }
        public static Point3D operator *(Transform t, Point3D p) {
            return t.m * p;
        }
        public static Ray operator *(Transform t, Ray r) {
            //TODO errorCalc
            Point3D o = t * r.O;
            Vector3D d = r.D;

            return new Ray(o, d, r.TMax, r.Time);
        }
        public static Transform operator *(Transform t, Transform t2)
        {
            return new Transform(t.m * t2.m, t2.mInv * t.mInv);
        }
        public static bool operator ==(Transform t1, Transform t)
        {
            return t.m == t1.m && t.mInv == t1.mInv;
        }
        public static bool operator !=(Transform t, Transform t1)
        {
            return t.m != t1.m && t.mInv != t1.mInv;
        }
        public static bool operator <(Transform t, Transform t1)
        {
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    if (t.m[i, j] < t1.m[i, j]) return true;
                    if (t.m[i, j] > t1.m[i, j]) return false;
                }
            return false;
        }
        public static bool operator >(Transform t, Transform t1)
        {
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    if (t.m[i, j] < t1.m[i, j]) return false;
                    if (t.m[i, j] > t1.m[i, j]) return true;
                }
            return true;
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

            throw new System.NotImplementedException();
        }
        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }
}
