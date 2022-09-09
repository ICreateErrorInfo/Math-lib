using System.Numerics;

namespace Moarx.Math;

public readonly record struct Point3D<T>(T X, T Y, T Z)
    where T :
    INumber<T>,
    IRootFunctions<T>
{
    public static Point3D<T> Min(Point3D<T> p, Point3D<T> p1) => new()
    {
        X = T.Min(p.X, p1.X),
        Y = T.Min(p.Y, p1.Y),
        Z = T.Min(p.Z, p1.Z)
    };
    public static Point3D<T> Max(Point3D<T> p, Point3D<T> p1) => new()
    {
        X = T.Max(p.X, p1.X),
        Y = T.Max(p.Y, p1.Y),
        Z = T.Max(p.Z, p1.Z)
    };
    public static Point3D<T> Permute(Point3D<T> p, int x, int y, int z) => new()
    {
        X = p[x],
        Y = p[y],
        Z = p[z]
    };

    //operator overload
    public static Point3D<T> operator +(Vector3D<T> left, Point3D<T> right) => new()
    {
        X = left.X + right.X,
        Y = left.Y + right.Y,
        Z = left.Z + right.Z
    };

    public static Point3D<T> operator +(Point3D<T> left, Vector3D<T> right) => new()
    {
        X = left.X + right.X,
        Y = left.Y + right.Y,
        Z = left.Z + right.Z
    };

    public static Vector3D<T> operator -(Point3D<T> left, Point3D<T> right) => new()
    {
        X = left.X - right.X,
        Y = left.Y - right.Y,
        Z = left.Z - right.Z
    };

    public static Point3D<T> operator -(Point3D<T> left, Vector3D<T> right) => new()
    {
        X = left.X - right.X,
        Y = left.Y - right.Y,
        Z = left.Z - right.Z
    };

    public static Point3D<T> operator -(Point3D<T> point) => new()
    {
        X = -point.X,
        Y = -point.Y,
        Z = -point.Z
    };

    public T this[int i]
    {
        get
        {
            if (i == 0)
            {
                return X;
            }
            if (i == 1)
            {
                return Y;
            }
            if (i == 2)
            {
                return Z;
            }

            throw new IndexOutOfRangeException();
        }
    }
}