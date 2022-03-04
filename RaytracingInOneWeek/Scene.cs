﻿using Math_lib;

namespace Raytracing
{
    public struct Scene
    {
        public HittableList Objects;
        public int SamplesPerPixel;
        public int MaxDepth;
        public Point3D Lookfrom;
        public Point3D Lookat;
        public int VFov;
        public double Aperture;
        public Vector3D Background;
        public int ImageWidth;
        public int ImageHeight;
        public double FocusDistance;

        public Scene(HittableList   objs,
                     int            spp,
                     int            maxD,
                     Point3D        lookfrom,
                     Point3D        lookat,
                     int            vFov,
                     double         aperture,
                     Vector3D       background,
                     double focusDistance = 0,
                     int            imageWidth = 400, 
                     int            imageHeight = 200)
        {
            if(focusDistance == 0)
            {
                FocusDistance = (lookfrom - lookat).GetLength();
            }
            else
            {
                FocusDistance = focusDistance;
            }

            Objects = objs;
            SamplesPerPixel = spp;
            MaxDepth = maxD;
            Lookfrom = lookfrom;
            Lookat = lookat;
            VFov = vFov;
            Aperture = aperture;
            Background = background;
            ImageWidth = imageWidth;
            ImageHeight = imageHeight;
        }
    }
}