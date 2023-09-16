using Moarx.Math;
using System;

namespace Raytracing.Color; 
public class RGBToSpectrumTable {

    public const int Res = 64;
    private double[] _zNodes;
    private double[,,,,] _coeffs = new double[3, Res, Res, Res, 3];

    public RGBToSpectrumTable(double[] zNodes, double[,,,,] coeffs) {
        _zNodes = zNodes;
        _coeffs = coeffs;
    }

    public RGBSigmoidPolynomial ToPolynom(RGB rgb) {
        if (rgb.R == rgb.G && rgb.G == rgb.B) {
            return new RGBSigmoidPolynomial(0, 0, (rgb[0] - .5f) / Math.Sqrt(rgb[0] * (1 - rgb[0])));
        }

        int maxc = (rgb[0] > rgb[1]) ? ((rgb[0] > rgb[2]) ? 0 : 2) : ((rgb[1] > rgb[2]) ? 1 : 2);
        double z = rgb[maxc];
        double x = rgb[(maxc + 1) % 3] * (Res - 1) / z;
        double y = rgb[(maxc + 2) % 3] * (Res - 1) / z;

        int xi = System.Math.Min((int)x, Res - 2),
            yi = System.Math.Min((int)y, Res - 2),
            zi = MathmaticMethods.FindInterval(Res, (int i) => _zNodes[i] < z );

        double dx = x - xi,
               dy = y - yi,
               dz = (z - _zNodes[zi]) / (_zNodes[zi + 1] - _zNodes[zi]);

        double[] c = new double[3];
        for(int i = 0; i < 3; i++) {
            Func<int, int, int, double> co = delegate(int dx, int dy, int dz){
                return _coeffs[maxc, zi + dz, yi + dy, xi + dx, i];
            };

            c[i] = MathmaticMethods.Lerp(dz, MathmaticMethods.Lerp(dy, MathmaticMethods.Lerp(dx, co(0, 0, 0), co(1, 0, 0)),
                                                                       MathmaticMethods.Lerp(dx, co(0, 1, 0), co(1, 1, 0))),
                                             MathmaticMethods.Lerp(dy, MathmaticMethods.Lerp(dx, co(0, 0, 1), co(1, 0, 1)),
                                                                       MathmaticMethods.Lerp(dx, co(0, 1, 1), co(1, 1, 1))));
        }

        return new RGBSigmoidPolynomial(c[0], c[1], c[2]);
    }
    public static void Init() {
        sRGB = new RGBToSpectrumTable(sRGBConstants.sRGBToSpectrumTableScale, sRGBConstants.sRGBToSpectrumTableData);
        DCI_P3 = new RGBToSpectrumTable(DCPI_P3Constants.DCI_P3ToSpectrumTableScale, DCPI_P3Constants.DCI_P3ToSpectrumTableData);
    }


    public static RGBToSpectrumTable sRGB, DCI_P3, Rec2020, ACES2065_1;
}
