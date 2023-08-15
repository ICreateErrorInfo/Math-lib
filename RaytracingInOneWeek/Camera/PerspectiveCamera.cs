using Math_lib;
using System;
using System.Linq;
using System.Windows.Media.Imaging;

namespace Raytracing.Camera;
public class PerspectiveCamera: ProjectiveCamera {

    private Vector3D dxCamera, dyCamera;
    private readonly Transform rotationMatrix;

    public PerspectiveCamera(Transform cameraToWorld,
                             double shutterOpenTime,
                             double shutterCloseTime,
                             double resolutionWidth,
                             double resolutionHeight,
                             Bounds2D screenWindow,
                             double lensRadius, 
                             double focalDistance,
                             double fov,
                             Point3D lookAt) : base(cameraToWorld, shutterOpenTime, shutterCloseTime, resolutionWidth, resolutionHeight, Transform.Perspective(fov, 1e-2f, 1000), screenWindow, lensRadius, focalDistance) {

        dxCamera = (_RasterToCamera* new Point3D(1, 0, 0) -
                    _RasterToCamera* new Point3D(0, 0, 0));
        dyCamera = (_RasterToCamera* new Point3D(0, 1, 0) -
                    _RasterToCamera* new Point3D(0, 0, 0));

        rotationMatrix = new Transform(Matrix.LookAt(cameraToWorld * new Point3D(0, 0, 0), lookAt, new(0, -1, 0)));
    }

    public override CameraRayInformation GenerateRay(CameraSample sample) {
        Point3D pointOnFilm = new(sample.pointOnFilm.X, sample.pointOnFilm.Y, 0);
        Point3D pCamera = _RasterToCamera * pointOnFilm;

        Ray ray = new Ray(new(0,0,0), Vector3D.Normalize(pCamera.ToVector()));

        if (_LensRadius > 0) {
            throw new NotImplementedException("Depth of field not implemented");
        }

        ray.Time = Mathe.Lerp(sample.time, ShutterOpenTime, ShutterCloseTime);
        ray = CameraToWorld * ray;
        ray = new(ray.O, rotationMatrix * ray.D, ray.TMax, ray.Time);
        return new CameraRayInformation { generatedRay = ray, arrivedRadiance = 1 };
    }
}
