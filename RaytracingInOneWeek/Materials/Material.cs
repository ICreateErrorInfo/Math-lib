using Moarx.Math;
using Raytracing.Color;
using Raytracing.Mathmatic;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public abstract class Material
    {
        protected RGBColorSpace _ColorSpace;

        public Material(RGBColorSpace colorSpace) {
            _ColorSpace = colorSpace;   
        }

        public virtual ISpectrum Emitted(double u, double v,Point3D<double> p)
        {
            return new RGBAlbedoSpectrum(_ColorSpace, new(0,0,0));
        }
        public abstract SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction);
    }
}
