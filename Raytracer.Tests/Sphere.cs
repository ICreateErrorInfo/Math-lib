using NUnit.Framework;


namespace Raytracing.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Ctor()
        {
            var s = new Sphere(new(0, 0, 0), 12, new Metal(new(0, 0, 0), 1));
            Assert.That(s.Radius, Is.EqualTo(12));
        }
    }
}