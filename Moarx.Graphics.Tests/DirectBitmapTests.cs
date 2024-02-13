using NUnit.Framework;

namespace Moarx.Graphics.Tests;

[TestFixture]
public class DirectBitmapTests {

    [Test]
    public void TestCtor() {
        var bitmap = DirectBitmap.Create(1920, 1080);

        Assert.That(bitmap.Height,      Is.EqualTo(1080));
        Assert.That(bitmap.Width,       Is.EqualTo(1920));
        Assert.That(bitmap.Bits.Length, Is.EqualTo(1080 * 1920 * 4));
    }

    [Test]
    public void TestCtor2() {
        byte[]       bytes  = new byte[1920 * 1080 * 3];
        var bitmap = DirectBitmap.Create(1920, 1080, bytes, 3);

        Assert.That(bitmap.Height,      Is.EqualTo(1080));
        Assert.That(bitmap.Width,       Is.EqualTo(1920));
        Assert.That(bitmap.Bits.Length, Is.EqualTo(1080 * 1920 * 3));
    }

    [Test]
    public void TestClear() {
        byte[]       bytes  = { 1,2,3,4 };
        var bitmap = DirectBitmap.Create(1, 1, bytes, 4);

        bitmap.Clear();
        Assert.That(bitmap.Bits[0], Is.EqualTo(0));
        Assert.That(bitmap.Bits[1], Is.EqualTo(0));
        Assert.That(bitmap.Bits[2], Is.EqualTo(0));
        Assert.That(bitmap.Bits[3], Is.EqualTo(0));
    }

    [Test]
    public void TestSetPixel() {
        byte[]       bytes  = new byte[1920 * 1080 * 3];
        var bitmap = DirectBitmap.Create(1920, 1080, bytes, 3);

        bitmap.SetPixel(110, 600, DirectColor.FromRgb(255, 255, 255));
        Assert.That(bitmap.Bits[3456330], Is.EqualTo(255));
        Assert.That(bitmap.Bits[3456331], Is.EqualTo(255));
        Assert.That(bitmap.Bits[3456332], Is.EqualTo(255));
    }

    [Test]
    public void TestGetPixel() {
        byte[]       bytes  = new byte[1920 * 1080 * 3];
        var bitmap = DirectBitmap.Create(1920, 1080, bytes, 3);

        bitmap[110, 600] = DirectColor.FromRgb(255, 255, 255);

        Assert.That(bitmap.GetPixel(110, 600), Is.EqualTo(DirectColor.FromRgb(255, 255, 255)));
    }

    [Test]
    public void TestOperator() {
        byte[]       bytes  = new byte[1920 * 1080 * 3];
        var bitmap = DirectBitmap.Create(1920, 1080, bytes, 3);

        bitmap[110, 600] = DirectColor.FromRgb(255, 255, 255);

        Assert.That(bitmap[110, 600], Is.EqualTo(DirectColor.FromRgb(255, 255, 255)));
    }

}