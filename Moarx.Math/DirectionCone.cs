namespace Moarx.Math;

public class DirectionCone {

    public readonly Vector3D<double> W;
    public readonly double CosTheta;

    public DirectionCone() {
        CosTheta = double.PositiveInfinity;
    }
    public DirectionCone(Vector3D<double> w, double cosTheta) {
        w = w.Normalize();
        CosTheta = cosTheta;
    }
    public DirectionCone(Vector3D<double> w) {
        w = w.Normalize();
        CosTheta = 1;
    }

    public bool IsEmpty() {
        return CosTheta == double.PositiveInfinity;
    }
    public static DirectionCone EntireSphere() {
        return new DirectionCone(new(0,0,1), -1);
    }
    public bool Inside(Vector3D<double> w) {
        return !IsEmpty() && (W * w.Normalize()) >= CosTheta;
    }
    public DirectionCone BoundSubtendedDirections(Bounds3D<double> b, Point3D<double> p) {
        double radius;
        Point3D<double> pCenter;
        (pCenter, radius) = b.BoundingSphere();

        if((p - pCenter).GetLengthSquared() < (radius * radius)) {
            return EntireSphere();
        }

        Vector3D<double> w = (pCenter - p).Normalize();
        double sin2ThetaMax = (radius*radius) / (pCenter - p).GetLengthSquared();
        double cosThetaMax = MathmaticMethods.SafeSqrt(1 - sin2ThetaMax);
        return new(w, cosThetaMax);
    }
    public static DirectionCone Union(DirectionCone a, DirectionCone b) {
        if(a.IsEmpty()) { return b; }
        if(b.IsEmpty()) { return a; }

        double thetaA = MathmaticMethods.SafeACos(a.CosTheta), thetaB = MathmaticMethods.SafeACos(b.CosTheta);
        double thetaD = Vector3D<double>.AngleBetween(a.W, b.W);

        if(System.Math.Min(thetaD + thetaB, System.Math.PI) <= thetaA) {
            return a;
        }
        if(System.Math.Min(thetaD + thetaA, System.Math.PI) <= thetaB) {
            return b;
        }

        double thetaO = (thetaA +thetaB + thetaD) / 2;
        if(thetaO >= System.Math.PI) {
            return EntireSphere();
        }

        double thetaR = thetaO - thetaA;
        var wr = Vector3D<double>.CrossProduct(a.W, b.W);

        if(wr.GetLengthSquared() == 0) {
            return EntireSphere();
        }

        return null;
        //var w = Rotate(Degrees(thetaR), wr); //TODO
    }
}
