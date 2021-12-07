using System;
using System.Collections.Generic;
using System.Text;
using Math_lib;

namespace RaytracingInOneWeek
{
    class Perlin
    {
        public Perlin()
        {
            ranfloat = new Vector3D[point_count];
            for(int i = 0; i < point_count; i++)
            {
                ranfloat[i] = Vector3D.Normalize(Vector3D.Random(-1, 1));
            }

            perm_x = perlin_generate_perm();
            perm_y = perlin_generate_perm();
            perm_z = perlin_generate_perm();
        }
        ~Perlin()
        {
            ranfloat = new Vector3D[0];
            perm_x = new int[0];
            perm_y = new int[0];
            perm_z = new int[0];
        }
        private static int point_count = 256;
        private Vector3D[] ranfloat;
        int[] perm_x;
        int[] perm_y;
        int[] perm_z;

        public double noise(Vector3D p)
        {
            var u = p.X - Math.Floor(p.X);
            var v = p.Y - Math.Floor(p.Y);
            var w = p.Z - Math.Floor(p.Z);

            var i = Convert.ToInt32(Math.Floor(p.X));
            var j = Convert.ToInt32(Math.Floor(p.Y));
            var k = Convert.ToInt32(Math.Floor(p.Z));
            Vector3D[,,] c = new Vector3D[2,2,2];

            for(int di = 0; di < 2; di++)
            {
                for(int dj = 0; dj < 2; dj++)
                {
                    for (int dk = 0; dk < 2; dk++)
                    {
                        c[di, dj, dk] = ranfloat[
                            perm_x[(i+di) & 255] ^
                            perm_y[(j+dj) & 255] ^
                            perm_z[(k+dk) & 255]
                            ];
                    }
                }
            }

            return perlin_interp(c, u, v, w);
        }
        public double turb(Vector3D p, int depth = 7)
        {
            double accum = 0;
            var temp_p = p;
            double weight = 1;

            for(int i = 0; i < depth; i++)
            {
                accum += weight * noise(temp_p);
                weight *= 0.5;
                temp_p *= 2;
            }

            return Math.Abs(accum);
        }
        private static double perlin_interp(Vector3D[,,] c, double u, double v, double w)
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
                        Vector3D weight_v = new Vector3D(u-i, v-j, w-k);
                        accum += (i * uu + (1 - i) * (1 - uu)) *
                                 (j * vv + (1 - j) * (1 - vv)) *
                                 (k * ww + (1 - k) * (1 - ww)) * 
                                 Vector3D.Dot(c[i,j,k], weight_v);
                    }
                }
            }
            return accum;
        }
        private static int[] perlin_generate_perm()
        {
            var p = new int[point_count];

            for (int i = 0; i < Perlin.point_count; i++)
            {
                p[i] = i;
            }
            permute(p,point_count);

            return p;
        }
        private static int[] permute(int[] p, int n)
        {
            for(int i = n - 1; i > 0; i--)
            {
                int target = Convert.ToInt32(Mathe.random(1, i, 1));
                int tmp = p[i];
                p[i] = p[target];
                p[target] = tmp;
            }
            return p;
        }
    }
}
