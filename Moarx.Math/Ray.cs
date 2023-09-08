namespace Moarx.Math;

public class Ray{

    public readonly Point3D<double>  Origin;
    public readonly Vector3D<double> Direction;
    public double TMax;
    public double Time;

    public Ray() {
        TMax = double.PositiveInfinity;
        Time = 0;
    }

    public Ray(Point3D<double> origin, Vector3D<double> direction, double tMax = double.PositiveInfinity, double time = 0) {
        Origin    = origin;
        Direction = direction;
        TMax = tMax;
        Time = time;
    }

    public Point3D<double> At(double t) {
        return Origin + Direction * t;
    }

    public override string ToString() {
        return $"[o={Origin}, d={Direction}]";
    }

}