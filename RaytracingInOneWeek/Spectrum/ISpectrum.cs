using System;
using System.Diagnostics;

namespace Raytracing.Spectrum;
public interface ISpectrum {
    public double[] coefficients { get; set; }
    public int NumberSamples { get; set; }

    public ISpectrum Sqrt() {
        for (var i = 0; i < NumberSamples; i++) {
            coefficients[i] = Math.Sqrt(coefficients[i]);
        }
        return this;
    }
    public ISpectrum Pow(double n) {
        for (var i = 0; i < NumberSamples; i++) {
            coefficients[i] = Math.Pow(coefficients[i], n);
        }
        return this;
    }
    public ISpectrum Exp() {
        for (var i = 0; i < NumberSamples; i++) {
            coefficients[i] = Math.Exp(coefficients[i]);
        }
        return this;
    }
    public void Clamp(double low = 0, double high = double.PositiveInfinity) {
        for (var i = 0; i < NumberSamples; i++) {
            coefficients[i] = Math.Clamp(coefficients[i], low, high);
        }
    }
    public bool IsBlack() {
        Debug.Assert(!HasNaNs());
        for (var i = 0; i < NumberSamples; i++) {
            if (coefficients[i] != 0)
                return false;
        }
        return true;
    }
    public double MaxComponentValue() {
        double m = 0;
        for (var i = 0; i < NumberSamples; i++) {
            m = Math.Max(m, coefficients[i]);
        }
        Debug.Assert(!double.IsNaN(m));
        return m;
    }
    public bool HasNaNs() {
        for (var i = 0; i < NumberSamples; i++) {
            if (double.IsNaN(coefficients[i])) {
                return true;
            }
        }
        return false;
    }

    public abstract double[] ToRGB();
    public abstract ISpectrum Copy();
    public abstract ISpectrum ToIspectrum();

    public static ISpectrum operator +(ISpectrum s1, ISpectrum s2) {
        Debug.Assert(!s1.HasNaNs());
        Debug.Assert(!s2.HasNaNs());

        var sum = s1.Copy();
        for (var i = 0; i < s1.NumberSamples; i++) {
            sum.coefficients[i] += s2.coefficients[i];
        }

        return sum;
    }
    public static ISpectrum operator -(ISpectrum s1, ISpectrum s2) {
        Debug.Assert(!s1.HasNaNs());
        Debug.Assert(!s2.HasNaNs());

        var sum = s1.Copy();
        for (var i = 0; i < s1.NumberSamples; i++) {
            sum.coefficients[i] -= s2.coefficients[i];
        }

        return sum;
    }
    public static ISpectrum operator *(ISpectrum s1, ISpectrum s2) {
        Debug.Assert(!s1.HasNaNs());
        Debug.Assert(!s2.HasNaNs());

        var sum = s1.Copy();
        for (var i = 0; i < s1.NumberSamples; i++) {
            sum.coefficients[i] *= s2.coefficients[i];
        }

        return sum;
    }
    public static ISpectrum operator *(double s2, ISpectrum s1) {
        Debug.Assert(!s1.HasNaNs());
        Debug.Assert(!double.IsNaN(s2));

        var sum = s1.Copy();
        for (var i = 0; i < s1.NumberSamples; i++) {
            sum.coefficients[i] *= s2;
        }

        return sum;
    }
    public static ISpectrum operator /(ISpectrum s1, ISpectrum s2) {
        Debug.Assert(!s1.HasNaNs());
        Debug.Assert(!s2.HasNaNs());

        var sum = s1.Copy();
        for (var i = 0; i < s1.NumberSamples; i++) {
            Debug.Assert(s2.coefficients[i] != 0);
            sum.coefficients[i] /= s2.coefficients[i];
        }
        return sum;
    }

    public static ISpectrum operator -(ISpectrum s) {
        Debug.Assert(!s.HasNaNs());

        for (var i = 0; i < s.NumberSamples; ++i)
            s.coefficients[i] = -s.coefficients[i];
        return s;
    }

    public double this[int i] {
        get {
            Debug.Assert(i >= 0 && i < NumberSamples);
            Debug.Assert(!double.IsNaN(i));
            return coefficients[i];
        }
    }
}
