namespace Raytracing.Spectrum;
public class BlackbodySpectrum: ISpectrumNew {

    public BlackbodySpectrum(double T) {
        _T = T;
        double lambdaMax = 2.8977721e-3f / T;
        _NormalizationFactor = 1 / ISpectrumNew.Blackbody(lambdaMax * 1e9f, T);
    }

    public double GetValueAtLambda(double lambda) {
        return ISpectrumNew.Blackbody(lambda, _T) * _NormalizationFactor;
    }
    public double MaxValue() {
        return 1;
    }
    public SampledSpectrumNew Sample(SampledWavelengths lambda) {
        SampledSpectrumNew s = new SampledSpectrumNew();
        for(int i = 0; i < ISpectrumNew.NSpectrumSamples; i++) {
            s[i] = ISpectrumNew.Blackbody(lambda[i], _T) * _NormalizationFactor;
        }
        return s;
    }


    double _T;
    double _NormalizationFactor;
}
