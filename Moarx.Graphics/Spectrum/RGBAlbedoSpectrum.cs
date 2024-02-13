using Moarx.Graphics.Color;

namespace Moarx.Graphics.Spectrum;
public class RGBAlbedoSpectrum: ISpectrum {

    public RGBAlbedoSpectrum(RGBColorSpace cs, RGB rgb) {
        rsp = cs.ToRGBCoeffs(rgb);
    }

    public double GetValueAtLambda(double lambda) {
        return rsp.GetValueAtLambda(lambda);
    }
    public double MaxValue() {
        return rsp.MaxValue();
    }
    public SampledSpectrum Sample(SampledWavelengths lambda) {
        var s = new SampledSpectrum();
        for (var i = 0; i < ISpectrum.NSpectrumSamples; i++) {
            s[i] = rsp.GetValueAtLambda(lambda[i]);
        }
        return s;
    }

    RGBSigmoidPolynomial rsp;
}
