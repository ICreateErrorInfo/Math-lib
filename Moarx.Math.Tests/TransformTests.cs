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
    [Test]
    public void TestIsIdentityFalseForNonIdentityTransform() {
        Transform t = Transform.Translate(new(1, 0, 0));

        Assert.That(t.IsIdentity(), Is.False);
    }
    [Test]
    public void TestTranspose() {
        SquareMatrix m = new SquareMatrix(new double[,]{
            {2,1,4,0 },
            {0,2,4,2},
            {3,3,4,3},
            {0,4,4,1}
        });

        Transform t = new Transform(m);

        Assert.That(t.Transpose().GetMatrix()._matrix, Is.EqualTo(m.Transpose()._matrix));
        Assert.That(t.Transpose().GetInverse()._matrix, Is.EqualTo(t.GetInverse().Transpose()._matrix));
    }
    [Test]
    public void TestSwapHandnessFalseForIdentity() {
        Transform t = new Transform();

        Assert.That(t.SwapHandness(), Is.False);
    }
    [Test]
    public void TestSwapHandnessTrueForNegativeScale() {
        Transform t = Transform.Scale(-1, 1, 1);

        Assert.That(t.SwapHandness(), Is.True);
    }
    [Test]
    public void TestRotateAroundZAxisMatchesRotateZ() {
        Transform t = Transform.Rotate(90, new Vector3D<double>(0, 0, 1));

        Assert.That(System.Math.Round((t * new Point3D<double>(1, 0, 0)).X, 5), Is.EqualTo(0));
        Assert.That(System.Math.Round((t * new Point3D<double>(1, 0, 0)).Y, 5), Is.EqualTo(1));
        Assert.That(System.Math.Round((t * new Point3D<double>(1, 0, 0)).Z, 5), Is.EqualTo(0));
    }
    [Test]
    public void TestRotateInverseRoundTrips() {
        Transform t = Transform.Rotate(37, new Vector3D<double>(1, 1, 0));
        Point3D<double> p = new(3, -2, 5);

        Point3D<double> roundTripped = t.Inverse() * (t * p);

        Assert.That(roundTripped.X, Is.EqualTo(p.X).Within(1e-10));
        Assert.That(roundTripped.Y, Is.EqualTo(p.Y).Within(1e-10));
        Assert.That(roundTripped.Z, Is.EqualTo(p.Z).Within(1e-10));
    }
    [Test]
    public void TestScaleInverseRoundTrips() {
        Transform t = Transform.Scale(2, 3, 4);
        Point3D<double> p = new(3, -2, 5);

        Point3D<double> roundTripped = t.Inverse() * (t * p);

        Assert.That(roundTripped.X, Is.EqualTo(p.X).Within(1e-10));
        Assert.That(roundTripped.Y, Is.EqualTo(p.Y).Within(1e-10));
        Assert.That(roundTripped.Z, Is.EqualTo(p.Z).Within(1e-10));
    }
    [Test]
    public void TestRotateFromToMapsFromOntoTo() {
        Vector3D<double> from = new(1, 0, 0);
        Vector3D<double> to = new(0, 1, 0);

        Transform t = Transform.RotateFromTo(from, to);
        Point3D<double> mapped = t * from.ToPoint();

        Assert.That(mapped.X, Is.EqualTo(to.X).Within(1e-10));
        Assert.That(mapped.Y, Is.EqualTo(to.Y).Within(1e-10));
        Assert.That(mapped.Z, Is.EqualTo(to.Z).Within(1e-10));
    }
    [Test]
    public void TestRotateFromToWithOppositeAxis() {
        Vector3D<double> from = new(0, 0, 1);
        Vector3D<double> to = new(1, 0, 0);

        Transform t = Transform.RotateFromTo(from, to);
        Point3D<double> mapped = t * from.ToPoint();

        Assert.That(mapped.X, Is.EqualTo(to.X).Within(1e-10));
        Assert.That(mapped.Y, Is.EqualTo(to.Y).Within(1e-10));
        Assert.That(mapped.Z, Is.EqualTo(to.Z).Within(1e-10));
    }
    [Test]
    public void TestLookAtMapsCameraOriginToPosition() {
        Point3D<double> pos = new(0, 0, -5);
        Point3D<double> look = new(0, 0, 0);
        Vector3D<double> up = new(0, 1, 0);

        Transform t = Transform.LookAt(pos, look, up);

        // world-from-camera (the inverse) must map the camera's local origin to its world position.
        Point3D<double> mapped = t.Inverse() * new Point3D<double>(0, 0, 0);

        Assert.That(mapped, Is.EqualTo(pos));
    }
    [Test]
    public void TestLookAtWorldToCameraMapsPositionToOrigin() {
        Point3D<double> pos = new(0, 0, -5);
        Point3D<double> look = new(0, 0, 0);
        Vector3D<double> up = new(0, 1, 0);

        Transform t = Transform.LookAt(pos, look, up);

        Point3D<double> mapped = t * pos;

        Assert.That(mapped.X, Is.EqualTo(0).Within(1e-10));
        Assert.That(mapped.Y, Is.EqualTo(0).Within(1e-10));
        Assert.That(mapped.Z, Is.EqualTo(0).Within(1e-10));
    }
    [Test]
    public void TestOrthographicMapsNearFarToZeroOne() {
        Transform t = Transform.Orthographic(2, 5);

        Point3D<double> nearMapped = t * new Point3D<double>(0, 0, 2);
        Point3D<double> farMapped = t * new Point3D<double>(0, 0, 5);

        Assert.That(nearMapped.Z, Is.EqualTo(0).Within(1e-10));
        Assert.That(farMapped.Z, Is.EqualTo(1).Within(1e-10));
    }
    [Test]
    public void TestPerspectiveMapsNearFarOnAxisToZeroOne() {
        Transform t = Transform.Perspective(90, 1, 100);

        Point3D<double> nearMapped = t * new Point3D<double>(0, 0, 1);
        Point3D<double> farMapped = t * new Point3D<double>(0, 0, 100);

        Assert.That(nearMapped.X, Is.EqualTo(0).Within(1e-10));
        Assert.That(nearMapped.Y, Is.EqualTo(0).Within(1e-10));
        Assert.That(nearMapped.Z, Is.EqualTo(0).Within(1e-10));
        Assert.That(farMapped.Z, Is.EqualTo(1).Within(1e-10));
    }
    [Test]
    public void TestVectorOperatorIgnoresTranslation() {
        Transform t = Transform.Translate(new(5, 5, 5));

        Assert.That(t * new Vector3D<double>(1, 2, 3), Is.EqualTo(new Vector3D<double>(1, 2, 3)));
    }
    [Test]
    public void TestVectorOperatorAppliesScale() {
        Transform t = Transform.Scale(2, 3, 4);

        Assert.That(t * new Vector3D<double>(1, 1, 1), Is.EqualTo(new Vector3D<double>(2, 3, 4)));
    }
    [Test]
    public void TestNormalOperatorUsesInverseTranspose() {
        // Non-axis-aligned normal so anisotropic scale visibly changes its direction.
        var n = new Normal3D<double>(1, 1, 0);
        Transform t = Transform.Scale(2, 1, 1);

        var transformed = t * n;

        double rawX = 0.5 * n.X;
        double rawY = 1.0 * n.Y;
        double rawZ = 1.0 * n.Z;
        double rawLength = System.Math.Sqrt((rawX * rawX) + (rawY * rawY) + (rawZ * rawZ));

        Assert.That(transformed.X, Is.EqualTo(rawX / rawLength).Within(1e-10));
        Assert.That(transformed.Y, Is.EqualTo(rawY / rawLength).Within(1e-10));
        Assert.That(transformed.Z, Is.EqualTo(rawZ / rawLength).Within(1e-10));
    }
    [Test]
    public void TestRayOperatorTransformsOriginAndDirectionAndPreservesTMaxAndTime() {
        Transform t = Transform.Translate(new(5, 0, 0));
        Ray ray = new(new Point3D<double>(0, 0, 0), new Vector3D<double>(1, 0, 0), 42, 3.5);

        Ray transformed = t * ray;

        Assert.That(transformed.Origin, Is.EqualTo(new Point3D<double>(5, 0, 0)));
        Assert.That(transformed.Direction, Is.EqualTo(new Vector3D<double>(1, 0, 0)));
        Assert.That(transformed.TMax, Is.EqualTo(42));
        Assert.That(transformed.Time, Is.EqualTo(3.5));
    }
    [Test]
    public void TestBoundsOperatorTranslatesBounds() {
        Transform t = Transform.Translate(new(1, 2, 3));
        Bounds3D<double> b = new(new(-1, -1, -1), new(1, 1, 1));

        Bounds3D<double> transformed = t * b;

        Assert.That(transformed.PMin, Is.EqualTo(new Point3D<double>(0, 1, 2)));
        Assert.That(transformed.PMax, Is.EqualTo(new Point3D<double>(2, 3, 4)));
    }
    [Test]
    public void TestTransformCompositionAppliesRightToLeft() {
        Transform composed = Transform.Translate(new(1, 0, 0)) * Transform.Translate(new(2, 0, 0));

        Assert.That(composed * new Point3D<double>(0, 0, 0), Is.EqualTo(new Point3D<double>(3, 0, 0)));
    }
    [Test]
    public void TestEqualityOperators() {
        Transform t1 = Transform.Translate(new(1, 2, 3));
        Transform t2 = Transform.Translate(new(1, 2, 3));
        Transform t3 = Transform.Translate(new(1, 2, 4));

        Assert.That(t1 == t2, Is.True);
        Assert.That(t1 != t2, Is.False);
        Assert.That(t1 == t3, Is.False);
        Assert.That(t1 != t3, Is.True);
    }
}
