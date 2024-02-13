namespace Moarx.Graphics.Spectrum;
public class BlackbodySpectrum: ISpectrum {

    public BlackbodySpectrum(double T) {
        _T = T;
        var lambdaMax = 2.8977721e-3f / T;
        _NormalizationFactor = 1 / ISpectrum.Blackbody(lambdaMax * 1e9f, T);
    }

    public double GetValueAtLambda(double lambda) {
        return ISpectrum.Blackbody(lambda, _T) * _NormalizationFactor;
    }
    public double MaxValue() {
        return 1;
    }
    public SampledSpectrum Sample(SampledWavelengths lambda) {
        var s = new SampledSpectrum();
        for (var i = 0; i < ISpectrum.NSpectrumSamples; i++) {
            s[i] = ISpectrum.Blackbody(lambda[i], _T) * _NormalizationFactor;
        }
        return s;
    }


    double _T;
    double _NormalizationFactor;
}
