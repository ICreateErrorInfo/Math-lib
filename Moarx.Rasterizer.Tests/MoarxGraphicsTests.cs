using NUnit.Framework;
using System.Drawing;

namespace Moarx.Rasterizer.Tests;

[TestFixture]
public class MoarxGraphicsTests {

    [Test]
    public void TestFloodFill() {
        byte[] bytes  = new byte[30];
        var    bitmap = new DirectBitmap(10, 1, bytes, 3);
        var    grfx   = new MoarxGraphics(bitmap);

        grfx.FloodFill(5, 0, Color.FromArgb(255));

        Assert.That(bitmap.Bits[0], Is.EqualTo(255));
    }

}