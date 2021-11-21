using Math_lib;

namespace Projection
{
    class DefaultShader
    {
            public void BindRotation(Matrix rot)
            {
                rotation = rot;
            }

            public void BindTranslation(Vector3D translate)
            {
                translation = translate;
            }

            public Matrix rotation;
            public Vector3D translation;
    }
}
