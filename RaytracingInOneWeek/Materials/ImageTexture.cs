using Math_lib;
using Math_lib.Spectrum;
using System;
using System.Drawing;

namespace Raytracing.Materials
{
    class ImageTexture : Texture
    {
        public static int bytes_per_pixel = 3;

        private byte[] _data;
        private int _width, _height;
        private int _bytesPerScanline;

        public ImageTexture()
        {
            _data = null;
            _width = 0;
            _height = 0;
            _bytesPerScanline = 0;
        }

        public ImageTexture(string filename)
        {
            Image img = Image.FromFile(filename);
            Bitmap bit = new Bitmap(img);
            _width = img.Width;
            _height = img.Height;

            _data = new byte[_width * _height * bytes_per_pixel];

            for(int y = 0; y < _height; y++)
            {
                for(int x = 0; x < _width; x++)
                {
                    var p = y * _width + x;
                    var c = bit.GetPixel(x, y);

                    //data[p * bytes_per_pixel   ] = c.A;
                    _data[p * bytes_per_pixel ] = c.R;
                    _data[p * bytes_per_pixel +1] = c.G;
                    _data[p * bytes_per_pixel +2] = c.B;
                }
            }

            _bytesPerScanline = bytes_per_pixel * _width;
        }

        public override SampledSpectrum Value(double u, double v, Point3D p)
        {
            if (_data == null)
            {
                return SampledSpectrum.FromRGB(new double[] { 0, 1, 1 }, SampledSpectrum.SpectrumType.Reflectance);
            }

            u = Math.Clamp(u, 0.0f, 1);
            v = 1.0f - Math.Clamp(v, 0.0f, 1);

            var i = (int)(u * _width);
            var j = (int)(v * _height);

            if (i >= _width) i = _width - 1;
            if (j >= _height) j = _height - 1;

            var colorScale = 1.0 / 255;

            var index =  j * _bytesPerScanline + i * bytes_per_pixel;
            Vector3D pixel = new Vector3D(_data[index] * colorScale, _data[index + 1] * colorScale, _data[index + 2] * colorScale);

            return SampledSpectrum.FromRGB(new double[] { pixel.X, pixel.Y, pixel.Z }, SampledSpectrum.SpectrumType.Reflectance);
        }
        public Vector3D GetPixel(int x, int y)
        {
            var index = y * _bytesPerScanline + x * bytes_per_pixel;
            Vector3D pixel = new Vector3D(_data[index], _data[index + 1], _data[index + 2]);

            return new Vector3D(pixel.X, pixel.Y, pixel.Z);
        }
        public Bitmap ToBitmap()
        {
            Bitmap bmp = new Bitmap(_width, _height);

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var p = y * _width + x;
                    Vector3D c = GetPixel(x, y);
                    bmp.SetPixel(x, y, Color.FromArgb(255, (int)c.X, (int)c.Y, (int)c.Z));
                }
            }
            return bmp;
        }
    }
}
