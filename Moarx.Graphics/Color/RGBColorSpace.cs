using Moarx.Graphics.Spectrum;
using Moarx.Math;

namespace Moarx.Graphics.Color; 
public class RGBColorSpace {

    public RGBColorSpace(Point2D<double> r, Point2D<double> g, Point2D<double> b, ISpectrum illuminant, RGBToSpectrumTable rgbToSpectrumTable) {
        R = r; G = g; B = b;
        Illuminant = illuminant;
        RGBToSpectrumTable = rgbToSpectrumTable;

        XYZ w = XYZ.SpectrumToXYZ(illuminant);
        W = w.xy();
        XYZ rnew = XYZ.FromxyY(r), gnew = XYZ.FromxyY(g), bnew = XYZ.FromxyY(b);

        SquareMatrix rgb = new SquareMatrix(new double[,]{
            { rnew.X, gnew.X, bnew.X },
            { rnew.Y, gnew.Y, bnew.Y },
            { rnew.Z, gnew.Z, bnew.Z }
        });

        var tmp = rgb.Inverse().Value.Mul(new double[]{w.X, w.Y, w.Z });
        XYZ c = new(tmp[0], tmp[1], tmp[2]);
        XYZFromRGB = rgb * new SquareMatrix(new double[,] {
            {c.X, 0, 0 },
            {0, c.Y, 0 },
            {0, 0, c.Z }
        });
        RGBFromXYZ = XYZFromRGB.Inverse().Value;
    }

    public static void Init() {
        sRGB = new RGBColorSpace(new Point2D<double>(.64, .33), new Point2D<double>(.3, .6), new Point2D<double>(.15, .06), SampledSpectrumConstants.Illumd65, RGBToSpectrumTable.sRGB);
        DCI_P3 = new RGBColorSpace(new Point2D<double>(.68, .32), new Point2D<double>(.265, .690), new Point2D<double>(.15, .06), SampledSpectrumConstants.Illumd65, RGBToSpectrumTable.DCI_P3);
        Rec2020 = new RGBColorSpace(new Point2D<double>(.708, .292), new Point2D<double>(.170, .797), new Point2D<double>(.131, .046), SampledSpectrumConstants.Illumd65, RGBToSpectrumTable.Rec2020);
        ACES2065_1 = new RGBColorSpace(new Point2D<double>(.7347, .2653), new Point2D<double>(0, 1), new Point2D<double>(.0001, -.077), SampledSpectrumConstants.IllumAcesD60, RGBToSpectrumTable.ACES2065_1);
    }
    public RGB ToRGB(XYZ xyz) {
        var tmp = RGBFromXYZ.Mul(new double[]{xyz.X, xyz.Y, xyz.Z});
        return new RGB(tmp[0], tmp[1], tmp[2]);
    }
    public XYZ ToXYZ(RGB rgb) {
        var tmp = XYZFromRGB.Mul(new double[]{rgb.R, rgb.G, rgb.B});
        return new XYZ(tmp[0], tmp[1], tmp[2]);
    }
    public SquareMatrix ConvertRGBColorSpace(RGBColorSpace from, RGBColorSpace to) {
        if(from == to) {
            throw new NotImplementedException();
        }
        return to.RGBFromXYZ * from.XYZFromRGB;
    }
    public RGBSigmoidPolynomial ToRGBCoeffs(RGB rgb) {
        return RGBToSpectrumTable.ToPolynom(rgb.ClampZero());
    }

    public Point2D<double> R, G, B, W;
    public ISpectrum Illuminant;
    public SquareMatrix XYZFromRGB, RGBFromXYZ;
    public RGBToSpectrumTable RGBToSpectrumTable;
    public static RGBColorSpace sRGB, DCI_P3, Rec2020, ACES2065_1;
}
