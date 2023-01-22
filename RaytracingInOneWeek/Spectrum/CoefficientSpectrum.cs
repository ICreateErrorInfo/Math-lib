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
        public double[] c;
        public int NSamples = 0;

        public CoefficientSpectrum(int nSpectrumSamples, double v = 0)
        {
            NSamples = nSpectrumSamples;
            c = new double[nSpectrumSamples];
            for(int i = 0; i < nSpectrumSamples; i++)
            {
                c[i] = v;
            }
            Debug.Assert(!HasNaNs());
            Debug.Assert(!double.IsNaN(NSamples));
        }

        public static CoefficientSpectrum Sqrt(CoefficientSpectrum s)
        {
            CoefficientSpectrum ret = new CoefficientSpectrum(s.NSamples);
            for (int i = 0; i < s.NSamples; i++)
            {
                ret.c[i] = Math.Sqrt(s.c[i]);
            }
            Debug.Assert(!ret.HasNaNs());
            return ret;
        }
        public static CoefficientSpectrum Pow(CoefficientSpectrum s, double n)
        {
            CoefficientSpectrum ret = new CoefficientSpectrum(s.NSamples);
            for (int i = 0; i < s.NSamples; i++)
            {
                ret.c[i] = Math.Pow(s.c[i], n);
            }
            Debug.Assert(!ret.HasNaNs());
            return ret;
        }
        public static CoefficientSpectrum Exp(CoefficientSpectrum s)
        {
            CoefficientSpectrum ret = new CoefficientSpectrum(s.NSamples);
            for (int i = 0; i < s.NSamples; i++)
            {
                ret.c[i] = Math.Exp(s.c[i]);
            }
            Debug.Assert(!ret.HasNaNs());
            return ret;
        }
        public void Clamp(double low = 0, double high = double.PositiveInfinity)
        {
            CoefficientSpectrum ret = new CoefficientSpectrum(NSamples);
            for (int i = 0; i < NSamples; i++)
            {
                ret.c[i] = Math.Clamp(c[i], low, high);
            }
            Debug.Assert(!ret.HasNaNs());
            this.c = ret.c;
        }
        public bool IsBlack()
        {
            Debug.Assert(!this.HasNaNs());
            for (int i = 0; i < NSamples; i++)
            {
                if (c[i] != 0) return false;
            }
            return true;
        }
        public double MaxComponentValue()
        {
            double m = 0;
            for (int i = 0; i < NSamples; i++)
            {
                m = Math.Max(m, c[i]);
            }
            Debug.Assert(!double.IsNaN(m));
            return m;
        }
        public bool HasNaNs()
        {
            for(int i = 0; i < NSamples; i++)
            {
                if (double.IsNaN(c[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public CoefficientSpectrum Copy() {
            CoefficientSpectrum s = new CoefficientSpectrum(NSamples);
            s.c = (double[])c.Clone();
            return s;
        }

        public static CoefficientSpectrum operator +(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NSamples);
            for (int i = 0; i < s1.NSamples; i++) {
                sum.c[i] = s1.c[i] + s2.c[i];
            }

            return sum;
        }
        public static CoefficientSpectrum operator -(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NSamples);
            for (int i = 0; i < s1.NSamples; i++) {
                sum.c[i] = s1.c[i] - s2.c[i];
            }

            return sum;
        }
        public static CoefficientSpectrum operator *(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NSamples);
            for (int i = 0; i < s1.NSamples; i++) {
                sum.c[i] = s1.c[i] * s2.c[i];
            }

            return sum;
        }
        public static CoefficientSpectrum operator *(double s2, CoefficientSpectrum s1) {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!double.IsNaN(s2));

            CoefficientSpectrum sum = new CoefficientSpectrum(s1.NSamples);
            for (int i = 0; i < s1.NSamples; i++) {
                sum.c[i] = s1.c[i] * s2;
            }

            return sum;
        }
        public static CoefficientSpectrum operator /(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            CoefficientSpectrum sum = s1.Copy();
            for (int i = 0; i < s1.NSamples; i++)
            {
                Debug.Assert(s2.c[i] != 0);
                sum.c[i] /= s2.c[i];
            }
            return sum;
        }

        public static bool operator ==(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            for (int i = 0; i < s1.NSamples; ++i)
            {
                if (s1.c[i] != s2.c[i]) return false;
            }
            return true;
        }
        public static bool operator !=(CoefficientSpectrum s1, CoefficientSpectrum s2)
        {
            Debug.Assert(!s1.HasNaNs());
            Debug.Assert(!s2.HasNaNs());

            for (int i = 0; i < s1.NSamples; ++i)
            {
                if (s1.c[i] == s2.c[i]) return false;
            }
            return true;
        }

        public static CoefficientSpectrum operator -(CoefficientSpectrum s)
        {
            Debug.Assert(!s.HasNaNs());

            CoefficientSpectrum ret = s;
            for (int i = 0; i < s.NSamples; ++i) ret.c[i] = -s.c[i];
            return ret;
        }

        public double this[int i]
        {
            get
            {
                Debug.Assert(i >= 0 && i < NSamples);
                Debug.Assert(!double.IsNaN(i));
                return c[i];
            }
        }

        public override string ToString()
        {
            string str = "[";
            for(int i = 0; i < NSamples; i++)
            {
                str += c[i];
                if (i + 1 < NSamples) str += ", ";
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
