using Math_lib;
using Math_lib.Spectrum;

namespace Raytracing.Materials
{
    public abstract class Material
    {
        public virtual SampledSpectrum Emitted(double u, double v, Point3D p)
        {
            return SampledSpectrum.FromRGB(new double[] { 0, 0, 0 }, SampledSpectrum.SpectrumType.Reflectance);
        }
        public abstract bool Scatter(Ray rIn, ref SurfaceInteraction rec, out SampledSpectrum attenuation, out Ray scattered);
    }
}
