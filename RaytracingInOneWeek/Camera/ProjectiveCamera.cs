using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Camera;
public class ProjectiveCamera: ICamera {

    protected Transform _CameraToScreen, _RasterToCamera;
    protected Transform _ScreenToRaster, _RasterToScreen;
    protected double _LensRadius, _FocalDistance;


    public ProjectiveCamera(Transform cameraToWorld,
                            double shutterOpenTime,
                            double shutterCloseTime,
                            double resolutionWidth,
                            double resolutionHeight,
                            Transform CameraToScreen,
                            Bounds2D screenWindow,
                            double lensRadius,
                            double focalDistance) : base(cameraToWorld, shutterOpenTime, shutterCloseTime, resolutionWidth, resolutionHeight) {

        _CameraToScreen = CameraToScreen;
        _LensRadius = lensRadius;
        _FocalDistance = focalDistance;
        
        _ScreenToRaster = Transform.Scale(ResolutionWidth, resolutionHeight, 1) * 
                          Transform.Scale(1 / (screenWindow.pMax.X - screenWindow.pMin.X),
                                          1 / (screenWindow.pMin.Y - screenWindow.pMax.Y),
                                          1) * 
                          Transform.Translate(new Vector3D(-screenWindow.pMin.X, -screenWindow.pMax.Y, 0));
        _RasterToScreen = _ScreenToRaster.Inverse();

        _RasterToCamera = CameraToScreen.Inverse() * _RasterToScreen;
    }
}
