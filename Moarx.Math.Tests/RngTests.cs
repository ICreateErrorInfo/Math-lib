using NUnit.Framework;

namespace Moarx.Math.Tests;

[TestFixture]
public class RngTests {

    [Test]
    public void TestUniformIntIsDeterministicAcrossInstances() {
        // rng has no seeding API, so every new instance starts from the same fixed state.
        var rng1 = new rng();
        var rng2 = new rng();

        for (int i = 0; i < 10; i++) {
            Assert.That(rng1.UniformInt(), Is.EqualTo(rng2.UniformInt()));
        }
    }
    [Test]
    public void TestUniformIntProducesVaryingValues() {
        var rng = new rng();

        var values = new HashSet<uint>();
        for (int i = 0; i < 20; i++) {
            values.Add(rng.UniformInt());
        }

        Assert.That(values.Count, Is.GreaterThan(1));
    }
    [Test]
    public void TestUniformIsDeterministicAcrossInstances() {
        var rng1 = new rng();
        var rng2 = new rng();

        for (int i = 0; i < 10; i++) {
            Assert.That(rng1.Uniform(), Is.EqualTo(rng2.Uniform()));
        }
    }
    [Test]
    public void TestUniformStaysWithinZeroToOne() {
        var rng = new rng();

        for (int i = 0; i < 1000; i++) {
            double value = rng.Uniform();
            Assert.That(value, Is.InRange(0.0, 1.0));
        }
    }
}
