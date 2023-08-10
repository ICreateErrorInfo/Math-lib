using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib.Spectrum
{
    public class CoefficientSpectrum
    {
        public double[] coefficients;
        public int NumberSamples = 0;

        public CoefficientSpectrum(int nSpectrumSamples, double v = 0)
        {
            NumberSamples = nSpectrumSamples;
            coefficients = new double[nSpectrumSamples];
            for(int i = 0; i < nSpectrumSamples; i++)
            {
                coefficients[i] = v;
            }
            Debug.Assert(!HasNaNs());
            Debug.Assert(!double.IsNaN(NumberSamples));
        }

        public static CoefficientSpectrum Sqrt(CoefficientSpectrum s)
        {
            CoefficientSpectrum ret = new CoefficientSpectrum(s.NumberSamples);
            for (int i = 0; i < s.NumberSamples; i++)
            {
                ret.coefficients[i] = Math.Sqrt(s.coefficients[i]);
            }
            Debug.Assert(!ret.HasNaNs());
            return ret;
        }
        public static CoefficientSpectrum Pow(CoefficientSpectrum s, double n)
        {
            CoefficientSpectrum ret = new CoefficientSpectrum(s.NumberSamples);
            for (int i = 0; i < s.NumberSamples; i++)
            {
                ret.coefficients[i] = Math.Pow(s.coefficients[i], n);
            }
            Debug.Assert(!ret.HasNaNs());
            return ret;
        }
        public static CoefficientSpectrum Exp(CoefficientSpectrum s)
        {
            CoefficientSpectrum ret = new CoefficientSpectrum(s.NumberSamples);
            for (int i = 0; i < s.NumberSamples; i++)
            {
                ret.coefficients[i] = Math.Exp(s.coefficients[i]);
            }
            Debug.Assert(!ret.HasNaNs());
            return ret;
        }
        public void Clamp(double low = 0, double high = double.PositiveInfinity)
        {
            CoefficientSpectrum ret = new CoefficientSpectrum(NumberSamples);
            for (int i = 0; i < NumberSamples; i++)
            {
                ret.coefficients[i] = Math.Clamp(coefficients[i], low, high);
            }
            Debug.Assert(!ret.HasNaNs());
            this.coefficients = ret.coefficients;
        }
        public bool IsBlack()
        {
            Debug.Assert(!this.HasNaNs());
            for (int i = 0; i < NumberSamples; i++)
            {
                if (coefficients[i] != 0) return false;
            }
            return true;
        }
        public double MaxComponentValue()
        {
            double m = 0;
            for (int i = 0; i < NumberSamples; i++)
            {
                m = Math.Max(m, coefficients[i]);
            }
            Debug.Assert(!double.IsNaN(m));
            return m;
        }
        public bool HasNaNs()
        {
            for(int i = 0; i < NumberSamples; i++)
            {
                if (double.IsNaN(coefficients[i]))
                {
                    return true;
                }
            }
            return false;
        }

        public static CoefficientSpectrum operator +(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NumberSamples);
            for (int i = 0; i < s1.NumberSamples; i++) {
                sum.coefficients[i] = s1.coefficients[i] + s2.coefficients[i];
            }

            return sum;
        }
        public static CoefficientSpectrum operator -(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NumberSamples);
            for (int i = 0; i < s1.NumberSamples; i++) {
                sum.coefficients[i] = s1.coefficients[i] - s2.coefficients[i];
            }

            return sum;
        }
        public static CoefficientSpectrum operator *(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NumberSamples);
            for (int i = 0; i < s1.NumberSamples; i++) {
                sum.coefficients[i] = s1.coefficients[i] * s2.coefficients[i];
            }

            return sum;
        }
        public static CoefficientSpectrum operator *(double s2, CoefficientSpectrum s1) {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!double.IsNaN(s2));

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NumberSamples);
            for (int i = 0; i < s1.NumberSamples; i++) {
                sum.coefficients[i] = s1.coefficients[i] * s2;
            }

            return sum;
        }
        public static CoefficientSpectrum operator /(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NumberSamples);
            for (int i = 0; i < s1.NumberSamples; i++)
            {
                Debug.Assert(s2.coefficients[i] != 0);
                sum.coefficients[i] = s1.coefficients[i] / s2.coefficients[i];
            }
            return sum;
        }

        public static bool operator ==(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            for (int i = 0; i < s1.NumberSamples; ++i)
            {
                if (s1.coefficients[i] != s2.coefficients[i]) return false;
            }
            return true;
        }
        public static bool operator !=(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            for (int i = 0; i < s1.NumberSamples; ++i)
            {
                if (s1.coefficients[i] == s2.coefficients[i]) return false;
            }
            return true;
        }

        public static CoefficientSpectrum operator -(CoefficientSpectrum s)
        {
            Debug.Assert(!s.HasNaNs());

            CoefficientSpectrum ret = new CoefficientSpectrum(s.NumberSamples);
            for (int i = 0; i < s.NumberSamples; ++i) ret.coefficients[i] = -s.coefficients[i];
            return ret;
        }

        public double this[int i]
        {
            get
            {
                Debug.Assert(i >= 0 && i < NumberSamples);
                Debug.Assert(!double.IsNaN(i));
                return coefficients[i];
            }
        }

        public override string ToString()
        {
            string str = "[";
            for(int i = 0; i < NumberSamples; i++)
            {
                str += coefficients[i];
                if (i + 1 < NumberSamples) str += ", ";
            }
            str += "]";
            return str;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(this, obj)) {
                return true;
            }

            if (ReferenceEquals(obj, null)) {
                return false;
            }

            throw new NotImplementedException();
        }
        public override int GetHashCode() {
            throw new NotImplementedException();
        }
    }
}
