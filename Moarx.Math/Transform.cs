using System.ComponentModel.Design;
using System.Reflection.Emit;

namespace Moarx.Math;
public class Transform {
    private readonly SquareMatrix _m, _mInv;

    public Transform() {
        _m = new SquareMatrix(4);
        _mInv = new SquareMatrix(4);
    }
    public Transform(SquareMatrix m) {
        _m = m;

        if(m._size != 4) {
            throw new Exception("Matrix is not 4x4");
        }

        var inverse = m.Inverse();

        if(!inverse.HasValue) {
            throw new Exception("Matrix cant be inverted");   
        }

        _mInv = inverse.Value;
    }
    public Transform(double[,] matrix) : this(new SquareMatrix(matrix)) {
        
    }
    public Transform(SquareMatrix m, SquareMatrix mInv) {
        _m = m;
        _mInv = mInv;
    }

    public SquareMatrix GetMatrix() {
        return _m;
    }
    public SquareMatrix GetInverse() {
        return _mInv;
    }

    public bool SwapHandness() {
        SquareMatrix s = new SquareMatrix(new double[,]{
            { _m[0,0], _m[0,1], _m[0,2] },
            { _m[1,0], _m[1,1], _m[1,2] },
            { _m[2,0], _m[2,1], _m[2,2] }
        });

        return s.Determinant() < 0;
    }
    public Transform Inverse() {
        return new Transform(_mInv, _m);
    }
    public Transform Transpose() {
        return new Transform(_m.Transpose(), _mInv.Transpose());
    }
    public bool IsIdentity() {
        return _m.IsIdentity();
    }
    public static Transform Translate(Vector3D<double> delta) {
        SquareMatrix m = new SquareMatrix(new double[,]{
            {1, 0, 0, delta.X},
            {0, 1, 0, delta.Y},
            {0, 0, 1, delta.Z},
            {0, 0, 0, 1 } 
        });

        SquareMatrix mInv = new SquareMatrix(new double[,]{
            {1, 0, 0, -delta.X},
            {0, 1, 0, -delta.Y},
            {0, 0, 1, -delta.Z},
            {0, 0, 0, 1 }
        });

        return new Transform(m, mInv);
    }
    public static Transform Scale(double x, double y, double z) {
        SquareMatrix m = new SquareMatrix(new double[,]{
            {x, 0, 0, 0},
            {0, y, 0, 0},
            {0, 0, z, 0},
            {0, 0, 0, 1 }
        });

        SquareMatrix mInv = new SquareMatrix(new double[,]{
            {1/x, 0, 0, 0},
            {0, 1/y, 0, 0},
            {0, 0, 1/z, 0},
            {0, 0, 0, 1 }
        });

        return new Transform (m, mInv);
    }
    public bool HasScale(double tolerance = 1e-3) {
        double la2 = (this * new Vector3D<double>(1, 0, 0)).GetLengthSquared();
        double lb2 = (this * new Vector3D<double>(0, 1, 0)).GetLengthSquared();
        double lc2 = (this * new Vector3D<double>(0, 0, 1)).GetLengthSquared();

        return (System.Math.Abs(la2 - 1) > tolerance ||
                System.Math.Abs(lb2 - 1) > tolerance ||
                System.Math.Abs(lc2 - 1) > tolerance);
    }
    public static Transform RotateX(double theta) {
        double sinTheta = System.Math.Sin(MathmaticMethods.ConvertToRadians(theta));
        double cosTheta = System.Math.Cos(MathmaticMethods.ConvertToRadians(theta));

        SquareMatrix m = new SquareMatrix(new double[,]{
            {1,        0,         0, 0 },
            {0, cosTheta, -sinTheta, 0 },
            {0, sinTheta,  cosTheta, 0 },
            {0,        0,         0, 1 }
        });

        return new Transform(m, m.Transpose());
    }
    public static Transform RotateY(double theta) {
        double sinTheta = System.Math.Sin(MathmaticMethods.ConvertToRadians(theta));
        double cosTheta = System.Math.Cos(MathmaticMethods.ConvertToRadians(theta));

        SquareMatrix m = new SquareMatrix(new double[,]{
            {cosTheta , 0, sinTheta , 0 },
            {0        , 1, 0        , 0 },
            {-sinTheta, 0,  cosTheta, 0 },
            {0        , 0, 0        , 1 }
        });

        return new Transform(m, m.Transpose());
    }
    public static Transform RotateZ(double theta) {
        double sinTheta = System.Math.Sin(MathmaticMethods.ConvertToRadians(theta));
        double cosTheta = System.Math.Cos(MathmaticMethods.ConvertToRadians(theta));

        SquareMatrix m = new SquareMatrix(new double[,]{
            {cosTheta , -sinTheta, 0 , 0 },
            {sinTheta , cosTheta , 0 , 0 },
            {0        , 0        , 1 , 0 },
            {0        , 0        , 0 , 1 }
        });

        return new Transform(m, m.Transpose());
    }
    public static Transform Rotate(double sinTheta, double cosTheta, Vector3D<double> axis) {
        var a = axis.Normalize();
        double[,] m = new double[4,4];

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

        SquareMatrix squareMatrix = new SquareMatrix(m);

        return new Transform(squareMatrix, squareMatrix.Transpose());
    }
    public static Transform Rotate(double theta, Vector3D<double> axis) {
        double sinTheta = System.Math.Sin(MathmaticMethods.ConvertToRadians(theta));
        double cosTheta = System.Math.Cos(MathmaticMethods.ConvertToRadians(theta));
        return Rotate(sinTheta, cosTheta, axis);
    }
    public static Transform RotateFromTo(Vector3D<double> from, Vector3D<double> to) {

        Vector3D<double> refl;

        if (System.Math.Abs(from.X) < 0.72f && System.Math.Abs(to.X) < 0.72f)
            refl = new Vector3D<double>(1, 0, 0);
        else if (System.Math.Abs(from.Y) < 0.72f && System.Math.Abs(to.Y) < 0.72f)
            refl = new Vector3D<double>(0, 1, 0);
        else
            refl = new Vector3D<double>(0, 0, 1);

        Vector3D<double> u = refl - from, v = refl - to;
        double[,] r = new double[4, 4];
        for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 3; ++j) {
                r[i,j] = ((i == j) ? 1 : 0) - 2 / (u*u) * u[i] * u[j] -
                          2 / (v*v) * v[i] * v[j] +
                          4 * (u*v) / ((u*u) * (v*v)) * v[i] * u[j];
            }
        }

        SquareMatrix squareMatrix = new SquareMatrix(r);

        return new Transform(squareMatrix, squareMatrix.Transpose());
    }
    public static Transform LookAt(Point3D<double> pos, Point3D<double> look, Vector3D<double> up) {
        double[,] worldFromCamera = new double[4, 4];

        worldFromCamera[0,3] = pos.X;
        worldFromCamera[1,3] = pos.Y;
        worldFromCamera[2,3] = pos.Z;
        worldFromCamera[3,3] = 1;

        Vector3D<double> dir = (look - pos).Normalize();
        Vector3D<double> right = (Vector3D<double>.CrossProduct(up.Normalize(), dir)).Normalize();
        Vector3D<double> newUp = Vector3D<double>.CrossProduct(dir, right);
        worldFromCamera[0,0] = right.X;
        worldFromCamera[1,0] = right.Y;
        worldFromCamera[2,0] = right.Z;
        worldFromCamera[3,0] = 0;
        worldFromCamera[0,1] = newUp.X;
        worldFromCamera[1,1] = newUp.Y;
        worldFromCamera[2,1] = newUp.Z;
        worldFromCamera[3,1] = 0;
        worldFromCamera[0,2] = dir.X;
        worldFromCamera[1,2] = dir.Y;
        worldFromCamera[2,2] = dir.Z;
        worldFromCamera[3,2] = 0;

        SquareMatrix cameraFromWorld = new SquareMatrix(worldFromCamera).Inverse().Value;
        return new Transform(cameraFromWorld, new SquareMatrix(worldFromCamera));
    }
    public static Transform Orthographic(double zNear, double zFar) {
        return Scale(1, 1, (double)1 / (zFar - zNear)) *
               Translate(new Vector3D<double>(0, 0, -zNear));
    }
    public static Transform Perspective(double fov, double near, double far) {
        SquareMatrix persp = new SquareMatrix(new double[,] {
                {1, 0, 0, 0 },
                {0, 1, 0, 0 },
                {0, 0, far/(far-near), (-(far*near)) / (far - near)},
                {0, 0, 1,0 }
            });


        double invertTanAngle = (double)1 / System.Math.Tan(MathmaticMethods.ConvertToRadians(fov) / 2);

        return Scale(invertTanAngle, invertTanAngle, 1) * new Transform(persp);
    }


    public static Point3D<double> operator *(Transform t, Point3D<double> p) {
        SquareMatrix m = t.GetMatrix();

        double xp = m[0,0] * p.X + m[0,1] * p.Y + m[0,2] * p.Z + m[0,3];
        double yp = m[1,0] * p.X + m[1,1] * p.Y + m[1,2] * p.Z + m[1,3];
        double zp = m[2,0] * p.X + m[2,1] * p.Y + m[2,2] * p.Z + m[2,3];
        double wp = m[3,0] * p.X + m[3,1] * p.Y + m[3,2] * p.Z + m[3,3];
        if(wp == 1) {
            return new Point3D<double>(xp, yp, zp);
        } else {
            return new Point3D<double>(xp, yp, zp) / wp;
        }
    }
    public static Vector3D<double> operator *(Transform t, Vector3D<double> v) {
        SquareMatrix m = t.GetMatrix();

        return new  Vector3D<double>(m[0,0] * v.X + m[0,1] * v.Y + m[0,2] * v.Z,
                                     m[1,0] * v.X + m[1,1] * v.Y + m[1,2] * v.Z,
                                     m[2,0] * v.X + m[2,1] * v.Y + m[2,2] * v.Z);
    }
    public static Normal3D<double> operator *(Transform t, Normal3D<double> n) {
        SquareMatrix mInv = t.GetInverse();

        return new Normal3D<double>(mInv[0,0] * n.X + mInv[1,0] * n.Y + mInv[2,0] * n.Z,
                                    mInv[0,1] * n.X + mInv[1,1] * n.Y + mInv[2,1] * n.Z,
                                    mInv[0,2] * n.X + mInv[1,2] * n.Y + mInv[2,2] * n.Z);
    }
    public static Ray operator *(Transform t, Ray r) {
        Point3D<double> o = t * r.Origin;
        Vector3D<double> d = t * r.Direction;

        return new Ray(o, d, r.TMax, r.Time);
    }
    public static Bounds3D<double> operator *(Transform t, Bounds3D<double> b) {
        Bounds3D<double> bt = new Bounds3D<double>();
        for(int i = 0; i < 8; i++) {
            bt = Bounds3D<double>.Union(bt, t * b.Corner(i));
        }
        return bt;
    }
    public static Transform operator *(Transform t1, Transform t2) {
        return new Transform(t1._m * t2._m, t2._mInv * t1._mInv);
    }

    public static bool operator ==(Transform t1, Transform t2) {
        return t1._m == t2._m;
    }
    public static bool operator !=(Transform t1, Transform t2) {
        return t1._m != t2._m;
    }
}
