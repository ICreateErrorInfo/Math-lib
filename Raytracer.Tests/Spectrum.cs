using Math_lib.Spectrum;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Tests {
    [TestFixture]
    internal class Spectrum {
        [Test]
        public void TestCtor1() {
            SampledSpectrum s = new SampledSpectrum(1);

            Assert.That(s.c[0], Is.EqualTo(1));
        }

        [Test]
        public void TestInit() {
            SampledSpectrum.Init();
        }
    }
}
