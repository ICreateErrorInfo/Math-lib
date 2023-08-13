namespace Raytracing.Spectrum;

public abstract class SpectrumFactory {
    public abstract ISpectrum CreateSpectrum();
    public abstract ISpectrum CreateFromRGB(double[] rgb, SpectrumMaterialType materialType);
}

public class SampledSpectrumFactory : SpectrumFactory {
    public override ISpectrum CreateFromRGB(double[] rgb, SpectrumMaterialType materialType) => SampledSpectrum.FromRGB(rgb, materialType);
    public override ISpectrum CreateSpectrum() => new SampledSpectrum();
}

public class RGBSpectrumFactory : SpectrumFactory {
    public override ISpectrum CreateFromRGB(double[] rgb, SpectrumMaterialType materialType) => RGBSpectrum.FromRGB(rgb);
    public override ISpectrum CreateSpectrum() => new RGBSpectrum();
}
