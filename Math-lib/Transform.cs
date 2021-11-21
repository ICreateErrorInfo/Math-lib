namespace Math_lib
{
    public class Transform
    {
        //Properties
        public Matrix m { get; private init;}
        public Matrix mInv {get; private init; }


        //Ctors
        public Transform()
        {

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
        public Transform(Matrix m , Matrix mInv)
        {
            this.m = m;
            this.mInv = mInv;
        }


        //Mehtods
        public Transform Inverse(Transform t)
        {
            return new Transform(t.mInv, t.m);
        }
        public Transform Transpose(Transform t)
        {
            return new Transform(Matrix.Transpose4x4(t.m), Matrix.Transpose4x4(t.mInv));
        }
        public bool IsIdentity()
        {
            return (m[0,0] == 1 && m[0,1] == 0 && m[0,2] == 0 &&
                    m[0,3] == 0 && m[1,0] == 0 && m[1,1] == 1 &&
                    m[1,2] == 0 && m[1,3] == 0 && m[2,0] == 0 &&
                    m[2,1] == 0 && m[2,2] == 1 && m[2,3] == 0 &&
                    m[3,0] == 0 && m[3,1] == 0 && m[3,2] == 0 &&
                    m[3,3] == 1);
        }
        public Point3D Trans(Point3D p)
        {
            double x = p.X, y = p.Y, z = p.Z;

            double xp = m[0,0] * x + m[0,1] * y + m[0,2] * z + m[0,3];
            double yp = m[1,0] * x + m[1,1] * y + m[1,2] * z + m[1,3];
            double zp = m[2,0] * x + m[2,1] * y + m[2,2] * z + m[2,3];
            double wp = m[3,0] * x + m[3,1] * y + m[3,2] * z + m[3,3];

            if (wp == 1)
            {
                return new Point3D(xp, yp, zp);
            }
            else
            {
                return new Point3D(xp, yp, zp) / wp;
            }
        }
        public Vector3D Trans(Vector3D v)
        {
            double x = v.X, y = v.Y, z = v.Z;

            return new Vector3D(m[0,0] * x + m[0,1] * y + m[0,2] * z,
                                m[1,0] * x + m[1,1] * y + m[1,2] * z,
                                m[2,0] * x + m[2,1] * y + m[2,2] * z);
        }
        public Normal3D Trans(Normal3D n)
        {
            double x = n.X, y = n.Y, z = n.Z;
            return new Normal3D(mInv[0,0] * x + mInv[1,0] * y + mInv[2,0] * z,
                                mInv[0,1] * x + mInv[1,1] * y + mInv[2,1] * z,
                                mInv[0,2] * x + mInv[1,2] * y + mInv[2,2] * z);
        }
        public Ray Trans(Ray r)
        {
            Point3D o = Trans(r.o);
            Vector3D d = Trans(r.d);

            return new Ray(o, d, r.tMax, r.time);
        }
        public RayDifferential Trans(RayDifferential r)
        {
            Ray tr = Trans(r);
            RayDifferential ret = new RayDifferential(tr.o, tr.d, tr.time);
            ret.hasDifferentials = r.hasDifferentials;
            ret.rxOrigin = Trans(r.rxOrigin);
            ret.ryOrigin = Trans(r.ryOrigin);
            ret.rxDirection = Trans(r.rxDirection);
            ret.ryDirection = Trans(r.ryDirection);

            return ret;
        }
        public Bounds3D Trans(Bounds3D b)
        {
            Bounds3D ret = new(Trans(new Point3D(b.pMin.X, b.pMin.Y, b.pMin.Z)));
            ret = Bounds3D.Union(ret, Trans(new Point3D(b.pMin.X + b.Diagonal().X, b.pMin.Y + b.Diagonal().Y, b.pMin.Z + b.Diagonal().Z)));
            return ret;
        }
        public bool SwapsHandness()
        {
            double det = m[0,0] * (m[1,1] * m[2,2] - m[1,2] * m[2,1]) -
                         m[0,1] * (m[1,0] * m[2,2] - m[1,2] * m[2,0]) +
                         m[0,2] * (m[1,0] * m[2,1] - m[1,1] * m[2,0]);
            return det < 0;
        }


        //override
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
                    if (t.m[i,j] < t1.m[i,j]) return true;
                    if (t.m[i,j] > t1.m[i,j]) return false;
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
