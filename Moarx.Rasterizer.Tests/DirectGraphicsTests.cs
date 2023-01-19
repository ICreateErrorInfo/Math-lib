﻿using NUnit.Framework;

namespace Moarx.Rasterizer.Tests;

[TestFixture]
public class DirectGraphicsTests {

    [Test]
    public void TestFloodFill() {
        var    bitmap = DirectBitmap.Create(10, 1);
        var    grfx   = DirectGraphics.Create(bitmap);

        grfx.FloodFill(5, 0, DirectColors.White);

        Assert.That(bitmap.Bits[0], Is.EqualTo(255));
    }

}