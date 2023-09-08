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
        PMin = Point3D<T>.SmalestComponents(p1, p2);
        PMax = Point3D<T>.GreatestComponents(p1, p2);
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


    public bool IntersectP(Ray ray, out double hitt0, out double hitt1) {
        double t0 = 0;
        double t1 = ray.TMax;
        for (int i = 0; i < 3; i++) {
            double invRayDir = 1 / ray.Direction[i];
            double tNear = (Convert.ToDouble(PMin[i]) - ray.Origin[i]) * invRayDir;
            double tFar = (Convert.ToDouble(PMax[i]) - ray.Origin[i]) * invRayDir;
            if (tNear > tFar) {
                MathmaticMethods.Swap(ref tNear, ref tFar);
            }
            t0 = tNear > t0 ? tNear : t0;
            t1 = tFar < t1 ? tFar : t1;
            if (t0 > t1) {
                hitt0 = double.NegativeInfinity;
                hitt1 = double.NegativeInfinity;
                return false;
            }
        }
        hitt0 = t0;
        hitt1 = t1;
        return true;
    }
    public bool IntersectP(Ray ray, Vector3D<double> invDir, bool[] dirIsNegBool) {
        int[] dirIsNeg = { dirIsNegBool[0] == true ? 1 : 0,
                               dirIsNegBool[1] == true ? 1 : 0,
                               dirIsNegBool[2] == true ? 1 : 0 };

        double tMin =  (Convert.ToDouble(this[dirIsNeg[0]].X) - ray.Origin.X) * invDir.X;
        double tMax =  (Convert.ToDouble(this[1 - dirIsNeg[0]].X) - ray.Origin.X) * invDir.X;
        double tyMin = (Convert.ToDouble(this[dirIsNeg[1]].Y) - ray.Origin.Y) * invDir.Y;
        double tyMax = (Convert.ToDouble(this[1 - dirIsNeg[1]].Y) - ray.Origin.Y) * invDir.Y;

        if (tMin > tyMax || tyMin > tMax)
            return false;
        if (tyMin > tMin)
            tMin = tyMin;
        if (tyMax < tMax)
            tMax = tyMax;

        double tzMin = (Convert.ToDouble(this[dirIsNeg[2]].Z) - ray.Origin.Z) * invDir.Z;
        double tzMax = (Convert.ToDouble(this[1 - dirIsNeg[2]].Z) - ray.Origin.Z) * invDir.Z;

        if (tMin > tzMax || tzMin > tMax)
            return false;
        if (tzMin > tMin)
            tMin = tzMin;
        if (tzMax < tMax)
            tMax = tzMax;
        return (tMin < ray.TMax) && (tMax > 0);
    }

    public Point3D<T> Corner(int corner) {
        if (!(corner >= 0 && corner < 8)) {
            throw new IndexOutOfRangeException(nameof(corner));
        }

        if (double.IsNaN(corner)) {
            throw new ArgumentOutOfRangeException();
        }

        return new Point3D<T>(this[corner & 1].X,
                              this[(corner & 2) == 2 ? 1 : 0].Y,
                              this[(corner & 4) == 4 ? 1 : 0].Z);
    }

    public static Bounds3D<T> Union(Bounds3D<T> b, Point3D<T> p) {
        return new Bounds3D<T>(Point3D<T>.SmalestComponents(b.PMin, p),
                               Point3D<T>.GreatestComponents(b.PMax, p));
    }

    public static Bounds3D<T> Union(Bounds3D<T> b, Bounds3D<T> b1) {
        return new Bounds3D<T>(Point3D<T>.SmalestComponents(b.PMin, b1.PMin),
                               Point3D<T>.GreatestComponents(b.PMax, b1.PMax));
    }
    public static Bounds3D<T> Intersect(Bounds3D<T> b1, Bounds3D<T> b2) {
        Bounds3D<T> b = new Bounds3D<T>(
            Point3D<T>.GreatestComponents(b1.PMin, b2.PMin),
            Point3D<T>.SmalestComponents(b1.PMax, b2.PMax)
            );
        return b;
    }

    public static bool Overlaps(Bounds3D<T> b, Bounds3D<T> b1) {
        bool x = (b.PMax.X >= b1.PMin.X) && (b.PMin.X <= b1.PMax.X);
        bool y = (b.PMax.Y >= b1.PMin.Y) && (b.PMin.Y <= b1.PMax.Y);
        bool z = (b.PMax.Z >= b1.PMin.Z) && (b.PMin.Z <= b1.PMax.Z);

        return (x && y && z);
    }

    public static bool Inside(Point3D<T> p, Bounds3D<T> b) {
        return (p.X >= b.PMin.X && p.X <= b.PMax.X &&
                p.Y >= b.PMin.Y && p.Y <= b.PMax.Y &&
                p.Z >= b.PMin.Z && p.Z <= b.PMax.Z);
    }
    public static bool InsideExclusive(Point3D<T> p, Bounds3D<T> b) {
        return (p.X >= b.PMin.X && p.X < b.PMax.X &&
                p.Y >= b.PMin.Y && p.Y < b.PMax.Y &&
                p.Z >= b.PMin.Z && p.Z < b.PMax.Z);
    }

    public T DistanceSquared<U>(Point3D<U> p) where U: struct, INumber<U> {
        T dx = T.Max(PMin.X - (T)Convert.ChangeType(p.X, typeof(T)), (T)Convert.ChangeType(p.X, typeof(T)) - PMax.X);
        T dy = T.Max(PMin.Y - (T)Convert.ChangeType(p.Y, typeof(T)), (T)Convert.ChangeType(p.Y, typeof(T)) - PMax.Y);
        T dz = T.Max(PMin.Z - (T)Convert.ChangeType(p.Z, typeof(T)), (T)Convert.ChangeType(p.Z, typeof(T)) - PMax.Z);

        return dx * dx + dy * dy + dz * dz;
    }

    public static Bounds3D<T> Expand(Bounds3D<T> b, T delta) {
        if (T.IsNaN(delta)) {
            throw new ArgumentOutOfRangeException(nameof(delta));
        }

        return new Bounds3D<T>(b.PMin - new Vector3D<T>(delta),
                               b.PMax + new Vector3D<T>(delta));
    }

    public Vector3D<T> Diagonal() {
        return PMax - PMin;
    }

    public T Volume() {
        Vector3D<T> d = Diagonal();
        return d.X * d.Y * d.Z;
    }

    public T SurfaceArea() {
        Vector3D<T> d = Diagonal();
        return T.CreateChecked(2) * (d.X * d.Y + d.X * d.Z + d.Y * d.Z);
    }

    public int MaxDimension() {
        Vector3D<T> d = Diagonal();
        if (d.X > d.Y && d.X > d.Z) {
            return 0;
        } else if (d.Y > d.Z) {
            return 1;
        } else {
            return 2;
        }
    }

    public Point3D<double> Lerp(Point3D<double> t) {
        return new( MathmaticMethods.Lerp(t.X, Convert.ToDouble(PMin.X), Convert.ToDouble(PMax.X)),
                    MathmaticMethods.Lerp(t.Y, Convert.ToDouble(PMin.Y), Convert.ToDouble(PMax.Y)),
                    MathmaticMethods.Lerp(t.Z, Convert.ToDouble(PMin.Z), Convert.ToDouble(PMax.Z)));
    }

    public Vector3D<T> Offset(Point3D<T> p) {
        Vector3D<T> o = p - PMin;

        T newX = o.X, newY = o.Y, newZ = o.Z;

        if (PMax.X > PMin.X)
            newX /= PMax.X - PMin.X;
        if (PMax.Y > PMin.Y)
            newY /= PMax.Y - PMin.Y;
        if (PMax.Z > PMin.Z)
            newZ /= PMax.Z - PMin.Z;

        return new Vector3D<T>(newX, newY, newZ);
    }

    public (Point3D<T>, double) BoundingSphere() {

        var center = (PMin + PMax.ToVector()) / T.CreateChecked(2);
        var radius = Inside(center, this) ? (center - PMax).GetLength() : 0;

        return (center, radius);
    }

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