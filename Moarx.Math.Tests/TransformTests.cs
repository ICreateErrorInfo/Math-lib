using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
internal class TransformTests {
    [Test]
    public void TestCtor1() {
        Transform t = new Transform();

        Assert.That(t.GetMatrix().IsIdentity(), Is.True);
        Assert.That(t.GetInverse().IsIdentity(), Is.True);
    }

    [Test]
    public void TestCtor2() {
        Transform t = new Transform();

        Assert.That(t.GetMatrix().IsIdentity(), Is.True);
        Assert.That(t.GetInverse().IsIdentity(), Is.True);
    }

    [Test]
    public void TestCtor3() {
        SquareMatrix m = new SquareMatrix(new double[,]{
            {2,1,4,0 },
            {0,2,4,2},
            {3,3,4,3},
            {0,4,4,0}
        });

        Transform t = new Transform(m);

        Assert.That(t.GetMatrix(), Is.EqualTo(m));
    }

    [Test]
    public void TestCtor4() {
        double[,] m = new double[,]{
            {2,1,4,0 },
            {0,2,4,2 },
            {3,3,4,3 },
            {0,4,4,0 } 
        };

        Transform t = new Transform(m);

        Assert.That(t.GetMatrix()._matrix, Is.EqualTo(m));
    }

    [Test]
    public void TestInverse() {
        double[,] m = new double[,]{
            {2,1,4,0 },
            {0,2,4,2 },
            {3,3,4,3 },
            {0,4,4,0 }
        };

        Transform t = new Transform(m);

        Assert.That(t.Inverse().GetInverse()._matrix, Is.EqualTo(m));
    }

    [Test]
    public void TestTranslate() {
        Transform t = Transform.Translate(new(1, 2, 5));

        Assert.That(t * new Point3D<double>(0, 0, 0), Is.EqualTo(new Point3D<double>(1,2,5)));
    }

    [Test]
    public void TestScale() {
        Transform t = Transform.Scale(1, 2, 1);

        Assert.That(t * new Point3D<double>(1, 1, 10), Is.EqualTo(new Point3D<double>(1, 2, 10)));
    }

    [Test]
    public void TestHasScale() {
        SquareMatrix m = new SquareMatrix(new double[,]{
            {2,1,4,0 },
            {0,2,4,2},
            {3,3,4,3},
            {0,4,4,0}
        });

        Transform t = new Transform(m);

        Assert.That(t.HasScale(), Is.True);
    }

    [Test]
    public void TestRotateX() {
        Transform t = Transform.RotateX(-90);

        Assert.That((t * new Point3D<double>(0, 0, 1)).X, Is.EqualTo(0));
        Assert.That((t * new Point3D<double>(0, 0, 1)).Y, Is.EqualTo(1));
        Assert.That(System.Math.Round((t * new Point3D<double>(0, 0, 1)).Z, 5), Is.EqualTo(0));
    }

    [Test]
    public void TestRotateY() {
        Transform t = Transform.RotateY(90);

        Assert.That((t * new Point3D<double>(0, 0, 1)).X, Is.EqualTo(1));
        Assert.That((t * new Point3D<double>(0, 0, 1)).Y, Is.EqualTo(0));
        Assert.That(System.Math.Round((t * new Point3D<double>(0, 0, 1)).Z, 5), Is.EqualTo(0));
    }

    [Test]
    public void TestRotateZ() {
        Transform t = Transform.RotateZ(90);

        Assert.That(System.Math.Round((t * new Point3D<double>(1, 0, 0)).X, 5), Is.EqualTo(0));
        Assert.That((t * new Point3D<double>(1, 0, 0)).Y, Is.EqualTo(1));
        Assert.That(System.Math.Round((t * new Point3D<double>(1, 0, 0)).Z, 5), Is.EqualTo(0));
    }
}
