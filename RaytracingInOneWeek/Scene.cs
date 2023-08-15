using Math_lib;
using Raytracing.Accelerators;
using Raytracing.Camera;
using Raytracing.Primitives;
using Raytracing.Spectrum;

namespace Raytracing {
    public struct Scene
    {
        public ICamera Camera;
        public BVHAccelerator Accel;
        public int SamplesPerPixel;
        public int MaxDepth;
        public ISpectrum Background;
        public int ImageWidth;
        public int ImageHeight;

        public Scene(PrimitiveList  objs,
                     int            spp,
                     int            maxD,
                     ICamera        camera,
                     ISpectrum      background,
                     int            imageWidth = 400, 
                     int            imageHeight = 200)
        {
            Accel = new(objs.Objects, 2, BVHSplitMethod.Middle);
            SamplesPerPixel = spp;
            MaxDepth = maxD;
            Background = background;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
            Camera = camera;
        }
    }
}
