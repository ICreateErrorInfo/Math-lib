using Raytracing.Color;

namespace Raytracing.Spectrum;
internal class RGBIlluminantSpectrum: ISpectrum {

    public RGBIlluminantSpectrum(RGBColorSpace cs, RGB rgb) {
        _illuminant = new(cs.Illuminant);
        double m = System.Math.Max(rgb.R, System.Math.Max(rgb.G, rgb.B));

        _scale = 2 * m;

        _rsp = cs.ToRGBCoeffs(_scale != 0 ? rgb / _scale : new RGB(0, 0, 0));
    }

    public double GetValueAtLambda(double lambda) {
        if(_illuminant == null) {
            return 0;
        }
        return _scale * _rsp.GetValueAtLambda(lambda) * _illuminant.GetValueAtLambda(lambda);
    }
    public double MaxValue() {
        if (_illuminant == null) {
            return 0;
        }
        return _scale * _rsp.MaxValue() * _illuminant.MaxValue();
    }
    public SampledSpectrum Sample(SampledWavelengths lambda) {
        if(_illuminant == null) {
            return new SampledSpectrum(0);
        }
        SampledSpectrum s = new SampledSpectrum();

        for(int i = 0; i < ISpectrum.NSpectrumSamples; i++) {
            s[i] = _scale * _rsp.GetValueAtLambda(lambda[i]);
        }

        return s * _illuminant.Sample(lambda);
    }

    double _scale;
    RGBSigmoidPolynomial _rsp;
    DenselySampledSpectrum _illuminant;
}
