using System.Numerics;

namespace Moarx.Math {

    public static class Vector3DExtensions {

        public static Vector3D<T> Normalize<T>(this Vector3D<T> source) where T : struct, INumber<T>, IRootFunctions<T> {
            T length = source.GetLength();
            return new Vector3D<T>(
                x: source.X / length,
                y: source.Y / length,
                z: source.Z / length);
        }

        public static Normal3D<T> ToNormal<T>(this Vector3D<T> source) where T : struct, INumber<T>, IRootFunctions<T> {
            return new Normal3D<T>(
                x: source.X, 
                y: source.Y,
                z: source.Z);
        }

        public static T GetLength<T>(this Vector3D<T> source) where T : struct, INumber<T>, IRootFunctions<T> {
            return T.Sqrt(source.GetLengthSquared());
        }

    }

}