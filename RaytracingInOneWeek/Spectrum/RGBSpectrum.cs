namespace Raytracing.Spectrum;
public class RGBSpectrum: ISpectrum {
    public double[] coefficients { get; set; }
    public int NumberSamples { get; set; }

    public RGBSpectrum(double v) {
        NumberSamples = 3;
        coefficients[0] = v;
        coefficients[1] = v;
        coefficients[2] = v;
    }
    public RGBSpectrum() {
        NumberSamples = 3;
        coefficients = new double[NumberSamples];
    }

    public ISpectrum CreateNew() => new RGBSpectrum();
    public ISpectrum ToIspectrum() {
        ISpectrum s = this;
        return s;
    }
    public double[] ToRGB() {
        double[] rgb = new double[NumberSamples];

        rgb[0] = coefficients[0];
        rgb[1] = coefficients[1];
        rgb[2] = coefficients[2];

        return rgb;
    }
    public static RGBSpectrum FromRGB(double[] rgb) {
        RGBSpectrum spectrum = new RGBSpectrum();

        spectrum.coefficients[0] = rgb[0];
        spectrum.coefficients[1] = rgb[1];
        spectrum.coefficients[2] = rgb[2];

        return spectrum;
    }
}
