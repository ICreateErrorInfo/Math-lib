using System;
using Moarx.Math;

namespace Raytracing.Materials
{
    public class Perlin
    {
        private static int pointCount = 256;
        private readonly Vector3D<double>[] _randomFloat;
        private readonly int[] _permX;
        private readonly int[] _permY;
        private readonly int[] _permZ;

        public Perlin()
        {
            _randomFloat = new Vector3D<double>[pointCount];
            for(int i = 0; i < pointCount; i++)
            {
                _randomFloat[i] = (Vector3D<double>.Random(-1, 1)).Normalize();
            }

            _permX = PerlinGeneratePerm();
            _permY = PerlinGeneratePerm();
            _permZ = PerlinGeneratePerm();
        }

        public double Noise(Vector3D<double> p)
        {
            var u = p.X - Math.Floor(p.X);
            var v = p.Y - Math.Floor(p.Y);
            var w = p.Z - Math.Floor(p.Z);

            var i = Convert.ToInt32(Math.Floor(p.X));
            var j = Convert.ToInt32(Math.Floor(p.Y));
            var k = Convert.ToInt32(Math.Floor(p.Z));
            Vector3D<double>[,,] c = new Vector3D<double>[2,2,2];

            for(int di = 0; di < 2; di++)
            {
                for(int dj = 0; dj < 2; dj++)
                {
                    for (int dk = 0; dk < 2; dk++)
                    {
                        c[di, dj, dk] = _randomFloat[
                            _permX[(i+di) & 255] ^
                            _permY[(j+dj) & 255] ^
                            _permZ[(k+dk) & 255]
                            ];
                    }
                }
            }

            return PerlinInterp(c, u, v, w);
        }
        public double Turb(Vector3D<double> p, int depth = 7)
        {
            double accum = 0;
            var tempP = p;
            double weight = 1;

            for(int i = 0; i < depth; i++)
            {
                accum += weight * Noise(tempP);
                weight *= 0.5;
                tempP *= 2;
            }

            return Math.Abs(accum);
        }
        private static double PerlinInterp(Vector3D<double>[,,] c, double u, double v, double w)
        {
            var uu = u * u * (3 - 2 * u);
            var vv = v * v * (3 - 2 * v);
            var ww = w * w * (3 - 2 * w);
            double accum = 0;

            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        Vector3D<double> weight_v = new Vector3D<double>(u-i, v-j, w-k);
                        accum += (i * uu + (1 - i) * (1 - uu)) *
                                 (j * vv + (1 - j) * (1 - vv)) *
                                 (k * ww + (1 - k) * (1 - ww)) * 
                                 (c[i,j,k] * weight_v);
                    }
                }
            }
            return accum;
        }
        private static int[] PerlinGeneratePerm()
        {
            var p = new int[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                p[i] = i;
            }
            Permute(p,pointCount);

            return p;
        }
        private static int[] Permute(int[] p, int n)
        {
            for(int i = n - 1; i > 0; i--)
            {
                int target = Convert.ToInt32(MathmaticMethods.GetRandomDouble(1, i));
                int tmp = p[i];
                p[i] = p[target];
                p[target] = tmp;
            }
            return p;
        }
    }
}
