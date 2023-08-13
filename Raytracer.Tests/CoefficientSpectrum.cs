using NUnit.Framework;
using Raytracing.Spectrum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Tests {
    [TestFixture]
    internal class SpectrumTests {
        [Test]
        public void TestCtorSampledSpectrum() {
            var spectrum = new SampledSpectrum(20);

            foreach (var c in spectrum.coefficients) {
                Assert.That(c, Is.EqualTo(20));
            }
        }

        [Test]
        public void TestPlusSampledSpectrum() {
            var s = new SampledSpectrum(1);
            var r = new SampledSpectrum(2);

            ISpectrum sum = s.ToIspectrum() + r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(3));
            }
        }
        [Test]
        public void TestMinusSampledSpectrum() {
            var s = new SampledSpectrum(1);
            var r = new SampledSpectrum(2);

            ISpectrum sum = s.ToIspectrum() - r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(-1));
            }
        }
        [Test]
        public void TestMalSampledSpectrum() {
            var s = new SampledSpectrum(1);
            var r = new SampledSpectrum(2);

            Spectrum.ISpectrum sum = s.ToIspectrum() * r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(2));
            }
        }
        [Test]
        public void TestScalarMalSampledSpectrum() {
            var s = new SampledSpectrum(1);

            Spectrum.ISpectrum sum = 2 * s.ToIspectrum();

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(2));
            }
        }
        [Test]
        public void TestDivSampledSpectrum() {
            var s = new SampledSpectrum(1);
            var r = new SampledSpectrum(2);

            Spectrum.ISpectrum sum = s.ToIspectrum() / r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(0.5));
            }
        }
        [Test]
        public void TestNegSampledSpectrum() {
            var r = new SampledSpectrum(2);

            Spectrum.ISpectrum sum = -r.ToIspectrum();

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(-2));
            }
        }
    }
}
