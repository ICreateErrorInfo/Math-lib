using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_lib
{
    public interface IFactory<T> where T : Vertex
    {
        public abstract T CreateVertex(Point3D p);
        public abstract T CreateVertex(Vector3D p);
    }
}
