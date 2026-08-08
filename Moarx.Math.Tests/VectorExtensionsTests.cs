using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class VectorExtensionsTests {

    [Test]
    public void TestVector3DGetLengthGeneric() {
        var v = new Vector3D<double>(3, 4, 0);

        Assert.That(v.GetLength(), Is.EqualTo(5).Within(1e-10));
    }
    [Test]
    public void TestVector3DGetLengthFloatFastPath() {
        var v = new Vector3D<float>(3, 4, 0);

        Assert.That(v.GetLength(), Is.EqualTo(5f).Within(1e-6f));
    }
    [Test]
    public void TestVector3DNormalizeGeneric() {
        var v = new Vector3D<double>(3, 4, 0);

        var n = v.Normalize();

        Assert.That(n.X, Is.EqualTo(0.6).Within(1e-10));
        Assert.That(n.Y, Is.EqualTo(0.8).Within(1e-10));
        Assert.That(n.Z, Is.EqualTo(0).Within(1e-10));
        Assert.That(n.GetLengthSquared(), Is.EqualTo(1).Within(1e-10));
    }
    [Test]
    public void TestVector3DNormalizeFloatFastPath() {
        var v = new Vector3D<float>(3, 4, 0);

        var n = v.Normalize();

        Assert.That(n.X, Is.EqualTo(0.6f).Within(1e-6f));
        Assert.That(n.Y, Is.EqualTo(0.8f).Within(1e-6f));
        Assert.That(n.Z, Is.EqualTo(0f).Within(1e-6f));
    }
    [Test]
    public void TestVector3DNormalizeGenericAndFloatFastPathAgree() {
        var vGeneric = new Vector3D<double>(1, 2, 3);
        var vFloat = new Vector3D<float>(1, 2, 3);

        var nGeneric = vGeneric.Normalize();
        var nFloat = vFloat.Normalize();

        Assert.That(nFloat.X, Is.EqualTo((float)nGeneric.X).Within(1e-6f));
        Assert.That(nFloat.Y, Is.EqualTo((float)nGeneric.Y).Within(1e-6f));
        Assert.That(nFloat.Z, Is.EqualTo((float)nGeneric.Z).Within(1e-6f));
    }
    [Test]
    public void TestVector3DNormalizeOfZeroVectorThrows() {
        // Division by zero produces NaN components, which the Vector3D ctor's NaN guard rejects.
        var v = new Vector3D<double>(0, 0, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => v.Normalize());
    }

    [Test]
    public void TestVector2DGetLengthGeneric() {
        var v = new Vector2D<double>(3, 4);

        Assert.That(v.GetLength(), Is.EqualTo(5).Within(1e-10));
    }
    [Test]
    public void TestVector2DGetLengthFloatFastPath() {
        var v = new Vector2D<float>(3, 4);

        Assert.That(v.GetLength(), Is.EqualTo(5f).Within(1e-6f));
    }
    [Test]
    public void TestVector2DNormalizeGeneric() {
        var v = new Vector2D<double>(3, 4);

        var n = v.Normalize();

        Assert.That(n.X, Is.EqualTo(0.6).Within(1e-10));
        Assert.That(n.Y, Is.EqualTo(0.8).Within(1e-10));
    }
    [Test]
    public void TestVector2DNormalizeFloatFastPath() {
        var v = new Vector2D<float>(3, 4);

        var n = v.Normalize();

        Assert.That(n.X, Is.EqualTo(0.6f).Within(1e-6f));
        Assert.That(n.Y, Is.EqualTo(0.8f).Within(1e-6f));
    }
    [Test]
    public void TestVector2DNormalizeGenericAndFloatFastPathAgree() {
        var vGeneric = new Vector2D<double>(1, 2);
        var vFloat = new Vector2D<float>(1, 2);

        var nGeneric = vGeneric.Normalize();
        var nFloat = vFloat.Normalize();

        Assert.That(nFloat.X, Is.EqualTo((float)nGeneric.X).Within(1e-6f));
        Assert.That(nFloat.Y, Is.EqualTo((float)nGeneric.Y).Within(1e-6f));
    }
    [Test]
    public void TestVector2DNormalizeOfZeroVectorThrows() {
        // Division by zero produces NaN components, which the Vector2D ctor's NaN guard rejects.
        var v = new Vector2D<double>(0, 0);

        Assert.Throws<ArgumentOutOfRangeException>(() => v.Normalize());
    }
}
