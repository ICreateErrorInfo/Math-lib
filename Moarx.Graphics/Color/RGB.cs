﻿namespace Moarx.Graphics.Color;
public class RGB {

    public double R { get; init; }
    public double G { get; init; }
    public double B { get; init; }

    public RGB(double r, double g, double b) {
        R = r;
        G = g;
        B = b;
    }

    public RGB ClampZero() {
        return new RGB(System.Math.Max(0, R),
                       System.Math.Max(0, G),
                       System.Math.Max(0, B));
    }

    public static RGB operator *(double a, RGB rgb) {
        return new RGB(
            a * rgb.R,
            a * rgb.G,
            a * rgb.B);
    }
    public static RGB operator /(RGB rgb, double a) {
        return new RGB(
            rgb.R / a,
            rgb.G / a,
            rgb.B / a);
    }
    public static RGB operator +(RGB a, RGB rgb) {
        return new RGB(
            a.R + rgb.R,
            a.G + rgb.G,
            a.B + rgb.B);
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
}