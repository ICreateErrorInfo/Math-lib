using System.Numerics;

namespace Moarx.Math;

public readonly record struct Normal3D<T>
    where T : struct, INumber<T>, IRootFunctions<T> {

    readonly Vector3D<T> _vector;

    public Normal3D(T x, T y, T z) {
        if (x == T.CreateChecked(0) && y == T.CreateChecked(0) && z == T.CreateChecked(0))
            throw new ArgumentException("Normal cant be [0, 0, 0]");
        _vector = new Vector3D<T>(x, y, z);
    }

    public Normal3D(Vector3D<T> vector) {
        if (vector.X == T.CreateChecked(0) && vector.Y == T.CreateChecked(0) && vector.Z == T.CreateChecked(0))
            throw new ArgumentException("Normal cant be [0, 0, 0]");
        _vector = vector;
    }

    public Normal3D() {
        throw new ArgumentException("Normal cant be [0, 0, 0]");
    }

    public T X => _vector.X;
    public T Y => _vector.Y;
    public T Z => _vector.Z;

    public T this[int i] =>
        i switch {
            0 => X,
            1 => Y,
            2 => Z,
            _ => throw new IndexOutOfRangeException()
        };

    public Vector3D<T> ToVector() => _vector;

    public T GetLengthSquared() => T.CreateChecked(1);
    public T GetLength() => T.CreateChecked(1); //TODO
    public Normal3D<T> FaceForward(Vector3D<T> v) {
        return (this.ToVector() * v < T.CreateChecked(0)) ? -this : this;
    }


    public override string ToString() {
        return _vector.ToString();
    }

    public static Normal3D<T> operator +(Normal3D<T> left, Normal3D<T> right) => new(left._vector + right._vector);
    public static Normal3D<T> operator -(Normal3D<T> left, Normal3D<T> right) => new(left._vector - right._vector);

    public static Normal3D<T> operator -(Normal3D<T> vector) => new(-vector._vector);

    public static T operator *(Normal3D<T> left, Normal3D<T> right) => left._vector * right._vector;
    public static Normal3D<T> operator *(Normal3D<T> left, T right) => new Normal3D<T>(left._vector * right);

}