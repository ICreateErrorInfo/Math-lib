using Math_lib;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public abstract class Material
    {
        protected SpectrumFactory Factory { get; }

        public Material(SpectrumFactory factory) {
            Factory = factory;
        }

        public virtual ISpectrum Emitted(double u, double v, Point3D p)
        {
            return Factory.CreateFromRGB(new double[] { 0, 0, 0 }, SpectrumMaterialType.Reflectance);
        }
        public abstract SurfaceInteraction Scatter(Ray rIn, SurfaceInteraction rec);
    }
}
