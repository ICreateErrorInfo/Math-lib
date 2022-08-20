using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.Spectrum
{
    public class SampledSpectrum : CoefficientSpectrum
    {
        //TODO
        const int nCIESamples = 471;
        static readonly double[] CIEX = new double[]
        {

        };
        static readonly double[] CIEY = new double[]
        {

        };
        static readonly double[] CIEZ = new double[]
        {

        };
        static readonly double[] CIELambda = new double[]
        {

        };

        private static SampledSpectrum X, Y, Z;

        private const int sampledLambdaStart = 400;
        private const int sampledLambdaEnd = 700;
        private const int nSpectralSamples = 60;

        public SampledSpectrum(double v = 0) : base(nSpectralSamples, v)
        {

        }

        static void Init()
        {
            for (int i = 0; i < nSpectralSamples; ++i)
            {
                double wl0 = Mathe.Lerp((double)i / (double)nSpectralSamples,
                                 sampledLambdaStart, sampledLambdaEnd);
                double wl1 = Mathe.Lerp((double)(i + 1) / (double)nSpectralSamples,
                                 sampledLambdaStart, sampledLambdaEnd);
                X.c[i] = SpectrumMethods.AverageSpectrumSamples(CIELambda, CIEX, nCIESamples,
                                                wl0, wl1);
                Y.c[i] = SpectrumMethods.AverageSpectrumSamples(CIELambda, CIEY, nCIESamples,
                                                wl0, wl1);
                Z.c[i] = SpectrumMethods.AverageSpectrumSamples(CIELambda, CIEZ, nCIESamples,
                                                wl0, wl1);
            }
        }
        static SampledSpectrum FromSampled(double[] lambda, double[] v, int n)
        {
            if(!SpectrumMethods.SpectrumSamplesSorted(lambda, v, n))
            {
                double[] slambda = lambda;
                double[] sv = v;
                SpectrumMethods.SortSpectrumSamples(ref slambda, ref sv, n);
                return FromSampled(slambda, sv, n);
            }

            SampledSpectrum r = new SampledSpectrum();
            for(int i = 0; i < nSpectralSamples; i++)
            {
                double lambda0 = Mathe.Lerp(i / nSpectralSamples, sampledLambdaStart, sampledLambdaEnd);
                double lambda1 = Mathe.Lerp(i + 1 / nSpectralSamples, sampledLambdaStart, sampledLambdaEnd);

                r.c[i] = SpectrumMethods.AverageSpectrumSamples(lambda, v, n, lambda0, lambda1);
            }
            return r;
        }
    }
}
