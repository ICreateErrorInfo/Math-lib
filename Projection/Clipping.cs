using System.Collections.Generic;

using Math_lib;

namespace Projection
{
    class Clipping
    {
       

        //calc Intersection Point of two Lines
        public static Point3D IntersectPlane(Point3D planeP, Normal3D planeN, Point3D lineStart, Point3D lineEnd)
        {
            planeN = new(Vector3D.UnitVector(new(planeN)));
            double planeD = -Vector3D.Dot(new(planeN), new(planeP));
            double ad = Vector3D.Dot(new(lineStart), new(planeN));
            double bd = Vector3D.Dot(new(lineEnd), new(planeN));
            double t = (-planeD - ad) / (bd - ad);
            Vector3D lineStartToEnd = lineEnd - lineStart;
            Vector3D lineToIntersect = lineStartToEnd * t;
            return lineStart + lineToIntersect;
        }

        public static IEnumerable<Triangle3D> TriangleClipAgainstPlane(Point3D planeP, Normal3D planeN, Triangle3D inTri)
        {

            //Normalize the Normal
            planeN = new(Vector3D.UnitVector(new(planeN)));

            //Storage
            var insidePoints = new Point3D[3];
            var outsidePoints = new Point3D[3];
            int insidePointsCount  = 0;
            int outsidePointsCount = 0;

            //Distances
            double d0 = CalcDistance(inTri.Points[0], planeP, planeN);
            double d1 = CalcDistance(inTri.Points[1], planeP, planeN);
            double d2 = CalcDistance(inTri.Points[2], planeP, planeN);

            //Check if its a point outside or inside
            if(d0 > 0)
            {
                insidePoints[insidePointsCount++] = inTri.Points[0];
            }
            else
            {
                outsidePoints[outsidePointsCount++] = inTri.Points[0];
            }

            if (d1 > 0)
            {
                insidePoints[insidePointsCount++] = inTri.Points[1];
            }
            else
            {
                outsidePoints[outsidePointsCount++] = inTri.Points[1];
            }

            if (d2 > 0)
            {
                insidePoints[insidePointsCount++] = inTri.Points[2];
            }
            else
            {
                outsidePoints[outsidePointsCount++] = inTri.Points[2];
            }

            //all points outside
            if (insidePointsCount == 0)
            {
                yield break;
            }
            //all ponts inside
            if (insidePointsCount == 3)
            {
                yield return inTri;
            }

            //Clipping Triangle
            //One point inside
            if (insidePointsCount == 1 && outsidePointsCount == 2)
            {
                var outTri1 = new Triangle3D {
                    Points = {
                        [0] = insidePoints[0],
                        [1] = IntersectPlane(planeP, planeN, insidePoints[0], outsidePoints[0]),
                        [2] = IntersectPlane(planeP, planeN, insidePoints[0], outsidePoints[1])
                    }
                };

                yield return outTri1;
            }

            //Clipping Triangle
            //two points indide
            if (insidePointsCount == 2 && outsidePointsCount == 1)
            {
                var outTri1 = new Triangle3D {
                    Points = {
                        [0] = insidePoints[0],
                        [1] = insidePoints[1],
                        [2] = IntersectPlane(planeP, planeN, insidePoints[0], outsidePoints[0])
                    }
                };
                yield return outTri1;

                var outTri2 = new Triangle3D {
                    Points = {
                        [0] = insidePoints[1],
                        [1] = outTri1.Points[2],
                        [2] = IntersectPlane(planeP, planeN, insidePoints[1], outsidePoints[0])
                    }
                };

                yield return outTri2;
            }

        }

        static double CalcDistance(Point3D p, Point3D planeP, Normal3D planeN)
        {
            Vector3D erg = new(planeN * (p - planeP));

            return erg.X + erg.Y + erg.Z;
        }
    }
}
