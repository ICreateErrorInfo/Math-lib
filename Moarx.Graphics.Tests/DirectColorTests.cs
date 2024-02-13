using NUnit.Framework;

namespace Moarx.Graphics.Tests {

    [TestFixture]
    public class DirectColorTests {

        [Test]
        public void ChannelTest() {

            var c = DirectColor.FromRgba(red: 1, green: 2, blue: 3, alpha: 4);

            Assert.That(c[DirectColor.RedChannel],   Is.EqualTo(1));
            Assert.That(c[DirectColor.GreenChannel], Is.EqualTo(2));
            Assert.That(c[DirectColor.BlueChannel],  Is.EqualTo(3));
            Assert.That(c[DirectColor.AlphaChannel], Is.EqualTo(4));

            // Alpha zählt nicht als Farbkanal!
            Assert.That(c.GetMaxColorChannelValue(),     Is.EqualTo(3));
            Assert.That(c.GetAverageColorChannelValue(), Is.EqualTo(2));
        }

        [Test]
        public void WeißMitAlphaBlendAufSchwarzTest() {

            var wt = DirectColor.FromRgba(red: 255, green: 255, blue: 255, alpha: 128);

            var blended = DirectColor.Blend(wt, DirectColors.Black);

            Assert.That(blended.A, Is.EqualTo(255));
            Assert.That(blended.R, Is.EqualTo(128));
            Assert.That(blended.G, Is.EqualTo(128));
            Assert.That(blended.B, Is.EqualTo(128));

        }

        [Test]
        public void SolidFarbeAufFarbe() {

            var wt = DirectColor.FromRgba(red: 200, green: 100, blue: 50, alpha: 255);

            var blended = DirectColor.Blend(wt, DirectColors.Black);

            Assert.That(blended.R, Is.EqualTo(wt.R));
            Assert.That(blended.G, Is.EqualTo(wt.G));
            Assert.That(blended.B, Is.EqualTo(wt.B));
            Assert.That(blended.A, Is.EqualTo(wt.A));

        }

        [Test]
        public void FarbeMitAlphaBlendAufSchwarzTest() {

            var wt = DirectColor.FromRgba(red: 200, green: 100, blue: 50, alpha: 128);

            var blended = DirectColor.Blend(wt, DirectColors.Black);

            Assert.That(blended.A, Is.EqualTo(255));
            Assert.That(blended.R, Is.EqualTo(100));
            Assert.That(blended.G, Is.EqualTo(50));
            Assert.That(blended.B, Is.EqualTo(25));

        }

        [Test]
        public void BlauMitAlphaAufRotMitAlpha() {

            var blue = DirectColors.Blue with { A = 128 };
            var red  = DirectColors.Red with { A = 128 };

            var blended = blue.Blend(red);

            Assert.That(blended.R, Is.EqualTo(84));
            Assert.That(blended.G, Is.EqualTo(0));
            Assert.That(blended.B, Is.EqualTo(170));
            Assert.That(blended.A, Is.EqualTo(191));

        }

    }

}