using Math_lib;
using System;

namespace Raytracing
{
    class Camera
    {
        readonly Point3D  _origin;
        readonly Point3D  _lowerLeftCorner;
        readonly Vector3D _horizontal;
        readonly Vector3D _vertical;
        readonly Vector3D _u;
        readonly Vector3D _v;
        readonly Vector3D _w;
        readonly double _lensRadius;
        readonly double _time0;
        readonly double _time1;

        public Camera(Point3D  lookFrom,
                      Point3D  lookAt,
                      Vector3D vup, 
                      double   vFov, 
                      double   aspectRatio, 
                      double   aperture,
                      double   focusDist, 
                      double   time0 = 0,
                      double   time1 = 0)
        {
            var theta = Mathe.ToRad(vFov);
            var h = Math.Tan(theta / 2);
            var viewportHeight = 2 * h;
            var viewportWidth = aspectRatio * viewportHeight;

            _w = Vector3D.Normalize(lookFrom - lookAt);
            _u = Vector3D.Normalize(Vector3D.Cross(vup, _w));
            _v = Vector3D.Cross(_w, _u);

            _origin = lookFrom;
            _horizontal = focusDist * viewportWidth * _u;
            _vertical = focusDist * viewportHeight * _v;
            _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - focusDist * _w;

            _lensRadius = aperture / 2;
            _time0 = time0;
            _time1 = time1;
        }

        public Ray get_ray(double s, double t)
        {
            Vector3D rd = _lensRadius * Vector3D.RandomInUnitSphere();
            Vector3D offset = _u * rd.X + _v * rd.Y;

            return new Ray(_origin + offset,
                           _lowerLeftCorner + s * _horizontal + t * _vertical - _origin - offset, double.PositiveInfinity,
                           Mathe.GetRandomDouble(_time0, _time1));
        }
    }
}
