using Moarx.Graphics.Spectrum;
using Moarx.Math;
using Raytracing.Primitives;

namespace Raytracing.Mathmatic {
    public struct SurfaceInteraction {
        public Primitive Primitive;
        public Ray ScatteredRay;
        public ISpectrum Attenuation;
        public Point3D<double> P;
        public Normal3D<double> Normal;
        public double UCoordinate;
        public double VCoordinate;
        public bool FrontFace;
        public bool HasIntersection;
        public bool HasScattered;

        public void SetFaceNormal(Ray ray, Normal3D<double> outwardNormal) {
            FrontFace = (ray.Direction * outwardNormal.ToVector()) < 0;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
        }
    }
}
