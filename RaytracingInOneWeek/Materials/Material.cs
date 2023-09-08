using Moarx.Math;
using Raytracing.Mathmatic;
using Raytracing.Spectrum;

namespace Raytracing.Materials {
    public abstract class Material
    {
        protected SpectrumFactory Factory { get; }

        public Material(SpectrumFactory factory) {
            Factory = factory;
        }

        public virtual ISpectrum Emitted(double u, double v,Point3D<double> p)
        {
            return Factory.CreateFromRGB(new double[] { 0, 0, 0 }, SpectrumMaterialType.Reflectance);
        }
        public abstract SurfaceInteraction Scatter(Ray rayIn, SurfaceInteraction interaction);
    }
}
