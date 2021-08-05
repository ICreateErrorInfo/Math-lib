using Math_lib;

namespace Projection
{
    class Camera
    {
        public Point3 pos;
        public Vector3 up;
        public double yaw;
        public Vector3 lookDir;
        public Vector3 target;

        public Camera()
        {
            pos = new Point3(0, 0, 0);
            up = new Vector3(0, 1, 0);
            lookDir = new Vector3(0, 0, -1);
            target = new Vector3(0, 0, -1);
            yaw = 0;
        }
        public Camera(Point3 pos, Vector3 up, Vector3 lookDir, Vector3 target, double yaw)
        {
            this.pos = pos;
            this.up = up;
            this.lookDir = lookDir;
            this.target = target;
            this.yaw = yaw;
        }
    }
}
