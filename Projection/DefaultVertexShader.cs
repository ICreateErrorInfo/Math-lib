using Math_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projection
{
    class DefaultVertexShader
    {
        public void BindRotation(Matrix4x4 rot)
        {
            rotation = rot;
        }

        public void BindTranslation(Vector3D translate)
        {
            translation = translate;
        }

        public Matrix4x4 rotation;
        public Vector3D translation;
    }
}
