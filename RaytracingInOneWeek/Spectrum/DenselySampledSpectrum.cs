using System.Linq;

namespace Raytracing.Spectrum;
public class DenselySampledSpectrum: ISpectrumNew {

    public DenselySampledSpectrum(ISpectrumNew spectrum, int lambdaMin = ISpectrumNew.LambdaMin, int lambdaMax = ISpectrumNew.LambdaMax) {
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
    public SampledSpectrumNew Sample(SampledWavelengths lambda) {
        SampledSpectrumNew s = new SampledSpectrumNew();
        for(int i = 0; i < ISpectrumNew.NSpectrumSamples; i++) {
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
