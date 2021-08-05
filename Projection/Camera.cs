using Math_lib;

namespace Projection
{
    class Camera
    {
        public Point3D pos;
        public Vector3D up;
        public double yaw;
        public Vector3D lookDir;
        public Vector3D target;

        public Camera()
        {
            pos = new Point3D(0, 0, 0);
            up = new Vector3D(0, 1, 0);
            lookDir = new Vector3D(0, 0, -1);
            target = new Vector3D(0, 0, -1);
            yaw = 0;
        }
        public Camera(Point3D pos, Vector3D up, Vector3D lookDir, Vector3D target, double yaw)
        {
            this.pos = pos;
            this.up = up;
            this.lookDir = lookDir;
            this.target = target;
            this.yaw = yaw;
        }
    }
}
