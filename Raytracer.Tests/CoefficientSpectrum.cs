using Math_lib.Spectrum;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raytracing.Tests {
    [TestFixture]
    internal class CoefficientSpectrumTests {
        [Test]
        public void TestCtor1() {
            CoefficientSpectrum spectrum = new CoefficientSpectrum(50, 20);

            foreach (var c in spectrum.coefficients) {
                Assert.That(c, Is.EqualTo(20));
            }
        }

        [Test]
        public void TestPlus() {
            var s = new CoefficientSpectrum(100, 1);
            var r = new CoefficientSpectrum(100, 2);

            CoefficientSpectrum sum = s + r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(3));
            }
        }
        [Test]
        public void TestMinus() {
            var s = new CoefficientSpectrum(100, 1);
            var r = new CoefficientSpectrum(100, 2);

            CoefficientSpectrum sum = s - r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(-1));
            }
        }
        [Test]
        public void TestMal() {
            var s = new CoefficientSpectrum(100, 1);
            var r = new CoefficientSpectrum(100, 2);

            CoefficientSpectrum sum = s * r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(2));
            }
        }
        [Test]
        public void TestScalarMal() {
            var s = new CoefficientSpectrum(100, 1);

            CoefficientSpectrum sum = 2 * s;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(2));
            }
        }
        [Test]
        public void TestDiv() {
            var s = new CoefficientSpectrum(100, 1);
            var r = new CoefficientSpectrum(100, 2);

            CoefficientSpectrum sum = s / r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(0.5));
            }
        }
        [Test]
        public void TestNeg() {
            var r = new CoefficientSpectrum(100, 2);

            CoefficientSpectrum sum = -r;

            foreach (var c in sum.coefficients) {
                Assert.That(c, Is.EqualTo(-2));
            }
        }
    }
}
