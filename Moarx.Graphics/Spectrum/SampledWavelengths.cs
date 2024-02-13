using Moarx.Math;

namespace Moarx.Graphics.Spectrum;
public class SampledWavelengths {

    public SampledWavelengths() {
        lambda = new double[ISpectrum.NSpectrumSamples];
        pdf = new double[ISpectrum.NSpectrumSamples];
    }

    public static SampledWavelengths SampleUnifrom(double u, double lambdaMin = ISpectrum.LambdaMin, double lambdaMax = ISpectrum.LambdaMax) {
        var swl = new SampledWavelengths();

        swl.lambda[0] = MathmaticMethods.Lerp(u, lambdaMin, lambdaMax);

        var delta = (lambdaMax - lambdaMin) / ISpectrum.NSpectrumSamples;
        for (var i = 1; i < ISpectrum.NSpectrumSamples; i++) {
            swl.lambda[i] = swl.lambda[i - 1] + delta;
            if (swl.lambda[i] > lambdaMax) {
                swl.lambda[i] = lambdaMin + (swl.lambda[i] - lambdaMax);
            }
        }

        for (var i = 0; i < ISpectrum.NSpectrumSamples; i++) {
            swl.pdf[i] = 1 / (lambdaMax - lambdaMin);
        }

        return swl;
    }
    public SampledSpectrum PDF() {
        return new SampledSpectrum(pdf);
    }

    public void TerminateSecondary() {
        if (SecondaryTerminated())
            return;

        for (var i = 1; i < ISpectrum.NSpectrumSamples; ++i)
            pdf[i] = 0;
        pdf[0] /= ISpectrum.NSpectrumSamples;
    }
    public bool SecondaryTerminated() {
        for (var i = 1; i < ISpectrum.NSpectrumSamples; ++i)
            if (pdf[i] != 0)
                return false;
        return true;
    }

    public double this[int i] {
        get {
            return lambda[i];
        }
        set {
            lambda[i] = value;
            return;
        }
    }

    double[] lambda, pdf;
}
