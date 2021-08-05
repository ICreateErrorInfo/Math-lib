using Math_lib;

namespace Projection
{
    class Camera
    {
        public Camera()
        {
            Pos     = new Point3D(0, 0, 0);
            Up      = new Vector3D(0, 1, 0);
            LookDir = new Vector3D(0, 0, -1);
            Target  = new Vector3D(0, 0, -1);
            Yaw     = 0;
        }
        public Camera(Point3D pos, Vector3D up, Vector3D lookDir, Vector3D target, double yaw)
        {
            Pos     = pos;
            Up      = up;
            LookDir = lookDir;
            Target  = target;
            Yaw     = yaw;
        }

        public Point3D  Pos     { get; set; }
        public Vector3D Up      { get; set; }
        public double   Yaw     { get; set; }
        public Vector3D LookDir { get; set; }
        public Vector3D Target  { get; set; }
    }
}
