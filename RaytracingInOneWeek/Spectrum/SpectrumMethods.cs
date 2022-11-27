using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.Spectrum
{
    public static class SpectrumMethods
    {
        public static bool SpectrumSamplesSorted(double[] lambda, double[] values, int n)
        {
            for (int i = 0; i < n - 1; ++i)
            {
                if (lambda[i] > lambda[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
        public static void SortSpectrumSamples(ref double[] lambda, ref double[] vals, int n)
        {
            List<Tuple<double, double>> sortVec = new List<Tuple<double, double>>();
            for(int i = 0; i < n; i++)
            {
                sortVec.Add(new Tuple<double, double>(lambda[i], vals[i]));
            }

            sortVec.Sort();

            for(int i = 0; i < n; i++)
            {
                lambda[i] = sortVec[i].Item1;
                vals[i]   = sortVec[i].Item2;
            }
        }
        public static double AverageSpectrumSamples(double[] lambda, double[] vals, int n, double lambdaStart, double lambdaEnd)
        {
            if (lambdaEnd <= lambda[0]) return vals[0];
            if (lambdaStart >= lambda[n - 1]) return vals[n - 1];
            if (n == 1) return vals[0];
            double sum = 0;

            if (lambdaStart < lambda[0])
            {
                sum += vals[0] * (lambda[0] - lambdaStart);
            }
            if (lambdaEnd > lambda[n - 1])
            {
                sum += vals[n - 1] * (lambdaEnd - lambda[n - 1]);
            }

            int i = 0;
            while (lambdaStart > lambda[i + 1]) ++i;

            Func<double, int, double> interp = (double w, int i) =>
            {
                return Mathe.Lerp((double)(w - lambda[i]) / (lambda[i + 1] - lambda[i]), vals[i], vals[i + 1]);
            };

            for (; i + 1 < n && lambdaEnd >= lambda[i]; ++i)
            {
                double segLambdaStart = Math.Max(lambdaStart, lambda[i]);
                double segLambdaEnd = Math.Min(lambdaEnd, lambda[i + 1]);
                sum += 0.5 * (interp(segLambdaStart, i) + interp(segLambdaEnd, i)) *
                       (segLambdaEnd - segLambdaStart);
            }
            return (double)sum / (lambdaEnd - lambdaStart);
        }
    }
}
