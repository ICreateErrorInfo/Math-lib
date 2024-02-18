using Moarx.Math;
using System;

namespace Raytracing.Camera;

public struct CameraSample {
    public Point2D<double> pointOnFilm;
    public Point2D<double> pointOnLense;
    public double time;
}
public struct CameraRayInformation {
    public Ray generatedRay;
    public double arrivedRadiance;
}

public abstract class ICamera {

    public Film Film;
    public CameraTransform CameraTransfrom;
    public double ShutterOpenTime, ShutterCloseTime;
    public double ResolutionWidth, ResolutionHeight;

    public ICamera( CameraTransform cameraTransfrom,
                    double shutterOpenTime,
                    double shutterCloseTime,
                    double resolutionWidth,
                    double resolutionHeight) {

        ShutterOpenTime = shutterOpenTime;
        ShutterCloseTime = shutterCloseTime;
        ResolutionWidth = resolutionWidth;
        ResolutionHeight = resolutionHeight;

        Film = new Film((int)resolutionWidth, (int)resolutionHeight);
        CameraTransfrom = cameraTransfrom;
    }

    public virtual CameraRayInformation GenerateRay(CameraSample sample) {
        throw new NotImplementedException();
    }
}
