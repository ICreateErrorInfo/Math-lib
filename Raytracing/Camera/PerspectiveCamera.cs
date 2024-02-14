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

        rotationMatrix = Transform.LookAt(new Point3D<double>(0, 0, 0), lookAt, new(0, 1, 0));
        CameraToWorld *= rotationMatrix;
    }

    public override CameraRayInformation GenerateRay(CameraSample sample) {
        Point3D<double> pointOnFilm = new(sample.pointOnFilm.X, sample.pointOnFilm.Y, 0);
        Point3D<double> pCamera = _RasterToCamera * pointOnFilm;

        Ray ray = new Ray(new(0,0,0), (pCamera.ToVector()).Normalize());

        if (_LensRadius > 0) {
            Point2D<double> pLens = _LensRadius * MathmaticMethods.SampleUniformDiskConcentric(sample.pointOnLense);

            double tFocus = _FocalDistance / ray.Direction.Z;
            Point3D<double> pointOfFocus = ray.At(tFocus);

            ray = new Ray(new Point3D<double>(pLens.X, pLens.Y, 0), (pointOfFocus - new Point3D<double>(pLens.X, pLens.Y, 0)).Normalize());
        }

        ray.Time = MathmaticMethods.Lerp(sample.time, ShutterOpenTime, ShutterCloseTime);
        ray = CameraToWorld * ray;
        ray = new(ray.Origin, ray.Direction, ray.TMax, ray.Time);
        return new CameraRayInformation { generatedRay = ray, arrivedRadiance = 1 };
    }
}
