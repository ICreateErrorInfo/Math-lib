namespace Raytracing.Spectrum;
public class ConstantSpectrum: ISpectrum {

    public ConstantSpectrum(double c) {
        _c = c;
    }

    public double GetValueAtLambda(double lambda) {
        return _c;
    }
    public double MaxValue() {
        return _c;
    }
    public SampledSpectrum Sample(SampledWavelengths lambda) {
        return new SampledSpectrum(_c);
    }

    private double _c;
}
