using Math_lib;

namespace Projection
{
    class DefaultShader
    {
            public void BindRotation(Matrix3x3 rot)
            {
                rotation = rot;
            }

            public void BindTranslation(Vector3D translate)
            {
                translation = translate;
            }

            public Matrix3x3 rotation;
            public Vector3D translation;
    }
}
