using NUnit.Framework;
using System.Drawing;

namespace Moarx.Rasterizer.Tests;

[TestFixture]
public class DirectBitmapTests {

    [Test]
    public void TestCtor() {
        DirectBitmap bitmap = new DirectBitmap(1920, 1080);

        Assert.That(bitmap.Height,      Is.EqualTo(1080));
        Assert.That(bitmap.Width,       Is.EqualTo(1920));
        Assert.That(bitmap.Bits.Length, Is.EqualTo(1080 * 1920 * 3));
    }

    [Test]
    public void TestCtor2() {
        byte[]       bytes  = new byte[1920 * 1080 * 3];
        DirectBitmap bitmap = new DirectBitmap(1920, 1080, bytes);

        Assert.That(bitmap.Height,      Is.EqualTo(1080));
        Assert.That(bitmap.Width,       Is.EqualTo(1920));
        Assert.That(bitmap.Bits.Length, Is.EqualTo(1080 * 1920 * 3));
    }

    [Test]
    public void TestCtor3() {
        byte[]       bytes  = new byte[1920 * 1080 * 2];
        DirectBitmap bitmap = new DirectBitmap(1920, 1080, bytes, 2);

        Assert.That(bitmap.Height,      Is.EqualTo(1080));
        Assert.That(bitmap.Width,       Is.EqualTo(1920));
        Assert.That(bitmap.Bits.Length, Is.EqualTo(1080 * 1920 * 2));
    }

    [Test]
    public void TestClear() {
        byte[]       bytes  = new byte[] { 100 };
        DirectBitmap bitmap = new DirectBitmap(1, 1, bytes, 1);

        bitmap.Clear();
        Assert.That(bitmap.Bits[0], Is.EqualTo(0));
    }

    [Test]
    public void TestSetPixel() {
        byte[]       bytes  = new byte[1920 * 1080 * 3];
        DirectBitmap bitmap = new DirectBitmap(1920, 1080, bytes, 3);

        bitmap.SetPixel(110, 600, Color.FromArgb(255, 255, 255));
        Assert.That(bitmap.Bits[3456330], Is.EqualTo(255));
        Assert.That(bitmap.Bits[3456331], Is.EqualTo(255));
        Assert.That(bitmap.Bits[3456332], Is.EqualTo(255));
    }

    [Test]
    public void TestGetPixel() {
        byte[]       bytes  = new byte[1920 * 1080 * 3];
        DirectBitmap bitmap = new DirectBitmap(1920, 1080, bytes, 3);

        bitmap[110, 600] = Color.FromArgb(255, 255, 255);

        Assert.That(bitmap.GetPixel(110, 600), Is.EqualTo(Color.FromArgb(255, 255, 255)));
    }

    [Test]
    public void TestOperator() {
        byte[]       bytes  = new byte[1920 * 1080 * 3];
        DirectBitmap bitmap = new DirectBitmap(1920, 1080, bytes, 3);

        bitmap[110, 600] = Color.FromArgb(255, 255, 255);

        Assert.That(bitmap[110, 600], Is.EqualTo(Color.FromArgb(255, 255, 255)));
    }

}