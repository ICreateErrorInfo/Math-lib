namespace Raytracing.Spectrum;
public class BlackbodySpectrum: ISpectrum {

    public BlackbodySpectrum(double T) {
        _T = T;
        double lambdaMax = 2.8977721e-3f / T;
        _NormalizationFactor = 1 / ISpectrum.Blackbody(lambdaMax * 1e9f, T);
    }

    public double GetValueAtLambda(double lambda) {
        return ISpectrum.Blackbody(lambda, _T) * _NormalizationFactor;
    }
    public double MaxValue() {
        return 1;
    }
    public SampledSpectrum Sample(SampledWavelengths lambda) {
        SampledSpectrum s = new SampledSpectrum();
        for(int i = 0; i < ISpectrum.NSpectrumSamples; i++) {
            s[i] = ISpectrum.Blackbody(lambda[i], _T) * _NormalizationFactor;
        }
        return s;
    }


    double _T;
    double _NormalizationFactor;
}
