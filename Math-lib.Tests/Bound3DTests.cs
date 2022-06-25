using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class Bound3DTests
    {
        [Test]
        public void TestSyntax()
        {
            var b = new Bounds3D { pMin = new Point3D(1, 1, 1), pMax = new Point3D(2, 2, 2) };

            Assert.That(b.pMin, Is.EqualTo(new Point3D(1,1,1)));
            Assert.That(b.pMax, Is.EqualTo(new Point3D(2,2,2)));
        }
        [Test]
        public void TestEmptyBound3D()
        {
            var b = new Bounds3D();
            Assert.That(b.pMin, Is.EqualTo(new Point3D(double.MaxValue)));
            Assert.That(b.pMax, Is.EqualTo(new Point3D(double.MinValue)));
        }
        [Test]
        public void TestCtor1()
        {
            Point3D p = new Point3D(1);
            var b = new Bounds3D(p);

            Assert.That(b.pMin, Is.EqualTo(p));
            Assert.That(b.pMax, Is.EqualTo(p));
        }
        [Test]
        public void TestCtor2()
        {
            Point3D p1 = new Point3D(1);
            Point3D p2 = new Point3D(7);

            var b = new Bounds3D(p1, p2);

            Assert.That(b.pMin, Is.EqualTo(p1));
            Assert.That(b.pMax, Is.EqualTo(p2));
        }

        [Test]
        public void TestIsNaN()
        {
            Bounds3D b = new Bounds3D { pMax = new Point3D { X = double.NaN } };

            Assert.That(Bounds3D.IsNaN(b), Is.True);
        }
        [Test]
        public void TestIntersectP1()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b = new Bounds3D(pMin, pMax);

            Point3D origin = new Point3D(-3,-3,-0.5);
            Vector3D direction = new Vector3D(2,2,0.5);

            Ray r = new Ray(origin, direction);

            double t0 = 0;
            double t1 = 0;

            bool hitFound = b.IntersectP(r, out t0, out t1);

            Assert.That(hitFound, Is.True);
            Assert.That(t0, Is.EqualTo(1));
            Assert.That(t1, Is.EqualTo(2));
        }
        [Test]
        public void TestIntersectP2()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b = new Bounds3D(pMin, pMax);

            Point3D origin = new Point3D(-3, -3, -0.5);
            Vector3D direction = new Vector3D(2, 2, 0.5);

            Ray r = new Ray(origin, direction);

            Vector3D invDir = 1 / r.D;
            bool[] dirIsNeg = new bool[] { false, false, false };

            bool hitFound = b.IntersectP(r, invDir, dirIsNeg);

            Assert.That(hitFound, Is.True);
        }
        [Test]
        public void TestCorner()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b = new Bounds3D(pMin, pMax);

            Assert.That(b.Corner(0), Is.EqualTo(new Point3D(-2.5, -1, -1.5)));
            Assert.That(b.Corner(1), Is.EqualTo(new Point3D(2.5, -1, -1.5)));
            Assert.That(b.Corner(2), Is.EqualTo(new Point3D(-2.5, 1, -1.5)));
            Assert.That(b.Corner(3), Is.EqualTo(new Point3D(2.5, 1, -1.5)));
            Assert.That(b.Corner(4), Is.EqualTo(new Point3D(-2.5, -1, 1)));
            Assert.That(b.Corner(5), Is.EqualTo(new Point3D(2.5, -1, 1)));
            Assert.That(b.Corner(6), Is.EqualTo(new Point3D(-2.5, 1, 1)));
            Assert.That(b.Corner(7), Is.EqualTo(new Point3D(2.5, 1, 1)));
        }
        [Test]
        public void TestUnion()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Point3D pMax1 = new Point3D(1, 1, 2);

            Bounds3D b3 = new Bounds3D(new(-2.5, -1, -1.5), new(2.5, 1, 2));

            Assert.That(Bounds3D.Union(b1, pMax1).pMin, Is.EqualTo(b3.pMin));
            Assert.That(Bounds3D.Union(b1, pMax1).pMax, Is.EqualTo(b3.pMax));
        }
        [Test]
        public void TestUnion2()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Point3D pMin1 = new Point3D(-1,-1,0);
            Point3D pMax1 = new Point3D(1,1,2);

            Bounds3D b2 = new Bounds3D(pMin1, pMax1);

            Bounds3D b3 = new Bounds3D(new(-2.5, -1, -1.5), new(2.5, 1, 2));

            Assert.That(Bounds3D.Union(b1, b2).pMin, Is.EqualTo(b3.pMin));
            Assert.That(Bounds3D.Union(b1, b2).pMax, Is.EqualTo(b3.pMax));
        }
        [Test]
        public void TestIntersection()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Point3D pMin1 = new Point3D(-1, -1, 0);
            Point3D pMax1 = new Point3D(1, 1, 2);

            Bounds3D b2 = new Bounds3D(pMin1, pMax1);

            Bounds3D b3 = new Bounds3D(new(-1,-1,0), new(1,1,1));

            Assert.That(Bounds3D.Intersect(b1, b2).pMin, Is.EqualTo(b3.pMin));
            Assert.That(Bounds3D.Intersect(b1, b2).pMax, Is.EqualTo(b3.pMax));
        }
        [Test]
        public void TestOverlap()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Point3D pMin1 = new Point3D(-1, -1, 0);
            Point3D pMax1 = new Point3D(1, 1, 2);

            Bounds3D b2 = new Bounds3D(pMin1, pMax1);

            Assert.That(Bounds3D.Overlaps(b1, b2), Is.True);
        }
        [Test]
        public void TestInside()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Point3D p = new(1.5, 0.4, 0.3);
            Point3D p1 = new(15, 0.4, 0.3);

            Assert.That(Bounds3D.Inside(p, b1), Is.True);
            Assert.That(Bounds3D.Inside(p1, b1), Is.False);
        }
        [Test]
        public void TestExpand()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Bounds3D b2 = new Bounds3D((Point3D)(pMin - new Point3D(1,1,1)), pMax + new Point3D(1,1,1));

            Assert.That(Bounds3D.Expand(b1, 1).pMin, Is.EqualTo(b2.pMin));
            Assert.That(Bounds3D.Expand(b1, 1).pMax, Is.EqualTo(b2.pMax));
        }
        [Test]
        public void TestDiagonal()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Vector3D v = pMax - pMin;

            Assert.That(b1.Diagonal(), Is.EqualTo(v));
        }
        [Test]
        public void TestVolume()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            double v = b1.Diagonal().X * b1.Diagonal().Y * b1.Diagonal().Z;

            Assert.That(b1.Volume(), Is.EqualTo(v));
        }
        [Test]
        public void TestSurfaceArea()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            double v = 2 * (b1.Diagonal().X * b1.Diagonal().Y + b1.Diagonal().X * b1.Diagonal().Z + b1.Diagonal().Y * b1.Diagonal().Z);

            Assert.That(b1.SurfaceArea(), Is.EqualTo(v));
        }
        [Test]
        public void TestMaximumExtend()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Assert.That(b1.MaximumExtent(), Is.EqualTo(0));
            Assert.That(b1.MaximumExtent(), !Is.EqualTo(1));
        }
        [Test]
        public void TestLerp()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Point3D p = new Point3D(1,1,1);

            Point3D ex = new(Mathe.Lerp(1, pMin.X, pMax.X), Mathe.Lerp(1, pMin.Y, pMax.Y), Mathe.Lerp(1, pMin.Z, pMax.Z)); 

            Assert.That(b1.Lerp(p), Is.EqualTo(ex));
        }
        [Test]
        public void TestOffset()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Point3D p = new(1,1,1);

            Vector3D o = p - pMin;

            if (pMax.X > pMin.X) o /= new Vector3D(pMax.X - pMin.X, 1, 1);
            if (pMax.Y > pMin.Y) o /= new Vector3D(1, pMax.Y - pMin.Y, 1);
            if (pMax.Z > pMin.Z) o /= new Vector3D(1, 1, pMax.Z - pMin.Z);

            Assert.That(b1.Offset(p), Is.EqualTo(o));
        }
        [Test]
        public void TestGet()
        {
            Point3D pMin = new Point3D(-2.5, -1, -1.5);
            Point3D pMax = new Point3D(2.5, 1, 1);

            Bounds3D b1 = new Bounds3D(pMin, pMax);

            Assert.That(b1[0], Is.EqualTo(pMin));
            Assert.That(b1[1], Is.EqualTo(pMax));
        }
    }
}
