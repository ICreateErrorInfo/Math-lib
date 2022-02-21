using Math_lib;

namespace Raytracing
{
    public struct SurfaceInteraction
    {
        public Ray scattered;
        public Vector3D attenuation;
        public Point3D p;
        public Normal3D normal;
        public material mat_ptr;
        public double t;
        public double u;
        public double v;
        public bool front_face;

        public void set_face_normal(Ray r, Normal3D outwardNormal)
        {
            front_face = Vector3D.Dot(r.d, (Vector3D)outwardNormal) < 0;
            normal = front_face ? outwardNormal : -outwardNormal;
        }
    }
}
