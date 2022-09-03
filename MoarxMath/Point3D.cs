using System.Numerics;

namespace MoarxMath
{
    public record Point3D<T>(T X, T Y, T Z) where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>, INumber<T>
    {
        public static Point3D<T> operator +(Point3D<T> left, Point3D<T> right) => left with 
        { 
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z 
        };
        public static Point3D<T> operator +(Vector3D<T> left, Point3D<T> right) => right with
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z
        };
        public static Point3D<T> operator +(Point3D<T> left, Vector3D<T> right) => left with
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z
        };

        public static Vector3D<T> operator -(Point3D<T> left, Point3D<T> right) => new Vector3D<T>
        (
            left.X - right.X,
            left.Y - right.Y,
            left.Z - right.Z
        );
        public static Point3D<T> operator -(Point3D<T> left, Vector3D<T> right) => left with
        {              
            X = left.X - right.X,
            Y = left.Y - right.Y,
            Z = left.Z - right.Z
        };
    }
}