using System.Numerics;

namespace MoarxMath
{
    public record Point3D<T>(T X, T Y, T Z) where T : IAdditionOperators<T, T, T>
    {
        public static Point3D<T> operator +(Point3D<T> left, Point3D<T> right) => left with { X = left.X + right.X, Y = left.Y + right.Y, Z = left.Z + right.Z };
    }
}