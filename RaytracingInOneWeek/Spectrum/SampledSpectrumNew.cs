using Raytracing.Color;
using System.CodeDom;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Threading;

namespace Raytracing.Spectrum;
public class SampledSpectrumNew {

    public SampledSpectrumNew() {
        Values = new double[ISpectrumNew.NSpectrumSamples];
    }
    public SampledSpectrumNew(double c) {
        Values = new double[ISpectrumNew.NSpectrumSamples];
        for(int i = 0; i < ISpectrumNew.NSpectrumSamples; i++) {
            Values[i] = c;
        }
    }
    public SampledSpectrumNew(double[] v) {
        for(int i = 0;i < ISpectrumNew.NSpectrumSamples;i++) {
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
    public SampledSpectrumNew SafeDiv(SampledSpectrumNew a, SampledSpectrumNew b) {
        SampledSpectrumNew r = new SampledSpectrumNew();
        for(int i = 0;i <ISpectrumNew.NSpectrumSamples; ++i) {
            r[i] = (b[i] != 0) ? a[i] / b[i] : 0;
        }
        return r;
    }
    public double Average() {
        double sum = Values[0];
        for(int i = 0; i < ISpectrumNew.NSpectrumSamples; i++) {
            sum += Values[i];
        }
        return sum / ISpectrumNew.NSpectrumSamples;
    }
    public XYZ ToXYZ(SampledWavelengths lambda) {
        SampledSpectrumNew X = SampledSpectrumConstants.XNew.Sample(lambda);
        SampledSpectrumNew Y = SampledSpectrumConstants.YNew.Sample(lambda);
        SampledSpectrumNew Z = SampledSpectrumConstants.ZNew.Sample(lambda);

        SampledSpectrumNew pdf = lambda.PDF();
        return new XYZ(SafeDiv(X * this, pdf).Average(),
                       SafeDiv(Y * this, pdf).Average(),
                       SafeDiv(Z * this, pdf).Average()) / SampledSpectrumConstants.CIE_Y_integral;
    }                  
    public RGB ToRGB(SampledWavelengths lambda, RGBColorSpace cs) {
        XYZ xyz = ToXYZ(lambda);
        return cs.ToRGB(xyz);
    }

    public static SampledSpectrumNew operator +(SampledSpectrumNew s1, SampledSpectrumNew s2) {
        SampledSpectrumNew ret = new SampledSpectrumNew();

        for(int i = 0; i < ISpectrumNew.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] + s2.Values[i];
        }
        return s1;
    }

    public static SampledSpectrumNew operator -(SampledSpectrumNew s1, SampledSpectrumNew s2) {
        SampledSpectrumNew ret = new SampledSpectrumNew();

        for (int i = 0; i < ISpectrumNew.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] - s2.Values[i];
        }
        return s1;
    }
    public static SampledSpectrumNew operator -(double a, SampledSpectrumNew s1) {
        SampledSpectrumNew ret = new SampledSpectrumNew();

        for (int i = 0; i < ISpectrumNew.NSpectrumSamples; ++i) {
            ret.Values[i] = a - s1.Values[i];
        }
        return s1;
    }

    public static SampledSpectrumNew operator *(SampledSpectrumNew s1, SampledSpectrumNew s2) {
        SampledSpectrumNew ret = new SampledSpectrumNew();

        for (int i = 0; i < ISpectrumNew.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] * s2.Values[i];
        }
        return s1;
    }
    public static SampledSpectrumNew operator *(double a, SampledSpectrumNew s1) {
        SampledSpectrumNew ret = new SampledSpectrumNew();

        for (int i = 0; i < ISpectrumNew.NSpectrumSamples; ++i) {
            ret.Values[i] = a * s1.Values[i];
        }
        return s1;
    }

    public static SampledSpectrumNew operator /(SampledSpectrumNew s1, SampledSpectrumNew s2) {
        SampledSpectrumNew ret = new SampledSpectrumNew();

        for (int i = 0; i < ISpectrumNew.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] / s2.Values[i];
        }
        return s1;
    }
    public static SampledSpectrumNew operator /( SampledSpectrumNew s1, double a) {
        SampledSpectrumNew ret = new SampledSpectrumNew();

        for (int i = 0; i < ISpectrumNew.NSpectrumSamples; ++i) {
            ret.Values[i] = s1.Values[i] / a;
        }
        return s1;
    }

    public double this[int i] {
        get { return Values[i]; }       
        set { Values[i] = value; }
    }

    public double[] Values;
}
