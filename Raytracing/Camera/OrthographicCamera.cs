﻿using Moarx.Math;
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


        rotationMatrix = Transform.LookAt(cameraToWorld * new Point3D<double>(0, 0, 0), lookAt, new(0, -1, 0));
    }

    public override CameraRayInformation GenerateRay(CameraSample sample) {
        Point3D<double> pointOnFilm = new(sample.pointOnFilm.X, sample.pointOnFilm.Y, 0);
        Point3D<double> pCamera = _RasterToCamera * pointOnFilm;

        Ray ray = new Ray(new(pCamera.X, -pCamera.Y, pCamera.Z), rotationMatrix * new Vector3D<double>(0, 0, 1));

        if (_LensRadius > 0) {
            //TODO
            //Point2D<double> pLens = _LensRadius * MathmaticMethods.SampleUniformDiskConcentric(sample.pointOnLense);

            //double tFocus = _FocalDistance / ray.Direction.Z;
            //Point3D<double> pointOfFocus = ray.At(tFocus);

            //ray = new Ray(new Point3D<double>(pLens.X, pLens.Y, 0), (pointOfFocus - new Point3D<double>(pLens.X, pLens.Y, 0)).Normalize());
        }

        ray.Time = MathmaticMethods.Lerp(sample.time, ShutterOpenTime, ShutterCloseTime);
        ray = CameraToWorld * ray;
        return new CameraRayInformation { generatedRay = ray, arrivedRadiance = 1 };
    }
}
