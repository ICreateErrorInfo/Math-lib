using NUnit.Framework;
using System.Drawing;

namespace Moarx.Rasterizer.Tests;

[TestFixture]
public class DirectGraphicsTests {

    [Test]
    public void TestFloodFill() {
        var    bitmap = DirectBitmap.Create(10, 1);
        var    grfx   = new DirectGraphics(bitmap);

        grfx.FloodFill(5, 0, Color.FromArgb(255));

        Assert.That(bitmap.Bits[0], Is.EqualTo(255));
    }

}