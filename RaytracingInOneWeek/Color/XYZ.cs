using Moarx.Math;
using Raytracing.Spectrum;

namespace Raytracing.Color; 
public class XYZ {
    public XYZ(double x, double y, double z) {
        X = x; Y = y; Z = z;
    }

    public static XYZ SpectrumToXYZ(ISpectrum s) {
        return new XYZ(ISpectrum.InnerProduct(SampledSpectrumConstants.XNew, s),
                       ISpectrum.InnerProduct(SampledSpectrumConstants.YNew, s),
                       ISpectrum.InnerProduct(SampledSpectrumConstants.ZNew, s)) / SampledSpectrumConstants.CIE_Y_integral;
    }
    public Point2D<double> xy() {
        return new Point2D<double>(X / (X + Y + Z), Y / (X + Y + Z));
    }
    public static XYZ FromxyY(Point2D<double> xy, double Y = 1) {
        if(xy.Y == 0) {
            return new XYZ(0, 0, 0);
        }
        return new XYZ(xy.X * Y / xy.Y, Y, (1 - xy.X - xy.Y) * Y / xy.Y);
    }

    public static XYZ operator /(XYZ xyz, double a) {
        return new(xyz.X / a, xyz.Y / a, xyz.Z / a);
    }
    public static XYZ operator *(XYZ xyz, XYZ a) {
        return new(xyz.X / a.X, xyz.Y / a.Y, xyz.Z / a.Z);
    }

    public double X = 0, Y = 0, Z = 0;
}
