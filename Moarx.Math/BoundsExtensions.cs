using System.Numerics;

namespace Moarx.Math; 
public static class BoundsExtensions {
    public static double Distance<T, U>(this Bounds3D<T> b, Point3D<U> p) where T : struct, INumber<T> where U : struct, INumber<U> {
        var dist2 = Convert.ToDouble(b.DistanceSquared(p));

        return System.Math.Sqrt(dist2);
    }

}
