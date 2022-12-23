namespace Moarx.Math;
public class Ray {
    public readonly Point3D<double> O;
    public readonly Vector3D<double> D;

    public Ray() {
    }
    public Ray(Point3D<double> o, Vector3D<double> d) {
        O = o;
        D = d;
    }

    public Point3D<double> At(double t) {
        return O + D * t;
    }

    public override string ToString() {
        return $"[o={O}, d={D}]";
    }
}

