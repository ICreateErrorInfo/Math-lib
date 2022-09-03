using System.Numerics;

namespace MoarxMath
{
    public readonly record struct Point3D<T>(T X, T Y, T Z) where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>, INumber<T>
    {
        //operator overload
        public static Point3D<T> operator +(Vector3D<T> left, Point3D<T> right) => new Point3D<T>
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z
        };
        public static Point3D<T> operator +(Point3D<T> left, Vector3D<T> right) => new Point3D<T>
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z
        };

        public static Vector3D<T> operator -(Point3D<T> left, Point3D<T> right) => new Vector3D<T>
        {
            X = left.X - right.X,
            Y = left.Y - right.Y,
            Z = left.Z - right.Z
        };
        public static Point3D<T> operator -(Point3D<T> left, Vector3D<T> right) => new Point3D<T>
        {              
            X = left.X - right.X,
            Y = left.Y - right.Y,
            Z = left.Z - right.Z
        };
        public static Point3D<T> operator -(Point3D<T> point) => new Point3D<T>
        {
            X = -point.X,
            Y = -point.Y,
            Z = -point.Z
        };
    }
}