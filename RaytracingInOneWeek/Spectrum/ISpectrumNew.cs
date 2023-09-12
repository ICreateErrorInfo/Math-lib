namespace Raytracing.Spectrum;
public interface ISpectrumNew {
    const int LambdaMin = 360, LambdaMax = 830;
    const int NSpectrumSamples = 4;

    public static double Blackbody(double lambda, double t) {
        if(t <= 0) {
            return 0;
        }

        const double c = 299792458;
        const double h = 6.62606957e-34f;
        const double kb = 1.3806488e-23f;

        double l = lambda * 1e-9f;
        double le = (2 * h * c * c) / (System.Math.Pow(l, 5) * (System.Math.Exp((h * c) / (l * kb * t)) - 1));

        return le;
    }
    public static double InnerProduct(ISpectrumNew f, ISpectrumNew g) {
        double integral = 0;
        for (double lambda = LambdaMin; lambda <= LambdaMax; lambda++) {
            integral += f.GetValueAtLambda(lambda) * g.GetValueAtLambda(lambda);
        }
        return integral;
    }

    public abstract double GetValueAtLambda(double lambda);
    public abstract double MaxValue();
    public abstract SampledSpectrumNew Sample(SampledWavelengths lambda);
}
