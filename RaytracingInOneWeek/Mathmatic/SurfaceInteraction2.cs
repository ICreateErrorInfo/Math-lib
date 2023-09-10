using Moarx.Math;

namespace Raytracing.Mathmatic;
public class SurfaceInteraction2: Interaction {
    public Vector3D<double> Dpdu, Dpdv;
    public Normal3D<double> Dndu, Dndv;
    public ShadingProperties Shading;

    public struct ShadingProperties {
        public Normal3D<double> N;
        public Vector3D<double> Dpdu, Dpdv;
        public Normal3D<double> Dndu, Dndv;
    }

    public SurfaceInteraction2(Point3D<double> pi,
                               Point2D<double> uv,
                               Vector3D<double> wo,
                               Vector3D<double> dpdu,
                               Vector3D<double> dpdv,
                               Normal3D<double> dndu,
                               Normal3D<double> dndv,
                               double time,
                               bool flipNormal) : base(pi, new(Vector3D<double>.CrossProduct(dpdu, dpdv).Normalize()), uv, wo, time) {

        Dpdu = dpdu;
        Dpdv = dpdv;
        Dndu = dndu;
        Dndv = dndv;

        Shading.N = N;
        Shading.Dpdu = Dpdu;
        Shading.Dpdv = Dpdv;
        Shading.Dndu = Dndu;
        Shading.Dndv = Dndv;

        if(flipNormal ) {
            N *= -1;
            Shading.N *= -1;
        }

    }

    public void SetShadingGeometry(Normal3D<double> ns,
                                   Vector3D<double> dpdus,
                                   Vector3D<double> dpdvs,
                                   Normal3D<double> dndus,
                                   Normal3D<double> dndvs,
                                   bool orientationIsAuthoritative) {
        Shading.N = ns;

        if(orientationIsAuthoritative ) {
            N = N.FaceForward(Shading.N.ToVector());
        } else {
            Shading.N = Shading.N.FaceForward(N.ToVector());
        }

        Shading.Dpdu = dpdus;
        Shading.Dpdv = dpdvs;
        Shading.Dndu = dndus;
        Shading.Dndv = dndvs;
    }
}
