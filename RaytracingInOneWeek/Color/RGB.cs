using System;

namespace Raytracing.Color; 
public class RGB {

    public RGB(double r, double g, double b) {
        R = r; G = g; B = b;
    }

    public RGB ClampZero() {
        return new RGB(Math.Max(0, R), Math.Max(0, G),
                       Math.Max(0, B));
    }

    public double this[int i] {
        get {
            switch (i) {
                case 0: return R;
                case 1: return G;
                case 2: return B;
            }
            throw new IndexOutOfRangeException();
        }
    }

    public double R = 0, G = 0, B = 0;
}
