using System;

namespace Raytracing.Spectrum;

public enum SpectrumType { SampledSpectrum, RGBSpectrum };

public abstract class SpectrumFactory {
    public abstract ISpectrum CreateSpectrum();
    public abstract ISpectrum CreateFromRGB(double[] rgb, SpectrumMaterialType materialType);
}

public class SampledSpectrumFactory: SpectrumFactory {
    public override ISpectrum CreateFromRGB(double[] rgb, SpectrumMaterialType materialType) => SampledSpectrum.FromRGB(rgb, materialType);
    public override ISpectrum CreateSpectrum() => new SampledSpectrum();
}
