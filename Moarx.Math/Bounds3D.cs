using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math;
public readonly record struct Bounds3D<T>
where T : struct, INumber<T> {

    readonly Point3D<T> _pMin, _pMax;

    public Bounds3D() {
        T minNum = T.CreateChecked(double.MinValue);
        T maxNum = T.CreateChecked(double.MaxValue);
        _pMin = new Point3D<T>(maxNum);
        _pMax = new Point3D<T>(minNum);
    }
    public Bounds3D(Point3D<T> p) {
        PMin = p;
        PMax = p;
    }
    public Bounds3D(Point3D<T> p1, Point3D<T> p2) {
        //TODO
        //PMin = Point3D<T>.Min(p1, p2);
        //PMax = Point3D<T>.Max(p1, p2);
    }

    public Point3D<T> PMin {
        get => _pMin;
        init {
            if (T.IsNaN(value.X) || T.IsNaN(value.Y) || T.IsNaN(value.Z)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Bounds data has NaN");
            }
            _pMin = value;
        }
    }
    public Point3D<T> PMax {
        get => _pMax;
        init {
            if (T.IsNaN(value.X) || T.IsNaN(value.Y) || T.IsNaN(value.Z)) {
                throw new ArgumentOutOfRangeException(nameof(value), "Bounds data has NaN");
            }
            _pMax = value;
        }
    }

    //TODO Method implementation

    public Point3D<T> this[int i] {
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
