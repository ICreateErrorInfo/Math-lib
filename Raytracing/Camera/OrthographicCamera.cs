using Moarx.Math;
using System;
using System.Linq;

namespace Raytracing.Camera;
public class OrthographicCamera: ProjectiveCamera {

    private Vector3D<double> dxCamera, dyCamera;
    private readonly Transform rotationMatrix;

    public OrthographicCamera(Transform cameraToWorld,
                              double shutterOpenTime,
                              double shutterCloseTime,
                              double resolutionWidth,
                              double resolutionHeight,
                              Bounds2D<double> screenWindow,
                              double lensRadius,
                              double focalDistance,
                              Point3D<double> lookAt) : base(cameraToWorld, shutterOpenTime, shutterCloseTime, resolutionWidth, resolutionHeight, Transform.Orthographic(0, 1), screenWindow, lensRadius, focalDistance) {
    
        dxCamera = _RasterToCamera * new Vector3D<double>(1, 0, 0);
        dyCamera = _RasterToCamera * new Vector3D<double>(0, 1, 0);


        rotationMatrix = Transform.LookAt(new Point3D<double>(0, 0, 0), lookAt, new(0, -1, 0));

        CameraToWorld *= rotationMatrix;
    }

    public override CameraRayInformation GenerateRay(CameraSample sample) {
        Point3D<double> pointOnFilm = new(sample.pointOnFilm.X, sample.pointOnFilm.Y, 0);
        Point3D<double> pCamera = _RasterToCamera * pointOnFilm;

        Ray ray = new Ray(pCamera, new Vector3D<double>(0, 0, 1));

        if (_LensRadius > 0) {
            //TODO depth of Field

            //Point2D<double> pLens = _LensRadius * MathmaticMethods.SampleUniformDiskConcentric(sample.pointOnLense);

            //double tFocus = _FocalDistance / ray.Direction.Z;
            //Point3D<double> pointOfFocus = ray.At(tFocus);

            //Point3D<double> origin = new Point3D<double>(pLens.X, pLens.Y, 0);
            //Vector3D<double> direction = (pointOfFocus - origin).Normalize();

            //ray = new Ray(origin, direction);
        }

        ray.Time = MathmaticMethods.Lerp(sample.time, ShutterOpenTime, ShutterCloseTime);
        ray = CameraToWorld * ray;
        return new CameraRayInformation { generatedRay = ray, arrivedRadiance = 1 };
    }
}
