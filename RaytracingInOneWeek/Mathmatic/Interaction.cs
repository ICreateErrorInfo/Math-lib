using Moarx.Math;

namespace Raytracing.Mathmatic; 
public struct Interaction {
    public Point3D<double> Pi;
    public double Time;
    public Vector3D<double> WO;
    public Normal3D<double> N;
    public Point2D<double> UV;

    public Interaction(Point3D<double> pi, Normal3D<double> n, Point2D<double> uv, Vector3D<double> wo, double time) {
        Pi = pi;
        N = n;
        UV = uv;
        WO = wo;
        Time = time;
    }

    public bool IsSurfaceInteraction() {
        return N != new Normal3D<double>(0, 0, 0);
    }
    public bool IsMediumInteraction() {
        return !IsSurfaceInteraction();
    }
}
