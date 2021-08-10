using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    public class ZBuffer
    {
        private int width;
        private int height;
        public double[] pBuffer;

        public ZBuffer(int width, int height)
        {
            this.width = width;
            this.height = height;
            pBuffer = new double[width * height];
        }
        public void Clear()
        {
            int nDeths = width * height;
            for(int i = 0; i < nDeths; i++)
            {
                pBuffer[i] = double.NegativeInfinity;
            }
        }
        public double At(int x, int y)
        {
            return pBuffer[y * width + x];
        }
        public bool TestAndSet(int x, int y, double depth)
        {
            if (x == 540 && y == 600)
            {

            }

            double depthInBuffer = At(x, y);
            if (depth > depthInBuffer)
            {
                pBuffer[y * width + x] = depth;
                return true;
            }
            return false;
        }
    }
}
