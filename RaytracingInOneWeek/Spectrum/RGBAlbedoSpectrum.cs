using Raytracing.Color;

namespace Raytracing.Spectrum; 
public class RGBAlbedoSpectrum: ISpectrumNew {

    public RGBAlbedoSpectrum(RGBColorSpace cs, RGB rgb) {
        rsp = cs.ToRGBCoeffs(rgb);
    }

    public double GetValueAtLambda(double lambda) {
        return rsp.GetValueAtLambda(lambda);
    }
    public double MaxValue() {
       return rsp.MaxValue();
    }
    public SampledSpectrumNew Sample(SampledWavelengths lambda) {
        SampledSpectrumNew s = new SampledSpectrumNew();
        for(int i = 0; i < ISpectrumNew.NSpectrumSamples; i++) {
            s[i] = rsp.GetValueAtLambda(lambda[i]);
        }
        return s;
    }

    RGBSigmoidPolynomial rsp;
}
