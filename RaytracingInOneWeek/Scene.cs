using Math_lib;

namespace Raytracing
{
    public struct Scene
    {
        public hittable_list Objects;
        public int samplesPerPixel;
        public int maxDepth;
        public Point3D lookfrom;
        public Point3D lookat;
        public int vfov;
        public double aperture;
        public Vector3D background;
        public int imageWidth;
        public int imageHeight;

        public Scene(hittable_list  objs,
                     int            spp,
                     int            maxD,
                     Point3D        lookfrom,
                     Point3D        lookat,
                     int            vfov,
                     double         aperture,
                     Vector3D       background,
                     int            imageWidth = 400, 
                     int            imageHeight = 200)
        {
            Objects = objs;
            samplesPerPixel = spp;
            maxDepth = maxD;
            this.lookfrom = lookfrom;
            this.lookat = lookat;
            this.vfov = vfov;
            this.aperture = aperture;
            this.background = background;
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
        }
    }
}
