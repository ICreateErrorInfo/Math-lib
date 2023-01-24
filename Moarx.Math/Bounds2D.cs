using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Moarx.Math;
public readonly record struct Bounds2D<T>
    where T : struct, INumber<T> {

    readonly Point2D<T> _pMin, _pMax;

    public Bounds2D() {
        T minNum = T.CreateChecked(double.MinValue);
        T maxNum = T.CreateChecked(double.MaxValue);
        _pMin = new Point2D<T>(maxNum);
        _pMax = new Point2D<T>(minNum);
    }
    public Bounds2D(Point2D<T> p) {
        PMin = p;
        PMax = p;
    }
    public Bounds2D(Point2D<T> p1, Point2D<T> p2) {
        PMin = new Point2D<T>(T.Min(p1.X, p2.X),
                              T.Min(p1.Y, p2.Y));
        PMax = new Point2D<T>(T.Max(p1.X, p2.X),
                              T.Max(p1.Y, p2.Y));
    }

    public Point2D<T> PMin {
        get => _pMin;
        init {
            if (T.IsNaN(value.X) || T.IsNaN(value.Y)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Bounds data has NaN");
            }

            _pMin = value;
        }
    }
    public Point2D<T> PMax {
        get => _pMax;
        init {
            if (T.IsNaN(value.X) || T.IsNaN(value.Y)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Bounds data has NaN");
            }

            _pMax = value;
        }
    }

    public Rectangle2D<T> ToRectangle() {
        return new Rectangle2D<T>(PMin, PMax);
    }

    public Point2D<T> this[int i] {
        get {
            if (i == 0) {
                return _pMin;
            }

            if (i == 1) {
                return _pMax;
            }

            throw new IndexOutOfRangeException();
        }
    }
}
