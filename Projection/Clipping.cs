using Math_lib;

using System.Collections.Generic;

namespace Projection
{
    class Clipping
    {
        //calc Intersection Point of two Lines
        public static Vertex IntersectPlane(Point3D planeP, Normal3D planeN, Vertex lineStart1, Vertex lineEnd1)
        {
            Point3D lineStart = lineStart1.Pos;
            Point3D lineEnd = lineEnd1.Pos;

            planeN = Normal3D.Normalize(planeN);
            double planeD = -Normal3D.Dot(planeN, (Vector3D)planeP);
            double ad = Normal3D.Dot((Vector3D)lineStart,planeN);
            double bd = Normal3D.Dot((Vector3D)lineEnd, planeN);
            double t = (-planeD - ad) / (bd - ad);
            Vector3D lineStartToEnd = lineEnd - lineStart;
            Vector3D lineToIntersect = lineStartToEnd * t;
            return new Vertex(lineStart + lineToIntersect, lineStart1.Attributes);
        }

        public static IEnumerable<Triangle3D> TriangleClipAgainstPlane(Point3D planeP, Normal3D planeN, Triangle3D inTri)
        {

            //Normalize the Normal
            planeN = Normal3D.Normalize(planeN);

            //Storage
            var insidePoints = new Vertex[3];
            var outsidePoints = new Vertex[3];
            int insidePointsCount = 0;
            int outsidePointsCount = 0;

            //Distances
            double d0 = CalcDistance(inTri.Points[0], planeP, planeN);
            double d1 = CalcDistance(inTri.Points[1], planeP, planeN);
            double d2 = CalcDistance(inTri.Points[2], planeP, planeN);

            //Check if its a point outside or inside
            if (d0 > 0)
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
                var outTri1 = new Triangle3D
                {
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
                var outTri1 = new Triangle3D
                {
                    Points = {
                        [0] = insidePoints[0],
                        [1] = insidePoints[1],
                        [2] = IntersectPlane(planeP, planeN, insidePoints[0], outsidePoints[0])
                    }
                };
                yield return outTri1;

                var outTri2 = new Triangle3D
                {
                    Points = {
                        [0] = insidePoints[1],
                        [1] = outTri1.Points[2],
                        [2] = IntersectPlane(planeP, planeN, insidePoints[1], outsidePoints[0])
                    }
                };

                yield return outTri2;
            }

        }

        static double CalcDistance(Vertex p, Point3D planeP, Normal3D planeN)
        {
            Vector3D erg = (Vector3D)planeN * (p.Pos - planeP);

            return erg.X + erg.Y + erg.Z;
        }
    }
}
