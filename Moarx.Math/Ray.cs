using System.Numerics;

namespace Moarx.Math;

public static class Ray {

    public static Ray<T> Create<T>(Point3D<T> origin, Vector3D<T> direction) where T : struct, INumber<T> {
        return new(origin, direction);
    }

    public static Ray<T> Empty<T>() where T : struct, INumber<T> {
        return new();
    }
}

public readonly record struct Ray<T>
    where T : struct, INumber<T> {

    public readonly Point3D<T>  Origin;
    public readonly Vector3D<T> Direction;

    public Ray() {
    }

    public Ray(Point3D<T> origin, Vector3D<T> direction) {
        Origin    = origin;
        Direction = direction;
    }

    public Point3D<T> At(T t) {
        return Origin + Direction * t;
    }

    public override string ToString() {
        return $"[o={Origin}, d={Direction}]";
    }

}