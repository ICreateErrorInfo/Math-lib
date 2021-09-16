using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaytracingInOneWeek
{
    struct IntersectionData
    {
        public Point3D p;
        public Normal3D normal;
        public double t;
        bool frontFace;

        public void setFaceNormal(Ray r, Normal3D outwardNormal)
        {
            frontFace = Vector3D.Dot(r.d, (Vector3D)outwardNormal) < 0;
            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }

    abstract class Shape
    {
        public abstract bool hit(Ray r, double tMin, ref IntersectionData ID);
    }
}
