namespace Moarx.Graphics.Color; 
public class RGBSigmoidPolynomial {

    public RGBSigmoidPolynomial(double c0, double c1, double c2) {
        _c0 = c0;
        _c1 = c1;
        _c2 = c2;
    }

    public double GetValueAtLambda(double lambda) {
        return Sigmoid((_c0*lambda*lambda) + (_c1 * lambda) + _c2);
    }
    public double MaxValue() {
        double result = System.Math.Max(GetValueAtLambda(360), GetValueAtLambda(830));
        double lambda = -_c1 / (2 * _c0);
        if (lambda >= 360 && lambda <= 830)
            result = System.Math.Max(result, GetValueAtLambda(lambda));
        return result;
    }
    public static double Sigmoid(double x) {
        if(double.IsInfinity(x)) return x > 0 ? 1 : 0;
        return 0.5 + x / (2 * System.Math.Sqrt(1 + (x * x)));
    }

    double _c0, _c1, _c2;
}
