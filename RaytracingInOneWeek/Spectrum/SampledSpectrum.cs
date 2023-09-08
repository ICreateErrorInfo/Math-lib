using Moarx.Math;
using System;
using System.Collections.Immutable;

namespace Raytracing.Spectrum;
public class SampledSpectrum: ISpectrum {
    public double[] coefficients { get; set; }
    public int NumberSamples { get; set; }

    public SampledSpectrum(double v) {
        NumberSamples = SampledSpectrumConstants.nSpectralSamples;
        coefficients = new double[NumberSamples];
        for (var i = 0; i < NumberSamples; i++) {
            coefficients[i] = v;
        }
    }
    public SampledSpectrum(){
        NumberSamples = SampledSpectrumConstants.nSpectralSamples;
        coefficients = new double[NumberSamples];
    }

    public ISpectrum CreateNew() {
        return new SampledSpectrum();
    }
    public ISpectrum ToIspectrum() {
        ISpectrum s = this;
        return s;
    }

    private double[] ToXYZ() {
        var xyz = new double[3];
        xyz[0] = xyz[1] = xyz[2] = 0;

        for (var i = 0; i < NumberSamples; ++i) {
            xyz[0] += SampledSpectrumConstants.X.coefficients[i] * coefficients[i];
            xyz[1] += SampledSpectrumConstants.Y.coefficients[i] * coefficients[i];
            xyz[2] += SampledSpectrumConstants.Z.coefficients[i] * coefficients[i];
        }
        double scale = (double)(SampledSpectrumConstants.sampledLambdaEnd - SampledSpectrumConstants.sampledLambdaStart) /
                  (double)(SampledSpectrumConstants.CIE_Y_integral * NumberSamples);

        xyz[0] *= scale;
        xyz[1] *= scale;
        xyz[2] *= scale;

        return xyz;
    }
    private double y() {
        double yy = 0;

        for (var i = 0; i < NumberSamples; i++) {
            yy += SampledSpectrumConstants.Y.coefficients[i] * coefficients[i];
        }

        return yy * (SampledSpectrumConstants.sampledLambdaEnd - SampledSpectrumConstants.sampledLambdaStart) /
                    NumberSamples;
    }
    private double[] XYZToRGB(double[] xyz) {

        if (xyz.Length != 3) {
            throw new ArgumentException();
        }

        var rgb = new double[3];

        rgb[0] = 3.240479f * xyz[0] - 1.537150f * xyz[1] - 0.498535f * xyz[2];
        rgb[1] = -0.969256f * xyz[0] + 1.875991f * xyz[1] + 0.041556f * xyz[2];
        rgb[2] = 0.055648f * xyz[0] - 0.204043f * xyz[1] + 1.057311f * xyz[2];

        return rgb;
    }
    private double[] RGBToXYZ(double[] rgb) {
        if (rgb.Length != 3) {
            throw new ArgumentException();
        }

        var xyz = new double[3];

        xyz[0] = 0.412453f * rgb[0] + 0.357580f * rgb[1] + 0.180423f * rgb[2];
        xyz[1] = 0.212671f * rgb[0] + 0.715160f * rgb[1] + 0.072169f * rgb[2];
        xyz[2] = 0.019334f * rgb[0] + 0.119193f * rgb[1] + 0.950227f * rgb[2];

        return xyz;
    }
    public double[] ToRGB() {

        var xyz = new double[3];
        xyz = ToXYZ();
        var rgb = new double[3];
        rgb = XYZToRGB(xyz);

        rgb[0] *= .8;

        return rgb;
    }

    //TODO
    //public static SampledSpectrum FromSampled(ImmutableArray<double> lambda, ImmutableArray<double> v, int n) {
    //    if (!SpectrumMethods.SpectrumSamplesSorted(lambda, v, n)) {
    //        var slambda = lambda.ToImmutableArray();
    //        var sv = v.ToImmutableArray();
    //        SpectrumMethods.SortSpectrumSamples(slambda, sv, n);
    //        return FromSampled(slambda, sv, n);
    //    }

    //    SampledSpectrum r = new SampledSpectrum();
    //    for (int i = 0; i < nSpectralSamples; i++) {
    //        double lambda0 = Mathe.Lerp((double)(i) / nSpectralSamples, sampledLambdaStart, sampledLambdaEnd);
    //        double lambda1 = Mathe.Lerp((double)(i + 1) / nSpectralSamples, sampledLambdaStart, sampledLambdaEnd);

    //        r.coefficients[i] = SpectrumMethods.AverageSpectrumSamples(lambda, v, n, lambda0, lambda1);
    //    }
    //    return r;
    //}
    //public static bool SpectrumSamplesSorted(ImmutableArray<double> lambda, ImmutableArray<double> values, int n)
    //{
    //    for (int i = 0; i < n - 1; ++i)
    //    {
    //        if (lambda[i] > lambda[i + 1])
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}
    //public static void SortSpectrumSamples(double[] lambda, double[] vals, int n)
    //{
    //    List<Tuple<double, double>> sortVec = new List<Tuple<double, double>>();
    //    for(int i = 0; i < n; i++)
    //    {
    //        sortVec.Add(new Tuple<double, double>(lambda[i], vals[i]));
    //    }

    //    sortVec.Sort();

    //    for(int i = 0; i < n; i++)
    //    {
    //        lambda[i] = sortVec[i].Item1;
    //        vals[i]   = sortVec[i].Item2;
    //    }
    //}
    public static double AverageSpectrumSamples(ImmutableArray<double> lambda, ImmutableArray<double> vals, int n, double lambdaStart, double lambdaEnd) {
        if (lambdaEnd <= lambda[0])
            return vals[0];
        if (lambdaStart >= lambda[n - 1])
            return vals[n - 1];
        if (n == 1)
            return vals[0];
        double sum = 0;

        if (lambdaStart < lambda[0]) {
            sum += vals[0] * (lambda[0] - lambdaStart);
        }
        if (lambdaEnd > lambda[n - 1]) {
            sum += vals[n - 1] * (lambdaEnd - lambda[n - 1]);
        }

        int i = 0;
        while (lambdaStart > lambda[i + 1])
            ++i;

        Func<double, int, double> interp = (double w, int i) =>
        {
            return MathmaticMethods.Lerp((double)(w - lambda[i]) / (lambda[i + 1] - lambda[i]), vals[i], vals[i + 1]);
        };

        for (; i + 1 < n && lambdaEnd >= lambda[i]; ++i) {
            double segLambdaStart = Math.Max(lambdaStart, lambda[i]);
            double segLambdaEnd = Math.Min(lambdaEnd, lambda[i + 1]);
            sum += 0.5 * (interp(segLambdaStart, i) + interp(segLambdaEnd, i)) *
                   (segLambdaEnd - segLambdaStart);
        }
        return (double)sum / (lambdaEnd - lambdaStart);
    }

    public static SampledSpectrum FromRGB(double[] rgb, SpectrumMaterialType type) {
        ISpectrum r = new SampledSpectrum();
        if (type == SpectrumMaterialType.Reflectance) {
            if (rgb[0] <= rgb[1] && rgb[0] <= rgb[2]) {

                r += rgb[0] * SampledSpectrumConstants.rgbRefl2SpectWhite.ToIspectrum();
                if (rgb[1] <= rgb[2]) {
                    r += (rgb[1] - rgb[0]) * SampledSpectrumConstants.rgbRefl2SpectCyan.ToIspectrum();
                    r += (rgb[2] - rgb[1]) * SampledSpectrumConstants.rgbRefl2SpectBlue.ToIspectrum();
                } else {
                    r += (rgb[2] - rgb[0]) * SampledSpectrumConstants.rgbRefl2SpectCyan.ToIspectrum();
                    r += (rgb[1] - rgb[2]) * SampledSpectrumConstants.rgbRefl2SpectGreen.ToIspectrum();
                }
            } else if (rgb[1] <= rgb[0] && rgb[1] <= rgb[2]) {

                r += rgb[1] * SampledSpectrumConstants.rgbRefl2SpectWhite.ToIspectrum();
                if (rgb[0] <= rgb[2]) {
                    r += (rgb[0] - rgb[1]) * SampledSpectrumConstants.rgbRefl2SpectMagenta.ToIspectrum();
                    r += (rgb[2] - rgb[0]) * SampledSpectrumConstants.rgbRefl2SpectBlue.ToIspectrum();
                } else {
                    r += (rgb[2] - rgb[1]) * SampledSpectrumConstants.rgbRefl2SpectMagenta.ToIspectrum();
                    r += (rgb[0] - rgb[2]) * SampledSpectrumConstants.rgbRefl2SpectRed.ToIspectrum();
                }
            } else {

                r += rgb[2] * SampledSpectrumConstants.rgbRefl2SpectWhite.ToIspectrum();
                if (rgb[0] <= rgb[1]) {
                    r += (rgb[0] - rgb[2]) * SampledSpectrumConstants.rgbRefl2SpectYellow.ToIspectrum();
                    r += (rgb[1] - rgb[0]) * SampledSpectrumConstants.rgbRefl2SpectGreen.ToIspectrum();
                } else {
                    r += (rgb[1] - rgb[2]) * SampledSpectrumConstants.rgbRefl2SpectYellow.ToIspectrum();
                    r += (rgb[0] - rgb[1]) * SampledSpectrumConstants.rgbRefl2SpectRed.ToIspectrum();
                }
            }
            r = .94 * r;
        } else {

            if (rgb[0] <= rgb[1] && rgb[0] <= rgb[2]) {

                r += rgb[0] * SampledSpectrumConstants.rgbIllum2SpectWhite.ToIspectrum();
                if (rgb[1] <= rgb[2]) {
                    r += (rgb[1] - rgb[0]) * SampledSpectrumConstants.rgbIllum2SpectCyan.ToIspectrum();
                    r += (rgb[2] - rgb[1]) * SampledSpectrumConstants.rgbIllum2SpectBlue.ToIspectrum();
                } else {
                    r += (rgb[2] - rgb[0]) * SampledSpectrumConstants.rgbIllum2SpectCyan.ToIspectrum();
                    r += (rgb[1] - rgb[2]) * SampledSpectrumConstants.rgbIllum2SpectGreen.ToIspectrum();
                }
            } else if (rgb[1] <= rgb[0] && rgb[1] <= rgb[2]) {

                r += rgb[1] * SampledSpectrumConstants.rgbIllum2SpectWhite.ToIspectrum();
                if (rgb[0] <= rgb[2]) {
                    r += (rgb[0] - rgb[1]) * SampledSpectrumConstants.rgbIllum2SpectMagenta.ToIspectrum();
                    r += (rgb[2] - rgb[0]) * SampledSpectrumConstants.rgbIllum2SpectBlue.ToIspectrum();
                } else {
                    r += (rgb[2] - rgb[1]) * SampledSpectrumConstants.rgbIllum2SpectMagenta.ToIspectrum();
                    r += (rgb[0] - rgb[2]) * SampledSpectrumConstants.rgbIllum2SpectRed.ToIspectrum();
                }
            } else {

                r += rgb[2] * SampledSpectrumConstants.rgbIllum2SpectWhite.ToIspectrum();
                if (rgb[0] <= rgb[1]) {
                    r += (rgb[0] - rgb[2]) * SampledSpectrumConstants.rgbIllum2SpectYellow.ToIspectrum();
                    r += (rgb[1] - rgb[0]) * SampledSpectrumConstants.rgbIllum2SpectGreen.ToIspectrum();
                } else {
                    r += (rgb[1] - rgb[2]) * SampledSpectrumConstants.rgbIllum2SpectYellow.ToIspectrum();
                    r += (rgb[0] - rgb[1]) * SampledSpectrumConstants.rgbIllum2SpectRed.ToIspectrum();
                }
            }
            r = .86445f * r;
        }

        var s = new SampledSpectrum();
        r.Clamp();
        s.coefficients = r.coefficients;

        return s;
    }
    public SampledSpectrum FromXYZ(double[] xyz, SpectrumMaterialType type = SpectrumMaterialType.Reflectance) {
        var rgb = new double[3];
        rgb = XYZToRGB(xyz);
        return FromRGB(rgb, type);
    }
}
