﻿
using System.Numerics;

namespace MoarxMath
{
    public record Vector3D<T>(T X, T Y, T Z) where T : IAdditionOperators<T, T, T>, ISubtractionOperators<T, T, T>, IMultiplyOperators<T, T, T>, INumber<T>
    { 
        public static Vector3D<T> operator +(Vector3D<T> left, Vector3D<T> right) => left with 
        {
            X = left.X + right.X,
            Y = left.Y + right.Y,
            Z = left.Z + right.Z 
        };

        public static Vector3D<T> operator -(Vector3D<T> left, Vector3D<T> right) => left with 
        { 
            X = left.X - right.X,
            Y = left.Y - right.Y,
            Z = left.Z - right.Z 
        };

        public static T operator *(Vector3D<T> left, Vector3D<T> right) => (left.X * right.X) + (left.Y * right.Y);

        public static Vector3D<T> operator /(Vector3D<T> left, T scalar) => left with
        {
            X = left.X / scalar,
            Y = left.Y / scalar,
            Z = left.Z / scalar
        };
    }
}
