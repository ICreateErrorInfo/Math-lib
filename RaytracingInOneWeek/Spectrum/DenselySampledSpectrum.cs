using System.Linq;

namespace Raytracing.Spectrum;
public class DenselySampledSpectrum: ISpectrum {

    public DenselySampledSpectrum(ISpectrum spectrum, int lambdaMin = ISpectrum.LambdaMin, int lambdaMax = ISpectrum.LambdaMax) {
        _lambdaMin = lambdaMin;
        _lambdaMax = lambdaMax;

        values = new double[lambdaMax - lambdaMin + 1];
        
        for(int lambda = lambdaMin; lambda <= lambdaMax; lambda++) {
            values[lambda - lambdaMin] = spectrum.GetValueAtLambda(lambda);
        }
    }

    public double GetValueAtLambda(double lambda) {
        int offset = (int)System.Math.Round(lambda) - _lambdaMin;
        if(offset < 0 || offset >= values.Length) {
            return 0;
        }
        return values[offset];
    }
    public double MaxValue() {
        return values.Max();
    }
    public SampledSpectrum Sample(SampledWavelengths lambda) {
        SampledSpectrum s = new SampledSpectrum();
        for(int i = 0; i < ISpectrum.NSpectrumSamples; i++) {
            int offset = (int)System.Math.Round(lambda[i]) - _lambdaMin;
            if(offset < 0 || offset >= values.Length) {
                s[i] = 0;
            } else {
                s[i] = values[offset];
            }
        }
        return s;
    }

    int _lambdaMin, _lambdaMax;
    double[] values;
}
