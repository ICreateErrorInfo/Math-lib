using Math_lib;
using Raytracing.Accelerators;
using Raytracing.Spectrum;

namespace Raytracing {
    public struct Scene
    {
        public ISpectrum Spectrum;
        public BVHAccelerator Accel;
        public int SamplesPerPixel;
        public int MaxDepth;
        public Point3D Lookfrom;
        public Point3D Lookat;
        public Vector3D VUp;
        public int VFov;
        public double Aperture;
        public ISpectrum Background;
        public int ImageWidth;
        public int ImageHeight;
        public double FocusDistance;

        public Scene(HittableList   objs,
                     int            spp,
                     int            maxD,
                     Point3D        lookfrom,
                     Point3D        lookat,
                     Vector3D       vUp,
                     int            vFov,
                     double         aperture,
                     ISpectrum       background,
                     ISpectrum spectrum,
                     double focusDistance = 0,
                     int            imageWidth = 400, 
                     int            imageHeight = 200)
        {
            if(focusDistance == 0)
            {
                FocusDistance = (lookfrom - lookat).GetLength();
            }
            else
            {
                FocusDistance = focusDistance;
            }

            Accel = new(objs.Objects, 2, BVHSplitMethod.Middle);
            SamplesPerPixel = spp;
            MaxDepth = maxD;
            Lookfrom = lookfrom;
            Lookat = lookat;
            VFov = vFov;
            Aperture = aperture;
            Background = background;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
            VUp = vUp;
            Spectrum = spectrum;
        }
    }
}
