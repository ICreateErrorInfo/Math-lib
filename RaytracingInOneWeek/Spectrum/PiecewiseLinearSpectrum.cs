using Moarx.Math;
using System.Collections.Generic;
using System.Linq;

namespace Raytracing.Spectrum;
public class PiecewiseLinearSpectrum: ISpectrumNew {

    public PiecewiseLinearSpectrum(double[] lambdas, double[] values) {
        _lambdas = lambdas;
        _values = values;
    }

    public static PiecewiseLinearSpectrum FromInterleaved(double[] samples, bool normalize) {
        int n = samples.Length / 2;
        List<double> lambda = new List<double>(), v = new List<double>();

        if (samples[0] > ISpectrumNew.LambdaMin) {
            lambda.Add(ISpectrumNew.LambdaMin - 1);
            v.Add(samples[1]);
        }
        for(int i = 0; i < n; i++) {
            lambda.Add(samples[2 * i]);
            v.Add(samples[2 * i + 1]);
        }
        if (lambda.Last() < ISpectrumNew.LambdaMax) {
            lambda.Add(ISpectrumNew.LambdaMax + 1);
            v.Add(v.Last());
        }

        PiecewiseLinearSpectrum spec = new PiecewiseLinearSpectrum(lambda.ToArray(), v.ToArray());

        if (normalize) {
            spec.Scale(SampledSpectrumConstants.CIE_Y_integral / ISpectrumNew.InnerProduct(spec, SampledSpectrumConstants.YNew));
        }

        return spec;
    }
    public void Scale(double s) {
        for (int i = 0; i < _values.Length - 1; i++) {
            _values[i] *= s;
        }
    }
    public double GetValueAtLambda(double lambda) {
        if(_lambdas.Length == 0 || lambda < _lambdas[0] || lambda > _lambdas[_lambdas.Length - 1]) {
            return 0;
        }

        int o = MathmaticMethods.FindInterval(_lambdas.Length, (int x) => _lambdas[x] <= lambda);
        double t = (lambda - _lambdas[o]) / (_lambdas[o+1] - _lambdas[o]);
        return MathmaticMethods.Lerp(t, _values[o], _values[o + 1]);
    }
    public double MaxValue() {
        if(_values.Length == 0) {
            return 0;
        }
        return _values.Max();
    }
    public SampledSpectrumNew Sample(SampledWavelengths lambda) {
        SampledSpectrumNew s = new SampledSpectrumNew();
        for(int i = 0; i < ISpectrumNew.NSpectrumSamples; i++) {
            s[i] = GetValueAtLambda(lambda[i]);
        }
        return s;
    }


    double[] _lambdas, _values;
}
