namespace Raytracing.Spectrum;
public class ConstantSpectrum: ISpectrumNew {

    public ConstantSpectrum(double c) {
        _c = c;
    }

    public double GetValueAtLambda(double lambda) {
        return _c;
    }
    public double MaxValue() {
        return _c;
    }
    public SampledSpectrumNew Sample(SampledWavelengths lambda) {
        return new SampledSpectrumNew(_c);
    }

    private double _c;
}
