using System.Diagnostics;
using System.Numerics;

namespace MoarxMath
{
    public readonly record struct Vector3D<T>(T X, T Y, T Z) where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>, INumber<T>, IRootFunctions<T>
    {
        public static Vector3D<T> CrossProduct(Vector3D<T> vector1, Vector3D<T> vector2) => new Vector3D<T>
        {
            X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y),
            Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z),
            Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X)
        };
        public static Vector3D<T> Normalize(Vector3D<T> vector) => vector / vector.GetLength();
        public T GetLength() => T.Sqrt(GetLengthSqrt());    
        public T GetLengthSqrt() => this * this;



        //operator overload
        public static Vector3D<T> operator +(Vector3D<T> left, Vector3D<T> right) => new Vector3D<T>
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z
        };

        public static Vector3D<T> operator -(Vector3D<T> left, Vector3D<T> right) => new Vector3D<T>
        {
            X = left.X - right.X,
            Y = left.Y - right.Y,
            Z = left.Z - right.Z
        };
        public static Vector3D<T> operator -(Vector3D<T> vector) => new Vector3D<T>
        {
            X = -vector.X,
            Y = -vector.Y,
            Z = -vector.Z
        };

        public static T operator *(Vector3D<T> left, Vector3D<T> right) => (left.X * right.X) + (left.Y * right.Y) + (left.Z * right.Z);
        public static Vector3D<T> operator *(Vector3D<T> left, T scalar) => new Vector3D<T>
        {
            X = left.X * scalar,
            Y = left.Y * scalar,
            Z = left.Z * scalar
        };
        public static Vector3D<T> operator *(T scalar, Vector3D<T> right) => new Vector3D<T>
        {
            X = right.X * scalar,
            Y = right.Y * scalar,
            Z = right.Z * scalar
        };

        public static Vector3D<T> operator /(Vector3D<T> left, T scalar) => new Vector3D<T>
        {
            X = left.X / scalar,
            Y = left.Y / scalar,
            Z = left.Z / scalar
        };
    }
}
