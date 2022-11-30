using System.Diagnostics;
using System.Numerics;

namespace Moarx.Math;
public readonly record struct Normal3D<T> 
    where T: struct, INumber<T>, IRootFunctions<T> {

    readonly Vector3D<T> _vector { get; init; }

    public Normal3D(T x, T y, T z) {
        if (T.IsNaN(x) || T.IsNaN(y) || T.IsNaN(z))
            throw new ArgumentOutOfRangeException("Data is NaN");

        _vector = new Vector3D<T>( x, y, z );
        this.Normalize();
    }
    public Normal3D(Vector3D<T> vector) {
        _vector = vector;
        this.Normalize();
    }

    public static readonly Point3D<T> Empty = new();

    public T X {
        get => _vector.X;
    }
    public T Y {
        get => _vector.Y;
    }
    public T Z {
        get => _vector.Y;
    }


    public T GetLengthSquared() => _vector.GetLengthSquared();
    public T GetLength() => T.Sqrt(GetLengthSquared());
    public Normal3D<T> Normalize() {
        return this / GetLength();
    }
    public bool IsNormalized() {
        return GetLengthSquared() == T.CreateChecked(1) ||
               GetLengthSquared() == T.CreateChecked(-1);
    }

    public static Normal3D<T> operator +(Normal3D<T> left, Normal3D<T> right) => new(left._vector + right._vector);

    public static Normal3D<T> operator -(Normal3D<T> left, Normal3D<T> right) => new(left._vector - right._vector);
    public static Normal3D<T> operator -(Normal3D<T> vector) => new(-vector._vector);

    public static T operator *(Normal3D<T> left, Normal3D<T> right) => left._vector * right._vector;
    public static Normal3D<T> operator *(Normal3D<T> left, T scalar) => new(left._vector * scalar);
    public static Normal3D<T> operator *(T scalar, Normal3D<T> right) => new(scalar * right._vector);

    public static Normal3D<T> operator /(Normal3D<T> left, T scalar) => new(left._vector / scalar);

    public T this[int i] {
        get {
            if (i == 0) {
                return X;
            }
            if (i == 1) {
                return Y;
            }
            if (i == 2) {
                return Z;
            }

            throw new IndexOutOfRangeException();
        }
    }

    public override string ToString() {
        return _vector.ToString();
    }
}

