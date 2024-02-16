﻿using Moarx.Math;
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
    public Transform CameraToWorld;
    public double ShutterOpenTime, ShutterCloseTime;
    public double ResolutionWidth, ResolutionHeight;

    public ICamera( Transform cameraToWorld,
                    double shutterOpenTime,
                    double shutterCloseTime,
                    double resolutionWidth,
                    double resolutionHeight) {

        CameraToWorld = cameraToWorld;
        ShutterOpenTime = shutterOpenTime;
        ShutterCloseTime = shutterCloseTime;
        ResolutionWidth = resolutionWidth;
        ResolutionHeight = resolutionHeight;

        Film = new Film((int)resolutionWidth, (int)resolutionHeight);
    }

    public virtual CameraRayInformation GenerateRay(CameraSample sample) {
        throw new NotImplementedException();
    }
}
