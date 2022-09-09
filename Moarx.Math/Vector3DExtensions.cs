using System.Numerics;

namespace Moarx.Math {

    public static class Vector3DExtensions{

        public static T GetLength<T>(this Vector3D<T> v) where T : INumber<T>, IRootFunctions<T> {
            return T.Sqrt(v.GetLengthSquared());
        }

        public static double GetLength(this Vector3D<int> v) {
            return System.Math.Sqrt(v.GetLengthSquared());
        }

        public static double GetLength(this Vector3D<byte> v) {
            return System.Math.Sqrt(v.GetLengthSquared());
        }

        public static double GetLength(this Vector3D<short> v) {
            return System.Math.Sqrt(v.GetLengthSquared());
        }
    
    }

}