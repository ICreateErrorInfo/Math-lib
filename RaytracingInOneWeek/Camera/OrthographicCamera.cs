using Math_lib;
using System;

namespace Raytracing.Camera;
public class OrthographicCamera: ProjectiveCamera {

    private Vector3D dxCamera, dyCamera;

    public OrthographicCamera(Transform cameraToWorld,
                              double shutterOpenTime,
                              double shutterCloseTime,
                              double resolutionWidth,
                              double resolutionHeight,
                              Bounds2D screenWindow,
                              double lensRadius,
                              double focalDistance) : base(cameraToWorld, shutterOpenTime, shutterCloseTime, resolutionWidth, resolutionHeight, Transform.Orthographic(0, 1), screenWindow, lensRadius, focalDistance) {
    
        dxCamera = _RasterToCamera * new Vector3D(1, 0, 0);
        dyCamera = _RasterToCamera * new Vector3D(0, 1, 0);

    }

    public override CameraRayInformation GenerateRay(CameraSample sample) {
        Point3D pointOnFilm = new(sample.pointOnFilm.X, sample.pointOnFilm.Y, 0);
        Point3D pCamera = _RasterToCamera * pointOnFilm;

        Ray ray = new Ray(pCamera, new Vector3D(0, 0, -1));

        if (_LensRadius > 0) {
            throw new NotImplementedException("Depth of field not implemented");
        }

        ray.Time = Mathe.Lerp(sample.time, ShutterOpenTime, ShutterCloseTime);
        ray = CameraToWorld * ray;
        return new CameraRayInformation { generatedRay = ray, arrivedRadiance = 1 };
    }
}
