using Math_lib;
using Raytracing.Materials;

namespace Raytracing
{
    public struct SurfaceInteraction
    {
        public Primitive Primitive;
        public Ray Scattered;
        public Vector3D Attenuation;
        public Point3D P;
        public Normal3D Normal;
        public double U;
        public double V;
        public bool FrontFace;

        public void SetFaceNormal(Ray r, Normal3D outwardNormal)
        {
            FrontFace = Vector3D.Dot(r.D, (Vector3D)outwardNormal) < 0;
            Normal = FrontFace ? outwardNormal : -outwardNormal;
        }
    }
}
