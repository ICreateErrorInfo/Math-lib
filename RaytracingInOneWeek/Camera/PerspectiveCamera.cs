using Moarx.Math;
using System;

namespace Raytracing.Camera;
public class PerspectiveCamera: ProjectiveCamera {

    private Vector3D<double> dxCamera, dyCamera;
    private readonly Transform rotationMatrix;

    public PerspectiveCamera(Transform cameraToWorld,
                             double shutterOpenTime,
                             double shutterCloseTime,
                             double resolutionWidth,
                             double resolutionHeight,
                             Bounds2D<double> screenWindow,
                             double lensRadius, 
                             double focalDistance,
                             double fov,
                             Point3D<double> lookAt) : base(cameraToWorld, shutterOpenTime, shutterCloseTime, resolutionWidth, resolutionHeight, Transform.Perspective(fov, 1e-2f, 1000), screenWindow, lensRadius, focalDistance) {

        dxCamera = (_RasterToCamera* new Point3D<double>(1, 0, 0) -
                    _RasterToCamera* new Point3D<double>(0, 0, 0));
        dyCamera = (_RasterToCamera* new Point3D<double>(0, 1, 0) -
                    _RasterToCamera* new Point3D<double>(0, 0, 0));

        rotationMatrix = Transform.LookAt(cameraToWorld * new Point3D<double>(0, 0, 0), lookAt, new(0, -1, 0));
    }

    public override CameraRayInformation GenerateRay(CameraSample sample) {
        Point3D<double> pointOnFilm = new(sample.pointOnFilm.X, sample.pointOnFilm.Y, 0);
        Point3D<double> pCamera = _RasterToCamera * pointOnFilm;

        Ray ray = new Ray(new(0,0,0), (pCamera.ToVector()).Normalize());

        if (_LensRadius > 0) {
            throw new NotImplementedException("Depth of field not implemented");
        }

        ray.Time = MathmaticMethods.Lerp(sample.time, ShutterOpenTime, ShutterCloseTime);
        ray = CameraToWorld * ray;
        ray = new(ray.Origin, rotationMatrix * ray.Direction, ray.TMax, ray.Time);
        return new CameraRayInformation { generatedRay = ray, arrivedRadiance = 1 };
    }
}
