using Moarx.Math;

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
                            Bounds2D<double> screenWindow,
                            double lensRadius,
                            double focalDistance) : base(cameraToWorld, shutterOpenTime, shutterCloseTime, resolutionWidth, resolutionHeight) {

        _CameraToScreen = CameraToScreen;
        _LensRadius = lensRadius;
        _FocalDistance = focalDistance;
        
        _ScreenToRaster = Transform.Scale(ResolutionWidth, -ResolutionHeight, 1) * 
                          Transform.Scale(1 / (screenWindow.PMax.X - screenWindow.PMin.X),
                                          1 / (screenWindow.PMax.Y - screenWindow.PMin.Y),
                                          1) * 
                          Transform.Translate(new Vector3D<double>(-screenWindow.PMin.X, -screenWindow.PMax.Y, 0));
        _RasterToScreen = _ScreenToRaster.Inverse();

        _RasterToCamera = CameraToScreen.Inverse() * _RasterToScreen;
    }
}
