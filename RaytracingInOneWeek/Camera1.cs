using Moarx.Math;
using System;

namespace Raytracing
{
    class Camera1
    {
        readonly Point3D<double>  _origin;
        readonly Point3D<double>  _lowerLeftCorner;
        readonly Vector3D<double> _horizontal;
        readonly Vector3D<double> _vertical;
        readonly Vector3D<double> _u;
        readonly Vector3D<double> _v;
        readonly Vector3D<double> _w;
        readonly double _lensRadius;
        readonly double _time0;
        readonly double _time1;

        public Camera1(Point3D<double> lookFrom,
                      Point3D<double> lookAt,
                      Vector3D<double> vup, 
                      double   vFov, 
                      double   aspectRatio, 
                      double   aperture,
                      double   focusDistance, 
                      double   time0 = 0,
                      double   time1 = 0)
        {
            var theta = MathmaticMethods.ConvertToRadians(vFov);
            var h = Math.Tan(theta / 2);
            var viewportHeight = 2 * h;
            var viewportWidth = aspectRatio * viewportHeight;

            _w = (lookFrom - lookAt).Normalize();
            _u = (Vector3D<double>.CrossProduct(vup, _w)).Normalize();
            _v = Vector3D<double>.CrossProduct(_w, _u);

            _origin = lookFrom;
            _horizontal = focusDistance * viewportWidth * _u;
            _vertical = focusDistance * viewportHeight * _v;
            _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - focusDistance * _w;

            _lensRadius = aperture / 2;
            _time0 = time0;
            _time1 = time1;
        }

        public Ray get_ray(double s, double t)
        {
            Vector3D<double> rd = _lensRadius * Vector3D<double>.RandomInUnitSphere();
            Vector3D<double> offset = _u * rd.X + _v * rd.Y;

            return new Ray(_origin + offset,
                           _lowerLeftCorner + s * _horizontal + t * _vertical - _origin - offset, double.PositiveInfinity,
                           MathmaticMethods.GetRandomDouble(_time0, _time1));
        }
    }
}
