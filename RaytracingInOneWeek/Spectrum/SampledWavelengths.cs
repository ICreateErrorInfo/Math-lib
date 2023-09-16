using Moarx.Math;

namespace Raytracing.Spectrum; 
public class SampledWavelengths {

    public SampledWavelengths() {
        lambda = new double[ISpectrum.NSpectrumSamples];
        pdf = new double[ISpectrum.NSpectrumSamples];
    }

    public static SampledWavelengths SampleUnifrom(double u, double lambdaMin = ISpectrum.LambdaMin, double lambdaMax = ISpectrum.LambdaMax) {
        SampledWavelengths swl = new SampledWavelengths();

        swl.lambda[0] = MathmaticMethods.Lerp(u, lambdaMin, lambdaMax);

        double delta = (lambdaMax - lambdaMin) / ISpectrum.NSpectrumSamples;
        for(int i = 1; i < ISpectrum.NSpectrumSamples; i++) {
            swl.lambda[i] = swl.lambda[i - 1] + delta;
            if (swl.lambda[i] > lambdaMax) {
                swl.lambda[i] = lambdaMin + (swl.lambda[i] - lambdaMax);
            }
        }

        for(int i = 0; i < ISpectrum.NSpectrumSamples; i++) {
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

        for (int i = 1; i < ISpectrum.NSpectrumSamples; ++i)
            pdf[i] = 0;
        pdf[0] /= ISpectrum.NSpectrumSamples;
    }
    public bool SecondaryTerminated() {
        for (int i = 1; i < ISpectrum.NSpectrumSamples; ++i)
            if (pdf[i] != 0)
                return false;
        return true;
    }

    public double this[int i] {
        get {
            return lambda[i];
        }
    }

    double[] lambda, pdf;
}
