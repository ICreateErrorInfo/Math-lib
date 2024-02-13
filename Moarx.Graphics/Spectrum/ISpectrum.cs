namespace Moarx.Graphics.Spectrum;
public interface ISpectrum {
    const int LambdaMin = 360, LambdaMax = 830;
    const int NSpectrumSamples = 8;

    public static double Blackbody(double lambda, double t) {
        if (t <= 0) {
            return 0;
        }

        const double c = 299792458;
        const double h = 6.62606957e-34f;
        const double kb = 1.3806488e-23f;

        var l = lambda * 1e-9f;
        var le = 2 * h * c * c / (System.Math.Pow(l, 5) * (System.Math.Exp(h * c / (l * kb * t)) - 1));

        return le;
    }
    public static double InnerProduct(ISpectrum f, ISpectrum g) {
        double integral = 0;
        for (double lambda = LambdaMin; lambda <= LambdaMax; lambda++) {
            integral += f.GetValueAtLambda(lambda) * g.GetValueAtLambda(lambda);
        }
        return integral;
    }

    public abstract double GetValueAtLambda(double lambda);
    public abstract double MaxValue();
    public abstract SampledSpectrum Sample(SampledWavelengths lambda);
}
