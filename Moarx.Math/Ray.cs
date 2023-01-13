namespace Moarx.Math;

public readonly record struct Ray {

    public readonly Point3D<double>  Origin;
    public readonly Vector3D<double> Direction;

    public Ray() {
    }

    public Ray(Point3D<double> origin, Vector3D<double> direction) {
        Origin    = origin;
        Direction = direction;
    }

    public Point3D<double> At(double t) {
        return Origin + Direction * t;
    }

    public override string ToString() {
        return $"[o={Origin}, d={Direction}]";
    }

}