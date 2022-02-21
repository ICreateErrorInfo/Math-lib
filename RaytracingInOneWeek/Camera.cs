using Math_lib;
using System;

namespace Raytracing
{
    class Camera
    {
        public Camera(Point3D lookfrom, Point3D lookat, Vector3D vup, double vfov, double aspect_ratio, double aperture, double focus_dist, double _time0 = 0, double _time1 = 0)
        {
            var theta = Mathe.ToRad(vfov);
            var h = Math.Tan(theta / 2);
            var viewport_height = 2 * h;
            var viewport_width = aspect_ratio * viewport_height;

            w = Vector3D.Normalize(lookfrom - lookat);
            u = Vector3D.Normalize(Vector3D.Cross(vup, w));
            v = Vector3D.Cross(w, u);

            origin = lookfrom;
            horizontal = focus_dist * viewport_width * u;
            vertical = focus_dist * viewport_height * v;
            lower_left_corner = origin - horizontal / 2 - vertical / 2 - focus_dist * w;

            lens_radius = aperture / 2;
            time0 = _time0;
            time1 = _time1;
        }

        public Ray get_ray(double s, double t)
        {
            Vector3D rd = lens_radius * Vector3D.RandomInUnitSphere();
            Vector3D offset = u * rd.X + v * rd.Y;

            return new Ray(origin + offset,
                           lower_left_corner + s * horizontal + t * vertical - origin - offset,
                           Mathe.random(time0, time1, 1));
        }

        Point3D origin;
        Point3D lower_left_corner;
        Vector3D horizontal;
        Vector3D vertical;
        Vector3D u;
        Vector3D v;
        Vector3D w;
        double lens_radius;
        double time0;
        double time1;
    }
}
