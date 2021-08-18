using Math_lib;

namespace Projection
{
    class DefaultShader
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
