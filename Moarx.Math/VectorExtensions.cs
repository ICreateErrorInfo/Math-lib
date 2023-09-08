using System.Numerics;

namespace Moarx.Math {

    public static class VectorExtensions {

        public static Vector3D<double> Normalize<T>(this Vector3D<T> source) where T : struct, INumber<T> {
            double length = source.GetLength();

            return new Vector3D<double>(
                x: Convert.ToDouble(source.X) / length,
                y: Convert.ToDouble(source.Y) / length,
                z: Convert.ToDouble(source.Z) / length);
        }
        public static Vector3D<float> Normalize(this Vector3D<float> source) {
            float length = source.GetLength();

            return new Vector3D<float>(
                x: source.X / length,
                y: source.Y / length,
                z: source.Z / length);
        }

        public static double GetLength<T>(this Vector3D<T> source) where T : struct, INumber<T> {
            return System.Math.Sqrt(Convert.ToDouble(source.GetLengthSquared()));
        }
        public static float GetLength(this Vector3D<float> source){
            return System.MathF.Sqrt(source.GetLengthSquared());
        }

        public static Vector2D<double> Normalize<T>(this Vector2D<T> source) where T : struct, INumber<T> {
            double length = source.GetLength();

            return new Vector2D<double>(
                x: Convert.ToDouble(source.X) / length,
                y: Convert.ToDouble(source.Y) / length);
        }
        public static Vector2D<float> Normalize(this Vector2D<float> source) {
            float length = source.GetLength();

            return new Vector2D<float>(
                x: source.X / length,
                y: source.Y / length);
        }

        public static double GetLength<T>(this Vector2D<T> source) where T : struct, INumber<T> {
            return System.Math.Sqrt(Convert.ToDouble(source.GetLengthSquared()));
        }
        public static float GetLength(this Vector2D<float> source) {
            return System.MathF.Sqrt(source.GetLengthSquared());
        }
    }

}