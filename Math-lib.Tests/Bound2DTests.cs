using NUnit.Framework;

namespace Math_lib.Tests
{
    [TestFixture]
    public class Bound2DTests
    {
        [Test]
        public void TestSyntax()
        {
            var b = new Bounds2D { pMin = new Point2D(1, 1), pMax = new Point2D(2, 2) };

            Assert.That(b.pMin, Is.EqualTo(new Point2D(1, 1)));
            Assert.That(b.pMax, Is.EqualTo(new Point2D(2, 2)));
        }
        [Test]
        public void TestEmptyBound3D()
        {
            var b = new Bounds2D();
            Assert.That(b.pMin, Is.EqualTo(new Point2D(double.MaxValue)));
            Assert.That(b.pMax, Is.EqualTo(new Point2D(double.MinValue)));
        }
        [Test]
        public void TestCtor1()
        {
            Point2D p = new Point2D(1);
            var b = new Bounds2D(p);

            Assert.That(b.pMin, Is.EqualTo(p));
            Assert.That(b.pMax, Is.EqualTo(p));
        }
        [Test]
        public void TestCtor2()
        {
            Point2D p1 = new Point2D(1);
            Point2D p2 = new Point2D(7);

            var b = new Bounds2D(p1, p2);

            Assert.That(b.pMin, Is.EqualTo(p1));
            Assert.That(b.pMax, Is.EqualTo(p2));
        }


        [Test]
        public void TestIsNaN()
        {
            Bounds2D b = new Bounds2D { pMax = new Point2D { X = double.NaN } };

            Assert.That(Bounds2D.IsNaN(b), Is.True);
        }
        [Test]
        public void TestIntersectP1()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b = new Bounds2D(pMin, pMax);

            Point3D origin = new Point3D(-3, -3, -0.5);
            Vector3D direction = new Vector3D(2, 2, 0.5);

            Ray r = new Ray(origin, direction);

            double t0 = 0;
            double t1 = 0;

            bool hitFound = b.IntersectP(r, ref t0, ref t1);

            Assert.That(hitFound, Is.True);
            Assert.That(t0, Is.EqualTo(1));
            Assert.That(t1, Is.EqualTo(2));
        }
        [Test]
        public void TestIntersectP2()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b = new Bounds2D(pMin, pMax);

            Point3D origin = new Point3D(-3, -3, -0.5);
            Vector3D direction = new Vector3D(2, 2, 0.5);

            Ray r = new Ray(origin, direction);

            Vector3D invDir = 1 / r.D;
            int[] dirIsNeg = new int[] { 0, 0, 0 };

            bool hitFound = b.IntersectP(r, invDir, dirIsNeg);

            Assert.That(hitFound, Is.True);
        }
        [Test]
        public void TestCorner()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b = new Bounds2D(pMin, pMax);

            Assert.That(b.Corner(0), Is.EqualTo(new Point2D(-2.5, -1)));
            Assert.That(b.Corner(1), Is.EqualTo(new Point2D(2.5, -1)));
            Assert.That(b.Corner(2), Is.EqualTo(new Point2D(-2.5, 1)));
            Assert.That(b.Corner(3), Is.EqualTo(new Point2D(2.5, 1)));
        }
        [Test]
        public void TestUnion()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Point2D pMax1 = new Point2D(1, 1);

            Bounds2D b3 = new Bounds2D(new(-2.5, -1), new(2.5, 1));

            Assert.That(Bounds2D.Union(b1, pMax1).pMin, Is.EqualTo(b3.pMin));
            Assert.That(Bounds2D.Union(b1, pMax1).pMax, Is.EqualTo(b3.pMax));
        }
        [Test]
        public void TestUnion2()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Point2D pMin1 = new Point2D(-1, -1);
            Point2D pMax1 = new Point2D(1, 1);

            Bounds2D b2 = new Bounds2D(pMin1, pMax1);

            Bounds2D b3 = new Bounds2D(new(-2.5, -1), new(2.5, 1));

            Assert.That(Bounds2D.Union(b1, b2).pMin, Is.EqualTo(b3.pMin));
            Assert.That(Bounds2D.Union(b1, b2).pMax, Is.EqualTo(b3.pMax));
        }
        [Test]
        public void TestIntersection()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Point2D pMin1 = new Point2D(-1, -1);
            Point2D pMax1 = new Point2D(1, 1);

            Bounds2D b2 = new Bounds2D(pMin1, pMax1);

            Bounds2D b3 = new Bounds2D(new(-1, -1), new(1, 1));

            Assert.That(Bounds2D.Intersect(b1, b2).pMin, Is.EqualTo(b3.pMin));
            Assert.That(Bounds2D.Intersect(b1, b2).pMax, Is.EqualTo(b3.pMax));
        }
        [Test]
        public void TestOverlap()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Point2D pMin1 = new Point2D(-1, -1);
            Point2D pMax1 = new Point2D(1, 1);

            Bounds2D b2 = new Bounds2D(pMin1, pMax1);

            Assert.That(Bounds2D.Overlaps(b1, b2), Is.True);
        }
        [Test]
        public void TestInside()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Point2D p = new(1.5, 0.4);
            Point2D p1 = new(15, 0.4);

            Assert.That(Bounds2D.Inside(p, b1), Is.True);
            Assert.That(Bounds2D.Inside(p1, b1), Is.False);
        }
        [Test]
        public void TestExpand()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Bounds2D b2 = new Bounds2D((Point2D)(pMin - new Point2D(1, 1)), pMax + new Point2D(1, 1));

            Assert.That(Bounds2D.Expand(b1, 1).pMin, Is.EqualTo(b2.pMin));
            Assert.That(Bounds2D.Expand(b1, 1).pMax, Is.EqualTo(b2.pMax));
        }
        [Test]
        public void TestDiagonal()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Vector2D v = pMax - pMin;

            Assert.That(b1.Diagonal(), Is.EqualTo(v));
        }
        [Test]
        public void TestVolume()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            double v = b1.Diagonal().X * b1.Diagonal().Y;

            Assert.That(b1.Volume(), Is.EqualTo(v));
        }
        [Test]
        public void TestSurfaceArea()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            double v = b1.Diagonal().X * b1.Diagonal().Y;

            Assert.That(b1.SurfaceArea(), Is.EqualTo(v));
        }
        [Test]
        public void TestMaximumExtend()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Assert.That(b1.MaximumExtent(), Is.EqualTo(0));
            Assert.That(b1.MaximumExtent(), !Is.EqualTo(1));
        }
        [Test]
        public void TestLerp()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Point2D p = new Point2D(1, 1);

            Point2D ex = new(Mathe.Lerp(1, pMin.X, pMax.X), Mathe.Lerp(1, pMin.Y, pMax.Y));

            Assert.That(b1.Lerp(p), Is.EqualTo(ex));
        }
        [Test]
        public void TestOffset()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Point2D p = new(1, 1);

            Vector2D o = p - pMin;

            if (pMax.X > pMin.X) o /= new Vector2D(pMax.X - pMin.X, 1);
            if (pMax.Y > pMin.Y) o /= new Vector2D(1, pMax.Y - pMin.Y);

            Assert.That(b1.Offset(p), Is.EqualTo(o));
        }
        [Test]
        public void TestGet()
        {
            Point2D pMin = new Point2D(-2.5, -1);
            Point2D pMax = new Point2D(2.5, 1);

            Bounds2D b1 = new Bounds2D(pMin, pMax);

            Assert.That(b1[0], Is.EqualTo(pMin));
            Assert.That(b1[1], Is.EqualTo(pMax));
        }
    }
}
