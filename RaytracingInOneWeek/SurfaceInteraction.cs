using Math_lib;
using Raytracing.Spectrum;

namespace Raytracing {
    public struct SurfaceInteraction
    {
        public Primitive Primitive;
        public Ray ScatteredRay;
        public ISpectrum Attenuation;
        public Point3D P;
        public Normal3D Normal;
        public double UCoordinate;
        public double VCoordinate;
        public bool FrontFace;
        public bool HasIntersection;
        public bool HasScattered;

        public void SetFaceNormal(Ray r, Normal3D outwardNormal)
        {
            FrontFace = Vector3D.Dot(r.D, (Vector3D)outwardNormal) < 0;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
        }
    }
}
