using Moarx.Math;
using System;

namespace Raytracing.Camera;
public class PerspectiveCamera: ProjectiveCamera {

    private Vector3D<double> dxCamera, dyCamera;
    private readonly double cosTotalWidth;

    public PerspectiveCamera(CameraTransform cameraToWorld,
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

        Point2D<double> radius = new Point2D<double>(0, 0); //TODO radius filter
        Point3D<double> pCorner = new(-radius.X, -radius.Y, 0);
        Vector3D<double> wCornerCamera = (_RasterToCamera * pCorner).ToVector().Normalize();
        cosTotalWidth = wCornerCamera.Z;
    }

    public override CameraRayInformation GenerateRay(CameraSample sample) {
        Point3D<double> pointOnFilm = new(sample.pointOnFilm.X, sample.pointOnFilm.Y, 0);
        Point3D<double> pCamera = _RasterToCamera * pointOnFilm;

        Ray ray = new Ray(new(0,0,0), pCamera.ToVector().Normalize());

        if (_LensRadius > 0) {
            Point2D<double> pLens = _LensRadius * MathmaticMethods.SampleUniformDiskConcentric(sample.pointOnLense);

            double tFocus = _FocalDistance / ray.Direction.Z;
            Point3D<double> pointOfFocus = ray.At(tFocus);

            Point3D<double> origin = new Point3D<double>(pLens.X, pLens.Y, 0);
            Vector3D<double> direction = (pointOfFocus - origin).Normalize();

            ray = new Ray(origin, direction);
        }

        ray.Time = MathmaticMethods.Lerp(sample.time, ShutterOpenTime, ShutterCloseTime);
        ray = CameraTransfrom.renderFromCamera * ray;
        return new CameraRayInformation { generatedRay = ray, arrivedRadiance = 1 };
    }
}
