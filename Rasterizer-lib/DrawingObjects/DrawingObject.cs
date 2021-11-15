using Math_lib;

namespace Rasterizer_lib.DrawingObjects 
{

    public abstract class DrawingObject
    {
        public abstract DirectBitmap Draw(DirectBitmap bmp);

        public override string ToString()
        {
            return base.ToString();
        }
    }

}