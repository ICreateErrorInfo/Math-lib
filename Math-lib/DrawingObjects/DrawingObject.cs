namespace Math_lib.DrawingObjects {

    public abstract class DrawingObject
    {
        public abstract DirectBitmap Draw(DirectBitmap bmp);

        public override string ToString()
        {
            return base.ToString();
        }
    }

}