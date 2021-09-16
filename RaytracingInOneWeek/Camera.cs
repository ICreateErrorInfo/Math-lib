using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaytracingInOneWeek
{
    class Camera
    {
        //Properties
        private Point3D origin;
        private Point3D lowerLeftCorner;
        private Vector3D horizontal;
        private Vector3D vertical;


        //Ctors
        public Camera(double aspectRatio)
        {
            double vHeight = 2;
            double vWidth = aspectRatio * vHeight;
            double focalLength = 1;

            //Calc Virtual Viewport
            origin = new Point3D(0, 0, 0);
            horizontal = new Vector3D(vWidth, 0, 0);
            vertical = new Vector3D(0, vHeight, 0);
            lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vector3D(0, 0, focalLength);
        }


        public Ray GetRay(double u, double v)
        {
            return new Ray(origin, lowerLeftCorner + u * horizontal + v * vertical - origin);
        }
    }
}
