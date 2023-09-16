using Moarx.Math;
using Raytracing.Mathmatic;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public abstract class Material
    {
        public virtual ISpectrum Emitted(double u, double v,Point3D<double> p)
        {
            return new RGBAlbedoSpectrum(Raytracer.ColorSpace, new(0,0,0));
        }
        public abstract SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction);
    }
}
