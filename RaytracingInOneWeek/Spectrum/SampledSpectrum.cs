using Raytracing.Color;
using System.CodeDom;
using System.DirectoryServices.ActiveDirectory;
using System.Net.Http.Headers;
using System.Windows.Threading;

namespace Raytracing.Spectrum;
public class SampledSpectrum {

    public SampledSpectrum() {
        Values = new double[ISpectrum.NSpectrumSamples];
    }
    public SampledSpectrum(double c) {
        Values = new double[ISpectrum.NSpectrumSamples];
        for(int i = 0; i < ISpectrum.NSpectrumSamples; i++) {
            Values[i] = c;
        }
    }
    public SampledSpectrum(double[] v) {
        Values = new double[ISpectrum.NSpectrumSamples];
        for(int i = 0;i < ISpectrum.NSpectrumSamples;i++) {
            Values[i] = v[i];
        }
    }

    public bool HasZeros() {
        foreach(var v in Values) {
            if (v != 0)
                return true;
        }
        return false;
    }
    public static SampledSpectrum SafeDiv(SampledSpectrum a, SampledSpectrum b) {
        SampledSpectrum r = new SampledSpectrum();
        for(int i = 0;i <ISpectrum.NSpectrumSamples; ++i) {
            r[i] = (b[i] != 0) ? a[i] / b[i] : 0;
        }
        return r;
    }
    public double Average() {
        double sum = Values[0];
        for(int i = 1; i < ISpectrum.NSpectrumSamples; i++) {
            sum += Values[i];
        }
        return sum / ISpectrum.NSpectrumSamples;
    }
    public XYZ ToXYZ(SampledWavelengths lambda) {
        SampledSpectrum X = SampledSpectrumConstants.XNew.Sample(lambda);
        SampledSpectrum Y = SampledSpectrumConstants.YNew.Sample(lambda);
        SampledSpectrum Z = SampledSpectrumConstants.ZNew.Sample(lambda);

        SampledSpectrum pdf = lambda.PDF();
        return new XYZ(SafeDiv((X * this), pdf).Average(),
                       SafeDiv((Y * this), pdf).Average(),
                       SafeDiv((Z * this), pdf).Average()) / SampledSpectrumConstants.CIE_Y_integral;
    }                  
    public RGB ToRGB(SampledWavelengths lambda, RGBColorSpace cs) {
        XYZ xyz = ToXYZ(lambda);
        var tmp = cs.ToRGB(xyz);
        tmp[0] *= 0.8;

        return tmp;
    }

    public static SampledSpectrum operator +(SampledSpectrum s1, SampledSpectrum s2) {
        SampledSpectrum ret = new SampledSpectrum();

        for(int i = 0; i < ISpectrum.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] + s2.Values[i];
        }
        return ret;
    }

    public static SampledSpectrum operator -(SampledSpectrum s1, SampledSpectrum s2) {
        SampledSpectrum ret = new SampledSpectrum();

        for (int i = 0; i < ISpectrum.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] - s2.Values[i];
        }
        return ret;
    }
    public static SampledSpectrum operator -(double a, SampledSpectrum s1) {
        SampledSpectrum ret = new SampledSpectrum();

        for (int i = 0; i < ISpectrum.NSpectrumSamples; ++i) {
            ret.Values[i] = a - s1.Values[i];
        }
        return ret;
    }

    public static SampledSpectrum operator *(SampledSpectrum s1, SampledSpectrum s2) {
        SampledSpectrum ret = new SampledSpectrum();

        for (int i = 0; i < ISpectrum.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] * s2.Values[i];
        }
        return ret;
    }
    public static SampledSpectrum operator *(double a, SampledSpectrum s1) {
        SampledSpectrum ret = new SampledSpectrum();

        for (int i = 0; i < ISpectrum.NSpectrumSamples; ++i) {
            ret.Values[i] = a * s1.Values[i];
        }
        return ret;
    }

    public static SampledSpectrum operator /(SampledSpectrum s1, SampledSpectrum s2) {
        SampledSpectrum ret = new SampledSpectrum();

        for (int i = 0; i < ISpectrum.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] / s2.Values[i];
        }
        return ret;
    }
    public static SampledSpectrum operator /( SampledSpectrum s1, double a) {
        SampledSpectrum ret = new SampledSpectrum();

        for (int i = 0; i < ISpectrum.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] / a;
        }
        return ret;
    }

    public double this[int i] {
        get { return Values[i]; }       
        set { Values[i] = value; }
    }

    public double[] Values;
}
