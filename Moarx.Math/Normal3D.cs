using System.Numerics;

namespace Moarx.Math;

public readonly record struct Normal3D<T>
    where T : struct, INumber<T>, IRootFunctions<T> {

    readonly Vector3D<T> _vector;

    public Normal3D(T x, T y, T z) {
        _vector = new Vector3D<T>(x, y, z).Normalize();
    }

    public Normal3D(Vector3D<T> vector) {
        _vector = vector.Normalize();
    }

    public T X => _vector.X;
    public T Y => _vector.Y;
    public T Z => _vector.Y;

    public T this[int i] =>
        i switch {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new IndexOutOfRangeException()
        };

    public Vector3D<T> ToVector() => _vector;

    public T GetLengthSquared() => T.CreateChecked(1); // Ist bei einem Normal per Definition immer 1!
    public T GetLength() => T.CreateChecked(1);        // Ist bei einem Normal per Definition immer 1!

    public override string ToString() {
        return _vector.ToString();
    }

    public static Normal3D<T> operator +(Normal3D<T> left, Normal3D<T> right) => new(left._vector + right._vector);
    public static Normal3D<T> operator -(Normal3D<T> left, Normal3D<T> right) => new(left._vector - right._vector);

    public static Normal3D<T> operator -(Normal3D<T> vector) => new(-vector._vector);

    public static T operator *(Normal3D<T> left, Normal3D<T> right) => left._vector * right._vector;

}